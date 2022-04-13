namespace SharpMeasures.Generators.Units.Pipeline;

using SharpMeasures.Generators.Providers;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

internal static class SecondStage
{
    public readonly record struct Result(MarkedDeclarationSyntaxProvider.OutputData Declaration, INamedTypeSymbol TypeSymbol);

    public static IncrementalValuesProvider<Result> Perform(IncrementalGeneratorInitializationContext context, IncrementalValuesProvider<FirstStage.Result> provider)
        => TypeSymbolProvider.Attach(provider, context.CompilationProvider, InputTransform, OutputTransform)
            .WhereNotNull();

    private static TypeDeclarationSyntax InputTransform(FirstStage.Result input) => input.Declaration.Type;
    private static Result? OutputTransform(FirstStage.Result input, INamedTypeSymbol? symbol) => symbol is null ? null : new(input.Declaration, symbol);
}
