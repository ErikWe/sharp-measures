namespace SharpMeasures.Generators.Diagnostics;

using Microsoft.CodeAnalysis;

using System.Diagnostics.CodeAnalysis;

[SuppressMessage("Style", "IDE0090", Justification = "Roslyn-analyzers related to analyzer releases does not consider target-typed new expressions.")]
public static partial class DiagnosticRules
{
    public static readonly DiagnosticDescriptor TypeNotPartial = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.TypeNotPartial,
        title: "Expected a partial type",
        messageFormat: "The source generator associated with the attribute {0} requires {1} to be a partial type",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true
    );

    public static readonly DiagnosticDescriptor TypeStatic = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.TypeStatic,
        title: "Expected a non-static type",
        messageFormat: "The source generator associated with the attribute {0} requires {1} to be a non-static type",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true
    );

    public static readonly DiagnosticDescriptor TypeNotStatic = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.TypeNotStatic,
        title: "Expected a static type",
        messageFormat: "The source generator associated with the attribute {0} requires {1} to be a static type",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true
    );

    public static readonly DiagnosticDescriptor TypeNotScalar = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.TypeNotScalar,
        title: "Expected a scalar quantity",
        messageFormat: $"Expected a scalar quantity. Decorate {{0}} with the attribute {Utility.FullAttributeName<ScalarQuantityAttribute>()}, or use another type.",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true
    );

    public static readonly DiagnosticDescriptor NullTypeNotScalar = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.TypeNotScalar,
        title: "Expected a scalar quantity",
        messageFormat: $"Expected a type decorated with the attribute {Utility.FullAttributeName<ScalarQuantityAttribute>()}",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true
    );

    public static readonly DiagnosticDescriptor TypeNotVector = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.TypeNotVector,
        title: "Expected a vector quantity",
        messageFormat: $"Expected a vector quantity. Decorate {{0}} with the attribute {Utility.FullAttributeName<VectorQuantityAttribute>()}, or use another type.",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true
    );

    public static readonly DiagnosticDescriptor NullTypeNotVector = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.TypeNotVector,
        title: "Expected a vector quantity",
        messageFormat: $"Expected a type decorated with the attribute {Utility.FullAttributeName<VectorQuantityAttribute>()}",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true
    );

    public static readonly DiagnosticDescriptor TypeNotVectorGroup = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.TypeNotVectorGroup,
        title: "Expected a vector group",
        messageFormat: $"Expected a vector group. Decorate {{0}} with the attribute {Utility.FullAttributeName<VectorGroupAttribute>()}, or use another type.",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true
    );

    public static readonly DiagnosticDescriptor NullTypeNotVectorGroup = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.TypeNotVectorGroup,
        title: "Expected a vector group",
        messageFormat: $"Expected a type decorated with the attribute {Utility.FullAttributeName<VectorGroupAttribute>()}",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true
    );

    public static readonly DiagnosticDescriptor TypeNotVectorGroupMember = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.TypeNotVectorGroupMember,
        title: "Expected a vector group member",
        messageFormat: $"Expected a vector group member. Decorate {{0}} with the attribute {Utility.FullAttributeName<VectorGroupMemberAttribute>()}, or use another type.",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true
    );

    public static readonly DiagnosticDescriptor NullTypeNotVectorGroupMember = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.TypeNotVectorGroupMember,
        title: "Expected a vector group member",
        messageFormat: $"Expected a type decorated with the attribute {Utility.FullAttributeName<VectorGroupMemberAttribute>()}",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true
    );

    public static readonly DiagnosticDescriptor TypeNotVectorGroupMemberSpecificGroup = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.TypeNotVectorGroupMember,
        title: "Expected a vector group member",
        messageFormat: $"Expected a member of the vector group {{0}}. Decorate {{1}} with the attribute {Utility.FullAttributeName<VectorGroupMemberAttribute>()} targetting {{0}}, or use another type.",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true
    );

    public static readonly DiagnosticDescriptor TypeNotQuantity = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.TypeNotQuantity,
        title: "Expected a SharpMeasures quantity",
        messageFormat: $"Expected a quantity. Decorate {{0}} with an attribute describing a quantity, such as {Utility.FullAttributeName<ScalarQuantityAttribute>()}, or use another type.",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true
    );

    public static readonly DiagnosticDescriptor NullTypeNotQuantity = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.TypeNotQuantity,
        title: "Expected a SharpMeasures quantity",
        messageFormat: $"Expected a type decorated with an attribute describing a quantity, such as {Utility.FullAttributeName<ScalarQuantityAttribute>()}",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true
    );

    public static readonly DiagnosticDescriptor TypeNotUnit = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.TypeNotUnit,
        title: "Expected a unit",
        messageFormat: $"Expected a unit. Decorate {{0}} with the attribute {Utility.FullAttributeName<UnitAttribute>()}, or use another type.",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true
    );

    public static readonly DiagnosticDescriptor NullTypeNotUnit = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.TypeNotUnit,
        title: "Expected a unit",
        messageFormat: $"Expected a type decorated with the attribute {Utility.FullAttributeName<UnitAttribute>()}",
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
        messageFormat: "{0} requires a unit that includes a bias term, which {1} does not",
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
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true
    );

    public static readonly DiagnosticDescriptor NullDerivationExpression = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.InvalidDerivationExpression,
        title: "Invalid derivation expression",
        messageFormat: $"The derivation expression must be defined. Common expressions can be found in {typeof(CommonAlgebraicDerivations).FullName}.",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true
    );

    public static readonly DiagnosticDescriptor NullDerivationSignature = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.InvalidDerivationSignature,
        title: "Invalid derivation signature",
        messageFormat: "The derivation signature must be defined",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true
    );

    public static readonly DiagnosticDescriptor EmptyDerivationSignature = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.InvalidDerivationSignature,
        title: "Invalid derivation signature",
        messageFormat: "The derivation signature should consist of at least one {0}",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true
    );

    public static readonly DiagnosticDescriptor SetRegexSubstitutionButNotPattern = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.SetRegexSubstitutionButNotPattern,
        title: "No regex pattern specified",
        messageFormat: "A regex substitution string was specified, but not the regex pattern",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true,
        customTags: WellKnownDiagnosticTags.Unnecessary
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
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true,
        customTags: WellKnownDiagnosticTags.Unnecessary
    );

    public static readonly DiagnosticDescriptor UnrecognizedEnumValue = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.UnrecognizedEnumValue,
        title: "Unrecognized enum value",
        messageFormat: "{0} was not recognized as a {1}",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true
    );
}
