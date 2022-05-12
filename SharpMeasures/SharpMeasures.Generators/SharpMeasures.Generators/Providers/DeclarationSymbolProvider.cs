namespace SharpMeasures.Generators.Providers;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using System.Threading;

internal static class DeclarationSymbolProvider
{
    public readonly record struct InputData(BaseTypeDeclarationSyntax TypeDeclaration, Compilation Compilation);

    public delegate InputData DInputTransform<in TIn>(TIn input);
    public delegate BaseTypeDeclarationSyntax DPartialInputTransform<in TIn>(TIn input);
    public delegate TOut DOutputTransform<in TIn, out TOut>(TIn input, INamedTypeSymbol symbol);

    private delegate TOut DNullableEraser<TOut, TNullableOut>(TNullableOut? output);

    public static IProviderBuilder<TIn, TOut> ConstructForValueType<TIn, TOut>(DInputTransform<TIn> inputTransform, DOutputTransform<TIn, TOut> outputTransform)
        where TOut : struct
    {
        return new ProviderBuilder<TIn, TOut, TOut?>(inputTransform, NullifyOutputTransform(outputTransform), NullableEraser);
    }

    public static IProviderBuilder<TIn, TOut> ConstructForReferenceType<TIn, TOut>(DInputTransform<TIn> inputTransform, DOutputTransform<TIn, TOut> outputTransform)
        where TOut : class
    {
        return new ProviderBuilder<TIn, TOut, TOut>(inputTransform, outputTransform, NullableEraser);
    }

    public static IPartialProviderBuilder<TIn, TOut> ConstructForValueType<TIn, TOut>(DPartialInputTransform<TIn> inputTransform,
        DOutputTransform<TIn, TOut> outputTransform)
        where TOut : struct
    {
        return new PartialProviderBuilder<TIn, TOut, TOut?>(inputTransform, NullifyOutputTransform(outputTransform), NullableEraser);
    }

    public static IPartialProviderBuilder<TIn, TOut> ConstructForReferenceType<TIn, TOut>(DPartialInputTransform<TIn> inputTransform,
        DOutputTransform<TIn, TOut> outputTransform)
        where TOut : class
    {
        return new PartialProviderBuilder<TIn, TOut, TOut>(inputTransform, outputTransform, NullableEraser);
    }

    public static IPartialProviderBuilder<TDeclaration, TOut> ConstructForValueType<TDeclaration, TOut>(DOutputTransform<TDeclaration, TOut> outputTransform)
        where TDeclaration : BaseTypeDeclarationSyntax
        where TOut : struct
    {
        return new PartialProviderBuilder<TDeclaration, TOut, TOut?>(inputTransform, NullifyOutputTransform(outputTransform), NullableEraser);

        static BaseTypeDeclarationSyntax inputTransform(TDeclaration declaration) => declaration;
    }

    public static IPartialProviderBuilder<TDeclaration, TOut> ConstructForReferenceType<TDeclaration, TOut>(DOutputTransform<TDeclaration, TOut> outputTransform)
        where TDeclaration : BaseTypeDeclarationSyntax
        where TOut : class
    {
        return new PartialProviderBuilder<TDeclaration, TOut, TOut>(inputTransform, outputTransform, NullableEraser);

        static BaseTypeDeclarationSyntax inputTransform(TDeclaration declaration) => declaration;
    }

    private static TOut NullableEraser<TOut>(TOut? result) where TOut : struct
    {
        return result!.Value;
    }

    private static TOut NullableEraser<TOut>(TOut? result) where TOut : class
    {
        return result!;
    }

    private static DOutputTransform<TIn, TOut?> NullifyOutputTransform<TIn, TOut>(DOutputTransform<TIn, TOut> outputTransform)
        where TOut : struct
    {
        return toNullable;

        TOut? toNullable(TIn input, INamedTypeSymbol symbol) => outputTransform(input, symbol);
    }

    public interface IProviderBuilder<TIn, TOut>
    {
        public abstract IncrementalValuesProvider<TOut> Attach(IncrementalValuesProvider<TIn> inputProvider);
    }

    public interface IPartialProviderBuilder<TIn, TOut>
    {
        public abstract IncrementalValuesProvider<TOut> Attach(IncrementalValuesProvider<TIn> inputProvider, IncrementalValueProvider<Compilation> compilationProvider);
    }

    private sealed class ProviderBuilder<TIn, TOut, TNullableOut> : IProviderBuilder<TIn, TOut>
    {
        private DInputTransform<TIn> InputTransform { get; }
        private DOutputTransform<TIn, TNullableOut> OutputTransform { get; }

        private DNullableEraser<TOut, TNullableOut> NullableEraser { get; }

        public ProviderBuilder(DInputTransform<TIn> inputTransform, DOutputTransform<TIn, TNullableOut> outputTransform,
            DNullableEraser<TOut, TNullableOut> nullableEraser)
        {
            InputTransform = inputTransform;
            OutputTransform = outputTransform;

            NullableEraser = nullableEraser;
        }

        public IncrementalValuesProvider<TOut> Attach(IncrementalValuesProvider<TIn> inputProvider)
        {
            return inputProvider.Select(ExtractSymbol).Where(IsCorrectlyExtracted).Select(EraseNullable);
        }

        private TNullableOut? ExtractSymbol(TIn input, CancellationToken token)
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

        private bool IsCorrectlyExtracted(TNullableOut? result)
        {
            return result is not null;
        }

        private TOut EraseNullable(TNullableOut? result, CancellationToken _) => NullableEraser(result);
    }

    private sealed class PartialProviderBuilder<TIn, TOut, TNullableOut> : IPartialProviderBuilder<TIn, TOut>
    {
        private ProviderBuilder<(TIn, Compilation), TOut, TNullableOut> ActualBuilder { get; }

        private DPartialInputTransform<TIn> InputTransform { get; }
        private DOutputTransform<TIn, TNullableOut> OutputTransform { get; }

        public PartialProviderBuilder(DPartialInputTransform<TIn> inputTransform, DOutputTransform<TIn, TNullableOut> outputTransform,
            DNullableEraser<TOut, TNullableOut> nullableEraser)
        {
            InputTransform = inputTransform;
            OutputTransform = outputTransform;

            ActualBuilder = new(ConstructInputData, ConstructOutput, nullableEraser);
        }

        public IncrementalValuesProvider<TOut> Attach(IncrementalValuesProvider<TIn> inputProvider, IncrementalValueProvider<Compilation> compilationProvider)
        {
            return ActualBuilder.Attach(inputProvider.Combine(compilationProvider));
        }

        private InputData ConstructInputData((TIn Input, Compilation Compilation) data) => new(InputTransform(data.Input), data.Compilation);
        private TNullableOut ConstructOutput((TIn Input, Compilation Compilation) data, INamedTypeSymbol symbol) => OutputTransform(data.Input, symbol);
    }
}
