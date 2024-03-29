﻿namespace SharpMeasures.Generators.Diagnostics;

using Microsoft.CodeAnalysis;

public static partial class DiagnosticRules
{
    public static readonly DiagnosticDescriptor UnresolvedDocumentationDependency = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.UnresolvedDocumentationDependency,
        title: "Unresolved documentation dependency",
        messageFormat: "The documentation file {0} has dependency \"{1}\", which was not recognized as a documentation file",
        category: "Documentation",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true
    );

    public static readonly DiagnosticDescriptor DocumentationFileDuplicateTag = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.DocumentationFileDuplicateTag,
        title: "Duplicate documentation tag",
        messageFormat: "The documentation file {0} contains multiple definitions of \"{1}\"",
        category: "Documentation",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true
    );

    public static readonly DiagnosticDescriptor DocumentationFileMissingRequestedTag = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.DocumentationFileMissingRequestedTag,
        title: "Documentation tag not found",
        messageFormat: "Documentation tag \"{1}\" was not recognized as part of {0} or any of its dependencies. If this file is only used as a dependency, add the following line to the file: \"# Utility: True\".",
        category: "Documentation",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true
    );
}
