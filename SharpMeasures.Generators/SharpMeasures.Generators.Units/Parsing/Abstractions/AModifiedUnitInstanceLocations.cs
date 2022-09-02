namespace SharpMeasures.Generators.Units.Parsing.Abstractions;

internal abstract record class AModifiedUnitInstanceLocations<TLocations> : AUnitInstanceLocations<TLocations>, IOpenModifiedUnitInstanceLocations<TLocations>
    where TLocations : AModifiedUnitInstanceLocations<TLocations>
{
    protected MinimalLocation? OriginalUnitInstance { get; private init; }

    protected bool ExplicitlySetOriginalUnitInstance => OriginalUnitInstance is not null;

    MinimalLocation? IModifiedUnitInstanceLocations.OriginalUnitInstance => OriginalUnitInstance;
    bool IModifiedUnitInstanceLocations.ExplicitlySetOriginalUnitInstance => ExplicitlySetOriginalUnitInstance;

    protected TLocations WithOriginalUnitInstance(MinimalLocation originalUnitInstance) => Locations with { OriginalUnitInstance = originalUnitInstance };
    TLocations IOpenModifiedUnitInstanceLocations<TLocations>.WithOriginalUnitInstance(MinimalLocation originalUnitInstance) => WithOriginalUnitInstance(originalUnitInstance);
}
