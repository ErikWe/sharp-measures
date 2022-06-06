namespace SharpMeasures.Generators.Attributes.Parsing.Vectors;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpMeasures.Generators.Attributes.Parsing.Utility;
using SharpMeasures.Generators.Scalars;

public static class VectorConstantParser
{
    public static IAttributeParser<RawVectorConstant> Parser { get; } = new AttributeParser();

    private static RawVectorConstant DefaultDefiniton() => RawVectorConstant.Empty;

    private class AttributeParser : AAttributeParser<RawVectorConstant, VectorConstantLocations, GeneratedScalarAttribute>
    {
        public AttributeParser() : base(DefaultDefiniton, VectorConstantProperties.AllProperties) { }

        protected override RawVectorConstant AddCustomData(RawVectorConstant definition, AttributeData attributeData, AttributeSyntax attributeSyntax)
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
