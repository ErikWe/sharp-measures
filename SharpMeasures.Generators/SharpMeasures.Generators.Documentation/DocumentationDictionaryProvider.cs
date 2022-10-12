namespace SharpMeasures.Generators.Documentation;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Configuration;
using SharpMeasures.Generators.Diagnostics;

using System;
using System.Collections.Immutable;
using System.Threading;

public sealed class DocumentationDictionaryProvider
{
    public static IncrementalValueProvider<DocumentationDictionary> AttachWithoutDiagnostics(IncrementalValuesProvider<AdditionalText> additionalTextProvider)
    {
        DocumentationDictionaryProvider outputProvider = new(new EmptyDiagnosticsStrategy());

        return Construct(outputProvider, additionalTextProvider).ExtractResult();
    }

    public static IncrementalValueProvider<DocumentationDictionary> AttachAndReport(IncrementalGeneratorInitializationContext context, IncrementalValuesProvider<AdditionalText> additionalTextProvider, IncrementalValueProvider<GlobalAnalyzerConfig> configurationProvider, IDiagnosticsStrategy diagnosticsStrategy)
    {
        DocumentationDictionaryProvider outputProvider = new(diagnosticsStrategy);

        return Construct(outputProvider, additionalTextProvider, configurationProvider).ReportDiagnostics(context);
    }

    private IDiagnosticsStrategy DiagnosticsStrategy { get; }

    private DocumentationDictionaryProvider(IDiagnosticsStrategy diagnosticsStrategy)
    {
        DiagnosticsStrategy = diagnosticsStrategy;
    }

    private IResultWithDiagnostics<DocumentationDictionary> ConstructDocumentationDictionary((ImmutableArray<Optional<AdditionalText>> AdditionalTexts, GlobalAnalyzerConfig Configuration) data, CancellationToken token)
    {
        if (token.IsCancellationRequested)
        {
            return ResultWithDiagnostics.Construct(DocumentationDictionary.Empty);
        }

        return DocumentationFileBuilder.Build(data.AdditionalTexts, DiagnosticsStrategy, data.Configuration);
    }

    private IResultWithDiagnostics<DocumentationDictionary> ConstructDocumentationDictionary(ImmutableArray<Optional<AdditionalText>> additionalTexts, CancellationToken token) => ConstructDocumentationDictionary((additionalTexts, GlobalAnalyzerConfig.Default), token);

    private static Optional<AdditionalText> EnsureFileHasCorrectExtension((AdditionalText File, GlobalAnalyzerConfig Configuration) data, CancellationToken token)
    {
        if (token.IsCancellationRequested)
        {
            return new Optional<AdditionalText>();
        }

        if (data.File.Path.EndsWith(data.Configuration.DocumentationFileExtension, StringComparison.OrdinalIgnoreCase))
        {
            return data.File;
        }

        return new Optional<AdditionalText>();
    }

    private static Optional<AdditionalText> EnsureFileHasCorrectExtension(AdditionalText file, CancellationToken token) => EnsureFileHasCorrectExtension((file, GlobalAnalyzerConfig.Default), token);

    private static IncrementalValueProvider<IResultWithDiagnostics<DocumentationDictionary>> Construct(DocumentationDictionaryProvider documentationProvider, IncrementalValuesProvider<AdditionalText> additionalTextProvider)
    {
        return additionalTextProvider.Select(EnsureFileHasCorrectExtension).Collect().Select(documentationProvider.ConstructDocumentationDictionary);
    }

    private static IncrementalValueProvider<IResultWithDiagnostics<DocumentationDictionary>> Construct(DocumentationDictionaryProvider documentationProvider, IncrementalValuesProvider<AdditionalText> additionalTextProvider, IncrementalValueProvider<GlobalAnalyzerConfig> configurationProvider)
    {
        return additionalTextProvider.Combine(configurationProvider).Select(EnsureFileHasCorrectExtension).Collect().Combine(configurationProvider).Select(documentationProvider.ConstructDocumentationDictionary);
    }
}
