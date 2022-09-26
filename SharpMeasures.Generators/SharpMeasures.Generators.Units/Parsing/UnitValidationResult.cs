namespace SharpMeasures.Generators.Units.Parsing;

using Microsoft.CodeAnalysis;

public sealed record class UnitValidationResult
{
    internal IncrementalValuesProvider<Optional<UnitType>> UnitProvider { get; }

    internal UnitValidationResult(IncrementalValuesProvider<Optional<UnitType>> unitProvider)
    {
        UnitProvider = unitProvider;
    }
}
