namespace SharpMeasures.Generators.Providers;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpMeasures.Generators.Diagnostics;

using System;
using System.Threading;

internal static class PartialDeclarationProvider
{
    public delegate TDeclarationSyntax DInputTransform<TIn, TDeclarationSyntax>(TIn input) where TDeclarationSyntax : BaseTypeDeclarationSyntax;

    public static IncrementalValuesProvider<TIn> Attach<TIn, TDeclarationSyntax>(IncrementalValuesProvider<TIn> inputProvider,
        DInputTransform<TIn, TDeclarationSyntax> inputTransform)
        where TDeclarationSyntax : BaseTypeDeclarationSyntax
    {
        Provider<TIn, TDeclarationSyntax> provider = new(inputTransform);

        return provider.Attach(inputProvider);
    }

    public static IncrementalValuesProvider<TDeclarationSyntax> Attach<TDeclarationSyntax>(IncrementalValuesProvider<TDeclarationSyntax> inputProvider)
        where TDeclarationSyntax : BaseTypeDeclarationSyntax
    {
        Provider<TDeclarationSyntax, TDeclarationSyntax> provider = new(static (x) => x);

        return provider.Attach(inputProvider);
    }

    public static IncrementalValuesProvider<TIn> AttachAndReport<TIn, TDeclarationSyntax, TAttribute>(IncrementalGeneratorInitializationContext context,
        IncrementalValuesProvider<TIn> Provider, DInputTransform<TIn, TDeclarationSyntax> inputTransform)
        where TDeclarationSyntax : BaseTypeDeclarationSyntax
    {
        return AttachAndReport(context, Provider, inputTransform, typeof(TAttribute));
    }

    public static IncrementalValuesProvider<TIn> AttachAndReport<TIn, TDeclarationSyntax>(IncrementalGeneratorInitializationContext context,
        IncrementalValuesProvider<TIn> inputProvider, DInputTransform<TIn, TDeclarationSyntax> inputTransform, Type attributeType)
        where TDeclarationSyntax : BaseTypeDeclarationSyntax
    {
        return AttachAndReport(context, inputProvider, inputTransform, attributeType.Name);
    }

    public static IncrementalValuesProvider<TIn> AttachAndReport<TIn, TDeclarationSyntax>(IncrementalGeneratorInitializationContext context,
        IncrementalValuesProvider<TIn> inputProvider, DInputTransform<TIn, TDeclarationSyntax> inputTransform, string attributeName)
        where TDeclarationSyntax : BaseTypeDeclarationSyntax
    {
        Provider<TIn, TDeclarationSyntax> provider = new(inputTransform, attributeName);

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

    private class Provider<TIn, TDeclarationSyntax>
        where TDeclarationSyntax : BaseTypeDeclarationSyntax
    {
        private DInputTransform<TIn, TDeclarationSyntax> InputTransform { get; }

        private string AttributeName { get; } = string.Empty;

        public Provider(DInputTransform<TIn, TDeclarationSyntax> inputTransform)
        {
            InputTransform = inputTransform;
        }

        public Provider(DInputTransform<TIn, TDeclarationSyntax> inputTransform, string attributeName)
        {
            InputTransform = inputTransform;

            AttributeName = attributeName;
        }

        public IncrementalValuesProvider<TIn> Attach(IncrementalValuesProvider<TIn> inputProvider)
        {
            return inputProvider.Where(DeclarationIsPartial);
        }

        public IncrementalValuesProvider<TIn> AttachAndReport(IncrementalGeneratorInitializationContext context, IncrementalValuesProvider<TIn> inputProvider)
        {
            IncrementalValuesProvider<LabeledInput> labeledInput = inputProvider.Select(AddLabel);

            IncrementalValuesProvider<Diagnostic> diagnostics = labeledInput.Where(DeclarationIsNotPartial).Select(CreateDiagnostics);

            context.ReportDiagnostics(diagnostics);
            return labeledInput.Where(DeclarationIsPartial).Select(RemoveLabel);
        }

        private bool DeclarationIsPartial(TIn input) => InputTransform(input).HasModifierOfKind(SyntaxKind.PartialKeyword);
        private static bool DeclarationIsPartial(LabeledInput labeledInput) => labeledInput.IsPartial;

        private static bool DeclarationIsNotPartial(LabeledInput labeledInput) => !DeclarationIsPartial(labeledInput);

        private LabeledInput AddLabel(TIn input, CancellationToken _) => new(DeclarationIsPartial(input), input);
        private TIn RemoveLabel(LabeledInput labeledInput, CancellationToken _) => labeledInput.Input;

        private Diagnostic CreateDiagnostics(TIn input, CancellationToken _)
                => TypeNotPartialDiagnostics.Create(InputTransform(input), AttributeName);
        private Diagnostic CreateDiagnostics(LabeledInput labeledInput, CancellationToken token)
                => CreateDiagnostics(labeledInput.Input, token);

        private readonly record struct LabeledInput(bool IsPartial, TIn Input);
    }
}
