namespace SharpMeasures.Generators.Scalars.Parsing.Diagnostics;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Providers;

internal class BaseScalarTypeDiagnostics : IPartialDeclarationProviderDiagnostics
{
    public static BaseScalarTypeDiagnostics Instance { get; } = new();

    private BaseScalarTypeDiagnostics() { }

    public Diagnostic TypeNotPartial(BaseTypeDeclarationSyntax declaration)
    {
        return DiagnosticConstruction.TypeNotPartial<SharpMeasuresScalarAttribute>(declaration.Identifier.GetLocation(), declaration.Identifier.Text);
    }
}
