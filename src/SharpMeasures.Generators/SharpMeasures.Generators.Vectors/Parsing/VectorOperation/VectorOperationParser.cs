namespace SharpMeasures.Generators.Vectors.Parsing.VectorOperation;

using SharpMeasures.Generators.Attributes.Parsing;

internal static class VectorOperationParser
{
    public static IAttributeParser<SymbolicVectorOperationDefinition> Parser { get; } = new AttributeParser();

    private static SymbolicVectorOperationDefinition DefaultDefinition() => SymbolicVectorOperationDefinition.Empty;

    private sealed class AttributeParser : AAttributeParser<SymbolicVectorOperationDefinition, VectorOperationLocations, VectorOperationAttribute>
    {
        public AttributeParser() : base(DefaultDefinition, VectorOperationProperties.AllProperties) { }
    }
}
