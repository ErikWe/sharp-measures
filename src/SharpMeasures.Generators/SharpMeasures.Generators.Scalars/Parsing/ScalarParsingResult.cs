namespace SharpMeasures.Generators.Scalars.Parsing;

using Microsoft.CodeAnalysis;

public sealed record class ScalarParsingResult
{
    internal IncrementalValuesProvider<Optional<RawScalarBaseType>> ScalarBaseProvider { get; }
    internal IncrementalValuesProvider<Optional<RawScalarSpecializationType>> ScalarSpecializationProvider { get; }

    internal ScalarParsingResult(IncrementalValuesProvider<Optional<RawScalarBaseType>> scalarBaseProvider, IncrementalValuesProvider<Optional<RawScalarSpecializationType>> scalarSpecializationProvider)
    {
        ScalarBaseProvider = scalarBaseProvider;
        ScalarSpecializationProvider = scalarSpecializationProvider;
    }
}
