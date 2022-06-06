namespace SharpMeasures.Generators.Diagnostics;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Units;

internal static partial class DiagnosticRules
{
    public static readonly DiagnosticDescriptor InvalidUnitDerivationExpression = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.InvalidUnitDerivationExpression,
        title: "Invalid unit derivation expression",
        messageFormat: "The unit derivation described by \"{0}\" is invalid",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true
    );

    public static readonly DiagnosticDescriptor InvalidUnitDerivationExpression_Null = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.InvalidUnitDerivationExpression,
        title: "Invalid unit derivation expression",
        messageFormat: "Expected an expression describing the derivation",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true
    );

    public static readonly DiagnosticDescriptor EmptyUnitDerivationSignature = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.EmptyUnitDerivationSignature,
        title: "Expected a non-empty signature",
        messageFormat: "The unit derivation signature should consist of at least 1 unit",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true
    );

    public static readonly DiagnosticDescriptor DuplicateUnitDerivationSignature = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.DuplicateUnitDerivationSignature,
        title: "Duplicate derivation signature",
        messageFormat: "The unit {0} already defines this derivation signature",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true
    );

    public static readonly DiagnosticDescriptor UnrecognizedUnitDerivationSignature = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.UnrecognizedDerivedUnitSignature,
        title: "Derivation signature not recognized",
        messageFormat: $"The signature was not recognized. {{0}} should be marked with a {typeof(DerivableUnitAttribute)} describing a matching signature.",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true
    );

    public static readonly DiagnosticDescriptor UnitListNotMatchingSignature = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.UnitListNotMatchingSignature,
        title: "Unit list not matching signature",
        messageFormat: "The number of items in the list should be {0}, to match the provided signature - but {1} was identified",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true
    );
}
