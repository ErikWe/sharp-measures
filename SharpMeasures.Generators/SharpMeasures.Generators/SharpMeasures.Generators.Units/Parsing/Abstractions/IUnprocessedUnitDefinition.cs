namespace SharpMeasures.Generators.Units.Parsing.Abstractions;

using SharpMeasures.Generators.Attributes.Parsing;

internal interface IUnprocessedUnitDefinition<out TLocations> : IAttributeDefinition<TLocations>
    where TLocations : IUnitLocations
{
    public abstract string? Name { get; }
    public abstract string? Plural { get; }
}

internal interface IOpenUnprocessedUnitDefinition<out TDefinition, TLocations> : IOpenAttributeDefinition<TDefinition, TLocations>, IUnprocessedUnitDefinition<TLocations>
    where TDefinition : IOpenUnprocessedUnitDefinition<TDefinition, TLocations>
    where TLocations : IUnitLocations
{
    public abstract TDefinition WithName(string? name);
    public abstract TDefinition WithPlural(string? plural);
}
