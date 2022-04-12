namespace SharpMeasures.Generators.Attributes.Parsing.Units;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing.Utility;

using System.Collections.Generic;
using System.Collections.Immutable;
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

        (bool success, values) = ArgumentParser.Parse(attributeData, values, ConstructorParameters, NamedParameters);

        return success ? values : null;
    }

    public static IDictionary<string, int> ParseIndices(AttributeData attributeData)
    {
        if (attributeData is null)
        {
            return ImmutableDictionary<string, int>.Empty;
        }

        return ArgumentIndexParser.Parse(attributeData, ConstructorParameters, NamedParameters);
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