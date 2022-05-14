namespace SharpMeasures.Generators.Attributes.Parsing.Units;

using SharpMeasures.Generators.Units;

using System.Collections.Generic;

internal static class PrefixedUnitProperties
{
    public static IReadOnlyList<AttributeProperty<PrefixedUnitDefinition>> AllProperties =>new[]
    {
        Name,
        Plural,
        From,
        MetricPrefixName,
        BinaryPrefixName
    };

    public static AttributeProperty<PrefixedUnitDefinition> Name { get; } = new
    (
        name: nameof(PrefixedUnitAttribute.Name),
        setter: static (definition, obj) => obj is string name ? definition with { Name = name } : definition,
        syntaxSetter: static (definition, syntax, index) => definition with { Locations = definition.Locations.LocateName(syntax, index) }
    );

    public static AttributeProperty<PrefixedUnitDefinition> Plural { get; } = new
    (
        name: nameof(PrefixedUnitAttribute.Plural),
        setter: static (definition, obj) => obj is string plural ? definition with { Plural = plural } : definition,
        syntaxSetter: static (definition, syntax, index) => definition with { Locations = definition.Locations.LocatePlural(syntax, index) }
    );

    public static AttributeProperty<PrefixedUnitDefinition> From { get; } = new
    (
        name: nameof(PrefixedUnitAttribute.From),
        setter: static (definition, obj) => obj is string from ? definition with { From = from } : definition,
        syntaxSetter: static (definition, syntax, index) => definition with { Locations = definition.Locations.LocateFrom(syntax, index) }
    );

    public static AttributeProperty<PrefixedUnitDefinition> MetricPrefixName { get; } = new
    (
        name: nameof(PrefixedUnitAttribute.MetricPrefixName),
        setter: static (definition, obj) => obj is int metricPrefixName ? definition.ParseMetricPrefix((MetricPrefixName)metricPrefixName) : definition,
        syntaxSetter: static (definition, syntax, index) => definition with { Locations = definition.Locations.LocateMetricPrefixName(syntax, index) }
    );

    public static AttributeProperty<PrefixedUnitDefinition> BinaryPrefixName { get; } = new
    (
        name: nameof(PrefixedUnitAttribute.BinaryPrefixName),
        setter: static (definition, obj) => obj is int binaryPrefixName ? definition.ParseBinaryPrefix((BinaryPrefixName)binaryPrefixName) : definition,
        syntaxSetter: static (definition, syntax, index) => definition with { Locations = definition.Locations.LocateBinaryPrefixName(syntax, index) }
    );
}
