namespace SharpMeasures.Generators.Diagnostics;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using System;

internal static class TypeNotPartialDiagnostics
{
    public static Diagnostic Create<TAttribute>(BaseTypeDeclarationSyntax declarationSyntax)
        => Create(declarationSyntax, typeof(TAttribute));

    public static Diagnostic Create(BaseTypeDeclarationSyntax declarationSyntax, Type attributeType)
        => Create(declarationSyntax, attributeType.FullName);

    public static Diagnostic Create(BaseTypeDeclarationSyntax declarationSyntax, string attributeName)
        => Diagnostic.Create(DiagnosticRules.TypeNotPartial, declarationSyntax.Identifier.GetLocation(),
            attributeName, declarationSyntax.Identifier.Text);
}
