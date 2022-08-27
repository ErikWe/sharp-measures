namespace SharpMeasures.Generators.Scalars;

using SharpMeasures.Generators.Quantities;

public interface IScalarSpecializationType : IScalarType, IQuantitySpecializationType
{
    new public abstract IScalarSpecialization Definition { get; }
}
