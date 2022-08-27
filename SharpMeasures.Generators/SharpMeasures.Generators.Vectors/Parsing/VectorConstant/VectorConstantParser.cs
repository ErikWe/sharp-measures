namespace SharpMeasures.Generators.Vectors.Parsing.VectorConstant;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Quantities.Utility;
using System.Collections.Immutable;

internal static class VectorConstantParser
{
    public static IAttributeParser<RawVectorConstantDefinition> Parser { get; } = new AttributeParser();

    private static RawVectorConstantDefinition DefaultDefiniton() => RawVectorConstantDefinition.Empty;

    private class AttributeParser : AAttributeParser<RawVectorConstantDefinition, VectorConstantLocations, VectorConstantAttribute>
    {
        public AttributeParser() : base(DefaultDefiniton, VectorConstantProperties.AllProperties) { }

        protected override RawVectorConstantDefinition AddCustomData(RawVectorConstantDefinition definition, AttributeData attributeData,
            AttributeSyntax attributeSyntax, ImmutableArray<IParameterSymbol> parameterSymbols)
        {
            definition = SetUnassignedDefaults(definition);

            return base.AddCustomData(definition, attributeData, attributeSyntax, parameterSymbols);
        }

        private static RawVectorConstantDefinition SetUnassignedDefaults(RawVectorConstantDefinition definition)
        {
            if (definition.Locations.ExplicitlySetMultiples is false)
            {
                definition = definition with
                {
                    Multiples = CommonConstantMultiplesPropertyNotations.PrependMultiplesOf
                };
            }

            return definition;
        }
    }
}
