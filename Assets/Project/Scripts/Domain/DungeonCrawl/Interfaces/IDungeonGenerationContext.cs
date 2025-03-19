/// <summary>
/// ダンジョン生成のためのコンテキスト。
/// </summary>
public interface IDungeonGenerationContext
{
    int RandomSeed { get; }
}