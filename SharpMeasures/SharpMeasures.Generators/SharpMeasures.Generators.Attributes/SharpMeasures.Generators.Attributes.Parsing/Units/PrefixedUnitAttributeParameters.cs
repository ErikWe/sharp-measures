namespace SharpMeasures.Generators.Attributes.Parsing.Units;

using SharpMeasures.Generators.Attributes.Parsing.Utility;
using SharpMeasures.Generators.Units;

using System.Collections.Generic;

public readonly record struct PrefixedUnitAttributeParameters(string Name, string Plural, string From, MetricPrefixName MetricPrefixName,
    BinaryPrefixName BinaryPrefixName, PrefixedUnitAttributeParameters.PrefixType SpecifiedPrefixType)
    : IUnitAttributeParameters, IDerivedUnitAttributeParameters
{
    public static ParameterParser<PrefixedUnitAttributeParameters, PrefixedUnitAttribute> Parser { get; }
        = new(Properties.AllProperties, Defaults);

    string IDerivedUnitAttributeParameters.DerivedFrom => From;

    private static PrefixedUnitAttributeParameters Defaults => new
    (
        Name: string.Empty,
        Plural: string.Empty,
        From: string.Empty,
        MetricPrefixName: MetricPrefixName.Identity,
        BinaryPrefixName: BinaryPrefixName.Identity,
        SpecifiedPrefixType: PrefixType.None
    );

    public enum PrefixType { None, Metric, Binary }

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