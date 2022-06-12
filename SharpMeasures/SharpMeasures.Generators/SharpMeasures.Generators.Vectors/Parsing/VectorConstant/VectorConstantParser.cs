namespace SharpMeasures.Generators.Vectors.Parsing.VectorConstant;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Attributes.Parsing.Utility;
using SharpMeasures.Generators.Scalars;

internal static class VectorConstantParser
{
    public static IAttributeParser<RawVectorConstantDefinition> Parser { get; } = new AttributeParser();

    private static RawVectorConstantDefinition DefaultDefiniton() => RawVectorConstantDefinition.Empty;

    private class AttributeParser : AAttributeParser<RawVectorConstantDefinition, VectorConstantLocations, VectorConstantAttribute>
    {
        public AttributeParser() : base(DefaultDefiniton, VectorConstantProperties.AllProperties) { }

        protected override RawVectorConstantDefinition AddCustomData(RawVectorConstantDefinition definition, AttributeData attributeData, AttributeSyntax attributeSyntax)
        {
            var modifiedParsingData = definition.ParsingData with
            {
                InterpretedMultiplesName = SimpleTextExpression.Interpret(definition.Name, definition.MultiplesName)
            };

            definition = definition with { ParsingData = modifiedParsingData };
            return base.AddCustomData(definition, attributeData, attributeSyntax);
        }
    }
}
