﻿namespace SharpMeasures.Generators.Diagnostics;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Quantities.Utility;
using SharpMeasures.Generators.Scalars.Utility;

internal static partial class DiagnosticRules
{
    public readonly static DiagnosticDescriptor DefineQuantityUnitAndSymbol_MissingUnit = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.DefineQuantityUnitAndSymbol,
        title: "Expected both unit name and symbol",
        messageFormat: "The symbol for the default unit of the quantity was specified, but not the name of the default unit",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true
    );

    public readonly static DiagnosticDescriptor DefineQuantityUnitAndSymbol_MissingSymbol = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.DefineQuantityUnitAndSymbol,
        title: "Expected both unit name and symbol",
        messageFormat: "The name of the default unit of the quantity was specified, but not the associated symbol",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true
    );

    public readonly static DiagnosticDescriptor InvalidConstantName = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.InvalidConstantName,
        title: "Invalid name of constant",
        messageFormat: "\"{0}\" can not be used as the name of a constant",
        category: "Naming",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true
    );

    public readonly static DiagnosticDescriptor InvalidConstantName_Null = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.InvalidConstantName,
        title: "Invalid name of constant",
        messageFormat: "The name of the constant must be defined",
        category: "Naming",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true
    );

    public readonly static DiagnosticDescriptor InvalidConstantMultiplesName = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.InvalidConstantMultiplesName,
        title: "Invalid name for multiples of constant",
        messageFormat: "\"{0}\" can not be used as the name for multiples of {1}. Try writing the name in full, or use a suitable notation" +
            $"from {typeof(ConstantMultiplesCodes).FullName}.",
        category: "Naming",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true
    );

    public readonly static DiagnosticDescriptor InvalidConstantMultiplesName_Null = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.InvalidConstantMultiplesName,
        title: "Invalid name for multiples of constant",
        messageFormat: "The name for multiples of {0} must be defined. Try writing the name in full, or use a suitable notation" +
            $"from {typeof(ConstantMultiplesCodes).FullName}.",
        category: "Naming",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true
    );

    public readonly static DiagnosticDescriptor DuplicateConstantName = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.DuplicateConstantName,
        title: "Duplicate name of constant",
        messageFormat: "The quantity {0} already defines a constant with the name {1}",
        category: "Naming",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true
    );

    public readonly static DiagnosticDescriptor DuplicateConstantMultiplesName = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.DuplicateConstantMultiplesName,
        title: "Duplicate name for multiples of constant",
        messageFormat: "The quantity {0} already uses the name {1} to describe the magnitude in multiples of a constant",
        category: "Naming",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true
    );

    public readonly static DiagnosticDescriptor ConstantSharesNameWithUnit = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.ConstantSharesNameWithUnit,
        title: "Constant shares name with unit",
        messageFormat: "The quantity {0} already associates the name {1} with a unit",
        category: "Naming",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true
    );

    public readonly static DiagnosticDescriptor UnrecognizedCastOperatorBehaviour = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.UnrecognizedCastOperatorBehaviour,
        title: "Unrecognized cast operator behaviour",
        messageFormat: $"{{0}} was not recognized as a {typeof(ConversionOperationBehaviour)}",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true
    );

    public readonly static DiagnosticDescriptor ContradictoryAttributes = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.ContradictoryAttributes,
        title: "Contradictory attributes",
        messageFormat: "Contradictory attributes {0} and {1} were used",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true
    );

    public readonly static DiagnosticDescriptor QuantityGroupMissingBase = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.QuantityGroupMissingBase,
        title: "Quantity group missing base quantity",
        messageFormat: "Could not identify the base quantity of the associated group of quantities. Exactly one quantity in the group should be decorated " +
            "with the attribute {0}.",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true
    );
}
