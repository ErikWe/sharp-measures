namespace SharpMeasures.Generators.Units.Parsing.Abstractions;

internal interface IRawModifiedUnitInstance<out TLocations> : IRawUnitInstance<TLocations>
    where TLocations : IModifiedUnitInstanceLocations
{
    public abstract string? OriginalUnitInstance { get; }
}

internal interface IOpenRawModifiedUnitInstance<out TDefinition, TLocations> : IOpenRawUnitInstance<TDefinition, TLocations>, IRawModifiedUnitInstance<TLocations>
    where TDefinition : IOpenRawModifiedUnitInstance<TDefinition, TLocations>
    where TLocations : IModifiedUnitInstanceLocations
{
    public abstract TDefinition WithOriginalUnitInstance(string? originalUnitInstance);
}
