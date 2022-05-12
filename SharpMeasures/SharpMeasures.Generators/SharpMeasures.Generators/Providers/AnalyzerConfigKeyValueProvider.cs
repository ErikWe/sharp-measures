namespace SharpMeasures.Generators.Providers;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;

using System.Collections.Generic;
using System.Threading;

internal static class AnalyzerConfigKeyValueProvider
{
    public delegate TOut DSingleOutputTransform<TOut>(string? value);
    public delegate TOut DMultiOutputTransform<TOut>(IReadOnlyDictionary<string, string?> keyValuePairs);

    public static IProviderBuilder<TOut> Construct<TOut>(DSingleOutputTransform<TOut> outputTransform, string key)
    {
        return new ProviderBuilder<TOut>(outputTransform, key);
    }

    public static IProviderBuilder<TOut> Construct<TOut>(DMultiOutputTransform<TOut> outputTransform, IEnumerable<string> keys)
    {
        return new ProviderBuilder<TOut>(outputTransform, keys);
    }

    public static class BooleanTransforms
    {
        public static DSingleOutputTransform<bool> FalseByDefault { get; } = (value) => value?.ToUpperInvariant() is "TRUE";
        public static DSingleOutputTransform<bool> TrueByDefault { get; } = (value) => value?.ToUpperInvariant() is "FALSE";
        public static DSingleOutputTransform<bool?> NullByDefault { get; } = (value) =>
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

    public interface IProviderBuilder<TOut>
    {
        public IncrementalValueProvider<TOut> Attach(IncrementalValueProvider<AnalyzerConfigOptionsProvider> optionsProvider);
        public IncrementalValueProvider<TOut> Attach(IncrementalValueProvider<AnalyzerConfigOptionsProvider> optionsProvider,
            IncrementalValueProvider<SyntaxTree> syntaxTreeProvider);
        public IncrementalValueProvider<TOut> Attach(IncrementalValueProvider<AnalyzerConfigOptionsProvider> optionsProvider,
            IncrementalValueProvider<AdditionalText> additionalTextProvider);
    }

    private sealed class ProviderBuilder<TOut> : IProviderBuilder<TOut>
    {
        private delegate TOut DOutputTransform(AnalyzerConfigOptions options);

        private DOutputTransform OutputTransform { get; }

        public ProviderBuilder(DSingleOutputTransform<TOut> outputTransform, string key)
        {
            OutputTransform = WrapOutputTransform(outputTransform, key);
        }

        public ProviderBuilder(DMultiOutputTransform<TOut> outputTransform, IEnumerable<string> keys)
        {
            OutputTransform = WrapOutputTransform(outputTransform, keys);
        }

        public IncrementalValueProvider<TOut> Attach(IncrementalValueProvider<AnalyzerConfigOptionsProvider> optionsProvider)
        {
            GlobalOptionsProvider outputProvider = new(OutputTransform);

            return outputProvider.Attach(optionsProvider);
        }

        public IncrementalValueProvider<TOut> Attach(IncrementalValueProvider<AnalyzerConfigOptionsProvider> optionsProvider,
            IncrementalValueProvider<SyntaxTree> syntaxTreeProvider)
        {
            SyntaxTreeOptionsProvider outputProvider = new(OutputTransform);

            return outputProvider.Attach(optionsProvider, syntaxTreeProvider);
        }

        public IncrementalValueProvider<TOut> Attach(IncrementalValueProvider<AnalyzerConfigOptionsProvider> optionsProvider,
            IncrementalValueProvider<AdditionalText> additionalTextProvider)
        {
            AdditionalTextOptionsProvider outputProvider = new(OutputTransform);

            return outputProvider.Attach(optionsProvider, additionalTextProvider);
        }

        private abstract class AProvider<TData>
        {
            private DOutputTransform OutputTransform { get; }

            public AProvider(DOutputTransform outputTransform)
            {
                OutputTransform = outputTransform;
            }

            public AProvider(DMultiOutputTransform<TOut> outputTransform, IEnumerable<string> keys)
            {
                OutputTransform = WrapOutputTransform(outputTransform, keys);
            }

            public IncrementalValueProvider<TOut> Attach(IncrementalValueProvider<TData> optionsProvider)
            {
                return optionsProvider.Select(ExtractValue);
            }

            protected abstract AnalyzerConfigOptions GetOptions(TData optionsProvider);

            private TOut ExtractValue(TData optionsProvider, CancellationToken _)
            {
                return OutputTransform(GetOptions(optionsProvider));
            }
        }

        private class GlobalOptionsProvider : AProvider<AnalyzerConfigOptionsProvider>
        {
            public GlobalOptionsProvider(DOutputTransform outputTransform) : base(outputTransform) { }

            protected override AnalyzerConfigOptions GetOptions(AnalyzerConfigOptionsProvider optionsProvider)
            {
                return optionsProvider.GlobalOptions;
            }
        }

        private class SyntaxTreeOptionsProvider : AProvider<(AnalyzerConfigOptionsProvider Options, SyntaxTree SyntaxTree)>
        {
            public SyntaxTreeOptionsProvider(DOutputTransform outputTransform) : base(outputTransform) { }

            public IncrementalValueProvider<TOut> Attach(IncrementalValueProvider<AnalyzerConfigOptionsProvider> optionsProvider,
                IncrementalValueProvider<SyntaxTree> syntaxTreeProvider)
            {
                return Attach(optionsProvider.Combine(syntaxTreeProvider));
            }

            protected override AnalyzerConfigOptions GetOptions((AnalyzerConfigOptionsProvider Options, SyntaxTree SyntaxTree) inputProviders)
            {
                return inputProviders.Options.GetOptions(inputProviders.SyntaxTree);
            }
        }

        private class AdditionalTextOptionsProvider : AProvider<(AnalyzerConfigOptionsProvider Options, AdditionalText AdditionalText)>
        {
            public AdditionalTextOptionsProvider(DOutputTransform outputTransform) : base(outputTransform) { }

            public IncrementalValueProvider<TOut> Attach(IncrementalValueProvider<AnalyzerConfigOptionsProvider> optionsProvider,
                IncrementalValueProvider<AdditionalText> syntaxTreeProvider)
            {
                return Attach(optionsProvider.Combine(syntaxTreeProvider));
            }

            protected override AnalyzerConfigOptions GetOptions((AnalyzerConfigOptionsProvider Options, AdditionalText AdditionalText) inputProviders)
            {
                return inputProviders.Options.GetOptions(inputProviders.AdditionalText);
            }
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
}
