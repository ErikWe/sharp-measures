namespace SharpMeasures.Generators.Quantities.Parsing.Diagnostics.Empty.Processing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Quantities.Parsing.QuantityProcess;

public sealed class EmptyQuantityProcessProcessingDiagnostics : IQuantityProcessProcessingDiagnostics
{
    public static EmptyQuantityProcessProcessingDiagnostics Instance { get; } = new();

    private EmptyQuantityProcessProcessingDiagnostics() { }

    public Diagnostic? NullName(IQuantityProcessProcessingContext context, RawQuantityProcessDefinition definition) => null;
    public Diagnostic? EmptyName(IQuantityProcessProcessingContext context, RawQuantityProcessDefinition definition) => null;
    public Diagnostic? DuplicateProcess(IQuantityProcessProcessingContext context, RawQuantityProcessDefinition definition) => null;
    public Diagnostic? NullExpression(IQuantityProcessProcessingContext context, RawQuantityProcessDefinition definition) => null;
    public Diagnostic? EmptyExpression(IQuantityProcessProcessingContext context, RawQuantityProcessDefinition definition) => null;
    public Diagnostic? PropertyCannotBeUsedWithParameters(IQuantityProcessProcessingContext context, RawQuantityProcessDefinition definition) => null;
    public Diagnostic? UnmatchedParameterDefinitions(IQuantityProcessProcessingContext context, RawQuantityProcessDefinition definition) => null;
    public Diagnostic? NullParameterTypeElement(IQuantityProcessProcessingContext context, RawQuantityProcessDefinition definition, int index) => null;
    public Diagnostic? NullParameterNameElement(IQuantityProcessProcessingContext context, RawQuantityProcessDefinition definition, int index) => null;
    public Diagnostic? EmptyParameterNameElement(IQuantityProcessProcessingContext context, RawQuantityProcessDefinition definition, int index) => null;
    public Diagnostic? DuplicateParameterNameElement(IQuantityProcessProcessingContext context, RawQuantityProcessDefinition definition, int index) => null;
}
