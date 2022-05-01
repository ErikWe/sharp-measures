namespace SharpMeasures.Generators.Attributes.Parsing.Units;

using SharpMeasures.Generators.Units;

using System;
using System.Collections.ObjectModel;

internal static class PrefixedUnitProperties
{
    public static ReadOnlyCollection<AttributeProperty<PrefixedUnitParameters>> AllProperties => Array.AsReadOnly(new[]
    {
        Name,
        Plural,
        From,
        MetricPrefixName,
        BinaryPrefixName
    });

    public static AttributeProperty<PrefixedUnitParameters> Name { get; } = new
    (
        name: nameof(PrefixedUnitAttribute.Name),
        setter: static (parameters, obj) => obj is string name ? parameters with { Name = name } : parameters
    );

    public static AttributeProperty<PrefixedUnitParameters> Plural { get; } = new
    (
        name: nameof(PrefixedUnitAttribute.Plural),
        setter: static (parameters, obj) => obj is string plural ? parameters with { Plural = plural } : parameters
    );

    public static AttributeProperty<PrefixedUnitParameters> From { get; } = new
    (
        name: nameof(PrefixedUnitAttribute.From),
        setter: static (parameters, obj) => obj is string from ? parameters with { From = from } : parameters
    );

    public static AttributeProperty<PrefixedUnitParameters> MetricPrefixName { get; } = new
    (
        name: nameof(PrefixedUnitAttribute.MetricPrefixName),
        setter: static (parameters, obj) => obj is int metricPrefiName
            ? parameters with { MetricPrefixName = (MetricPrefixName)metricPrefiName, SpecifiedPrefixType = PrefixedUnitParameters.PrefixType.Metric }
            : parameters
    );

    public static AttributeProperty<PrefixedUnitParameters> BinaryPrefixName { get; } = new
    (
        name: nameof(PrefixedUnitAttribute.BinaryPrefixName),
        setter: static (parameters, obj) => obj is int binaryPrefixName
            ? parameters with { BinaryPrefixName = (BinaryPrefixName)binaryPrefixName, SpecifiedPrefixType = PrefixedUnitParameters.PrefixType.Binary }
            : parameters
    );
}
