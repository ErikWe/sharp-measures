namespace SharpMeasures.Generators.Documentation;

using Microsoft.CodeAnalysis;

internal sealed class EmptyDiagnosticsStrategy : IDiagnosticsStrategy
{
    public bool GenerateDiagnostics => false;

    public Diagnostic? DocumentationFileMissingRequestedTag(Location location, string name, string tag) => null;
    public Diagnostic? UnresolvedDocumentationDependency(Location location, string name, string dependency) => null;
}

internal sealed class EmptyDiagnosticsStrategy<TIdentifier> : IDiagnosticsStrategy<TIdentifier>
{
    public bool GenerateDiagnostics => false;

    public Diagnostic? DocumentationFileMissingRequestedTag(Location location, string name, string tag) => null;
    public Diagnostic? UnresolvedDocumentationDependency(Location location, string name, string dependency) => null;
    public Diagnostic? NoMatchingDocumentationFile(TIdentifier identifier) => null;
}
