namespace SharpMeasures.Generators.Attributes.Parsing.Units;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing.Utility;
using SharpMeasures.Generators.Units;

using System.Collections.Generic;
using System.Linq;

public readonly record struct PrefixedUnitAttributeParameters(string Name, string Plural, string From, MetricPrefixName MetricPrefixName,
    BinaryPrefixName BinaryPrefixName, PrefixedUnitAttributeParameters.PrefixType SpecifiedPrefixType)
    : IUnitAttributeParameters, IDerivedUnitAttributeParameters
{
    public enum PrefixType { None, Metric, Binary }

    string IDerivedUnitAttributeParameters.DerivedFrom => From;

    public static PrefixedUnitAttributeParameters Parse(AttributeData attributeData)
        => ParameterParser.Parse(attributeData, Defaults, ConstructorParameters, NamedParameters);

    public static IEnumerable<PrefixedUnitAttributeParameters> Parse(INamedTypeSymbol symbol)
        => ParameterParser.Parse<PrefixedUnitAttributeParameters, PrefixedUnitAttribute>(symbol, Defaults, ConstructorParameters, NamedParameters);

    public static IEnumerable<PrefixedUnitAttributeParameters> Parse(IEnumerable<AttributeData> attributeData)
        => ParameterParser.Parse(attributeData, Defaults, ConstructorParameters, NamedParameters);

    public static IDictionary<string, int> ParseIndices(AttributeData attributeData)
        => ParameterParser.ParseIndices(attributeData, ConstructorParameters, NamedParameters);

    public static IEnumerable<IDictionary<string, int>> ParseIndices(INamedTypeSymbol symbol)
        => ParameterParser.ParseIndices<PrefixedUnitAttributeParameters, PrefixedUnitAttribute>(symbol, ConstructorParameters, NamedParameters);

    public static IEnumerable<IDictionary<string, int>> ParseIndices(IEnumerable<AttributeData> attributeData)
        => ParameterParser.ParseIndices(attributeData, ConstructorParameters, NamedParameters);

    private static PrefixedUnitAttributeParameters Defaults { get; } = new
    (
        Name: string.Empty,
        Plural: string.Empty,
        From: string.Empty,
        MetricPrefixName: MetricPrefixName.Identity,
        BinaryPrefixName: BinaryPrefixName.Identity,
        SpecifiedPrefixType: PrefixType.None
    );

    private static Dictionary<string, AttributeProperty<PrefixedUnitAttributeParameters>> ConstructorParameters { get; }
        = Properties.AllProperties.ToDictionary(static (x) => x.ParameterName);

    private static Dictionary<string, AttributeProperty<PrefixedUnitAttributeParameters>> NamedParameters { get; }
        = Properties.AllProperties.ToDictionary(static (x) => x.Name);

    private static class Properties
    {
        public static List<AttributeProperty<PrefixedUnitAttributeParameters>> AllProperties => new()
        {
            Name,
            Plural,
            From,
            MetricPrefixName,
            BinaryPrefixName
        };

        private static AttributeProperty<PrefixedUnitAttributeParameters> Name { get; } = new
        (
            name: nameof(PrefixedUnitAttribute.Name),
            setter: static (parameters, obj) => obj is string name ? parameters with { Name = name } : parameters
        );

        private static AttributeProperty<PrefixedUnitAttributeParameters> Plural { get; } = new
        (
            name: nameof(PrefixedUnitAttribute.Plural),
            setter: static (parameters, obj) => obj is string plural ? parameters with { Plural = plural } : parameters
        );

        private static AttributeProperty<PrefixedUnitAttributeParameters> From { get; } = new
        (
            name: nameof(PrefixedUnitAttribute.From),
            setter: static (parameters, obj) => obj is string from ? parameters with { From = from } : parameters
        );

        private static AttributeProperty<PrefixedUnitAttributeParameters> MetricPrefixName { get; } = new
        (
            name: nameof(PrefixedUnitAttribute.MetricPrefixName),
            setter: static (parameters, obj) => obj is int metricPrefiName
                ? parameters with { MetricPrefixName = (MetricPrefixName)metricPrefiName, SpecifiedPrefixType = PrefixType.Metric }
                : parameters
        );

        private static AttributeProperty<PrefixedUnitAttributeParameters> BinaryPrefixName { get; } = new
        (
            name: nameof(PrefixedUnitAttribute.BinaryPrefixName),
            setter: static (parameters, obj) => obj is int binaryPrefixName
                ? parameters with { BinaryPrefixName = (BinaryPrefixName)binaryPrefixName, SpecifiedPrefixType = PrefixType.Binary }
                : parameters
        );
    }
}