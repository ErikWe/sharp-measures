namespace SharpMeasures.Generators.Vectors.Parsing.Diagnostics;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Providers;

internal class VectorGroupMemberTypeDiagnostics : IPartialDeclarationProviderDiagnostics
{
    public static VectorGroupMemberTypeDiagnostics Instance { get; } = new();

    private VectorGroupMemberTypeDiagnostics() { }

    public Diagnostic TypeNotPartial(BaseTypeDeclarationSyntax declaration)
    {
        return DiagnosticConstruction.TypeNotPartial<SharpMeasuresVectorGroupMemberAttribute>(declaration.Identifier.GetLocation(), declaration.Identifier.Text);
    }
}
