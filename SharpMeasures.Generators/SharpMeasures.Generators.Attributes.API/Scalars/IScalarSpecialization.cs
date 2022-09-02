namespace SharpMeasures.Generators.Scalars;

using SharpMeasures.Generators.Quantities;

public interface IScalarSpecialization : IScalar, IQuantitySpecialization
{
    public abstract bool InheritBases { get; }

    new public abstract IScalarSpecializationLocations Locations { get; }
}

public interface IScalarSpecializationLocations : IScalarLocations, IQuantitySpecializationLocations
{
    public abstract MinimalLocation? InheritBases { get; }

    public abstract bool ExplicitlySetInheritBases { get; }
}
