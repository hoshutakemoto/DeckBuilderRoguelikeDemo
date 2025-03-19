using System;

/// <summary>
/// ドメイン層のエンティティの識別子。
/// 型の一貫性を保つために継承して使用。
/// </summary>
public abstract class BaseEntityId : IEquatable<BaseEntityId>
{
    public readonly string Value;

    public BaseEntityId()
    {
        Value = Guid.NewGuid().ToString();
    }

    public BaseEntityId(string value)
    {
        Value = value;
    }

    public bool Equals(BaseEntityId other)
    {
        return Value == other.Value;
    }

    public override bool Equals(object obj)
    {
        return obj is BaseEntityId other && Equals(other);
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }

    public override string ToString()
    {
        return Value;
    }
}