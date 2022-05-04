namespace SharpMeasures.Generators.Diagnostics.Documentation;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

internal static class NoMatchingDocumentationFileDiagnostics
{
    public static Diagnostic Create(BaseTypeDeclarationSyntax declaration)
    {
       return Diagnostic.Create(DiagnosticRules.NoMatchingDocumentationFile, declaration.GetLocation(), declaration.Identifier.Text);
    }
}
