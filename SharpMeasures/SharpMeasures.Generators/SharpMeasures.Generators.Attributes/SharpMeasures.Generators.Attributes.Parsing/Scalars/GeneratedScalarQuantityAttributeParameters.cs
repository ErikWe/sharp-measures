namespace SharpMeasures.Generators.Attributes.Parsing.Scalars;

using Microsoft.CodeAnalysis;

using System.Collections.Generic;
using System.Linq;

public readonly record struct GeneratedScalarQuantityAttributeParameters(string MagnitudePropertyName)
{
    private static GeneratedScalarQuantityAttributeParameters Defaults { get; } = new
    (
        MagnitudePropertyName: "Magnitude"
    );

    private static Dictionary<string, AttributeProperty<GeneratedScalarQuantityAttributeParameters>> ConstructorParameters { get; }
        = Properties.AllProperties.ToDictionary(static (x) => x.ParameterName);

    private static Dictionary<string, AttributeProperty<GeneratedScalarQuantityAttributeParameters>> NamedParameters { get; }
        = Properties.AllProperties.ToDictionary(static (x) => x.Name);

    public static GeneratedScalarQuantityAttributeParameters? Parse(AttributeData attributeData)
    {
        GeneratedScalarQuantityAttributeParameters values = Defaults;

        (bool success, values) = AttributeDataArgumentParser.Parse(attributeData, values, ConstructorParameters, NamedParameters);

        return success ? values : null;
    }

    private static class Properties
    {
        public static List<AttributeProperty<GeneratedScalarQuantityAttributeParameters>> AllProperties => new()
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