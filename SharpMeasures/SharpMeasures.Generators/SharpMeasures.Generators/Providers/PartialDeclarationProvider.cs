namespace SharpMeasures.Generators.Providers;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpMeasures.Generators.Diagnostics;

using System;
using System.Threading;

internal static class PartialDeclarationProvider
{
    public delegate TDeclarationSyntax DInputTransform<TData, TDeclarationSyntax>(TData input) where TDeclarationSyntax : BaseTypeDeclarationSyntax;

    public static IncrementalValuesProvider<TData> Attach<TData, TDeclarationSyntax>(IncrementalValuesProvider<TData> inputProvider,
        DInputTransform<TData, TDeclarationSyntax> inputTransform)
        where TDeclarationSyntax : BaseTypeDeclarationSyntax
    {
        Provider<TData, TDeclarationSyntax> provider = new(inputTransform);

        return provider.Attach(inputProvider);
    }

    public static IncrementalValuesProvider<TDeclarationSyntax> Attach<TDeclarationSyntax>(IncrementalValuesProvider<TDeclarationSyntax> inputProvider)
        where TDeclarationSyntax : BaseTypeDeclarationSyntax
    {
        Provider<TDeclarationSyntax, TDeclarationSyntax> provider = new(static (x) => x);

        return provider.Attach(inputProvider);
    }

    public static IncrementalValuesProvider<TData> AttachAndReport<TData, TDeclarationSyntax, TAttribute>(IncrementalGeneratorInitializationContext context,
        IncrementalValuesProvider<TData> inputProvider, DInputTransform<TData, TDeclarationSyntax> inputTransform)
        where TDeclarationSyntax : BaseTypeDeclarationSyntax
    {
        return AttachAndReport(context, inputProvider, inputTransform, typeof(TAttribute));
    }

    public static IncrementalValuesProvider<TData> AttachAndReport<TData, TDeclarationSyntax>(IncrementalGeneratorInitializationContext context,
        IncrementalValuesProvider<TData> inputProvider, DInputTransform<TData, TDeclarationSyntax> inputTransform, Type attributeType)
        where TDeclarationSyntax : BaseTypeDeclarationSyntax
    {
        return AttachAndReport(context, inputProvider, inputTransform, attributeType.Name);
    }

    public static IncrementalValuesProvider<TData> AttachAndReport<TData, TDeclarationSyntax>(IncrementalGeneratorInitializationContext context,
        IncrementalValuesProvider<TData> inputProvider, DInputTransform<TData, TDeclarationSyntax> inputTransform, string attributeName)
        where TDeclarationSyntax : BaseTypeDeclarationSyntax
    {
        Provider<TData, TDeclarationSyntax> provider = new(inputTransform, attributeName);

        return provider.AttachAndReport(context, inputProvider);
    }

    public static IncrementalValuesProvider<TData> AttachAndReport<TData, TDeclarationSyntax>(IncrementalGeneratorInitializationContext context,
        IncrementalValuesProvider<TData> inputProvider, DInputTransform<TData, TDeclarationSyntax> inputTransform, Func<TData, Type> attributeTypeDelegate)
        where TDeclarationSyntax : BaseTypeDeclarationSyntax
    {
        return AttachAndReport(context, inputProvider, inputTransform, attributeNameDelegate);

        string attributeNameDelegate(TData input)
        {
            return attributeTypeDelegate(input).FullName;
        }
    }

    public static IncrementalValuesProvider<TData> AttachAndReport<TData, TDeclarationSyntax>(IncrementalGeneratorInitializationContext context,
        IncrementalValuesProvider<TData> inputProvider, DInputTransform<TData, TDeclarationSyntax> inputTransform, Func<TData, string> attributeNameDelegate)
        where TDeclarationSyntax : BaseTypeDeclarationSyntax
    {
        DelegatedProvider<TData, TDeclarationSyntax> provider = new(inputTransform, attributeNameDelegate);

        return provider.AttachAndReport(context, inputProvider);
    }

    public static IncrementalValuesProvider<TDeclarationSyntax> AttachAndReport<TDeclarationSyntax, TAttribute>(IncrementalGeneratorInitializationContext context,
        IncrementalValuesProvider<TDeclarationSyntax> inputProvider)
        where TDeclarationSyntax : BaseTypeDeclarationSyntax
    {
        return AttachAndReport(context, inputProvider, typeof(TAttribute));
    }

    public static IncrementalValuesProvider<TDeclarationSyntax> AttachAndReport<TDeclarationSyntax>(IncrementalGeneratorInitializationContext context,
        IncrementalValuesProvider<TDeclarationSyntax> inputProvider, Type attributeType)
        where TDeclarationSyntax : BaseTypeDeclarationSyntax
    {
        return AttachAndReport(context, inputProvider, attributeType.Name);
    }

    public static IncrementalValuesProvider<TDeclarationSyntax> AttachAndReport<TDeclarationSyntax>(IncrementalGeneratorInitializationContext context,
        IncrementalValuesProvider<TDeclarationSyntax> inputProvider, string attributeName)
        where TDeclarationSyntax : BaseTypeDeclarationSyntax
    {
        Provider<TDeclarationSyntax, TDeclarationSyntax> provider = new(static (x) => x, attributeName);

        return provider.AttachAndReport(context, inputProvider);
    }

    private abstract class AProvider<TData, TDeclarationSyntax>
        where TDeclarationSyntax : BaseTypeDeclarationSyntax
    {
        private DInputTransform<TData, TDeclarationSyntax> InputTransform { get; }

        public AProvider(DInputTransform<TData, TDeclarationSyntax> inputTransform)
        {
            InputTransform = inputTransform;
        }

        public IncrementalValuesProvider<TData> Attach(IncrementalValuesProvider<TData> inputProvider)
        {
            return inputProvider.Where(DeclarationIsPartial);
        }

        public IncrementalValuesProvider<TData> AttachAndReport(IncrementalGeneratorInitializationContext context, IncrementalValuesProvider<TData> inputProvider)
        {
            IncrementalValuesProvider<LabeledInput> labeledInput = inputProvider.Select(AddLabel);

            IncrementalValuesProvider<Diagnostic> diagnostics = labeledInput.Where(DeclarationIsNotPartial).Select(CreateDiagnostics);

            context.ReportDiagnostics(diagnostics);
            return labeledInput.Where(DeclarationIsPartial).Select(RemoveLabel);
        }

        protected abstract string GetAttributeName(TData input);

        private bool DeclarationIsPartial(TData input) => InputTransform(input).HasModifierOfKind(SyntaxKind.PartialKeyword);
        private static bool DeclarationIsPartial(LabeledInput labeledInput) => labeledInput.IsPartial;

        private static bool DeclarationIsNotPartial(LabeledInput labeledInput) => !DeclarationIsPartial(labeledInput);

        private LabeledInput AddLabel(TData input, CancellationToken _) => new(DeclarationIsPartial(input), input);
        private TData RemoveLabel(LabeledInput labeledInput, CancellationToken _) => labeledInput.Input;

        private Diagnostic CreateDiagnostics(TData input, CancellationToken _)
                => TypeNotPartialDiagnostics.Create(InputTransform(input), GetAttributeName(input));
        private Diagnostic CreateDiagnostics(LabeledInput labeledInput, CancellationToken token)
                => CreateDiagnostics(labeledInput.Input, token);

        private readonly record struct LabeledInput(bool IsPartial, TData Input);
    }

    private class Provider<TData, TDeclarationSyntax> : AProvider<TData, TDeclarationSyntax>
        where TDeclarationSyntax : BaseTypeDeclarationSyntax
    {
        private string AttributeName { get; } = string.Empty;

        public Provider(DInputTransform<TData, TDeclarationSyntax> inputTransform) : base(inputTransform) { }

        public Provider(DInputTransform<TData, TDeclarationSyntax> inputTransform, string attributeName) : base(inputTransform)
        {
            AttributeName = attributeName;
        }

        protected override string GetAttributeName(TData input) => AttributeName;
    }

    private class DelegatedProvider<TData, TDeclarationSyntax> : AProvider<TData, TDeclarationSyntax>
        where TDeclarationSyntax : BaseTypeDeclarationSyntax
    {
        private Func<TData, string> AttributeNameDelegate { get; }

        public DelegatedProvider(DInputTransform<TData, TDeclarationSyntax> inputTransform, Func<TData, string> attributeNameDelegate) : base(inputTransform)
        {
            AttributeNameDelegate = attributeNameDelegate;
        }

        protected override string GetAttributeName(TData input) => AttributeNameDelegate(input);
    }
}
