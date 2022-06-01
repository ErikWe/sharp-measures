namespace SharpMeasures.Generators.Attributes.Parsing.Units;

public interface IRawUnitDefinition : IAttributeDefinition
{
    public abstract string? Name { get; }
    public abstract string? Plural { get; }

    new public abstract IUnitLocations Locations { get; }
    public abstract IUnitParsingData ParsingData { get; }
}
