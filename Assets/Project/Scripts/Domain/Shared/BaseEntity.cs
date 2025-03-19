/// <summary>
/// エンティティの基底クラス。
/// 自動でIDを生成する。
/// </summary>
/// <typeparam name="T">IDに使用するクラス</typeparam>
public abstract class BaseEntity<T> where T : BaseEntityId, new()
{
    /// <summary>
    /// エンティティのID。
    /// </summary>
    public T Id { get; }

    public BaseEntity()
    {
        Id = new T();
    }
}