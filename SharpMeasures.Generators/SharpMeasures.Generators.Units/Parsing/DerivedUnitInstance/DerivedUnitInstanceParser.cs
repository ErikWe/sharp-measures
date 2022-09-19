namespace SharpMeasures.Generators.Units.Parsing.DerivedUnitInstance;

using SharpMeasures.Generators.Attributes.Parsing;

internal static class DerivedUnitInstanceParser
{
    public static IAttributeParser<RawDerivedUnitInstanceDefinition> Parser { get; } = new AttributeParser();

    private static RawDerivedUnitInstanceDefinition DefaultDefinition() => RawDerivedUnitInstanceDefinition.Empty;

    private sealed class AttributeParser : AAttributeParser<RawDerivedUnitInstanceDefinition, DerivedUnitInstanceLocations, DerivedUnitInstanceAttribute>
    {
        public AttributeParser() : base(DefaultDefinition, DerivedUnitInstanceProperties.AllProperties) { }
    }
}
