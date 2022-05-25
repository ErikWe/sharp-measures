namespace SharpMeasures.Generators.Diagnostics;

using Microsoft.CodeAnalysis;

internal static partial class DiagnosticRules
{
    public readonly static DiagnosticDescriptor UnresolvedDocumentationDependency = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.UnresolvedDocumentationDependency,
        title: "Unresolved documentation dependency",
        messageFormat: "The documentation file {0} requires \"{1}\", which was not recognized as a documentation file",
        category: "Documentation",
        defaultSeverity: DiagnosticSeverity.Info,
        isEnabledByDefault: true
    );

    public readonly static DiagnosticDescriptor NoMatchingDocumentationFile = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.NoMatchingDocumentationFile,
        title: "No matching documentation file",
        messageFormat: "No matching documentation file was found for {0}. Add a file with the name \"{0}.doc.txt\" to the project, or disable documentation " +
            "through the relevant attribute.",
        category: "Documentation",
        defaultSeverity: DiagnosticSeverity.Info,
        isEnabledByDefault: true
    );

    public readonly static DiagnosticDescriptor DocumentationFileMissingRequestedTag = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.DocumentationFileMissingRequestedTag,
        title: "Documentation tag not found",
        messageFormat: "Documentation tag \"{1}\" was not recognized as part of {0} or any of its dependencies. If this file is " +
            "only used as a dependency, this message can be suppressed by adding the following line: \"# Utility: True\".",
        category: "Documentation",
        defaultSeverity: DiagnosticSeverity.Info,
        isEnabledByDefault: true
    );
}
