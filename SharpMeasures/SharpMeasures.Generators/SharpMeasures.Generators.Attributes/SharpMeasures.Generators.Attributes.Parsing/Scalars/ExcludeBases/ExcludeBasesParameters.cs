namespace SharpMeasures.Generators.Attributes.Parsing.Scalars;

using System;
using System.Collections.ObjectModel;

public readonly record struct ExcludeBasesParameters(ReadOnlyCollection<string> ExcludedBases)
{
    public static ExcludeBasesParameters Empty { get; } = new(Array.AsReadOnly(Array.Empty<string>()));
}