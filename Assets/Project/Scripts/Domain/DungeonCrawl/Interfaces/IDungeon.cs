/// <summary>
/// ダンジョンはルームとルームの間を移動するクロウラーを管理する。
/// </summary>
/// <typeparam name="TCoordinate">ダンジョンマップで用いる座標系</typeparam>
/// <typeparam name="TGenerationContext">ダンジョンの生成に用いる情報の形式</typeparam>
public interface IDungeon<TCoordinate, TGenerationContext>
    where TCoordinate : IDungeonCoordinate
    where TGenerationContext : IDungeonGenerationContext
{
    /// <summary>
    /// ダンジョンのマップ。
    /// </summary>
    IDungeonMap<TCoordinate> Map { get; }

    /// <summary>
    /// ダンジョンの生成に用いられた、そして用いられるコンテキスト。
    /// </summary>
    TGenerationContext GenerationContext { get; }

    /// <summary>
    /// クロウラーを生成し、そのIDを返す。
    /// </summary>
    /// <param name="behaviour">クロウラーに注入するふるまい</param>
    /// <param name="coordinate">クロウラーを生成する位置</param>
    /// <returns>生成したクロウラーのID</returns>
    CrawlerId CreateCrawler(IDungeonCrawlerBehaviour<TCoordinate> behaviour, TCoordinate coordinate);

    /// <summary>
    /// クロウラーをマップで指定された初期位置に生成し、そのIDを返す。
    /// </summary>
    /// <param name="behaviour">クロウラーに注入するふるまい</param>
    /// <param name="index">初期位置に割り振られたインデックス</param>
    /// <returns>生成したクロウラーのID</returns>
    CrawlerId CreateCrawlerAtStartRoom(IDungeonCrawlerBehaviour<TCoordinate> behaviour, int index);

    /// <summary>
    /// クロウラーを指定した位置から別の位置に移動させる。
    /// </summary>
    /// <param name="crawlerId">移動させるクロウラーのID</param>
    /// <param name="from">移動前の位置</param>
    /// <param name="to">移動後の位置</param>
    /// <param name="isForced">パスやルームのロックを無視するか</param>
    /// <returns>正常に移動できたかどうか</returns>
    bool MoveToRoom(CrawlerId crawlerId, TCoordinate from, TCoordinate to, bool isForced = false);

    /// <summary>
    /// クロウラーを破棄する。
    /// </summary>
    /// <param name="crawlerId">破棄するクロウラーのID</param>
    /// <param name="coordinate">破棄するクロウラーがいるルームの位置</param>
    /// <returns>正常に破棄できたかどうか</returns>
    bool DestroyCrawler(CrawlerId crawlerId, TCoordinate coordinate);
}