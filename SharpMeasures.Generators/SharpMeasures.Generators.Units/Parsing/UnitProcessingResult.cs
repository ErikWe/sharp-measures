namespace SharpMeasures.Generators.Units.Parsing;

using Microsoft.CodeAnalysis;

public sealed record class UnitProcessingResult
{
    internal IncrementalValuesProvider<Optional<UnitType>> UnitProvider { get; }

    internal IncrementalValueProvider<UnitProcessingData> ProcessingDataProvider { get; }

    internal UnitProcessingResult(IncrementalValuesProvider<Optional<UnitType>> unitProvider, IncrementalValueProvider<UnitProcessingData> processingDataProvider)
    {
        UnitProvider = unitProvider;

        ProcessingDataProvider = processingDataProvider;
    }
}
