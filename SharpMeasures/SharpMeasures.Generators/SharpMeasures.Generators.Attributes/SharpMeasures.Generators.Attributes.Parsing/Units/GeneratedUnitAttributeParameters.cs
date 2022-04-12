namespace SharpMeasures.Generators.Attributes.Parsing.Units;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing.Utility;

using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

public readonly record struct GeneratedUnitAttributeParameters(INamedTypeSymbol? Quantity)
{
    private static GeneratedUnitAttributeParameters Defaults => new
    (
        Quantity: null
    );

    private static Dictionary<string, AttributeProperty<GeneratedUnitAttributeParameters>> ConstructorParameters { get; }
        = Properties.AllProperties.ToDictionary(static (x) => x.ParameterName);

    private static Dictionary<string, AttributeProperty<GeneratedUnitAttributeParameters>> NamedParameters { get; }
        = Properties.AllProperties.ToDictionary(static (x) => x.Name);

    public static GeneratedUnitAttributeParameters? Parse(AttributeData attributeData)
    {
        GeneratedUnitAttributeParameters values = Defaults;

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
        public static List<AttributeProperty<GeneratedUnitAttributeParameters>> AllProperties => new()
        {
            Quantity
        };

        private static AttributeProperty<GeneratedUnitAttributeParameters> Quantity { get; } = new
        (
            name: nameof(GeneratedUnitAttribute.Quantity),
            setter: static (parameters, obj) => parameters with { Quantity = obj as INamedTypeSymbol }
        );
    }
}