namespace SharpMeasures.Generators.Diagnostics;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Units;

public static partial class DiagnosticRules
{
    public static readonly DiagnosticDescriptor InvalidUnitInstanceName = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.InvalidUnitInstanceName,
        title: "Invalid unit name",
        messageFormat: "\"{0}\" can not be used as the name of a unit",
        category: "Naming",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true
    );

    public static readonly DiagnosticDescriptor NullUnitInstanceName = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.InvalidUnitInstanceName,
        title: "Invalid unit name",
        messageFormat: "The name of the unit must be defined",
        category: "Naming",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true
    );

    public static readonly DiagnosticDescriptor InvalidUnitInstancePluralForm = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.InvalidUnitInstancePluralForm,
        title: "Invalid plural form of unit name",
        messageFormat: $"\"{{0}}\" could not be used to construct the plural form of \"{{1}}\". Write the plural form explicitly, or use a suitable notation from {typeof(CommonPluralNotations).FullName}.",
        category: "Naming",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true
    );

    public static readonly DiagnosticDescriptor NullUnitInstancePluralForm = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.InvalidUnitInstancePluralForm,
        title: "Invalid plural form of unit name",
        messageFormat: $"The plural form of the unit must be defined. Write the plural form explicitly, or use a suitable notation from {typeof(CommonPluralNotations).FullName}.",
        category: "Naming",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true
    );

    public static readonly DiagnosticDescriptor DuplicateUnitInstanceName = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.DuplicateUnitInstanceName,
        title: "Duplicate unit name",
        messageFormat: "{0} already defines a unit \"{1}\"",
        category: "Naming",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true
    );

    public static readonly DiagnosticDescriptor UnitInstanceNameReservedByUnitInstancePluralForm = new DiagnosticDescriptor
   (
       id: DiagnosticIDs.DuplicateUnitInstanceName,
       title: "Duplicate unit name",
       messageFormat: "{0} already associates the singular form of \"{1}\" with another unit",
       category: "Naming",
       defaultSeverity: DiagnosticSeverity.Warning,
       isEnabledByDefault: true
   );

    public static readonly DiagnosticDescriptor DuplicateUnitInstancePluralForm = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.DuplicateUnitInstancePluralForm,
        title: "Duplicate unit plural form",
        messageFormat: "{0} already defines a unit with plural form \"{1}\"",
        category: "Naming",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true
    );

    public static readonly DiagnosticDescriptor UnitInstancePluralFormReservedByUnitInstanceName = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.DuplicateUnitInstancePluralForm,
        title: "Duplicate unit plural form",
        messageFormat: "{0} already associates \"{1}\" with the singular form of another unit",
        category: "Naming",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true
    );

    public static readonly DiagnosticDescriptor UnrecognizedUnitInstanceName = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.UnrecognizedUnitInstanceName,
        title: "Expected the name of a unit",
        messageFormat: $"\"{{0}}\" was not recognized as the name of a {{1}}. \"{{0}}\" should be defined through an attribute applied to {{1}} - for example, {Utility.FullAttributeName<FixedUnitInstanceAttribute>()}.",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true
    );

    public static readonly DiagnosticDescriptor NullUnrecognizedUnitInstanceName = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.UnrecognizedUnitInstanceName,
        title: "Expected the name of a unit",
        messageFormat: "Expected the name of a {0}",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true
    );

    public static readonly DiagnosticDescriptor NullUnrecognizedUnitInstnaceNameUnknownType = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.UnrecognizedUnitInstanceName,
        title: "Expected the name of a unit",
        messageFormat: "Expected the name of a unit",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true
    );

    public static readonly DiagnosticDescriptor CyclicallyModifiedUnitInstances = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.CyclicallyModifiedUnitInstances,
        title: "Cyclic unit dependency",
        messageFormat: "\"{0}\" has a cyclic dependency on other instances of {1}",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true
    );

    public static readonly DiagnosticDescriptor DerivableUnitShouldNotUseFixed = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.DerivableUnitShouldNotUseFixed,
        title: "Derivable unit should not use FixedUnit",
        messageFormat: $"As {{0}} can be derived from other units, defining instances through {Utility.AttributeName(typeof(FixedUnitInstanceAttribute).Name)} would result in multiple independent units",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true
    );

    public static readonly DiagnosticDescriptor NullScaledUnitExpression = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.InvalidScaledUnitExpression,
        title: "Invalid scaled unit expression",
        messageFormat: "The expression describing the scaling factor must be defined",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true
    );

    public static readonly DiagnosticDescriptor NullBiasedUnitExpression = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.InvalidBiasedUnitExpression,
        title: "Invalid biased unit expression",
        messageFormat: "The expression describing the bias must be defined",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true
    );

    public static readonly DiagnosticDescriptor BiasedUnitDefinedButUnitNotBiased = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.BiasedUnitDefinedButUnitNotBiased,
        title: "Unit does not support biased instances",
        messageFormat: "The biased unit \"{0}\" could not be implemented, as {1} does not include a bias term",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true
    );

    public static readonly DiagnosticDescriptor UnitWithBiasTermCannotBeDerived = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.UnitWithBiasTermCannotBeDerived,
        title: "Unit with bias term cannot be derived",
        messageFormat: "As {0} includes a bias term, it can not be derived from other units",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true
    );
}
