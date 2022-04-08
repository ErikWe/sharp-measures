namespace SharpMeasures.SourceGenerators.Providers;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using System.Threading;

internal static class TypeSymbolProvider
{
    public readonly record struct InputData(TypeDeclarationSyntax TypeDeclaration, Compilation Compilation);
    public delegate TInternal DInputTransform<TIn, TInternal>(TIn input);
    public delegate TOut DOutputTransform<TIn, TOut>(TIn input, INamedTypeSymbol? symbol);

    public static IncrementalValuesProvider<TOut> Attach<TIn, TOut>(IncrementalValuesProvider<TIn> provider, DInputTransform<TIn, InputData> inputTransform,
        DOutputTransform<TIn, TOut> outputTransform)
    {
        return provider.Select(extractSymbol);

        TOut extractSymbol(TIn input, CancellationToken _) => outputTransform(input, ExtractSymbol(inputTransform(input)));
    }

    public static IncrementalValuesProvider<TOut> Attach<TIn, TOut>(IncrementalValuesProvider<TIn> dataProvider, IncrementalValueProvider<Compilation> compilationProvider,
        DInputTransform<TIn, TypeDeclarationSyntax> inputTransform, DOutputTransform<TIn, TOut> outputTransform)
        => Attach(dataProvider.Combine(compilationProvider),
            (input) => new InputData(inputTransform(input.Left), input.Right), (input, symbol) => outputTransform(input.Left, symbol));

    private static INamedTypeSymbol? ExtractSymbol(InputData data)
        => data.Compilation.GetSemanticModel(data.TypeDeclaration.SyntaxTree).GetDeclaredSymbol(data.TypeDeclaration) as INamedTypeSymbol;
}
