﻿namespace SharpMeasures.Generators.Diagnostics;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Units;

using System.Diagnostics.CodeAnalysis;

[SuppressMessage("Style", "IDE0090", Justification = "Roslyn-analyzers related to analyzer releases does not consider target-typed new expressions.")]
internal static partial class DiagnosticRules
{
    public readonly static DiagnosticDescriptor TypeNotPartial = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.TypeNotPartial,
        title: "Expected a partial type",
        messageFormat: "To enable a source generator, as indicated by the attribute {0}, {1} has to be made partial",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true
    );

    public readonly static DiagnosticDescriptor TypeNotScalarQuantity = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.TypeNotScalarQuantity,
        title: "Expected a scalar quantity",
        messageFormat: $"Expected a scalar quantity. Add the attribute {typeof(GeneratedScalarQuantityAttribute).FullName} to {{0}} " +
            $"for it to be recognized as a scalar quantity.",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true
    );

    public readonly static DiagnosticDescriptor TypeNotUnbiasedScalarQuantity = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.TypeNotUnbiasedScalarQuantity,
        title: "Expected an unbiased type",
        messageFormat: "Argument should be an unbiased scalar quantity. Make {0} an unbiased scalar quantity, or use another type.",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true
    );

    public readonly static DiagnosticDescriptor TypeNotUnit = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.TypeNotUnit,
        title: "Expected a unit",
        messageFormat: $"Expected a unit. Add the attribute {typeof(GeneratedUnitAttribute).FullName} to {{0}} for it to be recognized as a unit.",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true
    );
}
