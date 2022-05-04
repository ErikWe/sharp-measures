namespace SharpMeasures.Generators.Diagnostics;

using Microsoft.CodeAnalysis;

internal static partial class DiagnosticRules
{
    public readonly static DiagnosticDescriptor DocumentationFileDoesNotContainName = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.DocumentationFileDoesNotContainName,
        title: "Unnamed documentation file",
        messageFormat: "The documentation file {0} is unnamed. Add a line containing \"#Name: ...\".",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true
    );

    public readonly static DiagnosticDescriptor UnresolvedDocumentationDependency = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.UnresolvedDocumentationDependency,
        title: "Unresolved documentation dependency",
        messageFormat: "The documentation file {0} has dependency {1}, which was not recognized as the name of a documentation file",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true
    );

    public readonly static DiagnosticDescriptor NoMatchingDocumentationFile = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.NoMatchingDocumentationFile,
        title: "No matching documentation file",
        messageFormat: "No matching documentation file was found for {0}",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true
    );
}
