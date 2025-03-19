/// <summary>
/// ダンジョンクロウラーの振る舞い。
/// </summary>
/// <typeparam name="TCoordinate">ダンジョンマップで用いる座標系</typeparam>
public interface IDungeonCrawlerBehaviour<TCoordinate> where TCoordinate : IDungeonCoordinate
{
    /// <summary>
    /// クロウラーが生成されたときに呼び出される。
    /// </summary>
    /// <param name="coordinate">生成された座標</param>
    void OnCrawlerCreated(TCoordinate coordinate);

    /// <summary>
    /// クロウラーが破棄されたときに呼び出される。
    /// </summary>
    void OnCrawlerDestroyed();

    /// <summary>
    /// クロウラーがルームを移動したときに呼び出される。
    /// </summary>
    /// <param name="coordinate">移動先の座標</param>
    void OnCrawlerMoved(TCoordinate coordinate);
}