namespace SharpMeasures.Generators.Documentation;

using Microsoft.CodeAnalysis;

public interface IDiagnosticsStrategy
{
    public abstract bool GenerateDiagnostics { get; }
    public abstract Diagnostic? DocumentationFileMissingRequestedTag(Location location, string name, string tag);
    public abstract Diagnostic? UnresolvedDocumentationDependency(Location location, string name, string dependency);
}

public interface IDiagnosticsStrategy<in TIdentifier> : IDiagnosticsStrategy
{
    public abstract Diagnostic? NoMatchingDocumentationFile(TIdentifier identifier);
}
