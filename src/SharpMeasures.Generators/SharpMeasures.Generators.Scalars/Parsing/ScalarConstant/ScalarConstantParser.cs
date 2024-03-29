﻿namespace SharpMeasures.Generators.Scalars.Parsing.ScalarConstant;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;

using System.Collections.Generic;

internal static class ScalarConstantParser
{
    public static IAttributeParser<RawScalarConstantDefinition> Parser { get; } = new AttributeParser();

    private static RawScalarConstantDefinition DefaultDefiniton() => RawScalarConstantDefinition.Empty;

    private sealed class AttributeParser : AAttributeParser<RawScalarConstantDefinition, ScalarConstantLocations, ScalarConstantAttribute>
    {
        public AttributeParser() : base(DefaultDefiniton, ScalarConstantProperties.AllProperties) { }

        protected override RawScalarConstantDefinition AddCustomData(RawScalarConstantDefinition definition, AttributeData attributeData, IReadOnlyList<IParameterSymbol> parameterSymbols)
        {
            definition = SetUnassignedDefaults(definition);

            return base.AddCustomData(definition, attributeData, parameterSymbols);
        }

        private static RawScalarConstantDefinition SetUnassignedDefaults(RawScalarConstantDefinition definition)
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
