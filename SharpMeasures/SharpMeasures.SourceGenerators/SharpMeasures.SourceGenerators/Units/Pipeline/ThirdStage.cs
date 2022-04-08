namespace ErikWe.SharpMeasures.SourceGenerators.Units.Pipeline;

using ErikWe.SharpMeasures.SourceGenerators.Documentation;
using ErikWe.SharpMeasures.SourceGenerators.Providers;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using System.Collections.Generic;

internal static class ThirdStage
{
    public readonly record struct Result(MarkedDeclarationSyntaxProvider.OutputData Declaration, IEnumerable<DocumentationFile> Documentation, INamedTypeSymbol TypeSymbol);

    public static IncrementalValuesProvider<Result> Perform(IncrementalGeneratorInitializationContext context, IncrementalValuesProvider<SecondStage.Result> provider)
        => TypeSymbolProvider.Attach(provider, context.CompilationProvider, InputTransform, OutputTransform)
            .WhereNotNull();

    private static TypeDeclarationSyntax InputTransform(SecondStage.Result input) => input.Declaration.Type;
    private static Result? OutputTransform(SecondStage.Result input, INamedTypeSymbol? symbol)
        => symbol is null ? null : new(input.Declaration, input.Documentation, symbol);
}
