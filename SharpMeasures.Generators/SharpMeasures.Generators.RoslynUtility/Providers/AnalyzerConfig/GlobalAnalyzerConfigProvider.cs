namespace SharpMeasures.Generators.Providers.AnalyzerConfig;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;

using SharpMeasures.Generators.Configuration;

using System.Threading;

public static class GlobalAnalyzerConfigProvider
{
    public static IncrementalValueProvider<GlobalAnalyzerConfig> Attach(IncrementalValueProvider<AnalyzerConfigOptionsProvider> optionsProvider)
    {
        return optionsProvider.Select(GlobalAnalyzerParser.Parse);
    }

    private static class GlobalAnalyzerParser
    {
        public static GlobalAnalyzerConfig Parse(AnalyzerConfigOptionsProvider provider, CancellationToken token)
        {
            bool generateDocumentationByDefault = ParseGenerateDocumentationByDefault(provider, token);
            bool limitOneErrorPerDocumentationFile = ParseLimitOneErrorPerDocumentationFile(provider, token);

            return new(generateDocumentationByDefault, limitOneErrorPerDocumentationFile);
        }

        private static bool ParseGenerateDocumentationByDefault(AnalyzerConfigOptionsProvider provider, CancellationToken token)
        {
            if (token.IsCancellationRequested)
            {
                return true;
            }

            if (provider.GlobalOptions.TryGetValue(ConfigKeys.GenerateDocumentationByDefault, out string? value))
            {
                return BooleanTransforms.TrueByDefault(value);
            }

            return true;
        }

        private static bool ParseLimitOneErrorPerDocumentationFile(AnalyzerConfigOptionsProvider provider, CancellationToken token)
        {
            if (token.IsCancellationRequested)
            {
                return true;
            }

            if (provider.GlobalOptions.TryGetValue(ConfigKeys.LimitOneErrorPerDocumentationFile, out string? value))
            {
                return BooleanTransforms.TrueByDefault(value);
            }

            return true;
        }
    }
}
