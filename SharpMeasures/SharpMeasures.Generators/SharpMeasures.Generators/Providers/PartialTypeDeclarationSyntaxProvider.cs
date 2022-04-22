namespace SharpMeasures.Generators.Providers;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpMeasures.Generators.Diagnostics;

using System;

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
        IncrementalValuesProvider<TDeclarationSyntax> offendingDeclarations = declarationProvider.Where(DeclarationIsNotPartial);

        context.RegisterSourceOutput(offendingDeclarations, (context, declaration) => ReportDiagnostics(context, declaration, attributeName));
        return declarationProvider.Where(DeclarationIsPartial);
    }

    public static IncrementalValuesProvider<TDeclarationSyntax> Attach<TDeclarationSyntax>(IncrementalValuesProvider<TDeclarationSyntax> declarationProvider)
        where TDeclarationSyntax : BaseTypeDeclarationSyntax
        => declarationProvider.Where(DeclarationIsPartial);

    private static bool DeclarationIsPartial(MemberDeclarationSyntax declaration) => declaration.HasModifierOfKind(SyntaxKind.PartialKeyword);
    private static bool DeclarationIsNotPartial(MemberDeclarationSyntax declaration) => !DeclarationIsPartial(declaration);

    private static void ReportDiagnostics(SourceProductionContext context, BaseTypeDeclarationSyntax declaration, string attributeName)
    {
        Diagnostic diagnostics = TypeNotPartialDiagnostics.Create(declaration, attributeName);

        context.ReportDiagnostic(diagnostics);
    }
}
