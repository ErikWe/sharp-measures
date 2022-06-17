namespace SharpMeasures.Generators.Units.Parsing.Abstractions;

using SharpMeasures.Generators.Attributes.Parsing;

internal abstract record class AUnitLocations : AAttributeLocations, IUnitLocations
{
    public MinimalLocation? Name { get; init; }
    public MinimalLocation? Plural { get; init; }

    public bool ExplicitlySetName => Name is not null;
    public bool ExplicitlySetPlural => Plural is not null;
}
