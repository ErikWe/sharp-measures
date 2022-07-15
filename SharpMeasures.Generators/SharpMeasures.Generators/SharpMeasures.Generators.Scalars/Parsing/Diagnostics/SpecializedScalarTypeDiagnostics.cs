namespace SharpMeasures.Generators.Scalars.Parsing.Diagnostics;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Providers;

internal class SpecializedScalarTypeDiagnostics : IPartialDeclarationProviderDiagnostics
{
    public static SpecializedScalarTypeDiagnostics Instance { get; } = new();

    private SpecializedScalarTypeDiagnostics() { }

    public Diagnostic TypeNotPartial(BaseTypeDeclarationSyntax declaration)
    {
        return DiagnosticConstruction.TypeNotPartial<SpecializedSharpMeasuresScalarAttribute>(declaration.Identifier.GetLocation(), declaration.Identifier.Text);
    }
}
