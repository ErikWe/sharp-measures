namespace SharpMeasures.Generators.Units.Parsing.Abstractions;

using SharpMeasures.Generators.Attributes.Parsing;

internal interface IRawUnitDefinition : IAttributeDefinition
{
    public abstract string? Name { get; }
    public abstract string? Plural { get; }

    new public abstract IUnitLocations Locations { get; }
    public abstract IUnitParsingData ParsingData { get; }
}
