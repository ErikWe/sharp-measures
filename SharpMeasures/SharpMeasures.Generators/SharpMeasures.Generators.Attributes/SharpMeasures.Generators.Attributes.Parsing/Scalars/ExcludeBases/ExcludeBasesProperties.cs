namespace SharpMeasures.Generators.Attributes.Parsing.Scalars;

using SharpMeasures.Generators.Scalars;

using System;
using System.Collections.ObjectModel;

internal static class ExcludeBasesProperties
{
    public static ReadOnlyCollection<AttributeProperty<ExcludeBasesParameters>> AllProperties => Array.AsReadOnly(new[]
    {
        ExcludedBases
    });

    public static AttributeProperty<ExcludeBasesParameters> ExcludedBases { get; } = new
    (
        name: nameof(ExcludeBasesAttribute.ExcludedBases),
        setter: static (parameters, obj) => obj is string[] excludedUnits
            ? parameters with { ExcludedBases = Array.AsReadOnly(excludedUnits) }
            : parameters
    );
}
