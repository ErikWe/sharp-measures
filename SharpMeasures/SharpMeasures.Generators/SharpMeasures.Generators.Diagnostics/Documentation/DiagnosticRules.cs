namespace SharpMeasures.Generators.Diagnostics;

using Microsoft.CodeAnalysis;

public static partial class DiagnosticRules
{
    public static readonly DiagnosticDescriptor UnresolvedDocumentationDependency = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.UnresolvedDocumentationDependency,
        title: "Unresolved documentation dependency",
        messageFormat: "The documentation file {0} has dependency \"{1}\", which was not recognized as a documentation file",
        category: "Documentation",
        defaultSeverity: DiagnosticSeverity.Info,
        isEnabledByDefault: true
    );

    public static readonly DiagnosticDescriptor NoMatchingDocumentationFile = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.NoMatchingDocumentationFile,
        title: "No matching documentation file",
        messageFormat: "No matching documentation file was found for {0}. Add a file with the name \"{0}.doc.txt\" to the project, or disable generation of " +
            "documentation through the relevant attribute or through the global AnalyzerConfig using the entry \"SharpMeasures_GenerateDocumentation\".",
        category: "Documentation",
        defaultSeverity: DiagnosticSeverity.Info,
        isEnabledByDefault: true
    );

    public static readonly DiagnosticDescriptor DocumentationFileMissingRequestedTag = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.DocumentationFileMissingRequestedTag,
        title: "Documentation tag not found",
        messageFormat: "Documentation tag \"{1}\" was not recognized as part of {0} or any of its dependencies. If this file is " +
            "only used as a dependency, this message should be suppressed by adding the following line to the file: \"# Utility: True\".",
        category: "Documentation",
        defaultSeverity: DiagnosticSeverity.Info,
        isEnabledByDefault: true
    );
}
