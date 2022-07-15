namespace SharpMeasures.Generators.Units.Parsing.Diagnostics;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Providers;
using SharpMeasures.Generators.Units;

internal class UnitTypeDiagnostics : IPartialDeclarationProviderDiagnostics
{
    public static UnitTypeDiagnostics Instance { get; } = new();

    private UnitTypeDiagnostics() { }

    public Diagnostic TypeNotPartial(BaseTypeDeclarationSyntax declaration)
    {
        return DiagnosticConstruction.TypeNotPartial<SharpMeasuresUnitAttribute>(declaration.Identifier.GetLocation(), declaration.Identifier.Text);
    }
}
