namespace SharpMeasures.Generators.Units.Parsing.Diagnostics;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpMeasures.Generators.Diagnostics;

internal static class UnitTypeDiagnostics
{
    public static Diagnostic TypeNotPartial(BaseTypeDeclarationSyntax declaration)
    {
        return DiagnosticConstruction.TypeNotPartial<UnitAttribute>(declaration.Identifier.GetLocation(), declaration.Identifier.Text);
    }

    public static Diagnostic TypeStatic(BaseTypeDeclarationSyntax declaration)
    {
        return DiagnosticConstruction.TypeStatic<UnitAttribute>(declaration.Identifier.GetLocation(), declaration.Identifier.Text);
    }
}
