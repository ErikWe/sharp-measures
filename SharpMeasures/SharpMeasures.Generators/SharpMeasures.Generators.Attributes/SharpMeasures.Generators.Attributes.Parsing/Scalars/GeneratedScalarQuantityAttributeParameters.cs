namespace SharpMeasures.Generators.Attributes.Parsing.Scalars;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing.Utility;

using System.Collections.Generic;

public readonly record struct GeneratedScalarQuantityAttributeParameters(INamedTypeSymbol? Unit, bool Biased, string MagnitudePropertyName)
{
    public static ParameterParser<GeneratedScalarQuantityAttributeParameters, GeneratedScalarQuantityAttribute> Parser { get; }
        = new(Properties.AllProperties, Defaults);

    private static GeneratedScalarQuantityAttributeParameters Defaults => new
    (
        Unit: null,
        Biased: false,
        MagnitudePropertyName: "Magnitude"
    );

    private static class Properties
    {
        public static List<AttributeProperty<GeneratedScalarQuantityAttributeParameters>> AllProperties => new()
        {
            Unit,
            Biased,
            MagnitudePropertyName
        };

        private static AttributeProperty<GeneratedScalarQuantityAttributeParameters> Unit { get; } = new
        (
            name: nameof(GeneratedScalarQuantityAttribute.Unit),
            setter: static (parameters, obj) => parameters with { Unit = obj as INamedTypeSymbol }
        );

        private static AttributeProperty<GeneratedScalarQuantityAttributeParameters> Biased { get; } = new
        (
            name: nameof(GeneratedScalarQuantityAttribute.Biased),
            setter: static (parameters, obj) => obj is bool biased ? parameters with { Biased = biased } : parameters
        );

        private static AttributeProperty<GeneratedScalarQuantityAttributeParameters> MagnitudePropertyName { get; } = new
        (
            name: nameof(GeneratedScalarQuantityAttribute.MagnitudePropertyName),
            setter: static (parameters, obj) => obj is string magntiudePropertyName ? parameters with { MagnitudePropertyName =magntiudePropertyName } : parameters
        );
    }
}