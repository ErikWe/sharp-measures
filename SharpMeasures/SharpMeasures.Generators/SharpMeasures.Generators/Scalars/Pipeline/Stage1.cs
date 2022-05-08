namespace SharpMeasures.Generators.Scalars.Pipeline;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

internal static class Stage1
{
    public static IncrementalValuesProvider<SharpMeasuresGenerator.Result> ExtractRelevantDeclarations(IncrementalGeneratorInitializationContext context,
        IncrementalValuesProvider<SharpMeasuresGenerator.Result> declarationAndSymbolProvider)
    {
        return declarationAndSymbolProvider.Combine(context.CompilationProvider).Where(IncludesGeneratedScalarQuantityAttribute).Select((x, _) => x.Left);
    }

    private static bool IncludesGeneratedScalarQuantityAttribute((SharpMeasuresGenerator.Result DeclarationAndSymbol, Compilation Compilation) data)
    {
        SemanticModel semanticModel = data.Compilation.GetSemanticModel(data.DeclarationAndSymbol.Declaration.AttributeSyntax.SyntaxTree);

        return data.DeclarationAndSymbol.Declaration.AttributeSyntax.IsDescribedAttributeType<GeneratedScalarQuantityAttribute>(semanticModel);
    }
}
