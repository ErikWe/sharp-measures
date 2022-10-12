namespace SharpMeasures.Generators.Diagnostics;

using Microsoft.CodeAnalysis;

public static partial class DiagnosticConstruction
{
    public static Diagnostic UnresolvedDocumentationDependency(Location? location, string documentationFile, string dependency)
    {
        return Diagnostic.Create(DiagnosticRules.UnresolvedDocumentationDependency, location, documentationFile, dependency);
    }

    public static Diagnostic DocumentationFileDuplicateTag(Location? location, string documentationFile, string tag)
    {
        return Diagnostic.Create(DiagnosticRules.DocumentationFileDuplicateTag, location, documentationFile, tag);
    }

    public static Diagnostic DocumentationFileMissingRequestedTag(Location? location, string documentationFile, string tag)
    {
        return Diagnostic.Create(DiagnosticRules.DocumentationFileMissingRequestedTag, location, documentationFile, tag);
    }
}
