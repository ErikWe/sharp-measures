namespace SharpMeasures.SourceGeneration.Attributes.Parsing.Units;

using Microsoft.CodeAnalysis;

using System.Collections.Generic;
using System.Linq;

public readonly record struct GeneratedBiasedUnitAttributeParameters(INamedTypeSymbol? BiasedQuantity, INamedTypeSymbol? UnbiasedQuantity)
{
    private static GeneratedBiasedUnitAttributeParameters Defaults { get; } = new
    (
        BiasedQuantity: null,
        UnbiasedQuantity: null
    );

    private static Dictionary<string, AttributeProperty<GeneratedBiasedUnitAttributeParameters>> ConstructorParameters { get; }
        = Properties.AllProperties.ToDictionary(static (x) => x.ParameterName);

    private static Dictionary<string, AttributeProperty<GeneratedBiasedUnitAttributeParameters>> NamedParameters { get; }
        = Properties.AllProperties.ToDictionary(static (x) => x.Name);

    public static GeneratedBiasedUnitAttributeParameters? Parse(AttributeData attributeData)
    {
        GeneratedBiasedUnitAttributeParameters values = Defaults;

        (bool success, values) = AttributeDataArgumentParser.Parse(attributeData, values, ConstructorParameters, NamedParameters);

        return success ? values : null;
    }

    private static class Properties
    {
        public static List<AttributeProperty<GeneratedBiasedUnitAttributeParameters>> AllProperties => new()
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