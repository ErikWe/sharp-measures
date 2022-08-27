namespace SharpMeasures.Generators.Units.Parsing.Abstractions;

using SharpMeasures.Generators.Attributes.Parsing;

internal interface IRawUnitDefinition<out TLocations> : IAttributeDefinition<TLocations>
    where TLocations : IUnitLocations
{
    public abstract string? Name { get; }
    public abstract string? Plural { get; }
}

internal interface IOpenRawUnitDefinition<out TDefinition, TLocations> : IOpenAttributeDefinition<TDefinition, TLocations>, IRawUnitDefinition<TLocations>
    where TDefinition : IOpenRawUnitDefinition<TDefinition, TLocations>
    where TLocations : IUnitLocations
{
    public abstract TDefinition WithName(string? name);
    public abstract TDefinition WithPlural(string? plural);
}
