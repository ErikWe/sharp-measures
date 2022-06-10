namespace SharpMeasures.Generators.Scalars.Diagnostics;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpMeasures.Generators.Attributes.Parsing.ItemLists;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Providers;

internal class ScalarDiagnostics : IPartialDeclarationProviderDiagnostics
{
    public static ScalarDiagnostics Instance { get; } = new();

    private ScalarDiagnostics() { }

    public Diagnostic TypeNotPartial(BaseTypeDeclarationSyntax declaration)
    {
        return DiagnosticConstruction.TypeNotPartial<GeneratedScalarAttribute>(declaration.GetLocation(), declaration.Identifier.Text);
    }

    public static Diagnostic ContradictoryAttributes<TInclusionAttribute, TExclusionAttribute>(IItemListDefinition<string?> definition)
    {
        return DiagnosticConstruction.ContradictoryAttributes<TInclusionAttribute, TExclusionAttribute>(definition.Locations.Attribute.AsRoslynLocation());
    }
}
