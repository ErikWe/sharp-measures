namespace SharpMeasures.Generators.Providers.AnalyzerConfig;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;

using System.Collections.Generic;
using System.Threading;

public static class BooleanTransforms
{
    public static AnalyzerConfigKeyValueProvider.DSingleOutputTransform<bool> FalseByDefault { get; } = (value) => value?.ToUpperInvariant() is "TRUE";
    public static AnalyzerConfigKeyValueProvider.DSingleOutputTransform<bool> TrueByDefault { get; } = (value) => value?.ToUpperInvariant() is "FALSE" is false;
    public static AnalyzerConfigKeyValueProvider.DSingleOutputTransform<bool?> NullByDefault { get; } = (value) =>
    {
        if (value is null)
        {
            return null;
        }

        if (FalseByDefault(value) is true)
        {
            return true;
        }

        if (TrueByDefault(value) is false)
        {
            return false;
        }

        return null;
    };
}

public interface IProvider<TOut>
{
    public IncrementalValueProvider<TOut> Attach(IncrementalValueProvider<AnalyzerConfigOptionsProvider> optionsProvider);
    public IncrementalValueProvider<TOut> Attach(IncrementalValueProvider<AnalyzerConfigOptionsProvider> optionsProvider, IncrementalValueProvider<SyntaxTree> syntaxTreeProvider);
    public IncrementalValueProvider<TOut> Attach(IncrementalValueProvider<AnalyzerConfigOptionsProvider> optionsProvider, IncrementalValueProvider<AdditionalText> additionalTextProvider);
}

public interface ITransformedProvider<TIn, TOut> : IProvider<TOut>
{
    public IncrementalValueProvider<TOut> Attach(IncrementalValueProvider<AnalyzerConfigOptionsProvider> optionsProvider, IncrementalValueProvider<TIn> inputProvider);
}

public static class AnalyzerConfigKeyValueProvider
{
    public delegate TOut DSingleOutputTransform<out TOut>(string? value);
    public delegate TOut DMultiOutputTransform<out TOut>(IReadOnlyDictionary<string, string?> keyValuePairs);

    public delegate TData DInputTransform<in TIn, out TData>(TIn input);

    public static IProvider<TOut> Construct<TOut>(DSingleOutputTransform<TOut> outputTransform, string key) => new Provider<TOut>(outputTransform, key);
    public static IProvider<TOut> Construct<TOut>(DMultiOutputTransform<TOut> outputTransform, IEnumerable<string> keys) => new Provider<TOut>(outputTransform, keys);

    public static ITransformedProvider<TIn, TOut> Construct<TIn, TOut>(DInputTransform<TIn, SyntaxTree> inputTransform, DSingleOutputTransform<TOut> outputTransform, string key) => TransformedProvider<TIn, TOut, SyntaxTree>.Construct(inputTransform, outputTransform, key);
    public static ITransformedProvider<TIn, TOut> Construct<TIn, TOut>(DInputTransform<TIn, SyntaxTree> inputTransform, DMultiOutputTransform<TOut> outputTransform, IEnumerable<string> keys) => TransformedProvider<TIn, TOut, SyntaxTree>.Construct(inputTransform, outputTransform, keys);
    public static ITransformedProvider<TIn, TOut> Construct<TIn, TOut>(DInputTransform<TIn, AdditionalText> inputTransform, DSingleOutputTransform<TOut> outputTransform, string key) => TransformedProvider<TIn, TOut, AdditionalText>.Construct(inputTransform, outputTransform, key);
    public static ITransformedProvider<TIn, TOut> Construct<TIn, TOut>(DInputTransform<TIn, AdditionalText> inputTransform, DMultiOutputTransform<TOut> outputTransform, IEnumerable<string> keys) => TransformedProvider<TIn, TOut, AdditionalText>.Construct(inputTransform, outputTransform, keys);

    private class Provider<TOut> : IProvider<TOut>
    {
        protected delegate TOut DOutputTransform(AnalyzerConfigOptions options);

        protected DOutputTransform OutputTransform { get; }

        public Provider(DSingleOutputTransform<TOut> outputTransform, string key)
        {
            OutputTransform = WrapOutputTransform(outputTransform, key);
        }

        public Provider(DMultiOutputTransform<TOut> outputTransform, IEnumerable<string> keys)
        {
            OutputTransform = WrapOutputTransform(outputTransform, keys);
        }

        public IncrementalValueProvider<TOut> Attach(IncrementalValueProvider<AnalyzerConfigOptionsProvider> optionsProvider) => Attach(new GlobalOptionsStrategy(), optionsProvider);
        public IncrementalValueProvider<TOut> Attach(IncrementalValueProvider<AnalyzerConfigOptionsProvider> optionsProvider, IncrementalValueProvider<SyntaxTree> syntaxTreeProvider)
        {
            IOptionsProviderStrategy optionsProviderStrategy = new StrategizedOptionsProvider<SyntaxTree>(new SyntaxTreeOptionsStrategy(), OutputTransform, syntaxTreeProvider);

            return Attach(optionsProviderStrategy, optionsProvider);
        }

        public IncrementalValueProvider<TOut> Attach(IncrementalValueProvider<AnalyzerConfigOptionsProvider> optionsProvider, IncrementalValueProvider<AdditionalText> additionalTextProvider)
        {
            IOptionsProviderStrategy optionsProviderStrategy = new StrategizedOptionsProvider<AdditionalText>(new AdditionalTextOptionsStrategy(), OutputTransform, additionalTextProvider);

            return Attach(optionsProviderStrategy, optionsProvider);
        }

        protected IncrementalValueProvider<TOut> Attach(IOptionsStrategy optionsStrategy, IncrementalValueProvider<AnalyzerConfigOptionsProvider> optionsProvider)
        {
            return optionsProvider.Select(extractData);

            TOut extractData(AnalyzerConfigOptionsProvider options, CancellationToken _)
            {
                return OutputTransform(optionsStrategy.ExtractOptions(options));
            }
        }

        protected static IncrementalValueProvider<TOut> Attach(IOptionsProviderStrategy optionsProviderStrategy, IncrementalValueProvider<AnalyzerConfigOptionsProvider> optionsProvider)
        {
            return optionsProviderStrategy.Attach(optionsProvider);
        }

        protected interface IOptionsStrategy
        {
            public abstract AnalyzerConfigOptions ExtractOptions(AnalyzerConfigOptionsProvider optionsProvider);
        }

        protected interface IOptionsStrategy<TData>
        {
            public abstract AnalyzerConfigOptions ExtractOptions(AnalyzerConfigOptionsProvider optionsProvider, TData data);
        }

        protected interface IOptionsProviderStrategy
        {
            public abstract IncrementalValueProvider<TOut> Attach(IncrementalValueProvider<AnalyzerConfigOptionsProvider> optionsProvider);
        }

        protected sealed class GlobalOptionsStrategy : IOptionsStrategy
        {
            public AnalyzerConfigOptions ExtractOptions(AnalyzerConfigOptionsProvider optionsProvider) => optionsProvider.GlobalOptions;
        }

        protected sealed class SyntaxTreeOptionsStrategy : IOptionsStrategy<SyntaxTree>
        {
            public AnalyzerConfigOptions ExtractOptions(AnalyzerConfigOptionsProvider optionsProvider, SyntaxTree syntaxTree) => optionsProvider.GetOptions(syntaxTree);
        }

        protected sealed class AdditionalTextOptionsStrategy : IOptionsStrategy<AdditionalText>
        {
            public AnalyzerConfigOptions ExtractOptions(AnalyzerConfigOptionsProvider optionsProvider, AdditionalText additionalText) => optionsProvider.GetOptions(additionalText);
        }

        protected sealed class StrategizedOptionsProvider<TData> : IOptionsProviderStrategy
        {
            private IOptionsStrategy<TData> OptionsStrategy { get; }
            private DOutputTransform OutputTransform { get; }

            private IncrementalValueProvider<TData> InputProvider { get; }

            public StrategizedOptionsProvider(IOptionsStrategy<TData> optionsStrategy, DOutputTransform outputTransform, IncrementalValueProvider<TData> inputProvider)
            {
                OptionsStrategy = optionsStrategy;
                OutputTransform = outputTransform;

                InputProvider = inputProvider;
            }

            public IncrementalValueProvider<TOut> Attach(IncrementalValueProvider<AnalyzerConfigOptionsProvider> optionsProvider) => optionsProvider.Combine(InputProvider).Select(ExtractOptions);
            private TOut ExtractOptions((AnalyzerConfigOptionsProvider Options, TData Input) data, CancellationToken _) => OutputTransform(OptionsStrategy.ExtractOptions(data.Options, data.Input));
        }

        private static DOutputTransform WrapOutputTransform(DSingleOutputTransform<TOut> singleTransform, string key)
        {
            return outputTransform;

            TOut outputTransform(AnalyzerConfigOptions options)
            {
                options.TryGetValue(key, out string? value);

                return singleTransform(value);
            }
        }

        private static DOutputTransform WrapOutputTransform(DMultiOutputTransform<TOut> multiTransform, IEnumerable<string> keys)
        {
            return outputTransform;

            TOut outputTransform(AnalyzerConfigOptions options)
            {
                Dictionary<string, string?> keyValuePairs = new();

                foreach (string key in keys)
                {
                    options.TryGetValue(key, out string? value);

                    keyValuePairs.Add(key, value);
                }

                return multiTransform(keyValuePairs);
            }
        }
    }

    private sealed class TransformedProvider<TIn, TOut, TData> : Provider<TOut>, ITransformedProvider<TIn, TOut>
    {
        public static TransformedProvider<TIn, TOut, SyntaxTree> Construct(DInputTransform<TIn, SyntaxTree> inputTransform, DSingleOutputTransform<TOut> outputTransform, string key) => new(new SyntaxTreeOptionsStrategy(), inputTransform, outputTransform, key);
        public static TransformedProvider<TIn, TOut, SyntaxTree> Construct(DInputTransform<TIn, SyntaxTree> inputTransform, DMultiOutputTransform<TOut> outputTransform, IEnumerable<string> keys) => new(new SyntaxTreeOptionsStrategy(), inputTransform, outputTransform, keys);
        public static TransformedProvider<TIn, TOut, AdditionalText> Construct(DInputTransform<TIn, AdditionalText> inputTransform, DSingleOutputTransform<TOut> outputTransform, string key) => new(new AdditionalTextOptionsStrategy(), inputTransform, outputTransform, key);
        public static TransformedProvider<TIn, TOut, AdditionalText> Construct(DInputTransform<TIn, AdditionalText> inputTransform, DMultiOutputTransform<TOut> outputTransform, IEnumerable<string> keys) => new(new AdditionalTextOptionsStrategy(), inputTransform, outputTransform, keys);

        private TransformedOptionsStrategy OptionsStrategy { get; }

        private TransformedProvider(IOptionsStrategy<TData> optionsStrategy, DInputTransform<TIn, TData> inputTransform, DSingleOutputTransform<TOut> outputTransform, string key) : base(outputTransform, key)
        {
            OptionsStrategy = new(optionsStrategy, inputTransform);
        }

        private TransformedProvider(IOptionsStrategy<TData> optionsStrategy, DInputTransform<TIn, TData> inputTransform, DMultiOutputTransform<TOut> outputTransform, IEnumerable<string> keys) : base(outputTransform, keys)
        {
            OptionsStrategy = new(optionsStrategy, inputTransform);
        }

        public IncrementalValueProvider<TOut> Attach(IncrementalValueProvider<AnalyzerConfigOptionsProvider> optionsProvider, IncrementalValueProvider<TIn> inputProvider)
        {
            IOptionsProviderStrategy optionsProviderStrategy = new StrategizedOptionsProvider<TIn>(OptionsStrategy, OutputTransform, inputProvider);

            return Attach(optionsProviderStrategy, optionsProvider);
        }

        private sealed class TransformedOptionsStrategy : IOptionsStrategy<TIn>
        {
            private IOptionsStrategy<TData> BackboneStrategy { get; }
            private DInputTransform<TIn, TData> InputTransform { get; }

            public TransformedOptionsStrategy(IOptionsStrategy<TData> backboneStrategy, DInputTransform<TIn, TData> inputTransform)
            {
                BackboneStrategy = backboneStrategy;
                InputTransform = inputTransform;
            }

            public AnalyzerConfigOptions ExtractOptions(AnalyzerConfigOptionsProvider optionsProvider, TIn input) => BackboneStrategy.ExtractOptions(optionsProvider, InputTransform(input));
        }
    }
}
