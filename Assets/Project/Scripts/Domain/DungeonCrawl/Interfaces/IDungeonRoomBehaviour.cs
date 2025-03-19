/// <summary>
/// ダンジョンのルームの振る舞いを定義するインターフェース。
/// </summary>
public interface IDungeonRoomBehaviour
{
    void OnRoomCreated();
    void OnRoomDestroyed();

    void OnCrawlerEntered();
    void OnCrawlerLeft();

    bool CanCrawlerEnter();
}