namespace SharpMeasures.Generators.Attributes.Parsing.Scalars;

using SharpMeasures.Generators.Scalars;

using System;
using System.Collections.ObjectModel;

internal static class ExcludeUnitsProperties
{
    public static ReadOnlyCollection<AttributeProperty<ExcludeUnitsParameters>> AllProperties => Array.AsReadOnly(new[]
    {
        ExcludedUnits
    });

    public static AttributeProperty<ExcludeUnitsParameters> ExcludedUnits { get; } = new
    (
        name: nameof(ExcludeUnitsAttribute.ExcludedUnits),
        setter: static (parameters, obj) => obj is string[] excludedUnits
            ? parameters with { ExcludedUnits = Array.AsReadOnly(excludedUnits) }
            : parameters
    );
}
