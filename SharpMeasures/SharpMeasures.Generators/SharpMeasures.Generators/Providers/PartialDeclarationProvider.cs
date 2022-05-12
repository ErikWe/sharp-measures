namespace SharpMeasures.Generators.Providers;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpMeasures.Generators.Diagnostics;

using System;
using System.Threading;

internal static class PartialDeclarationProvider
{
    public delegate BaseTypeDeclarationSyntax DInputTransform<in TData>(TData input);

    public static IProviderBuilder<TData> Construct<TData>(DInputTransform<TData> inputTransform)
    {
        return new ProviderBuilder<TData>(inputTransform);
    }

    public static IProviderBuilder<TDeclaration> Construct<TDeclaration>()
        where TDeclaration : BaseTypeDeclarationSyntax
    {
        return new ProviderBuilder<TDeclaration>(extractDeclaration);

        static BaseTypeDeclarationSyntax extractDeclaration(TDeclaration declaration) => declaration;
    }

    public static IncrementalValuesProvider<TData> Attach<TData>(IncrementalValuesProvider<TData> inputProvider, DInputTransform<TData> inputTransform)
    {
        return Construct(inputTransform).Attach(inputProvider);
    }

    public static IncrementalValuesProvider<TDeclaration> Attach<TDeclaration>(IncrementalValuesProvider<TDeclaration> inputProvider)
        where TDeclaration : BaseTypeDeclarationSyntax
    {
        return Construct<TDeclaration>().Attach(inputProvider);
    }

    public interface IProviderBuilder<TData>
    {
        public delegate Type DAttributeTypeTransform(TData input);
        public delegate string DAttributeNameTransform(TData input);

        IncrementalValuesProvider<TData> Attach(IncrementalValuesProvider<TData> inputProvider);
        IncrementalValuesProvider<TData> AttachAndReport<TAttribute>(IncrementalGeneratorInitializationContext context,
            IncrementalValuesProvider<TData> inputProvider);
        IncrementalValuesProvider<TData> AttachAndReport(IncrementalGeneratorInitializationContext context,
            IncrementalValuesProvider<TData> inputProvider, Type attributeType);
        IncrementalValuesProvider<TData> AttachAndReport(IncrementalGeneratorInitializationContext context,
            IncrementalValuesProvider<TData> inputProvider, string attributeName);
        IncrementalValuesProvider<TData> AttachAndReport(IncrementalGeneratorInitializationContext context,
            IncrementalValuesProvider<TData> inputProvider, DAttributeTypeTransform attributeType);
        IncrementalValuesProvider<TData> AttachAndReport(IncrementalGeneratorInitializationContext context,
            IncrementalValuesProvider<TData> inputProvider, DAttributeNameTransform attributeName);
    }

    private sealed class ProviderBuilder<TData> : IProviderBuilder<TData>
    {
        private DInputTransform<TData> InputTransform { get; }

        public ProviderBuilder(DInputTransform<TData> inputTransform)
        {
            InputTransform = inputTransform;
        }

        public IncrementalValuesProvider<TData> Attach(IncrementalValuesProvider<TData> inputProvider)
        {
            AttachOnlyProvider outputProvider = new(InputTransform);

            return outputProvider.Attach(inputProvider);
        }

        public IncrementalValuesProvider<TData> AttachAndReport<TAttribute>(IncrementalGeneratorInitializationContext context,
            IncrementalValuesProvider<TData> inputProvider)
        {
            return AttachAndReport(context, inputProvider, typeof(TAttribute));
        }

        public IncrementalValuesProvider<TData> AttachAndReport(IncrementalGeneratorInitializationContext context,
            IncrementalValuesProvider<TData> inputProvider, Type attributeType)
        {
            return AttachAndReport(context, inputProvider, attributeType.FullName);
        }

        public IncrementalValuesProvider<TData> AttachAndReport(IncrementalGeneratorInitializationContext context,
            IncrementalValuesProvider<TData> inputProvider, string attributeName)
        {
            Provider outputProvider = new(InputTransform, attributeName);

            return AttachAndReport(context, inputProvider, outputProvider);
        }

        public IncrementalValuesProvider<TData> AttachAndReport(IncrementalGeneratorInitializationContext context,
            IncrementalValuesProvider<TData> inputProvider, IProviderBuilder<TData>.DAttributeTypeTransform attributeType)
        {
            return AttachAndReport(context, inputProvider, attributeName);

            string attributeName(TData input)
            {
                return attributeType(input).FullName;
            }
        }

        public IncrementalValuesProvider<TData> AttachAndReport(IncrementalGeneratorInitializationContext context,
            IncrementalValuesProvider<TData> inputProvider, IProviderBuilder<TData>.DAttributeNameTransform attributeName)
        {
            DelegatedProvider outputProvider = new(InputTransform, attributeName);

            return AttachAndReport(context, inputProvider, outputProvider);
        }

        private static IncrementalValuesProvider<TData> AttachAndReport(IncrementalGeneratorInitializationContext context,
            IncrementalValuesProvider<TData> inputProvider, AProvider outputProvider)
        {
            return outputProvider.AttachAndReport(context, inputProvider);
        }

        private abstract class AProvider
        {
            private DInputTransform<TData> InputTransform { get; }

            public AProvider(DInputTransform<TData> inputTransform)
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

        private class AttachOnlyProvider : AProvider
        {
            public AttachOnlyProvider(DInputTransform<TData> inputTransform) : base(inputTransform) { }

            protected override string GetAttributeName(TData input)
            {
                throw new NotSupportedException();
            }
        }

        private class Provider : AProvider
        {
            private string AttributeName { get; } = string.Empty;

            public Provider(DInputTransform<TData> inputTransform, string attributeName) : base(inputTransform)
            {
                AttributeName = attributeName;
            }

            protected override string GetAttributeName(TData input) => AttributeName;
        }

        private class DelegatedProvider : AProvider
        {
            private IProviderBuilder<TData>.DAttributeNameTransform AttributeName { get; }

            public DelegatedProvider(DInputTransform<TData> inputTransform, IProviderBuilder<TData>.DAttributeNameTransform attributeName) : base(inputTransform)
            {
                AttributeName = attributeName;
            }

            protected override string GetAttributeName(TData input) => AttributeName(input);
        }
    }
}
