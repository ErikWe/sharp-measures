namespace SharpMeasures.Generators.Diagnostics;

using Microsoft.CodeAnalysis;

public static partial class DiagnosticRules
{
    public static readonly DiagnosticDescriptor MultipleDerivationSignaturesButNotNamed = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.MultipleDerivationSignaturesButNotNamed,
        title: "Unnamed derivation signature",
        messageFormat: "The derivation ID must be specified, as {0} defines multiple derivations",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true
    );

    public static readonly DiagnosticDescriptor AmbiguousDerivationSignatureNotSpecified = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.AmbiguousDerivationSignatureNotSpecified,
        title: "Ambiguous unit derivation signature",
        messageFormat: $"{{0}} contains multiple derivation definitions, but the ID of the intended derivation was not specified",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true
    );

    public static readonly DiagnosticDescriptor DuplicateUnitDerivationID = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.DuplicateUnitDerivationID,
        title: "Duplicate derivation ID",
        messageFormat: "{0} already defines a derivation with ID \"{1}\"",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true
    );

    public static readonly DiagnosticDescriptor DuplicateUnitDerivationSignature = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.DuplicateUnitDerivationSignature,
        title: "Duplicate derivation signature",
        messageFormat: "{0} already defines a derivation with an identical signature",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true
    );

    public static readonly DiagnosticDescriptor UnrecognizedUnitDerivationID = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.UnrecognizedUnitDerivationID,
        title: "Derivation ID not recognized",
        messageFormat: "{0} does not define a derivation with ID \"{1}\"",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true
    );

    public static readonly DiagnosticDescriptor IncompatibleDerivedUnitListSize = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.IncompatibleDerivedUnitListSize,
        title: "Unit list length not matching signature",
        messageFormat: "Expected {0} units to match the derivation signature - but received {1}",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true
    );

    public static readonly DiagnosticDescriptor UnitNotDerivable = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.UnitNotDerivable,
        title: "Unit not derivable",
        messageFormat: $"{{0}} has no defined derivations. Add a definition using {Utility.FullAttributeName<DerivableUnitAttribute>()}.",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true
    );

    public static readonly DiagnosticDescriptor DerivationSignatureNotPermutable = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.DerivationSignatureNotPermutable,
        title: "Derivation signature is not permutable",
        messageFormat: $"The derivation signature cannot be permuted",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        customTags: WellKnownDiagnosticTags.Unnecessary
    );

    public static readonly DiagnosticDescriptor UnmatchedDerivationExpressionUnit = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.UnmatchedDerivationExpressionUnit,
        title: "Unmatched derivation expression unit",
        messageFormat: $"The signature does not contain a unit with index {{0}}",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true
    );

    public static readonly DiagnosticDescriptor ExpressionDoesNotIncludeUnit = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.ExpressionDoesNotIncludeUnit,
        title: "Unit is not included in expression",
        messageFormat: $"The expression does not include the unit with index {{0}}",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true
    );
}
