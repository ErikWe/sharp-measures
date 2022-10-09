namespace SharpMeasures.Generators.Configuration;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;

using SharpMeasures.Generators.Providers.AnalyzerConfig;

using System.Globalization;
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
            var generateDocumentationByDefault = ParseGenerateDocumentationByDefault(provider);
            var limitOneErrorPerDocumentationFile = ParseLimitOneErrorPerDocumentationFile(provider);
            var generatedFileHeaderContent = ParseGeneratedFileHeaderContent(provider);

            return GlobalAnalyzerConfig.Default with
            {
                DocumentationFileExtension = documentationFileExtension,
                GenerateDocumentationByDefault = generateDocumentationByDefault,
                LimitOneErrorPerDocumentationFile = limitOneErrorPerDocumentationFile,
                GeneratedFileHeaderContent = generatedFileHeaderContent
            };
        }

        private static string ParseDocumentationFileExtension(AnalyzerConfigOptionsProvider provider)
        {
            if (provider.GlobalOptions.TryGetValue(ConfigKeys.GenerateDocumentationByDefault, out var value) is false || value is null)
            {
                return ".doc.txt";
            }

            if (value[0] is not '.')
            {
                return $".{value}";
            }

            return value;
        }

        private static bool ParseGenerateDocumentationByDefault(AnalyzerConfigOptionsProvider provider)
        {
            if (provider.GlobalOptions.TryGetValue(ConfigKeys.GenerateDocumentationByDefault, out var value) is false)
            {
                return true;
            }

            return BooleanTransforms.TrueByDefault(value);
        }

        private static bool ParseLimitOneErrorPerDocumentationFile(AnalyzerConfigOptionsProvider provider)
        {
            if (provider.GlobalOptions.TryGetValue(ConfigKeys.LimitOneErrorPerDocumentationFile, out var value) is false)
            {
                return true;
            }

            return BooleanTransforms.TrueByDefault(value);
        }

        private static GeneratedFileHeaderContent ParseGeneratedFileHeaderContent(AnalyzerConfigOptionsProvider provider)
        {
            if (provider.GlobalOptions.TryGetValue(ConfigKeys.GeneratedFileHeaderContent, out var value) is false || value is null)
            {
                return GeneratedFileHeaderContent.All;
            }

            var components = value.Replace(" ", string.Empty).Split(',');

            var includedContent = GeneratedFileHeaderContent.None;

            for (int i = 0; i < components.Length; i++)
            {
                includedContent |= components[i].ToUpper(CultureInfo.InvariantCulture) switch
                {
                    "ALL" => GeneratedFileHeaderContent.All,
                    "TOOL" => GeneratedFileHeaderContent.Tool,
                    "VERSION" => GeneratedFileHeaderContent.Version,
                    "DATE" => GeneratedFileHeaderContent.Date,
                    "TIME" => GeneratedFileHeaderContent.Time,
                    _ => GeneratedFileHeaderContent.None
                };
            }

            return includedContent;
        }
    }
}
