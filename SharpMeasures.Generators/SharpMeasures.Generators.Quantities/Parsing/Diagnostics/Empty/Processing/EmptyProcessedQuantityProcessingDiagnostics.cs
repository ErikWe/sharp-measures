namespace SharpMeasures.Generators.Quantities.Parsing.Diagnostics.Empty.Processing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Quantities.Parsing.ProcessedQuantity;

public sealed class EmptyProcessedQuantityProcessingDiagnostics : IProcessedQuantityProcessingDiagnostics
{
    public static EmptyProcessedQuantityProcessingDiagnostics Instance { get; } = new();

    private EmptyProcessedQuantityProcessingDiagnostics() { }

    public Diagnostic? NullName(IProcessedQuantityProcessingContext context, RawProcessedQuantityDefinition definition) => null;
    public Diagnostic? EmptyName(IProcessedQuantityProcessingContext context, RawProcessedQuantityDefinition definition) => null;
    public Diagnostic? DuplicateName(IProcessedQuantityProcessingContext context, RawProcessedQuantityDefinition definition) => null;
    public Diagnostic? NullExpression(IProcessedQuantityProcessingContext context, RawProcessedQuantityDefinition definition) => null;
    public Diagnostic? EmptyExpression(IProcessedQuantityProcessingContext context, RawProcessedQuantityDefinition definition) => null;
    public Diagnostic? PropertyCannotBeUsedWithParameters(IProcessedQuantityProcessingContext context, RawProcessedQuantityDefinition definition) => null;
    public Diagnostic? UnmatchedParameterDefinitions(IProcessedQuantityProcessingContext context, RawProcessedQuantityDefinition definition) => null;
    public Diagnostic? NullParameterTypeElement(IProcessedQuantityProcessingContext context, RawProcessedQuantityDefinition definition, int index) => null;
    public Diagnostic? NullParameterNameElement(IProcessedQuantityProcessingContext context, RawProcessedQuantityDefinition definition, int index) => null;
    public Diagnostic? EmptyParameterNameElement(IProcessedQuantityProcessingContext context, RawProcessedQuantityDefinition definition, int index) => null;
    public Diagnostic? DuplicateParameterNameElement(IProcessedQuantityProcessingContext context, RawProcessedQuantityDefinition definition, int index) => null;
}
