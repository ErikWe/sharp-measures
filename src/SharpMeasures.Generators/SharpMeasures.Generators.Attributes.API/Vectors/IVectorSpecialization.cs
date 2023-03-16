namespace SharpMeasures.Generators.Vectors;

using SharpMeasures.Generators.Quantities;

public interface IVectorSpecialization : IVector, IQuantitySpecialization
{
    public abstract bool InheritProcesses { get; }
    public abstract bool InheritConstants { get; }

    new public abstract IVectorSpecializationLocations Locations { get; }
}

public interface IVectorSpecializationLocations : IVectorLocations, IQuantitySpecializationLocations
{
    public abstract MinimalLocation? InheritProcesses { get; }
    public abstract MinimalLocation? InheritConstants { get; }

    public abstract bool ExplicitlySetInheritProcesses { get; }
    public abstract bool ExplicitlySetInheritConstants { get; }
}
