namespace SharpMeasures.Generators.Vectors.Parsing.Diagnostics;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpMeasures.Generators.Diagnostics;

internal static class GroupBaseTypeDiagnostics
{
    public static Diagnostic TypeNotPartial(BaseTypeDeclarationSyntax declaration)
    {
        return DiagnosticConstruction.TypeNotPartial<SharpMeasuresVectorGroupAttribute>(declaration.Identifier.GetLocation(), declaration.Identifier.Text);
    }

    public static Diagnostic TypeNotStatic(BaseTypeDeclarationSyntax declaration)
    {
        return DiagnosticConstruction.TypeNotStatic<SharpMeasuresVectorGroupAttribute>(declaration.Identifier.GetLocation(), declaration.Identifier.Text);
    }
}
