namespace SharpMeasures.Generators.Diagnostics.Documentation;

using Microsoft.CodeAnalysis;

using System.IO;

internal static class UnresolvedDocumentationDependencyDiagnostics
{
    public static Diagnostic Create(AdditionalText file, Location location, string dependencyName)
    {
       return Diagnostic.Create(DiagnosticRules.UnresolvedDocumentationDependency, location, Path.GetFileName(file.Path), dependencyName);
    }
}
