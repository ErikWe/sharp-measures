namespace SharpMeasures.Generators.Attributes.Parsing.Units;

public abstract record class AUnitLocations : AAttributeLocations, IUnitLocations
{
    public MinimalLocation Name { get; init; }
    public MinimalLocation Plural { get; init; }
}
