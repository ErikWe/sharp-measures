namespace SharpMeasures.SourceGenerators.ScalarQuantities.Attributes;

using SharpMeasures.SourceGenerators.Utility;

using Microsoft.CodeAnalysis;

using System.Collections.Generic;
using System.Linq;

public readonly record struct ScalarQuantityAttributeParameters(string MagnitudePropertyName)
{
    private static Dictionary<string, AttributeProperty<ScalarQuantityAttributeParameters>> ConstructorArguments { get; }
        = Properties.AllProperties.ToDictionary(static (x) => x.ParameterName);

    private static Dictionary<string, AttributeProperty<ScalarQuantityAttributeParameters>> NamedArguments { get; }
        = Properties.AllProperties.ToDictionary(static (x) => x.Name);

    private static class Properties
    {
        public static List<AttributeProperty<ScalarQuantityAttributeParameters>> AllProperties => new()
        {
            MagnitudePropertyName
        };

        public static AttributeProperty<ScalarQuantityAttributeParameters> MagnitudePropertyName { get; } = new("MagnitudePropertyName", typeof(string), "Magnitude",
            static (x, y) => y is string yAsString ? x with { MagnitudePropertyName = yAsString } : x);
    }

    public static ScalarQuantityAttributeParameters? Parse(AttributeData attributeData)
    {
        ScalarQuantityAttributeParameters values = new(Properties.MagnitudePropertyName.DefaultValue as string ?? string.Empty);

        (bool success, values) = AttributeHelpers.ParseAttributeParameters(attributeData, values, ConstructorArguments, NamedArguments);

        return success ? values : null;
    }
}