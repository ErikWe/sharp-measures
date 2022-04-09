namespace SharpMeasures.Attributes.Parsing.Units;

using Microsoft.CodeAnalysis;

using System.Collections.Generic;
using System.Linq;

public readonly record struct GeneratedUnitAttributeParameters(INamedTypeSymbol? Quantity)
{
    public static GeneratedUnitAttributeParameters? Parse(AttributeData attributeData)
    {
        GeneratedUnitAttributeParameters values = Properties.Defaults;

        (bool success, values) = AttributeDataArgumentParser.Parse(attributeData, values, Properties.ConstructorParameters, Properties.NamedParameters);

        return success ? values : null;
    }

    private static class Properties
    {
        public static GeneratedUnitAttributeParameters Defaults { get; } = new
        (
            Quantity: null
        );

        public static Dictionary<string, AttributeProperty<GeneratedUnitAttributeParameters>> ConstructorParameters { get; }
        = AllProperties.ToDictionary(static (x) => x.ParameterName);

        public static Dictionary<string, AttributeProperty<GeneratedUnitAttributeParameters>> NamedParameters { get; }
            = AllProperties.ToDictionary(static (x) => x.Name);

        private static List<AttributeProperty<GeneratedUnitAttributeParameters>> AllProperties => new()
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