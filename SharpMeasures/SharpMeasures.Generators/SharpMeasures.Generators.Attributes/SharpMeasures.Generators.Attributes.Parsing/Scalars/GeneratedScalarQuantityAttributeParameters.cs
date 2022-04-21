namespace SharpMeasures.Generators.Attributes.Parsing.Scalars;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing.Utility;

using System.Collections.Generic;
using System.Linq;

public readonly record struct GeneratedScalarQuantityAttributeParameters(INamedTypeSymbol? Unit, bool Biased, string MagnitudePropertyName)
{
    public static GeneratedScalarQuantityAttributeParameters Parse(AttributeData attributeData)
        => ParameterParser.Parse(attributeData, Defaults, ConstructorParameters, NamedParameters);

    public static GeneratedScalarQuantityAttributeParameters Parse(INamedTypeSymbol symbol)
        => ParameterParser.ParseSingle<GeneratedScalarQuantityAttributeParameters, GeneratedScalarQuantityAttribute>(symbol, Defaults,
            ConstructorParameters, NamedParameters);

    public static GeneratedScalarQuantityAttributeParameters Parse(IEnumerable<AttributeData> attributeData)
        => ParameterParser.ParseSingle(attributeData, Defaults, ConstructorParameters, NamedParameters);

    public static IDictionary<string, int> ParseIndices(AttributeData attributeData)
        => ParameterParser.ParseIndices(attributeData, ConstructorParameters, NamedParameters);

    public static IDictionary<string, int> ParseSiIndices(INamedTypeSymbol symbol)
        => ParameterParser.ParseSingleIndices<GeneratedScalarQuantityAttributeParameters, GeneratedScalarQuantityAttribute>(symbol, ConstructorParameters,
            NamedParameters);

    public static IDictionary<string, int> ParseIndices(IEnumerable<AttributeData> attributeData)
        => ParameterParser.ParseSingleIndices(attributeData, ConstructorParameters, NamedParameters);

    private static GeneratedScalarQuantityAttributeParameters Defaults { get; } = new
    (
        Unit: null,
        Biased: false,
        MagnitudePropertyName: "Magnitude"
    );

    private static Dictionary<string, AttributeProperty<GeneratedScalarQuantityAttributeParameters>> ConstructorParameters { get; }
        = Properties.AllProperties.ToDictionary(static (x) => x.ParameterName);

    private static Dictionary<string, AttributeProperty<GeneratedScalarQuantityAttributeParameters>> NamedParameters { get; }
        = Properties.AllProperties.ToDictionary(static (x) => x.Name);

    private static class Properties
    {
        public static List<AttributeProperty<GeneratedScalarQuantityAttributeParameters>> AllProperties => new()
        {
            Unit,
            Biased,
            MagnitudePropertyName
        };

        private static AttributeProperty<GeneratedScalarQuantityAttributeParameters> Unit { get; } = new
        (
            name: nameof(GeneratedScalarQuantityAttribute.Unit),
            setter: static (parameters, obj) => parameters with { Unit = obj as INamedTypeSymbol }
        );

        private static AttributeProperty<GeneratedScalarQuantityAttributeParameters> Biased { get; } = new
        (
            name: nameof(GeneratedScalarQuantityAttribute.Biased),
            setter: static (parameters, obj) => obj is bool biased ? parameters with { Biased = biased } : parameters
        );

        private static AttributeProperty<GeneratedScalarQuantityAttributeParameters> MagnitudePropertyName { get; } = new
        (
            name: nameof(GeneratedScalarQuantityAttribute.MagnitudePropertyName),
            setter: static (parameters, obj) => obj is string magntiudePropertyName ? parameters with { MagnitudePropertyName =magntiudePropertyName } : parameters
        );
    }
}