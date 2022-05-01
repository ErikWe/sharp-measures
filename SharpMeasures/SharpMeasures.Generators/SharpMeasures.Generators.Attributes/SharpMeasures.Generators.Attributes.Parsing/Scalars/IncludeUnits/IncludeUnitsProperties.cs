namespace SharpMeasures.Generators.Attributes.Parsing.Scalars;

using SharpMeasures.Generators.Scalars;

using System;
using System.Collections.ObjectModel;

internal static class IncludeUnitsProperties
{
    public static ReadOnlyCollection<AttributeProperty<IncludeUnitsParameters>> AllProperties => Array.AsReadOnly(new[]
    {
        IncludedUnits
    });

    public static AttributeProperty<IncludeUnitsParameters> IncludedUnits { get; } = new
    (
        name: nameof(IncludeUnitsAttribute.IncludedUnits),
        setter: static (parameters, obj) => obj is string[] includedUnits
            ? parameters with { IncludedUnits = Array.AsReadOnly(includedUnits) }
            : parameters
    );
}
