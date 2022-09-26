namespace SharpMeasures.Generators.Vectors.Parsing.Diagnostics.Empty.Validation;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Vectors.Parsing.ConvertibleVector;

internal sealed class EmptyConvertibleVectorFilteringDiagnostics : IConvertibleVectorFilteringDiagnostics
{
    public static EmptyConvertibleVectorFilteringDiagnostics Instance { get; } = new();

    private EmptyConvertibleVectorFilteringDiagnostics() { }

    Diagnostic? IConvertibleVectorFilteringDiagnostics.TypeNotVector(IConvertibleVectorFilteringContext context, ConvertibleVectorDefinition definition, int index) => null;
    Diagnostic? IConvertibleVectorFilteringDiagnostics.TypeNotVectorGroup(IConvertibleVectorFilteringContext context, ConvertibleVectorDefinition definition, int index) => null;
    Diagnostic? IConvertibleVectorFilteringDiagnostics.DuplicateVector(IConvertibleVectorFilteringContext context, ConvertibleVectorDefinition definition, int index) => null;
    Diagnostic? IConvertibleVectorFilteringDiagnostics.VectorUnexpectedDimension(IConvertibleVectorFilteringContext context, ConvertibleVectorDefinition definition, int index, int dimension) => null;
    Diagnostic? IConvertibleVectorFilteringDiagnostics.VectorGroupLacksMemberOfMatchingDimension(IConvertibleVectorFilteringContext context, ConvertibleVectorDefinition definition, int index) => null;
}
