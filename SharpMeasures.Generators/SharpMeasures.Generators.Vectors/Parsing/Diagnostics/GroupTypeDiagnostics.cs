namespace SharpMeasures.Generators.Vectors.Parsing.Diagnostics;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpMeasures.Generators.Diagnostics;

internal static class GroupTypeDiagnostics
{
    public static Diagnostic TypeNotPartial<TAttribute>(BaseTypeDeclarationSyntax declaration)
    {
        return DiagnosticConstruction.TypeNotPartial<TAttribute>(declaration.Identifier.GetLocation(), declaration.Identifier.Text);
    }

    public static Diagnostic TypeNotStatic<TAttribute>(BaseTypeDeclarationSyntax declaration)
    {
        return DiagnosticConstruction.TypeNotStatic<TAttribute>(declaration.Identifier.GetLocation(), declaration.Identifier.Text);
    }
}
