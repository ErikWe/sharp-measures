namespace SharpMeasures.Generators.Diagnostics;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

internal static class TypeNotUnbiasedScalarQuantityDiagnostics
{
    public static Diagnostic Create(TypeOfExpressionSyntax typeofSyntax)
        => Diagnostic.Create(DiagnosticRules.TypeNotUnbiasedScalarQuantity, typeofSyntax.Type.GetLocation(),
            typeofSyntax.Type);
}
