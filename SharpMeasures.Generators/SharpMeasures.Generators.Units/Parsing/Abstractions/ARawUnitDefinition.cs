namespace SharpMeasures.Generators.Units.Parsing.Abstractions;

using SharpMeasures.Generators.Attributes.Parsing;

internal abstract record class ARawUnitDefinition<TDefinition, TLocations> : ARawAttributeDefinition<TDefinition, TLocations>, IOpenRawUnitDefinition<TDefinition, TLocations>
    where TDefinition : ARawUnitDefinition<TDefinition, TLocations>
    where TLocations : IOpenUnitLocations<TLocations>
{
    public string? Name { get; private init; }
    public string? Plural { get; private init; }

    protected ARawUnitDefinition(TLocations locations) : base(locations) { }

    protected TDefinition WithName(string? name) => Definition with { Name = name };
    protected TDefinition WithPlural(string? plural) => Definition with { Plural = plural };

    TDefinition IOpenRawUnitDefinition<TDefinition, TLocations>.WithName(string? name) => WithName(name);
    TDefinition IOpenRawUnitDefinition<TDefinition, TLocations>.WithPlural(string? plural) => WithPlural(plural);
}
