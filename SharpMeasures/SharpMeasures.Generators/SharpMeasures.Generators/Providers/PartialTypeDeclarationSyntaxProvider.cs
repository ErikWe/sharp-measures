namespace SharpMeasures.Generators.Providers;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpMeasures.Generators.Diagnostics;

using System;
using System.Threading;

internal static class PartialTypeDeclarationSyntaxProvider
{
    public static IncrementalValuesProvider<TDeclarationSyntax> AttachAndReport<TDeclarationSyntax, TAttribute>(IncrementalGeneratorInitializationContext context,
        IncrementalValuesProvider<TDeclarationSyntax> declarationProvider)
        where TDeclarationSyntax : BaseTypeDeclarationSyntax
        => AttachAndReport(context, declarationProvider, typeof(TAttribute));

    public static IncrementalValuesProvider<TDeclarationSyntax> AttachAndReport<TDeclarationSyntax>(IncrementalGeneratorInitializationContext context,
        IncrementalValuesProvider<TDeclarationSyntax> declarationProvider, Type attributeType)
        where TDeclarationSyntax : BaseTypeDeclarationSyntax
        => AttachAndReport(context, declarationProvider, attributeType.FullName);

    public static IncrementalValuesProvider<TDeclarationSyntax> AttachAndReport<TDeclarationSyntax>(IncrementalGeneratorInitializationContext context,
        IncrementalValuesProvider<TDeclarationSyntax> declarationProvider, string attributeName)
        where TDeclarationSyntax : BaseTypeDeclarationSyntax
    {
        IncrementalValuesProvider<Diagnostic> diagnostics = declarationProvider.Where(DeclarationIsNotPartial).Select(DiagnosticsCreator(attributeName));

        context.ReportDiagnostics(diagnostics);
        return declarationProvider.Where(DeclarationIsPartial);
    }

    public static IncrementalValuesProvider<TDeclarationSyntax> Attach<TDeclarationSyntax>(IncrementalValuesProvider<TDeclarationSyntax> declarationProvider)
        where TDeclarationSyntax : BaseTypeDeclarationSyntax
        => declarationProvider.Where(DeclarationIsPartial);

    private static bool DeclarationIsPartial(MemberDeclarationSyntax declaration) => declaration.HasModifierOfKind(SyntaxKind.PartialKeyword);
    private static bool DeclarationIsNotPartial(MemberDeclarationSyntax declaration) => !DeclarationIsPartial(declaration);
    private static Func<BaseTypeDeclarationSyntax, CancellationToken, Diagnostic> DiagnosticsCreator(string attributeName)
        => (declaration, token) => CreateDiagnostics(declaration, attributeName, token);
    private static Diagnostic CreateDiagnostics(BaseTypeDeclarationSyntax declaration, string attributeName, CancellationToken _)
        => TypeNotPartialDiagnostics.Create(declaration, attributeName);
}
