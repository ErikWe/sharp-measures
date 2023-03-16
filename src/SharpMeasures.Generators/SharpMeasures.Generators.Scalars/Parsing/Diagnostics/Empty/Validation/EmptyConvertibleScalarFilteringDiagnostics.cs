namespace SharpMeasures.Generators.Scalars.Parsing.Diagnostics.Empty.Validation;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Scalars.Parsing.ConvertibleScalar;

internal sealed class EmptyConvertibleScalarFilteringDiagnostics : IConvertibleScalarFilteringDiagnostics
{
    public static EmptyConvertibleScalarFilteringDiagnostics Instance { get; } = new();

    private EmptyConvertibleScalarFilteringDiagnostics() { }

    public Diagnostic? TypeNotScalar(IConvertibleScalarFilteringContext context, ConvertibleScalarDefinition definition, int index) => null;
    public Diagnostic? ScalarNotUnbiased(IConvertibleScalarFilteringContext context, ConvertibleScalarDefinition definition, int index) => null;
    public Diagnostic? ScalarNotBiased(IConvertibleScalarFilteringContext context, ConvertibleScalarDefinition definition, int index) => null;
}
