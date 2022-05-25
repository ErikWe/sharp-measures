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

    public delegate Type DAttributeTypeTransform<in TData>(TData input);
    public delegate string DAttributeNameTransform<in TData>(TData input);

    public static IImmediateProvider<TData> Construct<TData>(DInputTransform<TData> inputTransform)
    {
        return new ImmediateProvider<TData>(inputTransform);
    }

    public static IImmediateProvider<TDeclaration> Construct<TDeclaration>()
        where TDeclaration : BaseTypeDeclarationSyntax
    {
        return Construct<TDeclaration>(ExtractDeclaration);
    }

    public static IDelegatedProvider<TData> Construct<TData>(DInputTransform<TData> inputTransform, DAttributeNameTransform<TData> attributeNameDelegate)
    {
        return new DelegatedProvider<TData>(inputTransform, attributeNameDelegate);
    }

    public static IDelegatedProvider<TData> Construct<TData>(DInputTransform<TData> inputTransform, DAttributeTypeTransform<TData> attributeTypeDelegate)
    {
        return Construct(inputTransform, WrapAttributeTypeDelegate(attributeTypeDelegate));
    }

    private static BaseTypeDeclarationSyntax ExtractDeclaration<TDeclaration>(TDeclaration declaration) where TDeclaration : BaseTypeDeclarationSyntax => declaration;

    private static DAttributeNameTransform<TData> WrapAttributeTypeDelegate<TData>(DAttributeTypeTransform<TData> attributeTypeDelegate)
    {
        return (data) => attributeTypeDelegate(data).Name;
    }

    public interface IImmediateProvider<TData>
    {
        public abstract IncrementalValuesProvider<TData> AttachWithoutDiagnostics(IncrementalValuesProvider<TData> inputProvider);
        public abstract IncrementalValuesProvider<TData> AttachAndReport<TAttribute>(IncrementalGeneratorInitializationContext context,
            IncrementalValuesProvider<TData> inputProvider);
        public abstract IncrementalValuesProvider<TData> AttachAndReport(IncrementalGeneratorInitializationContext context,
            IncrementalValuesProvider<TData> inputProvider, Type attributeType);
        public abstract IncrementalValuesProvider<TData> AttachAndReport(IncrementalGeneratorInitializationContext context,
            IncrementalValuesProvider<TData> inputProvider, string attributeName);
    }

    public interface IDelegatedProvider<TData> : IImmediateProvider<TData>
    {
        public abstract IncrementalValuesProvider<TData> AttachAndReport(IncrementalGeneratorInitializationContext context,
            IncrementalValuesProvider<TData> inputProvider);
    }

    private class ImmediateProvider<TData> : IImmediateProvider<TData>
    {
        private DInputTransform<TData> InputTransform { get; }

        public ImmediateProvider(DInputTransform<TData> inputTransform)
        {
            InputTransform = inputTransform;
        }

        public IncrementalValuesProvider<TData> AttachWithoutDiagnostics(IncrementalValuesProvider<TData> inputProvider)
        {
            return Attach(inputProvider);
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
            IAttributeNameStrategy attributeNameStrategy = new ImmediateAttributeName(attributeName);

            return AttachAndReport(attributeNameStrategy, context, inputProvider);
        }

        public IncrementalValuesProvider<TData> Attach(IncrementalValuesProvider<TData> inputProvider)
        {
            return inputProvider.Where(DeclarationIsPartial);
        }

        protected interface IAttributeNameStrategy
        {
            public abstract string GetAttributeName(TData data);
        }

        private class ImmediateAttributeName : IAttributeNameStrategy
        {
            private string AttributeName { get; }

            public ImmediateAttributeName(string attributeName)
            {
                AttributeName = attributeName;
            }

            public string GetAttributeName(TData data) => AttributeName;
        }

        protected IncrementalValuesProvider<TData> AttachAndReport(IAttributeNameStrategy attributeNameStrategy, IncrementalGeneratorInitializationContext context,
            IncrementalValuesProvider<TData> inputProvider)
        {
            IncrementalValuesProvider<LabeledInput> labeledInput = inputProvider.Select(AddLabel);

            IncrementalValuesProvider<Diagnostic> diagnostics = labeledInput.Where(DeclarationIsNotPartial).Select(createDiagnostics);

            context.ReportDiagnostics(diagnostics);
            return labeledInput.Where(DeclarationIsPartial).Select(RemoveLabel);

            Diagnostic createDiagnostics(LabeledInput labeledInput, CancellationToken _) => CreateDiagnostics(labeledInput, attributeNameStrategy);
        }

        protected bool DeclarationIsPartial(TData input) => InputTransform(input).HasModifierOfKind(SyntaxKind.PartialKeyword);
        private static bool DeclarationIsPartial(LabeledInput labeledInput) => labeledInput.IsPartial;

        private static bool DeclarationIsNotPartial(LabeledInput labeledInput) => DeclarationIsPartial(labeledInput) is false;

        private LabeledInput AddLabel(TData input, CancellationToken _) => new(DeclarationIsPartial(input), input);
        private TData RemoveLabel(LabeledInput labeledInput, CancellationToken _) => labeledInput.Input;

        private Diagnostic CreateDiagnostics(TData input, IAttributeNameStrategy attributeNameStrategy)
        {
            var declaration = InputTransform(input);

            return DiagnosticConstruction.TypeNotPartial(declaration.GetLocation(), attributeNameStrategy.GetAttributeName(input), declaration.Identifier.Text);
        }

        private Diagnostic CreateDiagnostics(LabeledInput labeledInput, IAttributeNameStrategy attributeNameStrategy)
        {
            return CreateDiagnostics(labeledInput.Input, attributeNameStrategy);
        }

        private readonly record struct LabeledInput(bool IsPartial, TData Input);
    }

    private class DelegatedProvider<TData> : ImmediateProvider<TData>, IDelegatedProvider<TData>
    {
        private IAttributeNameStrategy AttributeNameStrategy { get; }

        public DelegatedProvider(DInputTransform<TData> inputTransform, DAttributeNameTransform<TData> attributeNameDelegate) : base(inputTransform)
        {
            AttributeNameStrategy = new DelegatedAttributeName(attributeNameDelegate);
        }

        public IncrementalValuesProvider<TData> AttachAndReport(IncrementalGeneratorInitializationContext context,
            IncrementalValuesProvider<TData> inputProvider)
        {
            return AttachAndReport(AttributeNameStrategy, context, inputProvider);
        }

        private class DelegatedAttributeName : IAttributeNameStrategy
        {
            private DAttributeNameTransform<TData> AttributeNameDelegate { get; }

            public DelegatedAttributeName(DAttributeNameTransform<TData> attributeNameDelegate)
            {
                AttributeNameDelegate = attributeNameDelegate;
            }

            public string GetAttributeName(TData data)
            {
                return AttributeNameDelegate(data);
            }
        }
    }
}
