namespace SharpMeasures.Generators.Vectors;

using SharpMeasures.Generators.Quantities;

public interface IVectorGroupType : IQuantityType
{
    new public abstract IVectorGroup Definition { get; }
}
