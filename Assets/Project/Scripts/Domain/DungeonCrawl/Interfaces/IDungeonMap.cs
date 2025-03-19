/// <summary>
/// ダンジョンのマップ。
/// </summary>
/// <typeparam name="TCoordinate">ダンジョンマップで用いる座標系</typeparam>
public interface IDungeonMap<TCoordinate> where TCoordinate : IDungeonCoordinate
{
    int StartRoomCount { get; }

    bool CanReach(TCoordinate from, TCoordinate to);

    bool HasPath(TCoordinate from, TCoordinate to);

    TCoordinate GetStartRoomCoordinate(int index);
}