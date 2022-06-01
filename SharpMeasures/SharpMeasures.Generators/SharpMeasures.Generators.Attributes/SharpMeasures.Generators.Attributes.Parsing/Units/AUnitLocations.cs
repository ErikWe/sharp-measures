namespace SharpMeasures.Generators.Attributes.Parsing.Units;

public abstract record class AUnitLocations : AAttributeLocations, IUnitLocations
{
    public MinimalLocation? Name { get; init; }
    public MinimalLocation? Plural { get; init; }

    public bool ExplicitlySetName => Name is not null;
    public bool ExplicitlySetPlural => Plural is not null;
}
