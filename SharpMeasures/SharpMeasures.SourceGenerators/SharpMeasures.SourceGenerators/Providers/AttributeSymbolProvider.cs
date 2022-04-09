namespace SharpMeasures.SourceGeneration.Providers;

using Microsoft.CodeAnalysis;

using System.Threading;

internal static class AttributeSymbolProvider
{
    public delegate Compilation DInputTransform<TIn>(TIn input);
    public delegate TOut DOutputTransform<TIn, TOut>(TIn input, INamedTypeSymbol? attributeSyntax);

    public static IncrementalValueProvider<INamedTypeSymbol?> Create<TAttribute>(IncrementalGeneratorInitializationContext context)
        => Attach<TAttribute, Compilation, INamedTypeSymbol?>(context.CompilationProvider, static (x) => x, static (x, y) => y);

    public static IncrementalValueProvider<TOut> Attach<TAttribute, TIn, TOut>(IncrementalValueProvider<TIn> provider, DInputTransform<TIn> inputTransform,
        DOutputTransform<TIn, TOut> outputTransform)
    {
        return provider.Select(extractAttribute);

        TOut extractAttribute(TIn input, CancellationToken _) => outputTransform(input, inputTransform(input).GetTypeByMetadataName(typeof(TAttribute).FullName));
    }
}
