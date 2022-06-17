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
        public static GlobalAnalyzerConfig Parse(AnalyzerConfigOptionsProvider provider, CancellationToken _)
        {
            bool generateDocumentationByDefault = ParseGenerateDocumentationByDefault(provider);
            bool limitOneErrorPerDocumentationFile = ParseLimitOneErrorPerDocumentationFile(provider);

            return new(generateDocumentationByDefault, limitOneErrorPerDocumentationFile);
        }

        private static bool ParseGenerateDocumentationByDefault(AnalyzerConfigOptionsProvider provider)
        {
            if (provider.GlobalOptions.TryGetValue(ConfigKeys.GenerateDocumentationByDefault, out string? value))
            {
                return BooleanTransforms.TrueByDefault(value);
            }

            return true;
        }

        private static bool ParseLimitOneErrorPerDocumentationFile(AnalyzerConfigOptionsProvider provider)
        {
            if (provider.GlobalOptions.TryGetValue(ConfigKeys.LimitOneErrorPerDocumentationFile, out string? value))
            {
                return BooleanTransforms.TrueByDefault(value);
            }

            return true;
        }
    }
}
