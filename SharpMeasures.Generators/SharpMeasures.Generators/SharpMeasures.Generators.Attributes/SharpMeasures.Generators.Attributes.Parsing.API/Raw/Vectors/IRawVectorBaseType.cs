namespace SharpMeasures.Generators.Raw.Vectors;

using SharpMeasures.Generators.Raw.Quantities;

public interface IRawVectorBaseType : IRawVectorType, IRawQuantityBaseType
{
    new public abstract IRawVectorBase Definition { get; }
}
