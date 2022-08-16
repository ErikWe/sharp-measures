namespace SharpMeasures.Generators.Raw.Scalars;

using SharpMeasures.Generators.Raw.Quantities;

public interface IRawScalarBaseType : IRawScalarType, IRawQuantityBaseType
{
    new public abstract IRawScalarBase Definition { get; }
}
