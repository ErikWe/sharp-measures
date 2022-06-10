namespace SharpMeasures.Generators.Providers.AnalyzerConfig;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;

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
            bool generateDocumentationByDefault = GenerateDocumentationByDefault(provider);

            return new(generateDocumentationByDefault);
        }

        private static class Keys
        {
            public const string GenerateDocumentationByDefault = "SharpMeasures_GenerateDocumentation";
        }

        private static bool GenerateDocumentationByDefault(AnalyzerConfigOptionsProvider provider)
        {
            if (provider.GlobalOptions.TryGetValue(Keys.GenerateDocumentationByDefault, out string? value))
            {
                return BooleanTransforms.FalseByDefault(value);
            }

            return true;
        }
    }
}
