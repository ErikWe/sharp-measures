namespace SharpMeasures.Generators.Diagnostics;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Units;
using SharpMeasures.Generators.Vectors;

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

    public readonly static DiagnosticDescriptor TypeNotScalar = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.TypeNotScalar,
        title: "Expected a scalar quantity",
        messageFormat: $"Expected a scalar quantity. Add the attribute {typeof(GeneratedScalarAttribute).FullName} to {{0}} " +
            $"for it to be recognized as a scalar quantity.",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true
    );

    public readonly static DiagnosticDescriptor TypeNotScalar_Null = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.TypeNotScalar,
        title: "Expected a scalar quantity",
        messageFormat: $"Expected a type marked with the attribute {typeof(GeneratedScalarAttribute).FullName}",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true
    );

    public readonly static DiagnosticDescriptor TypeNotVector = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.TypeNotVector,
        title: "Expected a vector quantity",
        messageFormat: $"Expected a vector quantity. Add the attribute {typeof(GeneratedVectorAttribute).FullName} to {{0}} " +
            $"for it to be recognized as a vector quantity.",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true
    );

    public readonly static DiagnosticDescriptor TypeNotVector_Null = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.TypeNotVector,
        title: "Expected a vector quantity",
        messageFormat: $"Expected a type marked with the attribute {typeof(GeneratedVectorAttribute).FullName}",
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

    public readonly static DiagnosticDescriptor TypeNotUnit_Null = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.TypeNotUnit,
        title: "Expected a unit",
        messageFormat: $"Expected a type marked with the attribute {typeof(GeneratedUnitAttribute).FullName}",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true
    );

    public readonly static DiagnosticDescriptor TypeAlreadyDefined = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.TypeAlreadyDefined,
        title: "Type already defined",
        messageFormat: "{0} cannot be defined as a {1}, as it was already defined as a {2}",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true
    );

    public readonly static DiagnosticDescriptor ScalarNotUnbiased_UnitDefinition = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.ScalarNotUnbiased,
        title: "Expected an unbiased scalar quantity",
        messageFormat: "Argument should be an unbiased scalar quantity. Make {0} an unbiased scalar quantity, or use another quantity.",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true,
        description: "The quantity associated with a unit should always be an unbiased quantity, regardless of whether the unit supports " +
            "biased quantities. As an example; \"UnitOfTemperature\" should be associated with the unbiased quantity \"TemperatureDifference\", " +
            "rather than the biased quantity \"Temperature\"."
    );

    public readonly static DiagnosticDescriptor ScalarNotUnbiased = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.ScalarNotUnbiased,
        title: "Expected an unbiased scalar quantity",
        messageFormat: "Expected an unbiased scalar quantity. Make {0} an unbiased scalar quantity, or use another quantity.",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true
    );

    public readonly static DiagnosticDescriptor ScalarNotBiased = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.ScalarNotBiased,
        title: "Expected a biased scalar quantity",
        messageFormat: "Expected a biased scalar quantity. Make {0} a biased scalar quantity, or use another quantity.",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true
    );

    public readonly static DiagnosticDescriptor UnitNotSupportingBias = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.UnitNotSupportingBias,
        title: "Unit does not support biased quantities",
        messageFormat: "The unit {0} does not support biased quantities",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true
    );

    public readonly static DiagnosticDescriptor EmptyList = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.EmptyList,
        title: "Expected a non-empty list",
        messageFormat: "Expected at least one {0}",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Info,
        isEnabledByDefault: true
    );

    public readonly static DiagnosticDescriptor DuplicateListing = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.DuplicateListing,
        title: "Item has already been listed",
        messageFormat: "The {0} {1} has already been listed",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Info,
        isEnabledByDefault: true
    );
}
