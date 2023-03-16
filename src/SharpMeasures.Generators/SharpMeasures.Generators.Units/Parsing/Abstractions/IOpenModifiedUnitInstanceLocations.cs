namespace SharpMeasures.Generators.Units.Parsing.Abstractions;

internal interface IOpenModifiedUnitInstanceLocations<out TLocations> : IOpenUnitInstanceLocations<TLocations>, IModifiedUnitInstanceLocations where TLocations : IOpenModifiedUnitInstanceLocations<TLocations>
{
    public abstract TLocations WithOriginalUnitInstance(MinimalLocation originalUnitInstance);
}
