namespace SharpMeasures.Generators.Attributes.Parsing.Scalars;

using System;
using System.Collections.ObjectModel;

public readonly record struct IncludeUnitsParameters(ReadOnlyCollection<string> IncludedUnits)
{
    public static IncludeUnitsParameters Empty { get; } = new(Array.AsReadOnly(Array.Empty<string>()));
}