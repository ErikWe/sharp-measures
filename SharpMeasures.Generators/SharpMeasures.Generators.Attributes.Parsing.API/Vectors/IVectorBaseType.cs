namespace SharpMeasures.Generators.Vectors;

using SharpMeasures.Generators.Quantities;

public interface IVectorBaseType : IVectorType, IQuantityBaseType
{
    new public abstract IVectorBase Definition { get; }
}
