namespace SharpMeasures.Generators.Diagnostics;

using Microsoft.CodeAnalysis;

public static partial class DiagnosticConstruction
{
    public static Diagnostic UnresolvedDocumentationDependency(Location? location, string documentationFile, string dependency)
    {
        return Diagnostic.Create(DiagnosticRules.UnresolvedDocumentationDependency, location, documentationFile, dependency);
    }

    public static Diagnostic NoMatchingDocumentationFile(Location? location, string typeName)
    {
        return Diagnostic.Create(DiagnosticRules.NoMatchingDocumentationFile, location, typeName);
    }

    public static Diagnostic DocumentationFileMissingRequestedTag(Location? location, string documentationFile, string tag)
    {
        return Diagnostic.Create(DiagnosticRules.DocumentationFileMissingRequestedTag, location, documentationFile, tag);
    }
}
