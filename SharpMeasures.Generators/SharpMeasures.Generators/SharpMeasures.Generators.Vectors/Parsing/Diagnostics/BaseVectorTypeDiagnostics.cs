namespace SharpMeasures.Generators.Vectors.Parsing.Diagnostics;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Providers;

internal class BaseVectorTypeDiagnostics : IPartialDeclarationProviderDiagnostics
{
    public static BaseVectorTypeDiagnostics Instance { get; } = new();

    private BaseVectorTypeDiagnostics() { }

    public Diagnostic TypeNotPartial(BaseTypeDeclarationSyntax declaration)
    {
        return DiagnosticConstruction.TypeNotPartial<SharpMeasuresVectorAttribute>(declaration.Identifier.GetLocation(), declaration.Identifier.Text);
    }
}
