namespace SharpMeasures.Generators.Providers.DeclarationFilter;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpMeasures.Generators;

using System;

public record struct StaticDeclarationFilter : IDeclarationFilter
{
    private Func<BaseTypeDeclarationSyntax, Diagnostic?> DiagnosticsDelegate { get; }

    public bool CheckValidity(BaseTypeDeclarationSyntax declaration) => declaration.HasModifierOfKind(SyntaxKind.StaticKeyword);
    public Diagnostic? ProduceDiagnostics(BaseTypeDeclarationSyntax declaration) => DiagnosticsDelegate(declaration);

    public StaticDeclarationFilter(Func<BaseTypeDeclarationSyntax, Diagnostic?> diagnosticsDelegate)
    {
        DiagnosticsDelegate = diagnosticsDelegate;
    }
}
