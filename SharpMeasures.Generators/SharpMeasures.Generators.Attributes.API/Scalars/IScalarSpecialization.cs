namespace SharpMeasures.Generators.Scalars;

using SharpMeasures.Generators.Quantities;

public interface IScalarSpecialization : IScalar, IQuantitySpecialization
{
    public abstract bool InheritProcesses { get; }
    public abstract bool InheritConstants { get; }
    public abstract bool InheritBases { get; }

    new public abstract IScalarSpecializationLocations Locations { get; }
}

public interface IScalarSpecializationLocations : IScalarLocations, IQuantitySpecializationLocations
{
    public abstract MinimalLocation? InheritProcesses { get; }
    public abstract MinimalLocation? InheritConstants { get; }
    public abstract MinimalLocation? InheritBases { get; }

    public abstract bool ExplicitlySetInheritProcesses { get; }
    public abstract bool ExplicitlySetInheritConstants { get; }
    public abstract bool ExplicitlySetInheritBases { get; }
}
