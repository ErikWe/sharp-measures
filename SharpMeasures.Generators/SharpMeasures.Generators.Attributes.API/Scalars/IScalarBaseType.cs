namespace SharpMeasures.Generators.Scalars;

using SharpMeasures.Generators.Quantities;

public interface IScalarBaseType : IScalarType, IQuantityBaseType
{
    new public abstract IScalarBase Definition { get; }
}
