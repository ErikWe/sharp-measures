namespace SharpMeasures.Generators.Diagnostics;

using Microsoft.CodeAnalysis;

internal static partial class DiagnosticRules
{
    public readonly static DiagnosticDescriptor InvalidVectorDimension = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.InvalidVectorDimension,
        title: "Invalid vector dimension",
        messageFormat: "{0} is not a valid dimension. The dimension should be an integer larger than 1.",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true
    );

    public readonly static DiagnosticDescriptor MissingVectorDimension = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.MissingVectorDimension,
        title: "Missing vector dimension",
        messageFormat: "Could not interpret the dimension for vector {0}. Specify the dimension manually, or change the name of the type to \"{0}X\", " +
            "where X is the dimension.",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true
    );

    public readonly static DiagnosticDescriptor DuplicateVectorDimension = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.DuplicateVectorDimension,
        title: "Duplicate vector dimension",
        messageFormat: "The group of resized vectors already contains a vector of dimension {0}",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true
    );
}
