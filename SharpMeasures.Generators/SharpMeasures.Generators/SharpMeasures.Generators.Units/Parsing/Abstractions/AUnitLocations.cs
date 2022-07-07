namespace SharpMeasures.Generators.Units.Parsing.Abstractions;

using SharpMeasures.Generators.Attributes.Parsing;

internal abstract record class AUnitLocations<TLocations> : AAttributeLocations<TLocations>, IOpenUnitLocations<TLocations>
    where TLocations : AUnitLocations<TLocations>
{
    public MinimalLocation? Name { get; private init; }
    public MinimalLocation? Plural { get; private init; }

    public bool ExplicitlySetName => Name is not null;
    public bool ExplicitlySetPlural => Plural is not null;

    protected TLocations WithName(MinimalLocation name) => Locations with { Name = name };
    protected TLocations WithPlural(MinimalLocation plural) => Locations with { Plural = plural };

    TLocations IOpenUnitLocations<TLocations>.WithName(MinimalLocation name) => WithName(name);
    TLocations IOpenUnitLocations<TLocations>.WithPlural(MinimalLocation plural) => WithPlural(plural);
}
