﻿namespace SharpMeasures.Generators.Diagnostics;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Units;
using SharpMeasures.Generators.Units.Utility;

public static partial class DiagnosticRules
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

    public static readonly DiagnosticDescriptor NullUnitName = new DiagnosticDescriptor
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
        messageFormat: $"\"{{0}}\" could not be used to construct the plural form of \"{{1}}\". Try writing the plural form in full, or use a suitable notation " +
            $"from {typeof(UnitPluralCodes).FullName}.",
        category: "Naming",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true
    );

    public static readonly DiagnosticDescriptor NullUnitPluralForm = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.InvalidUnitPluralForm,
        title: "Invalid plural form of unit name",
        messageFormat: $"The plural form of the unit must be defined. Try writing the plural form in full, or use a suitable notation " +
            $"from {typeof(UnitPluralCodes).FullName}.",
        category: "Naming",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true
    );

    public static readonly DiagnosticDescriptor DuplicateUnitName = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.DuplicateUnitName,
        title: "Duplicate unit name",
        messageFormat: "{0} already defines a unit with the name {1}",
        category: "Naming",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true
    );

    public static readonly DiagnosticDescriptor DuplicateUnitPluralForm = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.DuplicateUnitPluralForm,
        title: "Duplicate unit plural form",
        messageFormat: "{0} already defines a unit with the plural form {1}",
        category: "Naming",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true
    );

    public static readonly DiagnosticDescriptor UnrecognizedUnitName = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.UnrecognizedUnitName,
        title: "Expected the name of a unit",
        messageFormat: "\"{0}\" was not recognized as the name of a {1}. \"{0}\" should be defined through an attribute applied to {1} - for example; " +
            $"{typeof(FixedUnitAttribute).FullName}.",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true
    );

    public static readonly DiagnosticDescriptor NullUnrecognizedUnitName = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.UnrecognizedUnitName,
        title: "Expected the name of a unit",
        messageFormat: "Expected the name of a {0}",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true
    );

    public static readonly DiagnosticDescriptor NullUnrecognizedUnitNameUnknownType = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.UnrecognizedUnitName,
        title: "Expected the name of a unit",
        messageFormat: "Expected the name of a unit",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true
    );

    public static readonly DiagnosticDescriptor CyclicUnitDependency = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.CyclicUnitDependency,
        title: "Cyclic unit dependency",
        messageFormat: "{0} has a cyclic dependency on other instances of {1}",
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

    public static readonly DiagnosticDescriptor FixedUnitBiasSpecifiedButUnitNotBiased = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.FixedUnitBiasSpecifiedButUnitNotBiased,
        title: "The unit does not support biases",
        messageFormat: $"{{0}} does not support biases. Do not specify a bias term, or make {{0}} support biases through {typeof(SharpMeasuresUnitAttribute).FullName}.",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true,
        customTags: WellKnownDiagnosticTags.Unnecessary
    );

    public static readonly DiagnosticDescriptor BiasedUnitDefinedButUnitNotBiased = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.BiasedUnitDefinedButUnitNotBiased,
        title: "The unit does not support biases",
        messageFormat: $"{{0}} could not be implemented, as {{1}} does not support biases. Remove {{0}}, or make {{1}} support biases through " +
            $"{typeof(SharpMeasuresUnitAttribute).FullName}.",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true
    );
}
