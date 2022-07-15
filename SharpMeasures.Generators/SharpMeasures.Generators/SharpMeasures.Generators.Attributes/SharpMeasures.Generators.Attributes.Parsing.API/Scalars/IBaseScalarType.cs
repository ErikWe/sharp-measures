namespace SharpMeasures.Generators.Scalars;

public interface IBaseScalarType : IScalarType
{
    new public abstract IBaseScalar Definition { get; }
}
