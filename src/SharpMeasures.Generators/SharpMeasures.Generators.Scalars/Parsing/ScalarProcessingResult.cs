namespace SharpMeasures.Generators.Scalars.Parsing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Scalars.Parsing.Abstraction;

public sealed record class ScalarProcessingResult
{
    internal IncrementalValuesProvider<Optional<ScalarBaseType>> ScalarBaseProvider { get; }
    internal IncrementalValuesProvider<Optional<ScalarSpecializationType>> ScalarSpecializationProvider { get; }

    internal IncrementalValueProvider<ScalarProcessingData> ProcessingDataProvider { get; }

    internal ScalarProcessingResult(IncrementalValuesProvider<Optional<ScalarBaseType>> scalarBaseProvider, IncrementalValuesProvider<Optional<ScalarSpecializationType>> scalarSpecializationProvider, IncrementalValueProvider<ScalarProcessingData> processingDataProvider)
    {
        ScalarBaseProvider = scalarBaseProvider;
        ScalarSpecializationProvider = scalarSpecializationProvider;

        ProcessingDataProvider = processingDataProvider;
    }
}
