namespace SharpMeasures.Generators.Diagnostics;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

internal static class TypeNotScalarQuantityDiagnostics
{
    public static Diagnostic Create(TypeOfExpressionSyntax typeofSyntax)
        => Diagnostic.Create(DiagnosticRules.TypeNotScalarQuantity, typeofSyntax.Type.GetLocation(), typeofSyntax.Type);
}
