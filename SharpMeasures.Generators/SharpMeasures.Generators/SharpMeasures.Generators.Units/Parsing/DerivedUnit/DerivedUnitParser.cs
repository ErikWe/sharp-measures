namespace SharpMeasures.Generators.Units.Parsing.DerivedUnit;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Units.Parsing.Abstractions;

internal static class DerivedUnitParser
{
    public static IAttributeParser<RawDerivedUnitDefinition> Instance { get; } = new AttributeParser();

    private static RawDerivedUnitDefinition DefaultDefinition() => RawDerivedUnitDefinition.Empty;

    private class AttributeParser : AUnitParser<RawDerivedUnitDefinition, DerivedUnitParsingData, DerivedUnitLocations, DerivedUnitAttribute>
    {
        public AttributeParser() : base(DefaultDefinition, DerivedUnitProperties.AllProperties) { }
    }
}
