namespace SharpMeasures.Generators.Attributes.Parsing.Scalars;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpMeasures.Generators.Attributes.Parsing.Utility;
using SharpMeasures.Generators.Scalars;

public static class ScalarConstantParser
{
    public static IAttributeParser<RawScalarConstant> Parser { get; } = new AttributeParser();

    private static RawScalarConstant DefaultDefiniton() => RawScalarConstant.Empty;

    private class AttributeParser : AAttributeParser<RawScalarConstant, ScalarConstantLocations, GeneratedScalarAttribute>
    {
        public AttributeParser() : base(DefaultDefiniton, ScalarConstantProperties.AllProperties) { }

        protected override RawScalarConstant AddCustomData(RawScalarConstant definition, AttributeData attributeData, AttributeSyntax attributeSyntax)
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
