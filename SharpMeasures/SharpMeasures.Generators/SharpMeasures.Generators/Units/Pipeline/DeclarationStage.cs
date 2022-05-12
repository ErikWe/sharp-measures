namespace SharpMeasures.Generators.Units.Pipeline;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpMeasures.Generators.Providers;

internal static class DeclarationStage
{
    public readonly record struct Result(TypeDeclarationSyntax Declaration, INamedTypeSymbol TypeSymbol);

    public static IncrementalValuesProvider<Result> ExtractRelevantPartialDeclarationsWithSymbols(IncrementalGeneratorInitializationContext context)
    {
        var declarations = MarkedTypeDeclarationCandidateProvider.Construct().Attach<GeneratedUnitAttribute>(context.SyntaxProvider);
        var partialDeclarations = PartialDeclarationProvider.Construct<TypeDeclarationSyntax>().AttachAndReport<GeneratedUnitAttribute>(context, declarations);
        var withSymbols = DeclarationSymbolProvider.ConstructForValueType(ConstructFinalResult).Attach(partialDeclarations, context.CompilationProvider);

        return withSymbols;
    }

    private static DeclarationSymbolProvider.DOutputTransform<TypeDeclarationSyntax, Result> ConstructFinalResult { get; }
        = (declaration, symbol) => new(declaration, symbol);
}
