namespace SharpMeasures.Generators.Quantities.Parsing.Diagnostics.Processing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Quantities.Parsing.ProcessedQuantity;

using System.Globalization;

public sealed class ProcessedQuantityProcessingDiagnostics : IProcessedQuantityProcessingDiagnostics
{
    public static ProcessedQuantityProcessingDiagnostics Instance { get; } = new();

    private ProcessedQuantityProcessingDiagnostics() { }

    public Diagnostic NullName(IProcessedQuantityProcessingContext context, RawProcessedQuantityDefinition definition)
    {
        return DiagnosticConstruction.InvalidProcessName(definition.Locations.Name?.AsRoslynLocation());
    }

    public Diagnostic EmptyName(IProcessedQuantityProcessingContext context, RawProcessedQuantityDefinition definition) => NullName(context, definition);

    public Diagnostic DuplicateName(IProcessedQuantityProcessingContext context, RawProcessedQuantityDefinition definition)
    {
        return DiagnosticConstruction.DuplicateProcessName(definition.Locations.Name?.AsRoslynLocation(), context.Type.Name, definition.Name!);
    }

    public Diagnostic NullExpression(IProcessedQuantityProcessingContext context, RawProcessedQuantityDefinition definition)
    {
        return DiagnosticConstruction.InvalidProcessExpression(definition.Locations.Expression?.AsRoslynLocation());
    }

    public Diagnostic EmptyExpression(IProcessedQuantityProcessingContext context, RawProcessedQuantityDefinition definition) => NullExpression(context, definition);

    public Diagnostic PropertyCannotBeUsedWithParameters(IProcessedQuantityProcessingContext context, RawProcessedQuantityDefinition definition)
    {
        return DiagnosticConstruction.ProcessPropertyIncompatibleWithParameters(definition.Locations.ImplementAsProperty?.AsRoslynLocation(), definition.Name!);
    }

    public Diagnostic UnmatchedParameterDefinitions(IProcessedQuantityProcessingContext context, RawProcessedQuantityDefinition definition)
    {
        return DiagnosticConstruction.UnmatchedProcessParameterDefinitions(definition.Locations.AttributeName.AsRoslynLocation(), definition.Name!, definition.ParameterTypes.Count.ToString(CultureInfo.InvariantCulture), definition.ParameterNames.Count.ToString(CultureInfo.InvariantCulture));
    }

    public Diagnostic NullParameterTypeElement(IProcessedQuantityProcessingContext context, RawProcessedQuantityDefinition definition, int index)
    {
        return DiagnosticConstruction.NullProcessParameterType(definition.Locations.ParameterTypeElements[index].AsRoslynLocation());
    }

    public Diagnostic NullParameterNameElement(IProcessedQuantityProcessingContext context, RawProcessedQuantityDefinition definition, int index)
    {
        return DiagnosticConstruction.InvalidProcessParameterName(definition.Locations.ParameterNameElements[index].AsRoslynLocation());
    }

    public Diagnostic EmptyParameterNameElement(IProcessedQuantityProcessingContext context, RawProcessedQuantityDefinition definition, int index) => NullParameterNameElement(context, definition, index);

    public Diagnostic DuplicateParameterNameElement(IProcessedQuantityProcessingContext context, RawProcessedQuantityDefinition definition, int index)
    {
        return DiagnosticConstruction.DuplicateProcessParameterName(definition.Locations.ParameterNameElements[index].AsRoslynLocation(), definition.Name!, definition.ParameterNames[index]!);
    }
}
