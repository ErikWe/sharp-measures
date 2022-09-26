namespace SharpMeasures.Generators.Scalars.Parsing.Diagnostics.Empty.Validation;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Scalars.Parsing.ConvertibleScalar;

internal sealed class EmptyConvertibleScalarFilteringDiagnostics : IConvertibleScalarFilteringDiagnostics
{
    public static EmptyConvertibleScalarFilteringDiagnostics Instance { get; } = new();

    private EmptyConvertibleScalarFilteringDiagnostics() { }

    Diagnostic? IConvertibleScalarFilteringDiagnostics.TypeNotScalar(IConvertibleScalarFilteringContext context, ConvertibleScalarDefinition definition, int index) => null;
    Diagnostic? IConvertibleScalarFilteringDiagnostics.ScalarNotUnbiased(IConvertibleScalarFilteringContext context, ConvertibleScalarDefinition definition, int index) => null;
    Diagnostic? IConvertibleScalarFilteringDiagnostics.ScalarNotBiased(IConvertibleScalarFilteringContext context, ConvertibleScalarDefinition definition, int index) => null;
}
