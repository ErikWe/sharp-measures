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

    public static IUndiagnosableProvider<TIn, TOut, TDeclaration> Construct<TIn, TOut, TDeclaration>(DInputTransform<TIn, TDeclaration> inputTransform,
        DOutputTransform<TIn, TOut> outputTransform)
        where TDeclaration : BaseTypeDeclarationSyntax
    {
        return new UndiagnosableProvider<TIn, TOut, TDeclaration>(inputTransform, outputTransform, DeclarationIdentifier<TDeclaration>());
    }

    public static IUndiagnosableProvider<TIn, TOut, string> Construct<TIn, TOut>(DInputTransform<TIn, string> inputTransform,
        DOutputTransform<TIn, TOut> outputTransform)
    {
        return new UndiagnosableProvider<TIn, TOut, string>(inputTransform, outputTransform, StringIdentifier);
    }

    public static IDiagnosableProvider<TIn, TOut, TIdentifier> Construct<TIn, TOut, TIdentifier>(IncrementalGeneratorInitializationContext context,
        DInputTransform<TIn, TIdentifier> inputTransform, DOutputTransform<TIn, TOut> outputTransform, DIdentifierName<TIdentifier> identifierName,
        DIdentifierDeclaration<TIdentifier> identifierDeclaration)
    {
        return new DiagnosableProvider<TIn, TOut, TIdentifier>(context, inputTransform, outputTransform, identifierName, identifierDeclaration);
    }

    public static IDiagnosableProvider<TIn, TOut, TDeclaration> Construct<TIn, TOut, TDeclaration>(IncrementalGeneratorInitializationContext context,
        DInputTransform<TIn, TDeclaration> inputTransform, DOutputTransform<TIn, TOut> outputTransform)
        where TDeclaration : BaseTypeDeclarationSyntax
    {
        return new DiagnosableProvider<TIn, TOut, TDeclaration>(context, inputTransform, outputTransform,
            DeclarationIdentifier<TDeclaration>(), DeclarationDelegate<TDeclaration>());
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
        public abstract IncrementalValuesProvider<TOut> AttachAndReport(IncrementalValuesProvider<TIn> inputProvider,
            IncrementalValueProvider<AnalyzerConfigOptionsProvider> optionsProvider, IncrementalValuesProvider<AdditionalText> additionalTextProvider);

        public abstract IncrementalValuesProvider<TOut> AttachWithoutDiagnostics(IncrementalValuesProvider<TIn> inputProvider,
            IncrementalValueProvider<AnalyzerConfigOptionsProvider> optionsProvider, IncrementalValuesProvider<AdditionalText> additionalTextProvider);
    }

    protected abstract class AProvider<TIn, TOut, TIdentifier>
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

        public IncrementalValuesProvider<TOut> Attach(IncrementalValuesProvider<TIn> inputProvider,
            IncrementalValueProvider<AnalyzerConfigOptionsProvider> optionsProvider, IncrementalValuesProvider<AdditionalText> additionalTextProvider)
        {
            var defaultSettings = DefaultSettingsProvider.Attach(optionsProvider);
            var documentation = AttachDocumentationProvider(additionalTextProvider);

            var settingsAndDocumentation = defaultSettings.Combine(documentation);

            var resultAndDiagnostics = inputProvider.Combine(settingsAndDocumentation).Select(ExtractCorrectFile);

            ReportDiagnostics(resultAndDiagnostics);
            return resultAndDiagnostics.ExtractResult();
        }

        protected abstract Diagnostic? CreateNoMatchingDocumentationFileDiagnostics(TIdentifier identifier);
        protected abstract IncrementalValueProvider<DocumentationDictionary> AttachDocumentationProvider(
            IncrementalValuesProvider<AdditionalText> additionalTextProvider);
        protected abstract void ReportDiagnostics(IncrementalValuesProvider<ResultWithDiagnostics<TOut>> diagnostics);

        private ResultWithDiagnostics<TOut> ExtractCorrectFile((TIn Input,
            (DefaultSettingsProvider.Result DefaultSettings, DocumentationDictionary Documentation) State) data, CancellationToken _)
        {
            InputData<TIdentifier> inputData = InputTransform(data.Input);

            if (inputData.GenerateDocumentation is GenerateDocumentationState.ExplicitlyDisabled
                || inputData.GenerateDocumentation is GenerateDocumentationState.Default && data.State.DefaultSettings.GenerateDocumentationByDefault is false)
            {
                return ResultWithDiagnostics<TOut>.WithoutDiagnostics(OutputTransform(data.Input, DocumentationFile.Empty));
            }

            if (data.State.Documentation.TryGetValue(IdentifierNameDelegate(inputData.Identifier), out DocumentationFile file))
            {
                return ResultWithDiagnostics<TOut>.WithoutDiagnostics(OutputTransform(data.Input, file));
            }

            if (CreateNoMatchingDocumentationFileDiagnostics(inputData.Identifier) is not Diagnostic diagnostics)
            {
                return ResultWithDiagnostics<TOut>.WithoutDiagnostics(OutputTransform(data.Input, DocumentationFile.Empty));
            }

            return new ResultWithDiagnostics<TOut>(OutputTransform(data.Input, DocumentationFile.Empty), diagnostics);
        }
    }

    private class UndiagnosableProvider<TIn, TOut, TIdentifier> : AProvider<TIn, TOut, TIdentifier>, IUndiagnosableProvider<TIn, TOut, TIdentifier>
    {
        public UndiagnosableProvider(DInputTransform<TIn, TIdentifier> inputTransform, DOutputTransform<TIn, TOut> outputTransform,
            DIdentifierName<TIdentifier> identifierNameDelegate) : base(inputTransform, outputTransform, identifierNameDelegate) { }

        protected override Diagnostic? CreateNoMatchingDocumentationFileDiagnostics(TIdentifier identifier)
        {
            return null;
        }

        protected override IncrementalValueProvider<DocumentationDictionary> AttachDocumentationProvider(
            IncrementalValuesProvider<AdditionalText> additionalTextProvider)
        {
            return DocumentationDictionaryProvider.AttachWithoutDiagnostics(additionalTextProvider);
        }

        protected override void ReportDiagnostics(IncrementalValuesProvider<ResultWithDiagnostics<TOut>> diagnostics) { }
    }

    private class DiagnosableProvider<TIn, TOut, TIdentifier> : AProvider<TIn, TOut, TIdentifier>, IDiagnosableProvider<TIn, TOut, TIdentifier>
    {
        private bool ProduceDiagnostics { get; set; }

        private IncrementalGeneratorInitializationContext Context { get; }
        private DIdentifierDeclaration<TIdentifier> IdentifierDeclarationDelegate { get; }

        public DiagnosableProvider(IncrementalGeneratorInitializationContext context, DInputTransform<TIn, TIdentifier> inputTransform,
            DOutputTransform<TIn, TOut> outputTransform, DIdentifierName<TIdentifier> identifierNameDelegate,
            DIdentifierDeclaration<TIdentifier> identifierDeclarationDelegate)
            : base(inputTransform, outputTransform, identifierNameDelegate)
        {
            Context = context;
            IdentifierDeclarationDelegate = identifierDeclarationDelegate;
        }

        public IncrementalValuesProvider<TOut> AttachWithoutDiagnostics(IncrementalValuesProvider<TIn> inputProvider,
            IncrementalValueProvider<AnalyzerConfigOptionsProvider> optionsProvider, IncrementalValuesProvider<AdditionalText> additionalTextProvider)
        {
            ProduceDiagnostics = false;
            return Attach(inputProvider, optionsProvider, additionalTextProvider);
        }

        public IncrementalValuesProvider<TOut> AttachAndReport(IncrementalValuesProvider<TIn> inputProvider,
            IncrementalValueProvider<AnalyzerConfigOptionsProvider> optionsProvider, IncrementalValuesProvider<AdditionalText> additionalTextProvider)
        {
            ProduceDiagnostics = true;
            return Attach(inputProvider, optionsProvider, additionalTextProvider);
        }

        protected override Diagnostic? CreateNoMatchingDocumentationFileDiagnostics(TIdentifier identifier)
        {
            return NoMatchingDocumentationFileDiagnostics.Create(IdentifierDeclarationDelegate(identifier));
        }

        protected override IncrementalValueProvider<DocumentationDictionary> AttachDocumentationProvider(
            IncrementalValuesProvider<AdditionalText> additionalTextProvider)
        {
            return DocumentationDictionaryProvider.AttachAndReport(Context, additionalTextProvider);
        }

        protected override void ReportDiagnostics(IncrementalValuesProvider<ResultWithDiagnostics<TOut>> diagnostics)
        {
            if (ProduceDiagnostics)
            {
                Context.ReportDiagnostics(diagnostics);
            }
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
