namespace SharpMeasures.Generators.Documentation;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpMeasures.Generators.Diagnostics;

using System.Threading;

public readonly record struct ProviderData<TIdentifier>(TIdentifier Identifier, bool GenerateDocumentation);

public interface IDocumentationProvider<TIn, TOut, TIdentifier>
{
    public abstract IncrementalValuesProvider<TOut> AttachAndReport(IncrementalGeneratorInitializationContext context, IncrementalValuesProvider<TIn> inputProvider, IncrementalValueProvider<DocumentationDictionary> documentationDictionaryProvider, IDiagnosticsStrategy<TIdentifier> diagnosticsStrategy);
    public abstract IncrementalValuesProvider<TOut> AttachWithoutDiagnostics(IncrementalValuesProvider<TIn> inputProvider, IncrementalValueProvider<DocumentationDictionary> documentationDictionaryProvider);
}

public static class DocumentationProvider
{
    public delegate ProviderData<TIdentifier> DInputTransform<in TIn, TIdentifier>(TIn input);
    public delegate TOut DOutputTransform<in TIn, out TOut>(TIn input, DocumentationFile documentationFile);

    public delegate string DIdentifierName<in TIdentifier>(TIdentifier identifier);

    public static IDocumentationProvider<TIn, TOut, TIdentifier> Construct<TIn, TOut, TIdentifier>(DInputTransform<TIn, TIdentifier> inputTransform, DOutputTransform<TIn, TOut> outputTransform, DIdentifierName<TIdentifier> identifierNameDelegate)
    {
        return new Provider<TIn, TOut, TIdentifier>(inputTransform, outputTransform, identifierNameDelegate);
    }

    public static IDocumentationProvider<TIn, TOut, TDeclaration> Construct<TIn, TOut, TDeclaration>(DInputTransform<TIn, TDeclaration> inputTransform, DOutputTransform<TIn, TOut> outputTransform) where TDeclaration : BaseTypeDeclarationSyntax
    {
        return Construct(inputTransform, outputTransform);
    }

    private sealed class Provider<TIn, TOut, TIdentifier> : IDocumentationProvider<TIn, TOut, TIdentifier>
    {
        private DInputTransform<TIn, TIdentifier> InputTransform { get; }
        private DOutputTransform<TIn, TOut> OutputTransform { get; }

        private DIdentifierName<TIdentifier> IdentifierNameDelegate { get; }

        public Provider(DInputTransform<TIn, TIdentifier> inputTransform, DOutputTransform<TIn, TOut> outputTransform, DIdentifierName<TIdentifier> identifierNameDelegate)
        {
            InputTransform = inputTransform;
            OutputTransform = outputTransform;

            IdentifierNameDelegate = identifierNameDelegate;
        }

        public IncrementalValuesProvider<TOut> AttachWithoutDiagnostics(IncrementalValuesProvider<TIn> inputProvider, IncrementalValueProvider<DocumentationDictionary> documentationDictionaryProvider)
        {
            return inputProvider.Combine(documentationDictionaryProvider).Select(extractCorrectFile).ExtractResults();

            IResultWithDiagnostics<TOut> extractCorrectFile((TIn Input, DocumentationDictionary Dictionary) data, CancellationToken _)
            {
                return ExtractFile(data.Input, data.Dictionary, new EmptyDiagnosticsStrategy<TIdentifier>());
            }
        }

        public IncrementalValuesProvider<TOut> AttachAndReport(IncrementalGeneratorInitializationContext context, IncrementalValuesProvider<TIn> inputProvider, IncrementalValueProvider<DocumentationDictionary> documentationDictionaryProvider, IDiagnosticsStrategy<TIdentifier> diagnosticsStrategy)
        {
            return inputProvider.Combine(documentationDictionaryProvider).Select(extractCorrectFile).ReportDiagnostics(context);

            IResultWithDiagnostics<TOut> extractCorrectFile((TIn Input, DocumentationDictionary Dictionary) data, CancellationToken _)
            {
                return ExtractFile(data.Input, data.Dictionary, diagnosticsStrategy);
            }
        }

        private IResultWithDiagnostics<TOut> ExtractFile(TIn input, DocumentationDictionary dictionary, IDiagnosticsStrategy<TIdentifier> diagnosticsStrategy)
        {
            ProviderData<TIdentifier> inputData = InputTransform(input);

            if (dictionary.TryGetValue(IdentifierNameDelegate(inputData.Identifier), out DocumentationFile documentationFile))
            {
                return ResultWithDiagnostics.Construct(OutputTransform(input, documentationFile));
            }

            return ResultWithDiagnostics.Construct(OutputTransform(input, documentationFile), diagnosticsStrategy.NoMatchingDocumentationFile(inputData.Identifier));
        }
    }
}
