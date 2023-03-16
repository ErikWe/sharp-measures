namespace SharpMeasures.Generators.Vectors.Parsing.VectorConstant;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;

using System.Collections.Generic;

internal static class VectorConstantParser
{
    public static IAttributeParser<RawVectorConstantDefinition> Parser { get; } = new AttributeParser();

    private static RawVectorConstantDefinition DefaultDefiniton() => RawVectorConstantDefinition.Empty;

    private sealed class AttributeParser : AAttributeParser<RawVectorConstantDefinition, VectorConstantLocations, VectorConstantAttribute>
    {
        public AttributeParser() : base(DefaultDefiniton, VectorConstantProperties.AllProperties) { }

        protected override RawVectorConstantDefinition AddCustomData(RawVectorConstantDefinition definition, AttributeData attributeData, IReadOnlyList<IParameterSymbol> parameterSymbols)
        {
            definition = SetUnassignedDefaults(definition);

            return base.AddCustomData(definition, attributeData, parameterSymbols);
        }

        private static RawVectorConstantDefinition SetUnassignedDefaults(RawVectorConstantDefinition definition)
        {
            if (definition.Locations.ExplicitlySetMultiples is false)
            {
                definition = definition with
                {
                    Multiples = CommonPluralNotation.PrependMultiplesOf
                };
            }

            return definition;
        }
    }
}
