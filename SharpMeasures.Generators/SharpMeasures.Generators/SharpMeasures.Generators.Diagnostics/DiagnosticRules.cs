namespace SharpMeasures.Generators.Diagnostics;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Units;
using SharpMeasures.Generators.Utility;
using SharpMeasures.Generators.Vectors;

using System.Diagnostics.CodeAnalysis;

[SuppressMessage("Style", "IDE0090", Justification = "Roslyn-analyzers related to analyzer releases does not consider target-typed new expressions.")]
public static partial class DiagnosticRules
{
    public static readonly DiagnosticDescriptor TypeNotPartial = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.TypeNotPartial,
        title: "Expected a partial type",
        messageFormat: "To apply a SharpMeasures source generator, as indicated by the attribute {0}, {1} should be made partial",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true
    );

    public static readonly DiagnosticDescriptor TypeNotScalar = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.TypeNotScalar,
        title: "Expected a scalar quantity",
        messageFormat: $"Expected a scalar quantity. Decorate {{0}} with the attribute {typeof(SharpMeasuresScalarAttribute).FullName}, " +
            $"or use another type.",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true
    );

    public static readonly DiagnosticDescriptor NullTypeNotScalar = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.TypeNotScalar,
        title: "Expected a scalar quantity",
        messageFormat: $"Expected a type marked with the attribute {typeof(SharpMeasuresScalarAttribute).FullName}",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true
    );

    public static readonly DiagnosticDescriptor TypeNotVector = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.TypeNotVector,
        title: "Expected a vector quantity",
        messageFormat: $"Expected a vector quantity. Decorate {{0}} with the attribute {typeof(SharpMeasuresVectorAttribute).FullName}, " +
            $"or use another type.",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true
    );

    public static readonly DiagnosticDescriptor NullTypeNotVector = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.TypeNotVector,
        title: "Expected a vector quantity",
        messageFormat: $"Expected a type marked with the attribute {typeof(SharpMeasuresVectorAttribute).FullName}",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true
    );

    public static readonly DiagnosticDescriptor TypeNotUnit = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.TypeNotUnit,
        title: "Expected a unit",
        messageFormat: $"Expected a unit. Decorate {{0}} with the attribute {typeof(SharpMeasuresUnitAttribute).FullName}, or use another type.",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true
    );

    public static readonly DiagnosticDescriptor NullTypeNotUnit = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.TypeNotUnit,
        title: "Expected a unit",
        messageFormat: $"Expected a type marked with the attribute {typeof(SharpMeasuresUnitAttribute).FullName}",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true
    );

    public static readonly DiagnosticDescriptor TypeAlreadyDefined = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.TypeAlreadyDefined,
        title: "Type already defined",
        messageFormat: "{0} cannot be defined as a {1}, as it was already defined as a {2}",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true
    );

    public static readonly DiagnosticDescriptor TypeNotUnbiasedScalar = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.TypeNotUnbiasedScalar,
        title: "Expected a strictly unbiased scalar quantity",
        messageFormat: "Expected a scalar quantity that ignores unit bias. Mark {0} as not using unit biases, or choose another quantity.",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true
    );

    public static readonly DiagnosticDescriptor TypeNotBiasedScalar = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.TypeNotBiasedScalar,
        title: "Expected a biased scalar quantity",
        messageFormat: "Expected a scalar quantity that considers unit bias. Mark {0} as using unit biases, or choose another quantity.",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true
    );

    public static readonly DiagnosticDescriptor UnitNotIncludingBiasTerm = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.UnitNotIncludingBiasTerm,
        title: "Unit does not include a bias term",
        messageFormat: "The unit {0} does not include a bias term",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true
    );

    public static readonly DiagnosticDescriptor InvalidDerivationExpression = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.InvalidDerivationExpression,
        title: "Invalid derivation expression",
        messageFormat: $"The derivation expression \"{{0}}\" is invalid. Common expressions can be found in {typeof(CommonAlgebraicDerivations).FullName}.",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true
    );

    public static readonly DiagnosticDescriptor NullDerivationExpression = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.InvalidDerivationExpression,
        title: "Invalid derivation expression",
        messageFormat: $"The derivation expression must be defined. Common expressions can be found in {typeof(CommonAlgebraicDerivations).FullName}.",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true
    );

    public static readonly DiagnosticDescriptor NullDerivationSignature = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.InvalidDerivationSignature,
        title: "Invalid derivation signature",
        messageFormat: "The derivation signature must be defined",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true
    );

    public static readonly DiagnosticDescriptor EmptyDerivationSignature = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.InvalidDerivationSignature,
        title: "Invalid derivation signature",
        messageFormat: "The derivation signature should consist of at least 1 {0}",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true
    );

    public static readonly DiagnosticDescriptor EmptyList = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.EmptyList,
        title: "Expected a non-empty list",
        messageFormat: "Expected at least one {0}",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Info,
        isEnabledByDefault: true,
        customTags: WellKnownDiagnosticTags.Unnecessary
    );

    public static readonly DiagnosticDescriptor DuplicateListing = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.DuplicateListing,
        title: "Item has already been listed",
        messageFormat: "The {0} {1} has already been listed",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Info,
        isEnabledByDefault: true,
        customTags: WellKnownDiagnosticTags.Unnecessary
    );
}
