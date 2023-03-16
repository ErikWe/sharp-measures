namespace SharpMeasures.Generators.Diagnostics;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Documentation;

internal sealed class DocumentationDiagnostics : IDiagnosticsStrategy
{
    public static DocumentationDiagnostics Instance { get; } = new();

    public bool GenerateDiagnostics => true;

    private DocumentationDiagnostics() { }

    public Diagnostic UnresolvedDocumentationDependency(Location location, string name, string dependency) => DiagnosticConstruction.UnresolvedDocumentationDependency(location, name, dependency);
    public Diagnostic DocumentationFileDuplicateTag(Location location, string name, string tag) => DiagnosticConstruction.DocumentationFileDuplicateTag(location, name, tag);
    public Diagnostic DocumentationFileMissingRequestedTag(Location location, string name, string tag) => DiagnosticConstruction.DocumentationFileMissingRequestedTag(location, name, tag);
}
