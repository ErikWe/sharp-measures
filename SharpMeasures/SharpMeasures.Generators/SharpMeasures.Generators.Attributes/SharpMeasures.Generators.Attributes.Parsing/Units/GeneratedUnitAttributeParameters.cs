namespace SharpMeasures.Generators.Attributes.Parsing.Units;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing.Utility;

using System.Collections.Generic;

public readonly record struct GeneratedUnitAttributeParameters(INamedTypeSymbol? Quantity, bool Biased)
{
    public static ParameterParser<GeneratedUnitAttributeParameters, GeneratedUnitAttribute> Parser { get; }
        = new(Properties.AllProperties, Defaults);

    private static GeneratedUnitAttributeParameters Defaults => new
    (
        Quantity: null,
        Biased: false
    );

    private static class Properties
    {
        public static List<AttributeProperty<GeneratedUnitAttributeParameters>> AllProperties => new()
        {
            Quantity,
            Biased
        };

        private static AttributeProperty<GeneratedUnitAttributeParameters> Quantity { get; } = new
        (
            name: nameof(GeneratedUnitAttribute.Quantity),
            setter: static (parameters, obj) => parameters with { Quantity = obj as INamedTypeSymbol }
        );

        private static AttributeProperty<GeneratedUnitAttributeParameters> Biased { get; } = new
        (
            name: nameof(GeneratedUnitAttribute.Biased),
            setter: static (parameters, obj) => obj is bool biased ? parameters with { Biased = biased } : parameters
        );
    }
}