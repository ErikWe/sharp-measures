namespace SharpMeasures.Generators.Units.Parsing.Abstractions;

internal abstract record class ARawModifiedUnitDefinition<TDefinition, TLocations> : ARawUnitInstance<TDefinition, TLocations>, IOpenRawModifiedUnitInstance<TDefinition, TLocations>
    where TDefinition : ARawModifiedUnitDefinition<TDefinition, TLocations>
    where TLocations : IOpenModifiedUnitInstanceLocations<TLocations>
{
    public string? OriginalUnitInstance { get; private init; }

    protected ARawModifiedUnitDefinition(TLocations locations) : base(locations) { }

    protected TDefinition WithOriginalUnitInstance(string? originalUnitInstance) => Definition with { OriginalUnitInstance = originalUnitInstance };

    TDefinition IOpenRawModifiedUnitInstance<TDefinition, TLocations>.WithOriginalUnitInstance(string? originalUnitInstance) => WithOriginalUnitInstance(originalUnitInstance);
}
