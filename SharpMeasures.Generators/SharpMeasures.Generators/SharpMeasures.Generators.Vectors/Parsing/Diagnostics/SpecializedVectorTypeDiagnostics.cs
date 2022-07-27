namespace SharpMeasures.Generators.Vectors.Parsing.Diagnostics;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Providers;

internal class SpecializedVectorTypeDiagnostics : IPartialDeclarationProviderDiagnostics
{
    public static SpecializedVectorTypeDiagnostics Instance { get; } = new();

    private SpecializedVectorTypeDiagnostics() { }

    public Diagnostic TypeNotPartial(BaseTypeDeclarationSyntax declaration)
    {
        return DiagnosticConstruction.TypeNotPartial<SpecializedSharpMeasuresVectorAttribute>(declaration.Identifier.GetLocation(), declaration.Identifier.Text);
    }
}
