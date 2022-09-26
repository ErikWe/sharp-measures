namespace SharpMeasures.Generators.Scalars.Parsing;

using Microsoft.CodeAnalysis;

public sealed record class ScalarResolutionResult
{
    internal IncrementalValuesProvider<Optional<ResolvedScalarType>> ScalarBaseProvider { get; }
    internal IncrementalValuesProvider<Optional<ResolvedScalarType>> ScalarSpecializationProvider { get; }

    internal ScalarResolutionResult(IncrementalValuesProvider<Optional<ResolvedScalarType>> scalarBaseProvider, IncrementalValuesProvider<Optional<ResolvedScalarType>> scalarSpecializationProvider)
    {
        ScalarBaseProvider = scalarBaseProvider;
        ScalarSpecializationProvider = scalarSpecializationProvider;
    }
}
