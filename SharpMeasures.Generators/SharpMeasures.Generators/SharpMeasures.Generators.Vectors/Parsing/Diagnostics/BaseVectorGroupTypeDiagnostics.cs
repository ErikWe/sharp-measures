namespace SharpMeasures.Generators.Vectors.Parsing.Diagnostics;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Providers;

internal class BaseVectorGroupTypeDiagnostics : IPartialDeclarationProviderDiagnostics
{
    public static BaseVectorGroupTypeDiagnostics Instance { get; } = new();

    private BaseVectorGroupTypeDiagnostics() { }

    public Diagnostic TypeNotPartial(BaseTypeDeclarationSyntax declaration)
    {
        return DiagnosticConstruction.TypeNotPartial<SharpMeasuresVectorGroupAttribute>(declaration.Identifier.GetLocation(), declaration.Identifier.Text);
    }
}
