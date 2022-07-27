namespace SharpMeasures.Generators.Diagnostics;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Units;

public static partial class DiagnosticRules
{
    public static readonly DiagnosticDescriptor NullUnitDerivationID = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.InvalidUnitDerivationID,
        title: "Invalid derivation ID",
        messageFormat: "The derivation ID must be defined",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true
    );

    public static readonly DiagnosticDescriptor DuplicateUnitDerivationID = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.DuplicateUnitDerivationID,
        title: "Duplicate derivation ID",
        messageFormat: "{0} already defines a derivation with ID \"{1}\"",
        category: "Naming",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true
    );

    public static readonly DiagnosticDescriptor UnrecognizedUnitDerivationID = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.UnrecognizedUnitDerivationID,
        title: "Derivation ID not recognized",
        messageFormat: $"The derivation ID \"{{0}}\" was not recognized. {{1}} should be decorated by a {Utility.FullAttributeName<DerivableUnitAttribute>()} with this ID.",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true
    );

    public static readonly DiagnosticDescriptor IncompatibleDerivedUnitListSize = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.IncompatibleDerivedUnitListSize,
        title: "Unit list length not matching signature",
        messageFormat: "Expected {0} units to match the derivation signature - but {1} was identified",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true
    );

    public static readonly DiagnosticDescriptor UnitNotDerivable = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.UnitNotDerivable,
        title: "Unit not derivable",
        messageFormat: $"{{0}} has no defined derivations. Add a definition using {Utility.FullAttributeName<DerivableUnitAttribute>()}.",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true
    );

    public static readonly DiagnosticDescriptor AmbiguousDerivationSignatureNotSpecified = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.AmbiguousDerivationSignatureNotSpecified,
        title: "Ambiguous unit derivation signature",
        messageFormat: $"{{0}} contains multiple derivation definitions, but the ID of the intended derivation was not specified",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true
    );
}
