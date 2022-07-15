namespace SharpMeasures.Generators.Unresolved.Vectors;

public interface IUnresolvedBaseVectorGroup : IUnresolvedVectorGroup
{
    public abstract NamedType Unit { get; }

    new public abstract NamedType Difference { get; }
}
