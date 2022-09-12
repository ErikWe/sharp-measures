namespace SharpMeasures.Generators.Vectors;

using SharpMeasures.Generators.Quantities;

public interface IVectorSpecialization : IVector, IQuantitySpecialization
{
    new public abstract IVectorSpecializationLocations Locations { get; }
}

public interface IVectorSpecializationLocations : IVectorLocations, IQuantitySpecializationLocations { }
