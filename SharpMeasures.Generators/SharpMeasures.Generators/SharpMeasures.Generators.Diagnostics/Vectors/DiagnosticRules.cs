namespace SharpMeasures.Generators.Diagnostics;

using Microsoft.CodeAnalysis;

public static partial class DiagnosticRules
{
    public static readonly DiagnosticDescriptor InvalidVectorDimension = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.InvalidVectorDimension,
        title: "Invalid vector dimension",
        messageFormat: "{0} is not a valid vector dimension, a value greater than 1 was expected",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true
    );

    public static readonly DiagnosticDescriptor InvalidInterpretedVectorDimension = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.InvalidVectorDimension,
        title: "Invalid vector dimension",
        messageFormat: "Interpreted the dimension of {0} as {1}, which is not a valid dimension - a value greater than 1 was expected",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true
    );

    public static readonly DiagnosticDescriptor MissingVectorDimension = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.MissingVectorDimension,
        title: "Missing vector dimension",
        messageFormat: "Could not interpret the vector dimension of {0}. Specify the dimension manually, or change the name of the type to \"{0}X\", " +
            "where X is the dimension.",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true
    );

    public static readonly DiagnosticDescriptor DuplicateVectorDimension = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.DuplicateVectorDimension,
        title: "Vector group already contains dimension",
        messageFormat: "Vector group {0} already contains a member of dimension {1}",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true
    );

    public static readonly DiagnosticDescriptor VectorGroupLacksMemberOfDimension = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.VectorGroupLacksMemberOfDimension,
        title: "No vector of appropiate dimension in group",
        messageFormat: "Vector group {0} does not contain a member of dimension {1}",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true
    );

    public static readonly DiagnosticDescriptor VectorNameAndDimensionMismatch = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.VectorNameAndDimensionMismatch,
        title: "Vector name and dimension mismatch",
        messageFormat: "The name of vector {0} suggests that the dimension should be {1}, but the dimension was specified as {2}",
        category: "Naming",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true
    );

    public static readonly DiagnosticDescriptor VectorGroupNameSuggestsDimension = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.VectorGroupNameSuggestsDimension,
        title: "Vector group name suggests dimension",
        messageFormat: "The name of vector group {0} suggests dimension {1}, but a vector group allows members of any dimension",
        category: "Naming",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true
    );

    public static readonly DiagnosticDescriptor VectorConstantInvalidDimension = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.VectorConstantInvalidDimension,
        title: "Invalid dimension of vector constant",
        messageFormat: "Expected {0} elements, as {2} has dimension {0} - but received {1} element(s)",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true
    );
}
