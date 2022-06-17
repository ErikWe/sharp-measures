namespace SharpMeasures.Generators.Documentation;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Configuration;
using SharpMeasures.Generators.Diagnostics;

using System;
using System.Collections.Immutable;
using System.Threading;

public class DocumentationDictionaryProvider
{
    private const string DocumentationExtension = ".doc.txt";

    public static IncrementalValueProvider<DocumentationDictionary> AttachWithoutDiagnostics(IncrementalValuesProvider<AdditionalText> additionalTextProvider)
    {
        DocumentationDictionaryProvider outputProvider = new(new EmptyDiagnosticsStrategy());

        return Construct(outputProvider, additionalTextProvider).ExtractResult();
    }

    public static IncrementalValueProvider<DocumentationDictionary> AttachAndReport(IncrementalGeneratorInitializationContext context,
        IncrementalValuesProvider<AdditionalText> additionalTextProvider, IncrementalValueProvider<GlobalAnalyzerConfig> configurationProvider,
        IDiagnosticsStrategy diagnosticsStrategy)
    {
        DocumentationDictionaryProvider outputProvider = new(diagnosticsStrategy);

        var documentationAndDiagnostics = Construct(outputProvider, additionalTextProvider, configurationProvider);

        context.ReportDiagnostics(documentationAndDiagnostics);
        return documentationAndDiagnostics.ExtractResult();
    }

    private IDiagnosticsStrategy DiagnosticsStrategy { get; }

    private DocumentationDictionaryProvider(IDiagnosticsStrategy diagnosticsStrategy)
    {
        DiagnosticsStrategy = diagnosticsStrategy;
    }

    private IResultWithDiagnostics<DocumentationDictionary> ConstructDocumentationDictionary((ImmutableArray<AdditionalText> AdditionalTexts,
        GlobalAnalyzerConfig Configuration) data, CancellationToken _)
    {
        return DocumentationFileBuilder.Build(data.AdditionalTexts, DiagnosticsStrategy, data.Configuration);
    }

    private static bool FileHasCorrectExtension(AdditionalText file)
    {
        return file.Path.EndsWith(DocumentationExtension, StringComparison.Ordinal);
    }

    private static IncrementalValueProvider<IResultWithDiagnostics<DocumentationDictionary>> Construct(DocumentationDictionaryProvider documentationProvider,
        IncrementalValuesProvider<AdditionalText> additionalTextProvider)
    {
        return additionalTextProvider.Where(FileHasCorrectExtension).Collect().Select(addDefaultConfiguration)
            .Select(documentationProvider.ConstructDocumentationDictionary);

        static (ImmutableArray<AdditionalText>, GlobalAnalyzerConfig) addDefaultConfiguration(ImmutableArray<AdditionalText> input, CancellationToken _)
            => (input, GlobalAnalyzerConfig.Default);
    }

    private static IncrementalValueProvider<IResultWithDiagnostics<DocumentationDictionary>> Construct(DocumentationDictionaryProvider documentationProvider,
        IncrementalValuesProvider<AdditionalText> additionalTextProvider, IncrementalValueProvider<GlobalAnalyzerConfig> configurationProvider)
    {
        return additionalTextProvider.Where(FileHasCorrectExtension).Collect().Combine(configurationProvider)
            .Select(documentationProvider.ConstructDocumentationDictionary);
    }
}
