namespace SharpMeasures.Attributes.Parsing.Units;

using Microsoft.CodeAnalysis;

using System.Collections.Generic;
using System.Linq;

public readonly record struct GeneratedBiasedUnitAttributeParameters(INamedTypeSymbol? BiasedQuantity, INamedTypeSymbol? UnbiasedQuantity)
{
    public static GeneratedBiasedUnitAttributeParameters? Parse(AttributeData attributeData)
    {
        GeneratedBiasedUnitAttributeParameters values = Properties.Defaults;

        (bool success, values) = AttributeDataArgumentParser.Parse(attributeData, values, Properties.ConstructorParameters, Properties.NamedParameters);

        return success ? values : null;
    }

    private static class Properties
    {
        public static GeneratedBiasedUnitAttributeParameters Defaults { get; } = new
        (
            BiasedQuantity: null,
            UnbiasedQuantity: null
        );

        public static Dictionary<string, AttributeProperty<GeneratedBiasedUnitAttributeParameters>> ConstructorParameters { get; }
        = AllProperties.ToDictionary(static (x) => x.ParameterName);

        public static Dictionary<string, AttributeProperty<GeneratedBiasedUnitAttributeParameters>> NamedParameters { get; }
            = AllProperties.ToDictionary(static (x) => x.Name);

        private static List<AttributeProperty<GeneratedBiasedUnitAttributeParameters>> AllProperties => new()
        {
            BiasedQuantity,
            UnbiasedQuantity
        };

        private static AttributeProperty<GeneratedBiasedUnitAttributeParameters> BiasedQuantity { get; } = new
        (
            name: nameof(GeneratedBiasedUnitAttribute.BiasedQuantity),
            setter: static (parameters, obj) => parameters with { BiasedQuantity = obj as INamedTypeSymbol }
        );

        private static AttributeProperty<GeneratedBiasedUnitAttributeParameters> UnbiasedQuantity { get; } = new
        (
            name: nameof(GeneratedBiasedUnitAttribute.UnbiasedQuantity),
            setter: static (parameters, obj) => parameters with { UnbiasedQuantity = obj as INamedTypeSymbol }
        );
    }
}