namespace SharpMeasures.SourceGenerators.Analyzers;

using Microsoft.CodeAnalysis;

using System.Diagnostics.CodeAnalysis;

[SuppressMessage("Style", "IDE0090", Justification = "Roslyn-analyzers related to analyzer releases does not consider target-typed new expressions.")]
internal static class DiagnosticRules
{
    public readonly static DiagnosticDescriptor TypeIsNotPartial = new DiagnosticDescriptor(
        id: DiagnosticIDs.TypeIsNotPartial,
        title: "Source generation requires types to be partial",
        messageFormat: "To enable source generation, as indicated by the attribute {0}, {1} has to be made partial",
        category: "Syntax",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true
    );

    public readonly static DiagnosticDescriptor TypeIsNotScalarQuantity = new DiagnosticDescriptor(
        id: DiagnosticIDs.TypeIsNotScalarQuantity,
        title: "Expected a scalar quantity",
        messageFormat: "Argument has to be a scalar quantity. Add the attribute {0} to {1}.",
        category: "Design",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true
    );
}
