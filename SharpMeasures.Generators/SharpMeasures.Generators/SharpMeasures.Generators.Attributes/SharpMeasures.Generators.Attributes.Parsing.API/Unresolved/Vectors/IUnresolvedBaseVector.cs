namespace SharpMeasures.Generators.Unresolved.Vectors;

public interface IUnresolvedBaseVector : IUnresolvedVector
{
    public abstract NamedType Unit { get; }

    new public abstract NamedType Difference { get; }
}
