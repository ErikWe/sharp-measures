namespace SharpMeasures.Generators.Unresolved.Scalars;

public interface IUnresolvedBaseScalarType : IUnresolvedScalarType
{
    new public abstract IUnresolvedBaseScalar Definition { get; }
}
