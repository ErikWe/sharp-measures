namespace SharpMeasures.Generators.Attributes.Parsing.Scalars;

using System;
using System.Collections.ObjectModel;

public readonly record struct ExcludeUnitsParameters(ReadOnlyCollection<string> ExcludedUnits)
{
    public static ExcludeUnitsParameters Empty { get; } = new(Array.AsReadOnly(Array.Empty<string>()));
}