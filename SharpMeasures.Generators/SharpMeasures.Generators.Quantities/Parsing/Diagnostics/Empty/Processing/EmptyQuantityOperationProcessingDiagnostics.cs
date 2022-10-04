namespace SharpMeasures.Generators.Quantities.Parsing.Diagnostics.Empty.Processing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Quantities.Parsing.QuantityOperation;

public sealed class EmptyQuantityOperationProcessingDiagnostics : IQuantityOperationProcessingDiagnostics
{
    public static EmptyQuantityOperationProcessingDiagnostics Instance { get; } = new();

    private EmptyQuantityOperationProcessingDiagnostics() { }

    public Diagnostic? NullResult(IQuantityOperationProcessingContext context, RawQuantityOperationDefinition definition) => null;
    public Diagnostic? NullOther(IQuantityOperationProcessingContext context, RawQuantityOperationDefinition definition) => null;
    public Diagnostic? UnrecognizedOperatorType(IQuantityOperationProcessingContext context, RawQuantityOperationDefinition definition) => null;
    public Diagnostic? UnrecognizedPosition(IQuantityOperationProcessingContext context, RawQuantityOperationDefinition definition) => null;
    public Diagnostic? UnrecognizedImplementation(IQuantityOperationProcessingContext context, RawQuantityOperationDefinition definition) => null;
    public Diagnostic? NullMethodName(IQuantityOperationProcessingContext context, RawQuantityOperationDefinition definition) => null;
    public Diagnostic? EmptyMethodName(IQuantityOperationProcessingContext context, RawQuantityOperationDefinition definition) => null;
    public Diagnostic? MethodDisabledButNameSpecified(IQuantityOperationProcessingContext context, RawQuantityOperationDefinition definition) => null;
    public Diagnostic? DuplicateOperator(IQuantityOperationProcessingContext context, RawQuantityOperationDefinition definition) => null;
    public Diagnostic? DuplicateMirroredOperator(IQuantityOperationProcessingContext context, RawQuantityOperationDefinition definition) => null;
    public Diagnostic? DuplicateMethod(IQuantityOperationProcessingContext context, RawQuantityOperationDefinition definition, string name) => null;
    public Diagnostic? DuplicateMirroredMethod(IQuantityOperationProcessingContext context, RawQuantityOperationDefinition definition, string name) => null;
    public Diagnostic? MirrorNotSupported(IQuantityOperationProcessingContext context, RawQuantityOperationDefinition definition) => null;
    public Diagnostic? NullMirroredMethodName(IQuantityOperationProcessingContext context, RawQuantityOperationDefinition definition) => null;
    public Diagnostic? EmptyMirroredMethodName(IQuantityOperationProcessingContext context, RawQuantityOperationDefinition definition) => null;
    public Diagnostic? MirrorDisabledButMethodNameSpecified(IQuantityOperationProcessingContext context, RawQuantityOperationDefinition definition) => null;
    public Diagnostic? MethodDisabledButMirroredNameSpecified(IQuantityOperationProcessingContext context, RawQuantityOperationDefinition definition) => null;
    public Diagnostic? MirroredMethodNotSupportedButNameSpecified(IQuantityOperationProcessingContext context, RawQuantityOperationDefinition definition) => null;
}
