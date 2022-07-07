namespace SharpMeasures.Generators.Units.Parsing.DerivedUnit;

using SharpMeasures.Generators.Attributes.Parsing;

internal static class DerivedUnitParser
{
    public static IAttributeParser<RawDerivedUnitDefinition> Instance { get; } = new AttributeParser();

    private static RawDerivedUnitDefinition DefaultDefinition() => RawDerivedUnitDefinition.Empty;

    private class AttributeParser : AAttributeParser<RawDerivedUnitDefinition, DerivedUnitLocations, DerivedUnitAttribute>
    {
        public AttributeParser() : base(DefaultDefinition, DerivedUnitProperties.AllProperties) { }
    }
}
