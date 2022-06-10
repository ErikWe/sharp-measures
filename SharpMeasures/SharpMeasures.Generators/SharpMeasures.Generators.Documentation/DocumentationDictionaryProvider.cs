namespace SharpMeasures.Generators.Documentation;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;

using System;
using System.Collections.Immutable;
using System.Threading;

internal class DocumentationDictionaryProvider
{
    private const string DocumentationExtension = ".doc.txt";

    public static IncrementalValueProvider<DocumentationDictionary> AttachWithoutDiagnostics(IncrementalValuesProvider<AdditionalText> additionalTextProvider)
    {
        DocumentationDictionaryProvider outputProvider = new(new EmptyDiagnosticsStrategy());

        return Construct(outputProvider, additionalTextProvider).ExtractResult();
    }

    public static IncrementalValueProvider<DocumentationDictionary> AttachAndReport(IncrementalGeneratorInitializationContext context,
        IncrementalValuesProvider<AdditionalText> additionalTextProvider, IDiagnosticsStrategy diagnosticsStrategy)
    {
        DocumentationDictionaryProvider outputProvider = new(diagnosticsStrategy);

        var documentationAndDiagnostics = Construct(outputProvider, additionalTextProvider);

        context.ReportDiagnostics(documentationAndDiagnostics);
        return documentationAndDiagnostics.ExtractResult();
    }

    private IDiagnosticsStrategy DiagnosticsStrategy { get; }

    private DocumentationDictionaryProvider(IDiagnosticsStrategy diagnosticsStrategy)
    {
        DiagnosticsStrategy = diagnosticsStrategy;
    }

    private IResultWithDiagnostics<DocumentationDictionary> ConstructDocumentationDictionary(ImmutableArray<AdditionalText> additionalTexts,
        CancellationToken _)
    {
        return DocumentationFileBuilder.Build(additionalTexts, DiagnosticsStrategy);
    }

    private static bool FileHasCorrectExtension(AdditionalText file)
    {
        return file.Path.EndsWith(DocumentationExtension, StringComparison.Ordinal);
    }

    private static IncrementalValueProvider<IResultWithDiagnostics<DocumentationDictionary>> Construct(DocumentationDictionaryProvider documentationProvider,
        IncrementalValuesProvider<AdditionalText> additionalTextProvider)
    {
        return additionalTextProvider.Where(FileHasCorrectExtension).Collect().Select(documentationProvider.ConstructDocumentationDictionary);
    }
}
