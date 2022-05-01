namespace SharpMeasures.Generators.Attributes.Parsing.Scalars;

using SharpMeasures.Generators.Scalars;

using System;
using System.Collections.ObjectModel;

internal static class IncludeBasesProperties
{
    public static ReadOnlyCollection<AttributeProperty<IncludeBasesParameters>> AllProperties => Array.AsReadOnly(new[]
    {
        IncludedBases
    });

    public static AttributeProperty<IncludeBasesParameters> IncludedBases { get; } = new
    (
        name: nameof(IncludeBasesAttribute.IncludedBases),
        setter: static (parameters, obj) => obj is string[] includedUnits
            ? parameters with { IncludedBases = Array.AsReadOnly(includedUnits) }
            : parameters
    );
}
