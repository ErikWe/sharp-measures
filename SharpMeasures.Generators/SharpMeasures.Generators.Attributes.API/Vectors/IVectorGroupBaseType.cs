namespace SharpMeasures.Generators.Vectors;

using SharpMeasures.Generators.Quantities;

public interface IVectorGroupBaseType : IVectorGroupType, IQuantityBaseType
{
    new public abstract IVectorGroupBase Definition { get; }
}
