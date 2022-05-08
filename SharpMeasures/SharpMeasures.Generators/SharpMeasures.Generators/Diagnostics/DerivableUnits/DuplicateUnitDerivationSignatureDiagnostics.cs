namespace SharpMeasures.Generators.Diagnostics.DerivableUnits;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

internal static class DuplicateUnitDerivationSignatureDiagnostics
{
    public static Diagnostic Create(BaseTypeDeclarationSyntax declarationSyntax, string signature)
        => Diagnostic.Create(DiagnosticRules.DuplicateUnitDerivationSignature, declarationSyntax.Identifier.GetLocation(),
            declarationSyntax.Identifier.Text, signature);
}
