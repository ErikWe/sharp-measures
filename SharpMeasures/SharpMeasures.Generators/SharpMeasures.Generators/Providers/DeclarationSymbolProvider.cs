namespace SharpMeasures.Generators.Providers;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using System.Threading;

internal static class DeclarationSymbolProvider
{
    public readonly record struct InputData(TypeDeclarationSyntax TypeDeclaration, Compilation Compilation);

    public delegate TIntermediate DInputTransform<TIn, TIntermediate>(TIn input);
    public delegate TOut DOutputTransform<TIn, TOut>(TIn input, INamedTypeSymbol symbol);

    public static IncrementalValuesProvider<TOut> Attach<TIn, TOut>(IncrementalValuesProvider<TIn> inputProvider, DInputTransform<TIn, InputData> inputTransform,
        DOutputTransform<TIn, TOut> outputTransform)
        where TOut : class
    {
        Provider<TIn, TOut> provider = new(inputTransform, outputTransform);

        return provider.Attach(inputProvider).WhereNotNull();
    }

    public static IncrementalValuesProvider<TOut> AttachToValueType<TIn, TOut>(IncrementalValuesProvider<TIn> inputProvider,
        DInputTransform<TIn, InputData> inputTransform, DOutputTransform<TIn, TOut> outputTransform)
        where TOut : struct
    {
        return AttachToValueType(inputProvider, inputTransform, modifiedOutputTransform);

        TOut? modifiedOutputTransform(TIn input, INamedTypeSymbol typeSymbol) => outputTransform(input, typeSymbol);
    }

    public static IncrementalValuesProvider<TOut> AttachToValueType<TIn, TOut>(IncrementalValuesProvider<TIn> inputProvider,
        DInputTransform<TIn, InputData> inputTransform, DOutputTransform<TIn, TOut?> outputTransform)
        where TOut : struct
    {
        Provider<TIn, TOut?> provider = new(inputTransform, outputTransform);

        return provider.Attach(inputProvider).WhereNotNull();
    }

    public static IncrementalValuesProvider<TOut> Attach<TIn, TOut>(IncrementalValuesProvider<TIn> inputProvider,
        IncrementalValueProvider<Compilation> compilationProvider, DInputTransform<TIn, TypeDeclarationSyntax> inputTransform,
        DOutputTransform<TIn, TOut> outputTransform)
        where TOut : class
    {
        IntermediateProvider<TIn, TOut> intermediateProvider = new(inputTransform, outputTransform);

        return intermediateProvider.Attach(inputProvider, compilationProvider).WhereNotNull();
    }

    public static IncrementalValuesProvider<TOut> AttachToValueType<TIn, TOut>(IncrementalValuesProvider<TIn> inputProvider,
        IncrementalValueProvider<Compilation> compilationProvider, DInputTransform<TIn, TypeDeclarationSyntax> inputTransform,
        DOutputTransform<TIn, TOut> outputTransform)
        where TOut : struct
    {
        return AttachToValueType(inputProvider, compilationProvider, inputTransform, modifiedOutputTransform);

        TOut? modifiedOutputTransform(TIn input, INamedTypeSymbol typeSymbol) => outputTransform(input, typeSymbol);
    }

    public static IncrementalValuesProvider<TOut> AttachToValueType<TIn, TOut>(IncrementalValuesProvider<TIn> inputProvider,
        IncrementalValueProvider<Compilation> compilationProvider, DInputTransform<TIn, TypeDeclarationSyntax> inputTransform,
        DOutputTransform<TIn, TOut?> outputTransform)
        where TOut : struct
    {
        IntermediateProvider<TIn, TOut?> intermediateProvider = new(inputTransform, outputTransform);

        return intermediateProvider.Attach(inputProvider, compilationProvider).WhereNotNull();
    }

    public static IncrementalValuesProvider<TOut> Attach<TIn, TOut>(IncrementalValuesProvider<TIn> inputProvider,
        IncrementalValueProvider<Compilation> compilationProvider, DOutputTransform<TIn, TOut> outputTransform)
        where TIn : TypeDeclarationSyntax
        where TOut : class
    {
        IntermediateProvider<TIn, TOut> intermediateProvider = new(static (x) => x, outputTransform);

        return intermediateProvider.Attach(inputProvider, compilationProvider).WhereNotNull();
    }

    public static IncrementalValuesProvider<TOut> AttachToValueType<TIn, TOut>(IncrementalValuesProvider<TIn> inputProvider,
        IncrementalValueProvider<Compilation> compilationProvider, DOutputTransform<TIn, TOut> outputTransform)
        where TIn : TypeDeclarationSyntax
        where TOut : struct
    {
        return AttachToValueType(inputProvider, compilationProvider, modifiedOutputTransform);

        TOut? modifiedOutputTransform(TIn input, INamedTypeSymbol typeSymbol) => outputTransform(input, typeSymbol);
    }

    public static IncrementalValuesProvider<TOut> AttachToValueType<TIn, TOut>(IncrementalValuesProvider<TIn> inputProvider,
        IncrementalValueProvider<Compilation> compilationProvider, DOutputTransform<TIn, TOut?> outputTransform)
        where TIn : TypeDeclarationSyntax
        where TOut : struct
    {
        IntermediateProvider<TIn, TOut?> intermediateProvider = new(static (x) => x, outputTransform);

        return intermediateProvider.Attach(inputProvider, compilationProvider).WhereNotNull();
    }

    private class IntermediateProvider<TIn, TOut>
    {
        private Provider<(TIn, Compilation), TOut> ActualProvider { get; }

        private DInputTransform<TIn, TypeDeclarationSyntax> InputTransform { get; }
        private DOutputTransform<TIn, TOut> OutputTransform { get; }

        public IntermediateProvider(DInputTransform<TIn, TypeDeclarationSyntax> inputTransform, DOutputTransform<TIn, TOut> outputTransform)
        {
            InputTransform = inputTransform;
            OutputTransform = outputTransform;

            ActualProvider = new(ConstructInputData, ConstructOutput);
        }

        public IncrementalValuesProvider<TOut?> Attach(IncrementalValuesProvider<TIn> inputProvider, IncrementalValueProvider<Compilation> compilationProvider)
        {
            return ActualProvider.Attach(inputProvider.Combine(compilationProvider));
        }

        private InputData ConstructInputData((TIn Input, Compilation Compilation) data) => new(InputTransform(data.Input), data.Compilation);
        private TOut ConstructOutput((TIn Input, Compilation Compilation) data, INamedTypeSymbol symbol) => OutputTransform(data.Input, symbol);
    }

    private class Provider<TIn, TOut>
    {
        private DInputTransform<TIn, InputData> InputTransform { get; }
        private DOutputTransform<TIn, TOut> OutputTransform { get; }

        public Provider(DInputTransform<TIn, InputData> inputTransform, DOutputTransform<TIn, TOut> outputTransform)
        {
            InputTransform = inputTransform;
            OutputTransform = outputTransform;
        }

        public IncrementalValuesProvider<TOut?> Attach(IncrementalValuesProvider<TIn> inputProvider)
        {
            return inputProvider.Select(ExtractSymbol);
        }

        private TOut? ExtractSymbol(TIn input, CancellationToken token)
        {
            InputData data = InputTransform(input);

            if (data.Compilation.GetSemanticModel(data.TypeDeclaration.SyntaxTree).GetDeclaredSymbol(data.TypeDeclaration, token) is INamedTypeSymbol symbol)
            {
                return OutputTransform(input, symbol);
            }
            else
            {
                return default;
            }
        }
    }
}
