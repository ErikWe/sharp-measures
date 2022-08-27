namespace SharpMeasures.Generators.Scalars.Parsing.ScalarConstant;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Quantities.Utility;
using System.Collections.Immutable;

internal static class ScalarConstantParser
{
    public static IAttributeParser<RawScalarConstantDefinition> Parser { get; } = new AttributeParser();

    private static RawScalarConstantDefinition DefaultDefiniton() => RawScalarConstantDefinition.Empty;

    private class AttributeParser : AAttributeParser<RawScalarConstantDefinition, ScalarConstantLocations, ScalarConstantAttribute>
    {
        public AttributeParser() : base(DefaultDefiniton, ScalarConstantProperties.AllProperties) { }

        protected override RawScalarConstantDefinition AddCustomData(RawScalarConstantDefinition definition, AttributeData attributeData,
            AttributeSyntax attributeSyntax, ImmutableArray<IParameterSymbol> parameterSymbols)
        {
            definition = SetUnassignedDefaults(definition);

            return base.AddCustomData(definition, attributeData, attributeSyntax, parameterSymbols);
        }

        private static RawScalarConstantDefinition SetUnassignedDefaults(RawScalarConstantDefinition definition)
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
