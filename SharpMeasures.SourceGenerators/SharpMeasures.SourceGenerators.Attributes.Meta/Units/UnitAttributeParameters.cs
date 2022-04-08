namespace ErikWe.SharpMeasures.SourceGenerators.Attributes.Meta.Units;

using ErikWe.SharpMeasures.Attributes;

using Microsoft.CodeAnalysis;

using System.Collections.Generic;
using System.Linq;

internal readonly record struct UnitAttributeParameters(INamedTypeSymbol? Quantity)
{
    private static Dictionary<string, AttributeProperty<UnitAttributeParameters>> ConstructorArguments { get; }
        = Properties.AllProperties.ToDictionary(static (x) => x.ParameterName);

    private static Dictionary<string, AttributeProperty<UnitAttributeParameters>> NamedArguments { get; }
        = Properties.AllProperties.ToDictionary(static (x) => x.Name);

    private static class Properties
    {
        public static List<AttributeProperty<UnitAttributeParameters>> AllProperties => new()
        {
            Quantity
        };

        public static AttributeProperty<UnitAttributeParameters> Quantity { get; } = new(nameof(UnitAttribute.Quantity), typeof(INamedTypeSymbol),
            static (x, y) => x with { Quantity = y as INamedTypeSymbol });
    }

    public static UnitAttributeParameters? Parse(AttributeData attributeData)
    {
        UnitAttributeParameters values = new();

        (bool success, values) = AttributeParameterParser.Parse(attributeData, values, ConstructorArguments, NamedArguments);

        return success ? values : null;
    }
}