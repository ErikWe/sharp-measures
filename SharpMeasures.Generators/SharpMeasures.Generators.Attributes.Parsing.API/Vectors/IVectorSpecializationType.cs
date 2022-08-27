namespace SharpMeasures.Generators.Vectors;

using SharpMeasures.Generators.Quantities;

public interface IVectorSpecializationType : IVectorType, IQuantitySpecializationType
{
    new public abstract IVectorSpecialization Definition { get; }
}
