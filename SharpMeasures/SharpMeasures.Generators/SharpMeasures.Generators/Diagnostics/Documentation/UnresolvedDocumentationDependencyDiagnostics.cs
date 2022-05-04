namespace SharpMeasures.Generators.Diagnostics.Documentation;

using Microsoft.CodeAnalysis;

internal static class UnresolvedDocumentationDependencyDiagnostics
{
    public static Diagnostic Create(AdditionalText file, Location location, string dependency)
    {
       return Diagnostic.Create(DiagnosticRules.UnresolvedDocumentationDependency, location, file.Path, dependency);
    }
}
