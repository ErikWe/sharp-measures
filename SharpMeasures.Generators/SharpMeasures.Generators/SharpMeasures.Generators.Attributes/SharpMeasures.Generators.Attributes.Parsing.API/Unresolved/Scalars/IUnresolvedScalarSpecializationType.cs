namespace SharpMeasures.Generators.Unresolved.Scalars;

using SharpMeasures.Generators.Unresolved.Quantities;

public interface IUnresolvedScalarSpecializationType : IUnresolvedScalarType, IUnresolvedQuantitySpecializationType
{
    new public abstract IUnresolvedScalarSpecialization Definition { get; }
}
