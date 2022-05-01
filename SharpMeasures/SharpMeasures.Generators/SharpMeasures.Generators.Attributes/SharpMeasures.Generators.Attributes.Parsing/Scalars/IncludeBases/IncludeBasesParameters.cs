namespace SharpMeasures.Generators.Attributes.Parsing.Scalars;

using System;
using System.Collections.ObjectModel;

public readonly record struct IncludeBasesParameters(ReadOnlyCollection<string> IncludedBases)
{
    public static IncludeBasesParameters Empty { get; } = new(Array.AsReadOnly(Array.Empty<string>()));
}