namespace SharpMeasures.Generators.Diagnostics;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Documentation;

internal class DocumentationDiagnostics : IDiagnosticsStrategy
{
    public static DocumentationDiagnostics Instance { get; } = new();

    public bool GenerateDiagnostics => true;

    private DocumentationDiagnostics() { }

    public Diagnostic UnresolvedDocumentationDependency(Location location, string name, string dependency)
    {
        return DiagnosticConstruction.UnresolvedDocumentationDependency(location, name, dependency);
    }

    public Diagnostic DocumentationFileMissingRequestedTag(Location location, string name, string tag)
    {
        return DiagnosticConstruction.DocumentationFileMissingRequestedTag(location, name, tag);
    }
}
