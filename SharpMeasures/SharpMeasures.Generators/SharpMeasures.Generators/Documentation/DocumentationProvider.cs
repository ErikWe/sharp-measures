namespace SharpMeasures.Generators.Documentation;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;

using SharpMeasures.Generators.Diagnostics.Documentation;
using SharpMeasures.Generators.Providers;
using SharpMeasures.Generators.Utility;

using System;
using System.Collections.Immutable;
using System.Threading;

internal static class DocumentationProvider
{
    private const string AnalyzerKey = "SharpMeasures_GenerateDocumentation";
    private const string DocumentationExtension = ".doc.txt";

    public readonly record struct InputData<TIdentifier>(TIdentifier Identifier, GenerateDocumentationState GenerateDocumentation);
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
            IncrementalValueProvider<AnalyzerConfigOptionsProvider> optionsProvider, IncrementalValuesProvider<AdditionalText> additionalTextProvider);
    }

    public interface IDiagnosableProvider<TIn, TOut, TIdentifier>
    {
        public abstract IncrementalValuesProvider<TOut> AttachAndReport(IncrementalGeneratorInitializationContext context, IncrementalValuesProvider<TIn> inputProvider,
            IncrementalValueProvider<AnalyzerConfigOptionsProvider> optionsProvider, IncrementalValuesProvider<AdditionalText> additionalTextProvider);

        public abstract IncrementalValuesProvider<TOut> AttachWithoutDiagnostics(IncrementalValuesProvider<TIn> inputProvider,
            IncrementalValueProvider<AnalyzerConfigOptionsProvider> optionsProvider, IncrementalValuesProvider<AdditionalText> additionalTextProvider);
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
            IncrementalValueProvider<AnalyzerConfigOptionsProvider> optionsProvider, IncrementalValuesProvider<AdditionalText> additionalTextProvider)
        {
            var defaultSettings = DefaultSettingsProvider.Attach(optionsProvider);
            var documentation = diagnosticsStrategy.AttachDocumentationProvider(additionalTextProvider);

            var settingsAndDocumentation = defaultSettings.Combine(documentation);

            var resultAndDiagnostics = inputProvider.Combine(settingsAndDocumentation).Select(extractCorrectFile);

            diagnosticsStrategy.ReportDiagnostics(resultAndDiagnostics);
            return resultAndDiagnostics.ExtractResult();

            ResultWithDiagnostics<TOut> extractCorrectFile((TIn Input, (DefaultSettingsProvider.Result DefaultSettings, DocumentationDictionary Documentation) State) data,
                CancellationToken _) => ExtractCorrectFile(diagnosticsStrategy, data.Input, data.State.DefaultSettings, data.State.Documentation);
        }

        protected interface IDiagnosticsStrategy
        {
            public abstract IncrementalValueProvider<DocumentationDictionary> AttachDocumentationProvider(IncrementalValuesProvider<AdditionalText> additionalTextProvider);
            public abstract Diagnostic? CreateNoMatchingDocumentationFileDiagnostics(TIdentifier identifier);
            public abstract void ReportDiagnostics(IncrementalValuesProvider<ResultWithDiagnostics<TOut>> diagnostics);
        }

        private ResultWithDiagnostics<TOut> ExtractCorrectFile(IDiagnosticsStrategy diagnosticsStrategy, TIn input,
            DefaultSettingsProvider.Result defaultSettings, DocumentationDictionary documentation)
        {
            InputData<TIdentifier> inputData = InputTransform(input);

            if (inputData.GenerateDocumentation is GenerateDocumentationState.ExplicitlyDisabled
                || inputData.GenerateDocumentation is GenerateDocumentationState.Default && defaultSettings.GenerateDocumentationByDefault is false)
            {
                return ResultWithDiagnostics<TOut>.WithoutDiagnostics(OutputTransform(input, DocumentationFile.Empty));
            }

            if (documentation.TryGetValue(IdentifierNameDelegate(inputData.Identifier), out DocumentationFile file))
            {
                return ResultWithDiagnostics<TOut>.WithoutDiagnostics(OutputTransform(input, file));
            }

            if (diagnosticsStrategy.CreateNoMatchingDocumentationFileDiagnostics(inputData.Identifier) is not Diagnostic diagnostics)
            {
                return ResultWithDiagnostics<TOut>.WithoutDiagnostics(OutputTransform(input, DocumentationFile.Empty));
            }

            return new ResultWithDiagnostics<TOut>(OutputTransform(input, DocumentationFile.Empty), diagnostics);
        }
    }

    private class UndiagnosableProvider<TIn, TOut, TIdentifier> : AProvider<TIn, TOut, TIdentifier>, IUndiagnosableProvider<TIn, TOut, TIdentifier>
    {
        public UndiagnosableProvider(DInputTransform<TIn, TIdentifier> inputTransform, DOutputTransform<TIn, TOut> outputTransform,
            DIdentifierName<TIdentifier> identifierNameDelegate)
            : base(inputTransform, outputTransform, identifierNameDelegate) { }

        public IncrementalValuesProvider<TOut> Attach(IncrementalValuesProvider<TIn> inputProvider, IncrementalValueProvider<AnalyzerConfigOptionsProvider> optionsProvider,
            IncrementalValuesProvider<AdditionalText> additionalTextProvider)
        {
            IDiagnosticsStrategy diagnosticsStrategy = new NoDiagnosticsStrategy();

            return Attach(diagnosticsStrategy, inputProvider, optionsProvider, additionalTextProvider);
        }

        private class NoDiagnosticsStrategy : IDiagnosticsStrategy
        {
            public IncrementalValueProvider<DocumentationDictionary> AttachDocumentationProvider(IncrementalValuesProvider<AdditionalText> additionalTextProvider)
            {
                return DocumentationDictionaryProvider.AttachWithoutDiagnostics(additionalTextProvider);
            }

            public Diagnostic? CreateNoMatchingDocumentationFileDiagnostics(TIdentifier identifier) => null;
            public void ReportDiagnostics(IncrementalValuesProvider<ResultWithDiagnostics<TOut>> diagnostics) { }
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
            IncrementalValueProvider<AnalyzerConfigOptionsProvider> optionsProvider, IncrementalValuesProvider<AdditionalText> additionalTextProvider)
        {
            return Attach(inputProvider, optionsProvider, additionalTextProvider);
        }

        public IncrementalValuesProvider<TOut> AttachAndReport(IncrementalGeneratorInitializationContext context, IncrementalValuesProvider<TIn> inputProvider,
            IncrementalValueProvider<AnalyzerConfigOptionsProvider> optionsProvider, IncrementalValuesProvider<AdditionalText> additionalTextProvider)
        {
            IDiagnosticsStrategy diagnosticsStrategy = new DiagnosticsStrategy(context, IdentifierDeclarationDelegate);

            return Attach(diagnosticsStrategy, inputProvider, optionsProvider, additionalTextProvider);
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
                return NoMatchingDocumentationFileDiagnostics.Create(IdentifierDeclarationDelegate(identifier));
            }

            public IncrementalValueProvider<DocumentationDictionary> AttachDocumentationProvider(
                IncrementalValuesProvider<AdditionalText> additionalTextProvider)
            {
                return DocumentationDictionaryProvider.AttachAndReport(Context, additionalTextProvider);
            }

            public void ReportDiagnostics(IncrementalValuesProvider<ResultWithDiagnostics<TOut>> diagnostics)
            {
                Context.ReportDiagnostics(diagnostics);
            }
        }

        private static DIdentifierName<TIdentifier> DeclarationNameDelegate(DIdentifierDeclaration<TIdentifier> identifierDeclarationDelegate)
        {
            return (identifier) => DeclarationIdentifier<BaseTypeDeclarationSyntax>().Invoke(identifierDeclarationDelegate(identifier));
        }
    }

    private sealed class DocumentationDictionaryProvider
    {
        public static IncrementalValueProvider<DocumentationDictionary> AttachWithoutDiagnostics(IncrementalValuesProvider<AdditionalText> additionalTextProvider)
        {
            DocumentationDictionaryProvider outputProvider = new(false);

            return Construct(outputProvider, additionalTextProvider).ExtractResult();
        }

        public static IncrementalValueProvider<DocumentationDictionary> AttachAndReport(IncrementalGeneratorInitializationContext context,
            IncrementalValuesProvider<AdditionalText> additionalTextProvider)
        {
            DocumentationDictionaryProvider outputProvider = new(true);

            var documentationAndDiagnostics = Construct(outputProvider, additionalTextProvider);

            context.ReportDiagnostics(documentationAndDiagnostics);
            return documentationAndDiagnostics.ExtractResult();
        }

        private bool ProduceDiagnostics { get; }

        private DocumentationDictionaryProvider(bool produceDiagnostics)
        {
            ProduceDiagnostics = produceDiagnostics;
        }

        private ResultWithDiagnostics<DocumentationDictionary> ConstructDocumentationDictionary(ImmutableArray<AdditionalText> additionalTexts,
            CancellationToken _)
        {
            return DocumentationFileBuilder.Build(additionalTexts, ProduceDiagnostics);
        }

        private static bool FileHasCorrectExtension(AdditionalText file)
        {
            return file.Path.EndsWith(DocumentationExtension, StringComparison.Ordinal);
        }

        private static IncrementalValueProvider<ResultWithDiagnostics<DocumentationDictionary>> Construct(DocumentationDictionaryProvider documentationProvider,
            IncrementalValuesProvider<AdditionalText> additionalTextProvider)
        {
            return additionalTextProvider.Where(FileHasCorrectExtension).Collect().Select(documentationProvider.ConstructDocumentationDictionary);
        }
    }

    private static class DefaultSettingsProvider
    {
        public readonly record struct Result(bool GenerateDocumentationByDefault);

        public static IncrementalValueProvider<Result> Attach(IncrementalValueProvider<AnalyzerConfigOptionsProvider> optionsProvider)
        {
            return AnalyzerConfigKeyValueProvider.Construct(ConstructResult, AnalyzerKey).Attach(optionsProvider);
        }

        private static Result ConstructResult(string? value)
        {
            return new(AnalyzerConfigKeyValueProvider.BooleanTransforms.FalseByDefault(value));
        }
    }
}
