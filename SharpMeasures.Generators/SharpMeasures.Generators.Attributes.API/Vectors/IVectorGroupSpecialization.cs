namespace SharpMeasures.Generators.Vectors;

using SharpMeasures.Generators.Quantities;

public interface IVectorGroupSpecialization : IVectorGroup, IQuantitySpecialization
{
    new public abstract IVectorGroupSpecializationLocations Locations { get; }
}

public interface IVectorGroupSpecializationLocations : IVectorGroupLocations, IQuantitySpecializationLocations { }
