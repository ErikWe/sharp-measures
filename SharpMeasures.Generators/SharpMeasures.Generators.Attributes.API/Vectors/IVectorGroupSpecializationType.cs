namespace SharpMeasures.Generators.Vectors;

using SharpMeasures.Generators.Quantities;

public interface IVectorGroupSpecializationType : IVectorGroupType, IQuantitySpecializationType
{
    new public abstract IVectorGroupSpecialization Definition { get; }
}
