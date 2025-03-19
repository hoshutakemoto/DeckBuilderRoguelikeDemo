using System.Collections.Generic;

public sealed class Dungeon<TCoordinate, TGenerationContext>
    : BaseEntity<DungeonId>, IDungeon<TCoordinate, TGenerationContext>
    where TCoordinate : IDungeonCoordinate
    where TGenerationContext : IDungeonGenerationContext
{
    private sealed class Room
    {
        public readonly Dictionary<CrawlerId, Crawler> Crawlers;
        public readonly IDungeonRoomBehaviour Behaviour;

        public Room(IDungeonRoomBehaviour behaviour)
        {
            Crawlers = new();
            Behaviour = behaviour;
        }
    }

    private sealed class Crawler : BaseEntity<CrawlerId>
    {
        public readonly IDungeonCrawlerBehaviour<TCoordinate> Behaviour;

        public Crawler(IDungeonCrawlerBehaviour<TCoordinate> behaviour) => Behaviour = behaviour;
    }

    private readonly IDungeonRoomFactory<TCoordinate, TGenerationContext> _roomFactory;

    private readonly Dictionary<TCoordinate, Room> _rooms;

    public IDungeonMap<TCoordinate> Map { get; }

    public TGenerationContext GenerationContext { get; }

    public Dungeon(TGenerationContext generationContext,
        IDungeonMapFactory<TCoordinate, TGenerationContext> mapFactory,
        IDungeonRoomFactory<TCoordinate, TGenerationContext> roomFactory)
    {
        Map = mapFactory.CreateDungeonMap(generationContext);

        GenerationContext = generationContext;
        _roomFactory = roomFactory;
    }

    public CrawlerId CreateCrawler(IDungeonCrawlerBehaviour<TCoordinate> behaviour, TCoordinate coordinate)
    {
        var room = GetRoom(coordinate);
        var newCrawler = new Crawler(behaviour);

        room.Crawlers.Add(newCrawler.Id, newCrawler);
        room.Behaviour.OnCrawlerEntered();

        newCrawler.Behaviour.OnCrawlerCreated(coordinate);

        return newCrawler.Id;
    }

    public CrawlerId CreateCrawlerAtStartRoom(IDungeonCrawlerBehaviour<TCoordinate> behaviour, int index)
    {
        var coordinate = Map.GetStartRoomCoordinate(index);
        return CreateCrawler(behaviour, coordinate);
    }

    public bool MoveToRoom(CrawlerId crawlerId, TCoordinate from, TCoordinate to, bool isForced = false)
    {
        if (!_rooms.TryGetValue(from, out Room fromRoom)) return false;
        if (!_rooms.TryGetValue(to, out Room toRoom)) return false;

        if (!fromRoom.Crawlers.TryGetValue(crawlerId, out Crawler crawler)) return false;

        if (!isForced && (!toRoom.Behaviour.CanCrawlerEnter() || !Map.CanReach(from, to))) return false;

        fromRoom.Crawlers.Remove(crawlerId);
        fromRoom.Behaviour.OnCrawlerEntered();

        toRoom.Crawlers.Add(crawlerId, crawler);
        toRoom.Behaviour.OnCrawlerEntered();

        crawler.Behaviour.OnCrawlerMoved(to);

        CleanRoom(from);

        return true;
    }

    public bool DestroyCrawler(CrawlerId crawlerId, TCoordinate coordinate)
    {
        if (!_rooms.TryGetValue(coordinate, out Room room)) return false;

        if (!room.Crawlers.TryGetValue(crawlerId, out Crawler crawler)) return false;

        room.Crawlers.Remove(crawlerId);
        room.Behaviour.OnCrawlerLeft();

        crawler.Behaviour.OnCrawlerDestroyed();

        CleanRoom(coordinate);

        return true;
    }

    /// <summary>
    /// ファクトリーを使用して部屋を作成する。
    /// 既に作成されている場合はそのインスタンスを返す。
    /// </summary>
    /// <param name="coordinate">作成する部屋の位置</param>
    /// <returns>作成された部屋のエンティティ</returns>
    private Room GetRoom(TCoordinate coordinate)
    {
        if (_rooms.TryGetValue(coordinate, out Room room)) return room;

        var roomBehaviour = _roomFactory.CreateRoom(GenerationContext, coordinate);
        var newRoom = new Room(roomBehaviour);

        // 部屋の生成時メソッドを呼び出す。
        newRoom.Behaviour.OnRoomCreated();

        _rooms.Add(coordinate, newRoom);

        return newRoom;
    }

    /// <summary>
    /// 部屋にアクターが存在しなければ、部屋を削除する。
    /// </summary>
    /// <param name="coordinate">削除する部屋の位置</param>
    private void CleanRoom(TCoordinate coordinate)
    {
        if (!_rooms.TryGetValue(coordinate, out Room room)) return;

        // アクターが存在するなら削除しない。
        if (room.Crawlers.Count > 0) return;

        // 部屋の破棄時メソッドを呼び出す。
        room.Behaviour.OnRoomDestroyed();

        _rooms.Remove(coordinate);
    }
}