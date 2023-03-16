namespace SharpMeasures.Generators.Configuration;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;

using SharpMeasures.Generators.Providers.AnalyzerConfig;

using System.Collections.Generic;
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

            var allowAttributeAliases = ParseAllowAttributeAliases(provider);

            return GlobalAnalyzerConfig.Default with
            {
                DocumentationFileExtension = documentationFileExtension,
                PrintDocumentationTags = printDocumentationTags,
                GenerateDocumentation = generateDocumentation,
                LimitOneErrorPerDocumentationFile = limitOneErrorPerDocumentationFile,
                GeneratedFileHeaderContent = generatedFileHeaderContent,
                AllowAttributeAliases = allowAttributeAliases
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

        private static GeneratedFileHeaderContent ParseGeneratedFileHeaderContent(AnalyzerConfigOptionsProvider provider)
        {
            if (provider.GlobalOptions.TryGetValue(ConfigKeys.GeneratedFileHeaderContent, out var value) is false || value is null)
            {
                return GeneratedFileHeaderContent.All;
            }

            var content = GeneratedFileHeaderContent.None;

            foreach (var component in value.Replace(" ", string.Empty).Split(','))
            {
                if (GeneratedFileHeaderContentKeys.TryGetValue(component.ToUpperInvariant(), out var key))
                {
                    content |= key;
                }
            }

            return content;
        }

        private static bool ParseAllowAttributeAliases(AnalyzerConfigOptionsProvider provider)
        {
            if (provider.GlobalOptions.TryGetValue(ConfigKeys.AllowAttributeAliases, out var value) is false)
            {
                return false;
            }

            return BooleanTransforms.FalseByDefault(value);
        }

        private static Dictionary<string, GeneratedFileHeaderContent> GeneratedFileHeaderContentKeys { get; } = new()
        {
            { "NONE", GeneratedFileHeaderContent.None },
            { "HEADER", GeneratedFileHeaderContent.Header },
            { "TOOL", GeneratedFileHeaderContent.Tool },
            { "VERSION", GeneratedFileHeaderContent.Version },
            { "DATE", GeneratedFileHeaderContent.Date },
            { "TIME", GeneratedFileHeaderContent.Time },
            { "ALL", GeneratedFileHeaderContent.All }
        };
    }
}
