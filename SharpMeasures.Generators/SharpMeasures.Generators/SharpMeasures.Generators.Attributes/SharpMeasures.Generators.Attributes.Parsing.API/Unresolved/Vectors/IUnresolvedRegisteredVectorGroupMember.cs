namespace SharpMeasures.Generators.Unresolved.Vectors;

public interface IUnresolvedRegisteredVectorGroupMember
{
    public abstract NamedType Vector { get; }

    public abstract int Dimension { get; }
}
