namespace SharpMeasures.Generators.Units.Parsing;

using Microsoft.CodeAnalysis;

public sealed record class UnitParsingResult
{
    internal IncrementalValuesProvider<Optional<RawUnitType>> UnitProvider { get; }

    internal UnitParsingResult(IncrementalValuesProvider<Optional<RawUnitType>> unitProvider)
    {
        UnitProvider = unitProvider;
    }
}
