namespace SharpMeasures.Generators.Attributes.Parsing.Units;

using SharpMeasures.Generators.Units;

public static class GeneratedUnitParser
{
    public static IAttributeParser<RawGeneratedUnit> Parser { get; } = new AttributeParser();

    private static RawGeneratedUnit DefaultDefiniton() => RawGeneratedUnit.Empty;

    private class AttributeParser : AAttributeParser<RawGeneratedUnit, GeneratedUnitLocations, GeneratedUnitAttribute>
    {
        public AttributeParser() : base(DefaultDefiniton, GeneratedUnitProperties.AllProperties) { }
    }
}
