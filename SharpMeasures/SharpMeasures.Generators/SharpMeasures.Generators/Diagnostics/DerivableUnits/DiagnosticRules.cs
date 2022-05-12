namespace SharpMeasures.Generators.Diagnostics;

using Microsoft.CodeAnalysis;

internal static partial class DiagnosticRules
{
    public readonly static DiagnosticDescriptor InvalidUnitDerivationExpression = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.InvalidUnitDerivationExpression,
        title: "Invalid unit derivation expression",
        messageFormat: "The unit derivation described by \"{0}\" is invalid",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true
    );

    public readonly static DiagnosticDescriptor EmptyUnitDerivationSignature = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.EmptyUnitDerivationSignature,
        title: "Expected a non-empty signature",
        messageFormat: "The unit derivation signature should consist of at least 1 unit",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true
    );

    public readonly static DiagnosticDescriptor DuplicateUnitDerivationSignature = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.DuplicateUnitDerivationSignature,
        title: "Duplicate derivation signature",
        messageFormat: "The unit {0} has more than one definition of derivation signature [{1}]",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true
    );
}
