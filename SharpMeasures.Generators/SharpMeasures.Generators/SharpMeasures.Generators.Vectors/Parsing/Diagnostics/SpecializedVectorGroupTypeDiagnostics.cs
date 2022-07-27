namespace SharpMeasures.Generators.Vectors.Parsing.Diagnostics;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Providers;

internal class SpecializedVectorGroupTypeDiagnostics : IPartialDeclarationProviderDiagnostics
{
    public static SpecializedVectorGroupTypeDiagnostics Instance { get; } = new();

    private SpecializedVectorGroupTypeDiagnostics() { }

    public Diagnostic TypeNotPartial(BaseTypeDeclarationSyntax declaration)
    {
        return DiagnosticConstruction.TypeNotPartial<SpecializedSharpMeasuresVectorGroupAttribute>(declaration.Identifier.GetLocation(), declaration.Identifier.Text);
    }
}
