namespace SharpMeasures.Generators.Units.Parsing.Diagnostics;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Units;

internal static class UnitTypeDiagnostics
{
    public static Diagnostic TypeNotPartial(BaseTypeDeclarationSyntax declaration)
    {
        return DiagnosticConstruction.TypeNotPartial<SharpMeasuresUnitAttribute>(declaration.Identifier.GetLocation(), declaration.Identifier.Text);
    }

    public static Diagnostic TypeStatic(BaseTypeDeclarationSyntax declaration)
    {
        return DiagnosticConstruction.TypeStatic<SharpMeasuresUnitAttribute>(declaration.Identifier.GetLocation(), declaration.Identifier.Text);
    }
}
