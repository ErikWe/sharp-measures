namespace SharpMeasures.Generators.Documentation;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpMeasures.Generators.Diagnostics;

using System.Threading;

internal static class DocumentationProvider
{
    public readonly record struct InputData<TIdentifier>(TIdentifier Identifier, bool GenerateDocumentation);
    public delegate InputData<TIdentifier> DInputTransform<in TIn, TIdentifier>(TIn input);
    public delegate TOut DOutputTransform<in TIn, out TOut>(TIn input, DocumentationFile documentationFile);

    public delegate string DIdentifierName<in TIdentifier>(TIdentifier identifier);
    public delegate BaseTypeDeclarationSyntax DIdentifierDeclaration<in TIdentifier>(TIdentifier identifier);

    public static IUndiagnosableProvider<TIn, TOut, TIdentifier> Construct<TIn, TOut, TIdentifier>(DInputTransform<TIn, TIdentifier> inputTransform,
        DOutputTransform<TIn, TOut> outputTransform, DIdentifierName<TIdentifier> identifierNameDelegate)
    {
        return new UndiagnosableProvider<TIn, TOut, TIdentifier>(inputTransform, outputTransform, identifierNameDelegate);
    }

    public static IUndiagnosableProvider<TIn, TOut, string> Construct<TIn, TOut>(DInputTransform<TIn, string> inputTransform,
        DOutputTransform<TIn, TOut> outputTransform)
    {
        return Construct(inputTransform, outputTransform, StringIdentifier);
    }

    public static IDiagnosableProvider<TIn, TOut, TIdentifier> Construct<TIn, TOut, TIdentifier>(DInputTransform<TIn, TIdentifier> inputTransform,
        DOutputTransform<TIn, TOut> outputTransform, DIdentifierDeclaration<TIdentifier> identifierDeclarationDelegate)
    {
        return new DiagnosableProvider<TIn, TOut, TIdentifier>(inputTransform, outputTransform, identifierDeclarationDelegate);
    }

    public static IDiagnosableProvider<TIn, TOut, TDeclaration> Construct<TIn, TOut, TDeclaration>(DInputTransform<TIn, TDeclaration> inputTransform,
        DOutputTransform<TIn, TOut> outputTransform)
        where TDeclaration : BaseTypeDeclarationSyntax
    {
        return Construct(inputTransform, outputTransform, DeclarationDelegate<TDeclaration>());
    }

    private static DIdentifierName<TDeclaration> DeclarationIdentifier<TDeclaration>() where TDeclaration : BaseTypeDeclarationSyntax
    {
        return (declaration) => declaration.Identifier.Text;
    }

    private static DIdentifierName<string> StringIdentifier { get; } = (identifier) => identifier;

    private static DIdentifierDeclaration<TDeclaration> DeclarationDelegate<TDeclaration>() where TDeclaration : BaseTypeDeclarationSyntax
    {
        return (declaration) => declaration;
    }

    public interface IUndiagnosableProvider<TIn, TOut, TIdentifier>
    {
        public abstract IncrementalValuesProvider<TOut> Attach(IncrementalValuesProvider<TIn> inputProvider,
            IncrementalValueProvider<DocumentationDictionary> documentationDictionaryProvider);
    }

    public interface IDiagnosableProvider<TIn, TOut, TIdentifier>
    {
        public abstract IncrementalValuesProvider<TOut> AttachAndReport(IncrementalGeneratorInitializationContext context, IncrementalValuesProvider<TIn> inputProvider,
            IncrementalValueProvider<DocumentationDictionary> documentationDictionaryProvider);

        public abstract IncrementalValuesProvider<TOut> AttachWithoutDiagnostics(IncrementalValuesProvider<TIn> inputProvider,
            IncrementalValueProvider<DocumentationDictionary> documentationDictionaryProvider);
    }

    private abstract class AProvider<TIn, TOut, TIdentifier>
    {
        private DInputTransform<TIn, TIdentifier> InputTransform { get; }
        private DOutputTransform<TIn, TOut> OutputTransform { get; }

        private DIdentifierName<TIdentifier> IdentifierNameDelegate { get; }

        public AProvider(DInputTransform<TIn, TIdentifier> inputTransform, DOutputTransform<TIn, TOut> outputTransform, DIdentifierName<TIdentifier> identifierNameDelegate)
        {
            InputTransform = inputTransform;
            OutputTransform = outputTransform;

            IdentifierNameDelegate = identifierNameDelegate;
        }

        protected IncrementalValuesProvider<TOut> Attach(IDiagnosticsStrategy diagnosticsStrategy, IncrementalValuesProvider<TIn> inputProvider,
            IncrementalValueProvider<DocumentationDictionary> documentationDictionaryProvider)
        {
            var resultAndDiagnostics = inputProvider.Combine(documentationDictionaryProvider).Select(extractCorrectFile);

            diagnosticsStrategy.ReportDiagnostics(resultAndDiagnostics);
            return resultAndDiagnostics.ExtractResults();

            IResultWithDiagnostics<TOut> extractCorrectFile((TIn Input, DocumentationDictionary Dictionary) data,
                CancellationToken _) => ExtractCorrectFile(diagnosticsStrategy, data.Input, data.Dictionary);
        }

        protected interface IDiagnosticsStrategy
        {
            public abstract IncrementalValueProvider<DocumentationDictionary> AttachDocumentationProvider(IncrementalValuesProvider<AdditionalText> additionalTextProvider);
            public abstract Diagnostic? CreateNoMatchingDocumentationFileDiagnostics(TIdentifier identifier);
            public abstract void ReportDiagnostics(IncrementalValuesProvider<IResultWithDiagnostics<TOut>> diagnostics);
        }

        private IResultWithDiagnostics<TOut> ExtractCorrectFile(IDiagnosticsStrategy diagnosticsStrategy, TIn input, DocumentationDictionary dictionary)
        {
            InputData<TIdentifier> inputData = InputTransform(input);

            if (dictionary.TryGetValue(IdentifierNameDelegate(inputData.Identifier), out DocumentationFile documentationFile))
            {
                return ResultWithDiagnostics.Construct(OutputTransform(input, documentationFile));
            }

            if (diagnosticsStrategy.CreateNoMatchingDocumentationFileDiagnostics(inputData.Identifier) is not Diagnostic diagnostics)
            {
                return ResultWithDiagnostics.Construct(OutputTransform(input, DocumentationFile.Empty));
            }

            return ResultWithDiagnostics.Construct(OutputTransform(input, DocumentationFile.Empty), diagnostics);
        }
    }

    private class UndiagnosableProvider<TIn, TOut, TIdentifier> : AProvider<TIn, TOut, TIdentifier>, IUndiagnosableProvider<TIn, TOut, TIdentifier>
    {
        public UndiagnosableProvider(DInputTransform<TIn, TIdentifier> inputTransform, DOutputTransform<TIn, TOut> outputTransform,
            DIdentifierName<TIdentifier> identifierNameDelegate)
            : base(inputTransform, outputTransform, identifierNameDelegate) { }

        public IncrementalValuesProvider<TOut> Attach(IncrementalValuesProvider<TIn> inputProvider,
            IncrementalValueProvider<DocumentationDictionary> documentationDictionaryProvider)
        {
            IDiagnosticsStrategy diagnosticsStrategy = new NoDiagnosticsStrategy();

            return Attach(diagnosticsStrategy, inputProvider, documentationDictionaryProvider);
        }

        private class NoDiagnosticsStrategy : IDiagnosticsStrategy
        {
            public IncrementalValueProvider<DocumentationDictionary> AttachDocumentationProvider(IncrementalValuesProvider<AdditionalText> additionalTextProvider)
            {
                return DocumentationDictionaryProvider.AttachWithoutDiagnostics(additionalTextProvider);
            }

            public Diagnostic? CreateNoMatchingDocumentationFileDiagnostics(TIdentifier identifier) => null;
            public void ReportDiagnostics(IncrementalValuesProvider<IResultWithDiagnostics<TOut>> diagnostics) { }
        }
    }

    private class DiagnosableProvider<TIn, TOut, TIdentifier> : UndiagnosableProvider<TIn, TOut, TIdentifier>, IDiagnosableProvider<TIn, TOut, TIdentifier>
    {
        private DIdentifierDeclaration<TIdentifier> IdentifierDeclarationDelegate { get; }

        public DiagnosableProvider(DInputTransform<TIn, TIdentifier> inputTransform, DOutputTransform<TIn, TOut> outputTransform,
            DIdentifierDeclaration<TIdentifier> identifierDeclarationDelegate)
            : base(inputTransform, outputTransform, DeclarationNameDelegate(identifierDeclarationDelegate))
        {
            IdentifierDeclarationDelegate = identifierDeclarationDelegate;
        }

        public IncrementalValuesProvider<TOut> AttachWithoutDiagnostics(IncrementalValuesProvider<TIn> inputProvider,
            IncrementalValueProvider<DocumentationDictionary> documentationDictionaryProvider)
        {
            return Attach(inputProvider, documentationDictionaryProvider);
        }

        public IncrementalValuesProvider<TOut> AttachAndReport(IncrementalGeneratorInitializationContext context, IncrementalValuesProvider<TIn> inputProvider,
            IncrementalValueProvider<DocumentationDictionary> documentationDictionaryProvider)
        {
            IDiagnosticsStrategy diagnosticsStrategy = new DiagnosticsStrategy(context, IdentifierDeclarationDelegate);

            return Attach(diagnosticsStrategy, inputProvider, documentationDictionaryProvider);
        }

        private class DiagnosticsStrategy : IDiagnosticsStrategy
        {
            private IncrementalGeneratorInitializationContext Context { get; }
            private DIdentifierDeclaration<TIdentifier> IdentifierDeclarationDelegate { get; }

            public DiagnosticsStrategy(IncrementalGeneratorInitializationContext context, DIdentifierDeclaration<TIdentifier> identifierDeclarationDelegate)
            {
                Context = context;
                IdentifierDeclarationDelegate = identifierDeclarationDelegate;
            }

            public Diagnostic? CreateNoMatchingDocumentationFileDiagnostics(TIdentifier identifier)
            {
                var declaration = IdentifierDeclarationDelegate(identifier);
                return DiagnosticConstruction.NoMatchingDocumentationFile(declaration.GetLocation(), declaration.Identifier.Text);
            }

            public IncrementalValueProvider<DocumentationDictionary> AttachDocumentationProvider(
                IncrementalValuesProvider<AdditionalText> additionalTextProvider)
            {
                return DocumentationDictionaryProvider.AttachAndReport(Context, additionalTextProvider);
            }

            public void ReportDiagnostics(IncrementalValuesProvider<IResultWithDiagnostics<TOut>> diagnostics)
            {
                Context.ReportDiagnostics(diagnostics);
            }
        }

        private static DIdentifierName<TIdentifier> DeclarationNameDelegate(DIdentifierDeclaration<TIdentifier> identifierDeclarationDelegate)
        {
            return (identifier) => DeclarationIdentifier<BaseTypeDeclarationSyntax>().Invoke(identifierDeclarationDelegate(identifier));
        }
    }
}
