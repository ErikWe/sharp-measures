namespace SharpMeasures.Generators.Scalars.Parsing.ScalarConstant;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Attributes.Parsing.Utility;

internal static class ScalarConstantParser
{
    public static IAttributeParser<RawScalarConstantDefinition> Parser { get; } = new AttributeParser();

    private static RawScalarConstantDefinition DefaultDefiniton() => RawScalarConstantDefinition.Empty;

    private class AttributeParser : AAttributeParser<RawScalarConstantDefinition, ScalarConstantLocations, GeneratedScalarAttribute>
    {
        public AttributeParser() : base(DefaultDefiniton, ScalarConstantProperties.AllProperties) { }

        protected override RawScalarConstantDefinition AddCustomData(RawScalarConstantDefinition definition, AttributeData attributeData, AttributeSyntax attributeSyntax)
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
