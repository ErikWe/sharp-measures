namespace SharpMeasures.Generators.Scalars.Parsing;

using Microsoft.CodeAnalysis;

public sealed record class ScalarValidationResult
{
    internal IncrementalValuesProvider<Optional<ScalarBaseType>> ScalarBaseProcider { get; }
    internal IncrementalValuesProvider<Optional<ScalarSpecializationType>> ScalarSpecializationProvider { get; }

    internal ScalarValidationResult(IncrementalValuesProvider<Optional<ScalarBaseType>> scalarBaseProvider, IncrementalValuesProvider<Optional<ScalarSpecializationType>> scalarSpecializationProvider)
    {
        ScalarBaseProcider = scalarBaseProvider;
        ScalarSpecializationProvider = scalarSpecializationProvider;
    }
}
