namespace SharpMeasures.Generators.Documentation;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpMeasures.Generators.Diagnostics.Documentation;
using SharpMeasures.Generators.Utility;

using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Threading;

internal static class DocumentationProvider
{
    public delegate BaseTypeDeclarationSyntax DInputTransform<TIn>(TIn input);
    public delegate TOut DOutputTransform<TIn, TOut>(TIn input, DocumentationFile documentationFile);

    public static IncrementalValueProvider<IReadOnlyDictionary<string, DocumentationFile>> Attach(IncrementalGeneratorInitializationContext context)
    {
        var documentationWithDiagnostics
            = context.AdditionalTextsProvider.Where(IsFileInCorrectDirectoryAndCorrectExtension).Collect().Select(ConstructDocumentationFiles);

        context.ReportDiagnostics(documentationWithDiagnostics);
        return documentationWithDiagnostics.ExtractResult();
    }

    public static IncrementalValuesProvider<TOut> Attach<TIn, TOut>(IncrementalGeneratorInitializationContext context,
        IncrementalValuesProvider<TIn> inputProvider, DInputTransform<TIn> inputTransform, DOutputTransform<TIn, TOut> outputTransform)
    {
        Provider<TIn, TOut> provider = new(inputTransform, outputTransform);
        return provider.Attach(context, inputProvider);
    }

    public static IncrementalValuesProvider<TOut> Attach<TDeclarationSyntax, TOut>(IncrementalGeneratorInitializationContext context,
        IncrementalValuesProvider<TDeclarationSyntax> inputProvider, DOutputTransform<TDeclarationSyntax, TOut> outputTransform)
        where TDeclarationSyntax : BaseTypeDeclarationSyntax
    {
        return Attach(context, inputProvider, static (x) => x, outputTransform);
    }

    private static bool IsFileInCorrectDirectoryAndCorrectExtension(AdditionalText file)
    {
        return Path.GetExtension(file.Path) is ".txt"
            && Directory.GetParent(file.Path) is DirectoryInfo directory
            && directory.Parent.Name is "Documentation";
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

        public IncrementalValuesProvider<TOut> Attach(IncrementalGeneratorInitializationContext context, IncrementalValuesProvider<TIn> inputProvider)
        {
            var documentation = DocumentationProvider.Attach(context);
            var resultWithDiagnostics = inputProvider.Combine(documentation).Select(ExtractCorrectFileAndProduceDiagnostics);

            context.ReportDiagnostics(resultWithDiagnostics);
            return resultWithDiagnostics.ExtractResult();
        }

        private ResultWithDiagnostics<TOut> ExtractCorrectFileAndProduceDiagnostics((TIn input,
            IReadOnlyDictionary<string, DocumentationFile> documentation) data, CancellationToken _)
        {
            BaseTypeDeclarationSyntax declaration = InputTransform(data.input);

            if (data.documentation.TryGetValue(declaration.Identifier.Text, out DocumentationFile file))
            {
                return new ResultWithDiagnostics<TOut>(OutputTransform(data.input, file));
            }
            else
            {
                Diagnostic diagnostics = NoMatchingDocumentationFileDiagnostics.Create(declaration);

                return new ResultWithDiagnostics<TOut>(OutputTransform(data.input, DocumentationFile.Empty), diagnostics);
            }
        }
    }
}
