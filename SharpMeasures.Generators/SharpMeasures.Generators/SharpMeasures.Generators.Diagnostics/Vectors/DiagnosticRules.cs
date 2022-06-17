namespace SharpMeasures.Generators.Diagnostics;

using Microsoft.CodeAnalysis;

public static partial class DiagnosticRules
{
    public static readonly DiagnosticDescriptor InvalidVectorDimension = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.InvalidVectorDimension,
        title: "Invalid vector dimension",
        messageFormat: "{0} is not a valid dimension. The dimension should be an integer larger than 1.",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true
    );

    public static readonly DiagnosticDescriptor MissingVectorDimension = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.MissingVectorDimension,
        title: "Missing vector dimension",
        messageFormat: "Could not interpret the dimension for vector {0}. Specify the dimension manually, or change the name of the type to \"{0}X\", " +
            "where X is the dimension.",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true
    );

    public static readonly DiagnosticDescriptor DuplicateVectorDimension = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.DuplicateVectorDimension,
        title: "Duplicate vector dimension",
        messageFormat: "The group of resized vectors already contains a vector of dimension {0}",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true
    );

    public static readonly DiagnosticDescriptor VectorConstantInvalidDimension = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.VectorConstantInvalidDimension,
        title: "Invalid dimension of vector constant",
        messageFormat: "Expected {0} elements, as {1} has dimension {0}",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true
    );

    public static readonly DiagnosticDescriptor VectorGroupAlreadySpecified = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.VectorGroupAlreadySpecified,
        title: "Vector group already specified",
        messageFormat: "The vector group to which {0} belongs has already been specified",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Info,
        isEnabledByDefault: true,
        customTags: WellKnownDiagnosticTags.Unnecessary
    );
}
