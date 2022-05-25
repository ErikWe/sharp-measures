namespace SharpMeasures.Generators.Diagnostics;

using Microsoft.CodeAnalysis;

internal static partial class DiagnosticRules
{
    public readonly static DiagnosticDescriptor InvalidConstantName = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.InvalidConstantName,
        title: "Invalid name of constant",
        messageFormat: "\"{0}\" can not be used as the name of a constant",
        category: "Naming",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true
    );

    public readonly static DiagnosticDescriptor InvalidConstantMultiplesName = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.InvalidConstantMultiplesName,
        title: "Invalid name for multiples of constant",
        messageFormat: "\"{0}\" can not be used as the name for multiples of {1}",
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
        messageFormat: "The quantity {0} already uses the name {1} to describe multiples of a constant",
        category: "Naming",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true
    );

    public readonly static DiagnosticDescriptor DuplicateConstantMultiplesName_Unit = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.DuplicateConstantMultiplesName,
        title: "Duplicate name for multiples of constant",
        messageFormat: "The quantity {0} already uses the name {1} for describing it in a unit",
        category: "Naming",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true
    );

    public readonly static DiagnosticDescriptor ExcessiveExclusion = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.ExcessiveExclusion,
        title: "Excessive exclusion",
        messageFormat: "Complementary attributes {0} and {1} were used, making {1} redundant",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true
    );
}
