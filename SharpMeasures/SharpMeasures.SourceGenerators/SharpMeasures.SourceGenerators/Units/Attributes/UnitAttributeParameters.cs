namespace ErikWe.SharpMeasures.SourceGenerators.Units.Attributes;

using ErikWe.SharpMeasures.Attributes;
using ErikWe.SharpMeasures.SourceGenerators.Utility;

using Microsoft.CodeAnalysis;

using System.Collections.Generic;
using System.Linq;

internal readonly record struct UnitAttributeParameters(INamedTypeSymbol? Quantity, INamedTypeSymbol? BiasedQuantity, bool Biased)
{
    private static Dictionary<string, AttributeProperty<UnitAttributeParameters>> ConstructorArguments { get; }
        = Properties.AllProperties.ToDictionary(static (x) => x.ParameterName);

    private static Dictionary<string, AttributeProperty<UnitAttributeParameters>> NamedArguments { get; }
        = Properties.AllProperties.ToDictionary(static (x) => x.Name);

    private static class Properties
    {
        public static List<AttributeProperty<UnitAttributeParameters>> AllProperties => new()
        {
            Quantity,
            UnbiasedQuantity,
            BiasedQuantity
        };

        public static AttributeProperty<UnitAttributeParameters> Quantity { get; } = new("Quantity", typeof(INamedTypeSymbol),
            static (x, y) => x with { Quantity = y as INamedTypeSymbol });

        public static AttributeProperty<UnitAttributeParameters> UnbiasedQuantity { get; } = new("UnbiasedQuantity", typeof(INamedTypeSymbol),
            static (x, y) => x with { Quantity = y as INamedTypeSymbol });

        public static AttributeProperty<UnitAttributeParameters> BiasedQuantity { get; } = new("BiasedQuantity", typeof(INamedTypeSymbol),
            static (x, y) => x with { BiasedQuantity = y as INamedTypeSymbol });
    }

    public static UnitAttributeParameters? Parse(AttributeData attributeData)
    {
        UnitAttributeParameters values = new();

        if (attributeData.AttributeClass?.ToDisplayString() == typeof(BiasedUnitAttribute).FullName)
        {
            values = values with { Biased = true };
        }

        (bool success, values) = AttributeHelpers.ParseAttributeParameters(attributeData, values, ConstructorArguments, NamedArguments);

        return success ? values : null;
    }
}