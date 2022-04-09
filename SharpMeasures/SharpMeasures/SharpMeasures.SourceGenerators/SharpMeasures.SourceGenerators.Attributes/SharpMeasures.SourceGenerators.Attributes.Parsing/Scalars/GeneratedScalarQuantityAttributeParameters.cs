namespace SharpMeasures.Attributes.Parsing.Scalars;

using Microsoft.CodeAnalysis;

using System.Collections.Generic;
using System.Linq;

public readonly record struct GeneratedScalarQuantityAttributeParameters(string MagnitudePropertyName)
{
    public static GeneratedScalarQuantityAttributeParameters? Parse(AttributeData attributeData)
    {
        GeneratedScalarQuantityAttributeParameters values = Properties.Defaults;

        (bool success, values) = AttributeDataArgumentParser.Parse(attributeData, values, Properties.ConstructorParameters, Properties.NamedParameters);

        return success ? values : null;
    }

    private static class Properties
    {
        public static GeneratedScalarQuantityAttributeParameters Defaults { get; } = new
        (
            MagnitudePropertyName: "Magnitude"
        );

        public static Dictionary<string, AttributeProperty<GeneratedScalarQuantityAttributeParameters>> ConstructorParameters { get; }
        = AllProperties.ToDictionary(static (x) => x.ParameterName);

        public static Dictionary<string, AttributeProperty<GeneratedScalarQuantityAttributeParameters>> NamedParameters { get; }
            = AllProperties.ToDictionary(static (x) => x.Name);

        private static List<AttributeProperty<GeneratedScalarQuantityAttributeParameters>> AllProperties => new()
        {
            MagnitudePropertyName
        };

        private static AttributeProperty<GeneratedScalarQuantityAttributeParameters> MagnitudePropertyName { get; } = new
        (
            name: nameof(GeneratedScalarQuantityAttribute.MagnitudePropertyName),
            setter: static (parameters, obj) => obj is string magntiudePropertyName ? parameters with { MagnitudePropertyName =magntiudePropertyName } : parameters
        );
    }
}