namespace SharpMeasures.Generators.Vectors.Diagnostics;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpMeasures.Generators.Attributes.Parsing.ItemLists;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Providers;

internal class VectorDiagnostics : IPartialDeclarationProviderDiagnostics
{
    public static VectorDiagnostics Instance { get; } = new();

    private VectorDiagnostics() { }

    public Diagnostic TypeNotPartial(BaseTypeDeclarationSyntax declaration)
    {
        return DiagnosticConstruction.TypeNotPartial<SharpMeasuresVectorAttribute>(declaration.Identifier.GetLocation(), declaration.Identifier.Text);
    }

    public static Diagnostic ContradictoryAttributes<TInclusionAttribute, TExclusionAttribute>(IItemListDefinition<string?> definition)
    {
        return DiagnosticConstruction.ContradictoryAttributes<TInclusionAttribute, TExclusionAttribute>(definition.Locations.AttributeName.AsRoslynLocation());
    }
}
