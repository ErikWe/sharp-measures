namespace SharpMeasures.Generators.Providers;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using System;
using System.Threading;

public interface IPartialDeclarationProviderDiagnostics
{
    public abstract Diagnostic? TypeNotPartial(BaseTypeDeclarationSyntax declaration);
}

public interface IPartialDeclarationProvider<TData>
{
    public abstract IncrementalValuesProvider<TData> AttachWithoutDiagnostics(IncrementalValuesProvider<TData> inputProvider);
    public abstract IncrementalValuesProvider<TData> AttachAndReport(IncrementalGeneratorInitializationContext context,
        IncrementalValuesProvider<TData> inputProvider, IPartialDeclarationProviderDiagnostics diagnostics);
}

public static class PartialDeclarationProvider
{
    public delegate BaseTypeDeclarationSyntax DInputTransform<in TData>(TData input);

    public delegate Type DAttributeTypeTransform<in TData>(TData input);
    public delegate string DAttributeNameTransform<in TData>(TData input);

    public static IPartialDeclarationProvider<TData> Construct<TData>(DInputTransform<TData> inputTransform)
    {
        return new ImmediateProvider<TData>(inputTransform);
    }

    public static IPartialDeclarationProvider<TDeclaration> Construct<TDeclaration>()
        where TDeclaration : BaseTypeDeclarationSyntax
    {
        return Construct<TDeclaration>(ExtractDeclaration);
    }

    private static BaseTypeDeclarationSyntax ExtractDeclaration<TDeclaration>(TDeclaration declaration) where TDeclaration : BaseTypeDeclarationSyntax => declaration;

    private class ImmediateProvider<TData> : IPartialDeclarationProvider<TData>
    {
        private DInputTransform<TData> InputTransform { get; }

        public ImmediateProvider(DInputTransform<TData> inputTransform)
        {
            InputTransform = inputTransform;
        }

        public IncrementalValuesProvider<TData> AttachWithoutDiagnostics(IncrementalValuesProvider<TData> inputProvider)
        {
            return inputProvider.Where(DeclarationIsPartial);
        }

        public IncrementalValuesProvider<TData> AttachAndReport(IncrementalGeneratorInitializationContext context,
            IncrementalValuesProvider<TData> inputProvider, IPartialDeclarationProviderDiagnostics diagnosticsStrategy)
        {
            IncrementalValuesProvider<LabeledInput> labeledInput = inputProvider.Select(AddLabel);

            IncrementalValuesProvider<Diagnostic?> diagnostics = labeledInput.Where(DeclarationIsNotPartial).Select(createDiagnostics);

            context.ReportDiagnostics(diagnostics.WhereNotNull());
            return labeledInput.Where(DeclarationIsPartial).Select(RemoveLabel);

            Diagnostic? createDiagnostics(LabeledInput labeledInput, CancellationToken _)
            {
                var declaration = InputTransform(labeledInput.Input);

                return diagnosticsStrategy.TypeNotPartial(declaration);
            }
        }

        protected bool DeclarationIsPartial(TData input) => InputTransform(input).HasModifierOfKind(SyntaxKind.PartialKeyword);
        private static bool DeclarationIsPartial(LabeledInput labeledInput) => labeledInput.IsPartial;

        private static bool DeclarationIsNotPartial(LabeledInput labeledInput) => DeclarationIsPartial(labeledInput) is false;

        private LabeledInput AddLabel(TData input, CancellationToken _) => new(DeclarationIsPartial(input), input);
        private TData RemoveLabel(LabeledInput labeledInput, CancellationToken _) => labeledInput.Input;

        private readonly record struct LabeledInput(bool IsPartial, TData Input);
    }
}
