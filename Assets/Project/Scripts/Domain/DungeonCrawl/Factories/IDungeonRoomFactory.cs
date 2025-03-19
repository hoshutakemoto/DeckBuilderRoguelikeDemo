/// <summary>
/// ダンジョンのルームを生成するためのファクトリ。
/// コンテキストと座標に基づいて常に一意のルームを生成する。
/// </summary>
/// <typeparam name="TCoordinate">ダンジョンマップで用いる座標系</typeparam>
/// <typeparam name="TGenerationContext">ダンジョンの生成に用いる情報の形式</typeparam>
public interface IDungeonRoomFactory<TCoordinate, TGenerationContext>
    where TCoordinate : IDungeonCoordinate
    where TGenerationContext : IDungeonGenerationContext
{
    /// <summary>
    /// 指定した位置のルームを生成する。
    /// </summary>
    /// <param name="context">生成に用いるパラメーター</param>
    /// <param name="coordinate">部屋の位置</param>
    /// <returns>生成された部屋のふるまい</returns>
    IDungeonRoomBehaviour CreateRoom(TGenerationContext context, TCoordinate coordinate);
}