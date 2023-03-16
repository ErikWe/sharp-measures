namespace SharpMeasures.Generators.Vectors.Parsing.Diagnostics.Empty.Processing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Vectors.Parsing.VectorOperation;

internal sealed class EmptyVectorOperationProcessingDiagnostics : IVectorOperationProcessingDiagnostics
{
    public static EmptyVectorOperationProcessingDiagnostics Instance { get; } = new();

    private EmptyVectorOperationProcessingDiagnostics() { }

    public Diagnostic? NullResult(IVectorOperationProcessingContext context, RawVectorOperationDefinition definition) => null;
    public Diagnostic? NullOther(IVectorOperationProcessingContext context, RawVectorOperationDefinition definition) => null;
    public Diagnostic? UnrecognizedOperatorType(IVectorOperationProcessingContext context, RawVectorOperationDefinition definition) => null;
    public Diagnostic? UnrecognizedPosition(IVectorOperationProcessingContext context, RawVectorOperationDefinition definition) => null;
    public Diagnostic? MirrorNotSupported(IVectorOperationProcessingContext context, RawVectorOperationDefinition definition) => null;
    public Diagnostic? NullName(IVectorOperationProcessingContext context, RawVectorOperationDefinition definition) => null;
    public Diagnostic? EmptyName(IVectorOperationProcessingContext context, RawVectorOperationDefinition definition) => null;
    public Diagnostic? NullMirroredName(IVectorOperationProcessingContext context, RawVectorOperationDefinition definition) => null;
    public Diagnostic? EmptyMirroredName(IVectorOperationProcessingContext context, RawVectorOperationDefinition definition) => null;
    public Diagnostic? MirrorDisabledButNameSpecified(IVectorOperationProcessingContext context, RawVectorOperationDefinition definition) => null;
    public Diagnostic? DuplicateName(IVectorOperationProcessingContext context, RawVectorOperationDefinition definition, string name) => null;
    public Diagnostic? DuplicateMirroredName(IVectorOperationProcessingContext context, RawVectorOperationDefinition definition, string name) => null;
}
