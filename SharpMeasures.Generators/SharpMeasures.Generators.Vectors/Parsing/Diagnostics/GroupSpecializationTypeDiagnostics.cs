namespace SharpMeasures.Generators.Vectors.Parsing.Diagnostics;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpMeasures.Generators.Diagnostics;

internal static class GroupSpecializationTypeDiagnostics
{
    public static Diagnostic TypeNotPartial(BaseTypeDeclarationSyntax declaration)
    {
        return DiagnosticConstruction.TypeNotPartial<SpecializedSharpMeasuresVectorGroupAttribute>(declaration.Identifier.GetLocation(), declaration.Identifier.Text);
    }

    public static Diagnostic TypeNotStatic(BaseTypeDeclarationSyntax declaration)
    {
        return DiagnosticConstruction.TypeNotStatic<SpecializedSharpMeasuresVectorGroupAttribute>(declaration.Identifier.GetLocation(), declaration.Identifier.Text);
    }
}
