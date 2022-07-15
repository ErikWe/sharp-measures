namespace SharpMeasures.Generators.Unresolved.Scalars;

public interface IUnresolvedBaseScalar : IUnresolvedScalar
{
    public abstract NamedType Unit { get; }
    public abstract bool UseUnitBias { get; }

    new public abstract NamedType Difference { get; }
}
