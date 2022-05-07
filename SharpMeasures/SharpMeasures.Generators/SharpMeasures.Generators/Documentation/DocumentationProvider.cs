namespace SharpMeasures.Generators.Documentation;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;

using SharpMeasures.Generators.Diagnostics.Documentation;
using SharpMeasures.Generators.Utility;

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading;

internal static class DocumentationProvider
{
    public readonly record struct InputData(BaseTypeDeclarationSyntax Declaration, GenerateDocumentationState GenerateDocumentation);
    public delegate InputData DInputTransform<TIn>(TIn input);
    public delegate TOut DOutputTransform<TIn, TOut>(TIn input, DocumentationFile documentationFile);

    public static IncrementalValueProvider<IReadOnlyDictionary<string, DocumentationFile>> Attach(IncrementalGeneratorInitializationContext context)
    {
        var documentationWithDiagnostics
            = context.AdditionalTextsProvider.Where(FileHasCorrectExtension).Collect().Select(ConstructDocumentationFiles);

        context.ReportDiagnostics(documentationWithDiagnostics);
        return documentationWithDiagnostics.ExtractResult();
    }

    public static IncrementalValuesProvider<TOut> Attach<TIn, TOut>(IncrementalGeneratorInitializationContext context,
        IncrementalValuesProvider<TIn> inputProvider, DInputTransform<TIn> inputTransform, DOutputTransform<TIn, TOut> outputTransform)
    {
        Provider<TIn, TOut> provider = new(inputTransform, outputTransform);
        return provider.Attach(context, inputProvider);
    }

    private static bool FileHasCorrectExtension(AdditionalText file)
    {
        return file.Path.EndsWith(".doc.txt", StringComparison.Ordinal);
    }

    private static ResultWithDiagnostics<IReadOnlyDictionary<string, DocumentationFile>> ConstructDocumentationFiles(
        ImmutableArray<AdditionalText> additionalTexts, CancellationToken _)
    {
        return DocumentationFileBuilder.Build(additionalTexts);
    }

    private class Provider<TIn, TOut>
    {
        private DInputTransform<TIn> InputTransform { get; }
        private DOutputTransform<TIn, TOut> OutputTransform { get; }

        public Provider(DInputTransform<TIn> inputTransform, DOutputTransform<TIn, TOut> outputTransform)
        {
            InputTransform = inputTransform;
            OutputTransform = outputTransform;
        }

        public IncrementalValuesProvider<TOut> Attach(IncrementalGeneratorInitializationContext context,
            IncrementalValuesProvider<TIn> inputProvider)
        {
            IncrementalValueProvider<bool> defaultStateProvider = DefaultGenerateDocumentationState(context);

            var documentation = DocumentationProvider.Attach(context);

            var documentationAndDefaultState = defaultStateProvider.Combine(documentation);

            var resultWithDiagnostics = inputProvider.Combine(documentationAndDefaultState).Select(ExtractCorrectFileAndProduceDiagnostics);

            context.ReportDiagnostics(resultWithDiagnostics);
            return resultWithDiagnostics.ExtractResult();
        }

        private ResultWithDiagnostics<TOut> ExtractCorrectFileAndProduceDiagnostics((TIn Input, (bool Default,
            IReadOnlyDictionary<string, DocumentationFile> Documentation) State) data, CancellationToken _)
        {
            InputData inputData = InputTransform(data.Input);

            if (inputData.GenerateDocumentation is GenerateDocumentationState.ExplicitlyDisabled
                || inputData.GenerateDocumentation is GenerateDocumentationState.Default && data.State.Default is false)
            {
                return ResultWithDiagnostics<TOut>.WithoutDiagnostics(OutputTransform(data.Input, DocumentationFile.Empty));
            }

            if (data.State.Documentation.TryGetValue(inputData.Declaration.Identifier.Text, out DocumentationFile file))
            {
                return ResultWithDiagnostics<TOut>.WithoutDiagnostics(OutputTransform(data.Input, file));
            }
            else
            {
                Diagnostic diagnostics = NoMatchingDocumentationFileDiagnostics.Create(inputData.Declaration);

                return new ResultWithDiagnostics<TOut>(OutputTransform(data.Input, DocumentationFile.Empty), diagnostics);
            }
        }

        private static IncrementalValueProvider<bool> DefaultGenerateDocumentationState(IncrementalGeneratorInitializationContext context)
        {
            return context.AnalyzerConfigOptionsProvider.Select(ReadDefaultGenerateDocumentationState);
        }

        private static bool ReadDefaultGenerateDocumentationState(AnalyzerConfigOptionsProvider options, CancellationToken _)
        {
            if (options.GlobalOptions.TryGetValue("SharpMeasures_GenerateDocumentation", out string? generateDocumentation))
            {
                return generateDocumentation.ToUpperInvariant() is "TRUE";
            }

            return true;
        }
    }
}
