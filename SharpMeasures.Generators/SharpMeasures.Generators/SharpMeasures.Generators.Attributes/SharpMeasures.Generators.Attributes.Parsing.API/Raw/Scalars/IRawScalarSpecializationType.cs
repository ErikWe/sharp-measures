namespace SharpMeasures.Generators.Raw.Scalars;

using SharpMeasures.Generators.Raw.Quantities;

public interface IRawScalarSpecializationType : IRawScalarType, IRawQuantitySpecializationType
{
    new public abstract IRawScalarSpecialization Definition { get; }
}
