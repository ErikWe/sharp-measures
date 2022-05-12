namespace SharpMeasures.Generators.Diagnostics;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Units;
using SharpMeasures.Generators.Units.Utility;

internal static partial class DiagnosticRules
{
    public readonly static DiagnosticDescriptor InvalidUnitName = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.InvalidUnitName,
        title: "Invalid unit name",
        messageFormat: "The name of the unit described by \"{0}\" is invalid",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true
    );

    public readonly static DiagnosticDescriptor DuplicateUnitName = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.DuplicateUnitName,
        title: "Duplicate unit name",
        messageFormat: "The unit {0} already defines a unit with the name {1}",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true
    );

    public readonly static DiagnosticDescriptor UnitNameNotRecognized = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.UnitNameNotRecognized,
        title: "Expected the name of a unit",
        messageFormat: "\"{0}\" was not recognized as the name of a unit. The referenced unit must be defined in an attribute of {1}.",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true
    );

    public readonly static DiagnosticDescriptor InvalidUnitPluralForm = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.InvalidUnitPluralForm,
        title: "Invalid plural form of unit name",
        messageFormat: $"\"{{0}}\" was not recognized as the plural form of \"{{1}}\". If the plural form should be identical to the singular form, use " +
            $"{UnitPluralCodes.Unmodified}.",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true
    );

    public readonly static DiagnosticDescriptor DerivedUnitSignatureNotRecognized = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.DerivedUnitSignatureNotRecognized,
        title: "Signature not recognized",
        messageFormat: $"The signature was not recognized. {{0}} should be marked with a {typeof(DerivableUnitAttribute)} describing a matching signature.",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true
    );

    public readonly static DiagnosticDescriptor UnitListNotMatchingSignature = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.UnitListNotMatchingSignature,
        title: "Unit list not matching signature",
        messageFormat: "The number of items in the list should be {0}, to match the provided signature - but {1} was identified",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true
    );

    public readonly static DiagnosticDescriptor UndefinedPrefix = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.UndefinedPrefix,
        title: "Prefix not recognized",
        messageFormat: "The value {0} was not recognized as a {1}",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true
    );
}
