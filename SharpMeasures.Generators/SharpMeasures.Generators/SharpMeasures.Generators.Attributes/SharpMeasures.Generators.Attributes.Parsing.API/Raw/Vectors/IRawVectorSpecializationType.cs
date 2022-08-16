namespace SharpMeasures.Generators.Raw.Vectors;

using SharpMeasures.Generators.Raw.Quantities;

public interface IRawVectorSpecializationType : IRawVectorType, IRawQuantitySpecializationType
{
    new public abstract IRawVectorSpecialization Definition { get; }
}
