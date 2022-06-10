namespace SharpMeasures.Generators.Documentation;

using Microsoft.CodeAnalysis;

internal class EmptyDiagnosticsStrategy : IDiagnosticsStrategy
{
    public bool GenerateDiagnostics => false;
    public Diagnostic? DocumentationFileMissingRequestedTag(Location location, string name, string tag) => null;
    public Diagnostic? UnresolvedDocumentationDependency(Location location, string name, string dependency) => null;
}

internal class EmptyDiagnosticsStrategy<TIdentifier> : EmptyDiagnosticsStrategy, IDiagnosticsStrategy<TIdentifier>
{
    public Diagnostic? NoMatchingDocumentationFile(TIdentifier identifier) => null;
}
