namespace SharpMeasures.Generators.Units.Parsing.Abstractions;

using SharpMeasures.Generators.Attributes.Parsing;

internal abstract record class AUnprocessedUnitDefinition<TDefinition, TLocations> : AUnprocessedAttributeDefinition<TDefinition, TLocations>, IOpenUnprocessedUnitDefinition<TDefinition, TLocations>
    where TDefinition : AUnprocessedUnitDefinition<TDefinition, TLocations>
    where TLocations : IOpenUnitLocations<TLocations>
{
    public string? Name { get; private init; }
    public string? Plural { get; private init; }

    protected AUnprocessedUnitDefinition(TLocations locations) : base(locations) { }

    protected TDefinition WithName(string? name) => Definition with { Name = name };
    protected TDefinition WithPlural(string? plural) => Definition with { Plural = plural };

    TDefinition IOpenUnprocessedUnitDefinition<TDefinition, TLocations>.WithName(string? name) => WithName(name);
    TDefinition IOpenUnprocessedUnitDefinition<TDefinition, TLocations>.WithPlural(string? plural) => WithPlural(plural);
}
