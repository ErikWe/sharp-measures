namespace SharpMeasures.Generators.Vectors.Parsing.Diagnostics;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpMeasures.Generators.Diagnostics;

internal static class IndividualVectorSpecializationTypeDiagnostics
{
    public static Diagnostic TypeNotPartial(BaseTypeDeclarationSyntax declaration)
    {
        return DiagnosticConstruction.TypeNotPartial<SpecializedSharpMeasuresVectorAttribute>(declaration.Identifier.GetLocation(), declaration.Identifier.Text);
    }

    public static Diagnostic TypeStatic(BaseTypeDeclarationSyntax declaration)
    {
        return DiagnosticConstruction.TypeStatic<SpecializedSharpMeasuresVectorAttribute>(declaration.Identifier.GetLocation(), declaration.Identifier.Text);
    }
}
