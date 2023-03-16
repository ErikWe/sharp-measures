namespace SharpMeasures.Generators.Vectors.Parsing.Diagnostics.Empty.Validation;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Vectors.Parsing.ConvertibleVector;

internal sealed class EmptyConvertibleVectorFilteringDiagnostics : IConvertibleVectorFilteringDiagnostics
{
    public static EmptyConvertibleVectorFilteringDiagnostics Instance { get; } = new();

    private EmptyConvertibleVectorFilteringDiagnostics() { }

    public Diagnostic? TypeNotVector(IConvertibleVectorFilteringContext context, ConvertibleVectorDefinition definition, int index) => null;
    public Diagnostic? TypeNotVectorGroup(IConvertibleVectorFilteringContext context, ConvertibleVectorDefinition definition, int index) => null;
    public Diagnostic? DuplicateVector(IConvertibleVectorFilteringContext context, ConvertibleVectorDefinition definition, int index) => null;
    public Diagnostic? VectorUnexpectedDimension(IConvertibleVectorFilteringContext context, ConvertibleVectorDefinition definition, int index, int dimension) => null;
    public Diagnostic? VectorGroupLacksMemberOfMatchingDimension(IConvertibleVectorFilteringContext context, ConvertibleVectorDefinition definition, int index) => null;
}
