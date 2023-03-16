namespace SharpMeasures.Generators.Providers;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using System;
using System.Threading;

public readonly record struct DeclarationSymbolProviderData(BaseTypeDeclarationSyntax TypeDeclaration, Compilation Compilation);

public interface IDeclarationSymbolProvider<TIn, TOut>
{
    public abstract IncrementalValuesProvider<Optional<TOut>> Attach(IncrementalValuesProvider<Optional<TIn>> inputProvider);
}

public interface IPartialDeclarationSymbolProvider<TIn, TOut>
{
    public abstract IncrementalValuesProvider<Optional<TOut>> Attach(IncrementalValuesProvider<Optional<TIn>> inputProvider, IncrementalValueProvider<Compilation> compilationProvider);
}

public static class DeclarationSymbolProvider
{
    public delegate DeclarationSymbolProviderData DInputTransform<TIn>(TIn input);
    public delegate BaseTypeDeclarationSyntax DPartialInputTransform<TIn>(TIn input);
    public delegate Optional<TOut> DOutputTransform<TIn, TOut>(Optional<TIn> input, INamedTypeSymbol symbol);

    private delegate TOut DNullableEraser<TOut, TNullableOut>(TNullableOut? output);

    public static IDeclarationSymbolProvider<TIn, TOut> Construct<TIn, TOut>(DInputTransform<TIn> inputTransform, DOutputTransform<TIn, TOut> outputTransform) => new Provider<TIn, TOut>(inputTransform, outputTransform);
    public static IDeclarationSymbolProvider<TIn, INamedTypeSymbol> Construct<TIn>(DInputTransform<TIn> inputTransform) => new Provider<TIn, INamedTypeSymbol>(inputTransform, static (input, symbol) => new Optional<INamedTypeSymbol>(symbol));

    public static IPartialDeclarationSymbolProvider<TDeclaration, (TDeclaration Declaration, INamedTypeSymbol TypeSymbol)> Construct<TDeclaration>() where TDeclaration : BaseTypeDeclarationSyntax
    {
        return Construct<TDeclaration, (TDeclaration, INamedTypeSymbol)>(outputTransform);

        static Optional<(TDeclaration, INamedTypeSymbol)> outputTransform(Optional<TDeclaration> input, INamedTypeSymbol symbol)
        {
            if (input.HasValue is false)
            {
                return new Optional<(TDeclaration, INamedTypeSymbol)>();
            }

            return (input.Value, symbol);
        }
    }

    public static IPartialDeclarationSymbolProvider<TIn, TOut> Construct<TIn, TOut>(DPartialInputTransform<TIn> inputTransform, DOutputTransform<TIn, TOut> outputTransform) => new PartialProvider<TIn, TOut>(inputTransform, outputTransform);
    public static IPartialDeclarationSymbolProvider<TDeclaration, TOut> Construct<TDeclaration, TOut>(DOutputTransform<TDeclaration, TOut> outputTransform) where TDeclaration : BaseTypeDeclarationSyntax
    {
        return new PartialProvider<TDeclaration, TOut>(inputTransform, outputTransform);

        static BaseTypeDeclarationSyntax inputTransform(TDeclaration declaration) => declaration;
    }

    private sealed class Provider<TIn, TOut> : IDeclarationSymbolProvider<TIn, TOut>
    {
        private DInputTransform<TIn> InputTransform { get; }
        private DOutputTransform<TIn, TOut> OutputTransform { get; }

        public Provider(DInputTransform<TIn> inputTransform, DOutputTransform<TIn, TOut> outputTransform)
        {
            InputTransform = inputTransform;
            OutputTransform = outputTransform;
        }

        public IncrementalValuesProvider<Optional<TOut>> Attach(IncrementalValuesProvider<Optional<TIn>> inputProvider) => inputProvider.Select(ExtractSymbol);

        private Optional<TOut> ExtractSymbol(Optional<TIn> input, CancellationToken token)
        {
            if (token.IsCancellationRequested || input.HasValue is false)
            {
                return new Optional<TOut>();
            }

            var data = InputTransform(input.Value);

            SemanticModel semanticModel;

            try
            {
                semanticModel = data.Compilation.GetSemanticModel(data.TypeDeclaration.SyntaxTree);
            }
            catch (ArgumentException)
            {
                return default;
            }

            if (semanticModel.GetDeclaredSymbol(data.TypeDeclaration, token) is INamedTypeSymbol symbol)
            {
                return OutputTransform(input.Value, symbol);
            }

            return default;
        }
    }

    private sealed class PartialProvider<TIn, TOut> : IPartialDeclarationSymbolProvider<TIn, TOut>
    {
        private Provider<(TIn, Compilation), TOut> ActualBuilder { get; }

        private DPartialInputTransform<TIn> InputTransform { get; }
        private DOutputTransform<TIn, TOut> OutputTransform { get; }

        public PartialProvider(DPartialInputTransform<TIn> inputTransform, DOutputTransform<TIn, TOut> outputTransform)
        {
            InputTransform = inputTransform;
            OutputTransform = outputTransform;

            ActualBuilder = new(ConstructInputData, ConstructOutput);
        }

        public IncrementalValuesProvider<Optional<TOut>> Attach(IncrementalValuesProvider<Optional<TIn>> inputProvider, IncrementalValueProvider<Compilation> compilationProvider)
        {
            return ActualBuilder.Attach(inputProvider.Combine(compilationProvider).Select(transform));

            static Optional<(TIn, Compilation)> transform((Optional<TIn> Input, Compilation Compilation) data, CancellationToken _)
            {
                if (data.Input.HasValue is false)
                {
                    return new Optional<(TIn, Compilation)>();
                }

                return (data.Input.Value, data.Compilation);
            }
        }

        private DeclarationSymbolProviderData ConstructInputData((TIn Input, Compilation Compilation) data) => new(InputTransform(data.Input), data.Compilation);
        private Optional<TOut> ConstructOutput(Optional<(TIn Input, Compilation Compilation)> data, INamedTypeSymbol symbol)
        {
            if (data.HasValue is false)
            {
                return new Optional<TOut>();
            }

            return OutputTransform(data.Value.Input, symbol);
        }
    }
}
