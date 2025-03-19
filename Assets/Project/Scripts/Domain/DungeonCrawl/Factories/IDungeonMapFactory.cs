/// <summary>
/// ダンジョンのマップ生成のためのファクトリ。
/// コンテキストに基づいて常に一意のダンジョンを生成する。
/// </summary>
/// <typeparam name="TCoordinate">ダンジョンマップで用いる座標系</typeparam>
/// <typeparam name="TGenerationContext">ダンジョンの生成に用いる情報の形式</typeparam>
public interface IDungeonMapFactory<TCoordinate, TGenerationContext>
    where TCoordinate : IDungeonCoordinate
    where TGenerationContext : IDungeonGenerationContext
{
    /// <summary>
    /// ダンジョンマップを生成する。
    /// </summary>
    /// <param name="context">生成に用いるパラメーター</param>
    /// <returns>生成されたマップ</returns>
    IDungeonMap<TCoordinate> CreateDungeonMap(TGenerationContext context);
}