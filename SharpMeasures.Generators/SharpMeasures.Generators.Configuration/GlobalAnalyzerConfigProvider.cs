namespace SharpMeasures.Generators.Configuration;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;

using SharpMeasures.Generators.Providers.AnalyzerConfig;

using System.Threading;

public static class GlobalAnalyzerConfigProvider
{
    public static IncrementalValueProvider<GlobalAnalyzerConfig> Attach(IncrementalValueProvider<AnalyzerConfigOptionsProvider> optionsProvider) => optionsProvider.Select(GlobalAnalyzerParser.Parse);

    private static class GlobalAnalyzerParser
    {
        public static GlobalAnalyzerConfig Parse(AnalyzerConfigOptionsProvider provider, CancellationToken token)
        {
            if (token.IsCancellationRequested)
            {
                return GlobalAnalyzerConfig.Default;
            }

            var documentationFileExtension = ParseDocumentationFileExtension(provider);
            var printDocumentationTags = ParsePrintDocumentationTags(provider);
            var limitOneErrorPerDocumentationFile = ParseLimitOneErrorPerDocumentationFile(provider);
            var generateDocumentation = ParseGenerateDocumentation(provider);

            var generatedFileHeaderContent = ParseGeneratedFileHeaderContent(provider);

            return GlobalAnalyzerConfig.Default with
            {
                DocumentationFileExtension = documentationFileExtension,
                PrintDocumentationTags = printDocumentationTags,
                GenerateDocumentation = generateDocumentation,
                LimitOneErrorPerDocumentationFile = limitOneErrorPerDocumentationFile,
                GeneratedFileHeaderLevel = generatedFileHeaderContent
            };
        }

        private static string ParseDocumentationFileExtension(AnalyzerConfigOptionsProvider provider)
        {
            if (provider.GlobalOptions.TryGetValue(ConfigKeys.DocumentationFileExtension, out var value) is false || value is null)
            {
                return ".doc.txt";
            }

            if (value[0] is not '.')
            {
                return $".{value}";
            }

            return value;
        }

        private static bool ParsePrintDocumentationTags(AnalyzerConfigOptionsProvider provider)
        {
            if (provider.GlobalOptions.TryGetValue(ConfigKeys.PrintDocumentationTags, out var value) is false)
            {
                return false;
            }

            return BooleanTransforms.FalseByDefault(value);
        }

        private static bool ParseLimitOneErrorPerDocumentationFile(AnalyzerConfigOptionsProvider provider)
        {
            if (provider.GlobalOptions.TryGetValue(ConfigKeys.LimitOneErrorPerDocumentationFile, out var value) is false)
            {
                return true;
            }

            return BooleanTransforms.TrueByDefault(value);
        }

        private static bool ParseGenerateDocumentation(AnalyzerConfigOptionsProvider provider)
        {
            if (provider.GlobalOptions.TryGetValue(ConfigKeys.GenerateDocumentation, out var value) is false)
            {
                return true;
            }

            return BooleanTransforms.TrueByDefault(value);
        }

        private static int ParseGeneratedFileHeaderContent(AnalyzerConfigOptionsProvider provider)
        {
            if (provider.GlobalOptions.TryGetValue(ConfigKeys.GeneratedFileHeaderLevel, out var value) is false || value is null)
            {
                return -1;
            }

            if (int.TryParse(value.Trim(), out var integerValue))
            {
                return integerValue;
            }

            return -1;
        }
    }
}
