namespace SharpMeasures.Generators.Vectors;

using SharpMeasures.Generators.Quantities;

public interface IVectorGroupBase : IVectorGroup, IQuantityBase
{
    new public abstract IVectorGroupBaseLocations Locations { get; }
}

public interface IVectorGroupBaseLocations : IVectorGroupLocations, IQuantityBaseLocations { }
