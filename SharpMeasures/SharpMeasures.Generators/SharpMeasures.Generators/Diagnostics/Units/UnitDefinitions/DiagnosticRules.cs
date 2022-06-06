namespace SharpMeasures.Generators.Diagnostics;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Units.Utility;

internal static partial class DiagnosticRules
{
    public static readonly DiagnosticDescriptor InvalidUnitName = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.InvalidUnitName,
        title: "Invalid unit name",
        messageFormat: "\"{0}\" can not be used as the name of a unit",
        category: "Naming",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true
    );

    public static readonly DiagnosticDescriptor InvalidUnitName_Null = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.InvalidUnitName,
        title: "Invalid unit name",
        messageFormat: "The name of the unit must be defined",
        category: "Naming",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true
    );

    public static readonly DiagnosticDescriptor InvalidUnitPluralForm = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.InvalidUnitPluralForm,
        title: "Invalid plural form of unit name",
        messageFormat: $"\"{{0}}\" could not be used to construct the plural form of \"{{1}}\". Try writing the plural form in full, or use a suitable notation" +
            $"from {typeof(UnitPluralCodes).FullName}.",
        category: "Naming",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true
    );

    public static readonly DiagnosticDescriptor InvalidUnitPluralForm_Null = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.InvalidUnitPluralForm,
        title: "Invalid plural form of unit name",
        messageFormat: $"The plural form of the unit must be defined. Try writing the plural form in full, or use a suitable notation" +
            $"from {typeof(UnitPluralCodes).FullName}.",
        category: "Naming",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true
    );

    public static readonly DiagnosticDescriptor DuplicateUnitName = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.DuplicateUnitName,
        title: "Duplicate unit name",
        messageFormat: "The unit {0} already defines a unit with the name {1}",
        category: "Naming",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true
    );

    public static readonly DiagnosticDescriptor DuplicateUnitPluralForm = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.DuplicateUnitPluralForm,
        title: "Duplicate unit plural form",
        messageFormat: "The unit {0} already defines a unit with the plural form {1}",
        category: "Naming",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true
    );

    public static readonly DiagnosticDescriptor UnrecognizedUnitName = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.UnrecognizedUnitName,
        title: "Expected the name of a unit",
        messageFormat: "\"{0}\" was not recognized as the name of a unit of {1}. The referenced unit must be defined in an attribute on {1}.",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true
    );

    public static readonly DiagnosticDescriptor UnrecognizedUnitName_Null = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.UnrecognizedUnitName,
        title: "Expected the name of a unit",
        messageFormat: "Expected the name of a unit of {0}",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true
    );

    public static readonly DiagnosticDescriptor CyclicUnitDependency = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.CyclicUnitDependency,
        title: "Cyclic unit dependency",
        messageFormat: "{0} has a cyclic dependency on other units of {1}",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true
    );

    public static readonly DiagnosticDescriptor UnrecognizedPrefix = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.UnrecognizedPrefix,
        title: "Prefix not recognized",
        messageFormat: "{0} was not recognized as a {1}",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true
    );
}
