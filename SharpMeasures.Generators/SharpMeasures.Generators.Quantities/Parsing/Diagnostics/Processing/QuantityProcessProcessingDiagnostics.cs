namespace SharpMeasures.Generators.Quantities.Parsing.Diagnostics.Processing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Quantities.Parsing.QuantityProcess;

using System.Globalization;

public sealed class QuantityProcessProcessingDiagnostics : IQuantityProcessProcessingDiagnostics
{
    public static QuantityProcessProcessingDiagnostics Instance { get; } = new();

    private QuantityProcessProcessingDiagnostics() { }

    public Diagnostic NullName(IQuantityProcessProcessingContext context, RawQuantityProcessDefinition definition)
    {
        return DiagnosticConstruction.InvalidProcessName(definition.Locations.Name?.AsRoslynLocation());
    }

    public Diagnostic EmptyName(IQuantityProcessProcessingContext context, RawQuantityProcessDefinition definition) => NullName(context, definition);

    public Diagnostic DuplicateProcess(IQuantityProcessProcessingContext context, RawQuantityProcessDefinition definition)
    {
        return DiagnosticConstruction.DuplicateProcessName(definition.Locations.Name?.AsRoslynLocation(), context.Type.Name, definition.Name!);
    }

    public Diagnostic NullExpression(IQuantityProcessProcessingContext context, RawQuantityProcessDefinition definition)
    {
        return DiagnosticConstruction.InvalidProcessExpression(definition.Locations.Expression?.AsRoslynLocation());
    }

    public Diagnostic EmptyExpression(IQuantityProcessProcessingContext context, RawQuantityProcessDefinition definition) => NullExpression(context, definition);

    public Diagnostic PropertyCannotBeUsedWithParameters(IQuantityProcessProcessingContext context, RawQuantityProcessDefinition definition)
    {
        return DiagnosticConstruction.ProcessPropertyIncompatibleWithParameters(definition.Locations.ImplementAsProperty?.AsRoslynLocation(), definition.Name!);
    }

    public Diagnostic UnmatchedParameterDefinitions(IQuantityProcessProcessingContext context, RawQuantityProcessDefinition definition)
    {
        return DiagnosticConstruction.UnmatchedProcessParameterDefinitions(definition.Locations.AttributeName.AsRoslynLocation(), definition.Name!, definition.ParameterTypes.Count.ToString(CultureInfo.InvariantCulture), definition.ParameterNames.Count.ToString(CultureInfo.InvariantCulture));
    }

    public Diagnostic NullParameterTypeElement(IQuantityProcessProcessingContext context, RawQuantityProcessDefinition definition, int index)
    {
        return DiagnosticConstruction.NullProcessParameterType(definition.Locations.ParameterTypeElements[index].AsRoslynLocation());
    }

    public Diagnostic NullParameterNameElement(IQuantityProcessProcessingContext context, RawQuantityProcessDefinition definition, int index)
    {
        return DiagnosticConstruction.InvalidProcessParameterName(definition.Locations.ParameterNameElements[index].AsRoslynLocation());
    }

    public Diagnostic EmptyParameterNameElement(IQuantityProcessProcessingContext context, RawQuantityProcessDefinition definition, int index) => NullParameterNameElement(context, definition, index);

    public Diagnostic DuplicateParameterNameElement(IQuantityProcessProcessingContext context, RawQuantityProcessDefinition definition, int index)
    {
        return DiagnosticConstruction.DuplicateProcessParameterName(definition.Locations.ParameterNameElements[index].AsRoslynLocation(), definition.Name!, definition.ParameterNames[index]!);
    }
}
