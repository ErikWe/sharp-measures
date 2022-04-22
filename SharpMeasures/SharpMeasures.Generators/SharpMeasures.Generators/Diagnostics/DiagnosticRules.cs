namespace SharpMeasures.Generators.Diagnostics;

using Microsoft.CodeAnalysis;

using System.Diagnostics.CodeAnalysis;

[SuppressMessage("Style", "IDE0090", Justification = "Roslyn-analyzers related to analyzer releases does not consider target-typed new expressions.")]
internal static class DiagnosticRules
{
    public readonly static DiagnosticDescriptor TypeNotPartial = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.TypeNotPartial,
        title: "Source generation requires types to be partial",
        messageFormat: "To enable source generation, as indicated by the attribute {0}, {1} has to be made partial",
        category: "Design",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true
    );

    public readonly static DiagnosticDescriptor TypeNotScalarQuantity = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.TypeNotScalarQuantity,
        title: "Expected a scalar quantity",
        messageFormat: "Argument should be a scalar quantity. Add the attribute {0} to {1} for it to be recognized as a scalar quantity.",
        category: "Design",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true
    );

    public readonly static DiagnosticDescriptor TypeNotUnbiasedScalarQuantity = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.TypeNotUnbiasedScalarQuantity,
        title: "Expected an unbiased type",
        messageFormat: "Argument should be an unbiased scalar quantity. Make {0} an unbiased scalar quantity, or use another type.",
        category: "Design",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true
    );
}
