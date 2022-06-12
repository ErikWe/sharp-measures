namespace SharpMeasures.Generators.Units.Diagnostics;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Providers;

internal class UnitDiagnostics : IPartialDeclarationProviderDiagnostics
{
    public static UnitDiagnostics Instance { get; } = new();

    private UnitDiagnostics() { }

    public Diagnostic TypeNotPartial(BaseTypeDeclarationSyntax declaration)
    {
        return DiagnosticConstruction.TypeNotPartial<GeneratedUnitAttribute>(declaration.Identifier.GetLocation(), declaration.Identifier.Text);
    }
}
