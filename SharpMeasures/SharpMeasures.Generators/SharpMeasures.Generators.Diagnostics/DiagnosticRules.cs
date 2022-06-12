namespace SharpMeasures.Generators.Diagnostics;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Units;
using SharpMeasures.Generators.Vectors;

using System.Diagnostics.CodeAnalysis;

[SuppressMessage("Style", "IDE0090", Justification = "Roslyn-analyzers related to analyzer releases does not consider target-typed new expressions.")]
public static partial class DiagnosticRules
{
    public static readonly DiagnosticDescriptor TypeNotPartial = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.TypeNotPartial,
        title: "Expected a partial type",
        messageFormat: "To apply a SharpMeasures source generator, as suggested by the attribute {0}, {1} should be made partial",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true
    );

    public static readonly DiagnosticDescriptor TypeNotScalar = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.TypeNotScalar,
        title: "Expected a scalar quantity",
        messageFormat: $"Expected a scalar quantity. Decorate {{0}} with the attribute {typeof(GeneratedScalarAttribute).FullName}, " +
            $"or use another type.",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true
    );

    public static readonly DiagnosticDescriptor NullTypeNotScalar = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.TypeNotScalar,
        title: "Expected a scalar quantity",
        messageFormat: $"Expected a type marked with the attribute {typeof(GeneratedScalarAttribute).FullName}",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true
    );

    public static readonly DiagnosticDescriptor TypeNotVector = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.TypeNotVector,
        title: "Expected a vector quantity",
        messageFormat: $"Expected a vector quantity. Decorate {{0}} with the attribute {typeof(GeneratedVectorAttribute).FullName}, " +
            $"or use another type.",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true
    );

    public static readonly DiagnosticDescriptor NullTypeNotVector = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.TypeNotVector,
        title: "Expected a vector quantity",
        messageFormat: $"Expected a type marked with the attribute {typeof(GeneratedVectorAttribute).FullName}",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true
    );

    public static readonly DiagnosticDescriptor TypeNotUnit = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.TypeNotUnit,
        title: "Expected a unit",
        messageFormat: $"Expected a unit. Decorate {{0}} with the attribute {typeof(GeneratedUnitAttribute).FullName}, or use another type.",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true
    );

    public static readonly DiagnosticDescriptor NullTypeNotUnit = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.TypeNotUnit,
        title: "Expected a unit",
        messageFormat: $"Expected a type marked with the attribute {typeof(GeneratedUnitAttribute).FullName}",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true
    );

    public static readonly DiagnosticDescriptor TypeAlreadyDefined = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.TypeAlreadyDefined,
        title: "Type already defined",
        messageFormat: "{0} cannot be defined as a {1}, as it was already defined as a {2}",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true
    );

    public static readonly DiagnosticDescriptor UnitQuantityNotUnbiasedScalar = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.ScalarNotUnbiased,
        title: "Expected an unbiased scalar quantity",
        messageFormat: "Expected an unbiased scalar quantity. Make {0} an unbiased scalar quantity, or use another quantity.",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true,
        description: "The quantity associated with a unit should always be an unbiased quantity, regardless of whether the unit supports " +
            "biased quantities. As an example; \"UnitOfTemperature\" should be associated with the unbiased quantity \"TemperatureDifference\", " +
            "rather than the biased quantity \"Temperature\"."
    );

    public static readonly DiagnosticDescriptor ScalarNotUnbiased = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.ScalarNotUnbiased,
        title: "Expected an unbiased scalar quantity",
        messageFormat: "Expected an unbiased scalar quantity. Make {0} an unbiased scalar quantity, or use another quantity.",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true
    );

    public static readonly DiagnosticDescriptor ScalarNotBiased = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.ScalarNotBiased,
        title: "Expected a biased scalar quantity",
        messageFormat: "Expected a biased scalar quantity. Make {0} a biased scalar quantity, or use another quantity.",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true
    );

    public static readonly DiagnosticDescriptor UnitNotSupportingBias = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.UnitNotSupportingBias,
        title: "Unit does not support biased quantities",
        messageFormat: "The unit {0} does not support biased quantities",
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
        isEnabledByDefault: true
    );

    public static readonly DiagnosticDescriptor DuplicateListing = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.DuplicateListing,
        title: "Item has already been listed",
        messageFormat: "The {0} {1} has already been listed",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Info,
        isEnabledByDefault: true
    );
}
