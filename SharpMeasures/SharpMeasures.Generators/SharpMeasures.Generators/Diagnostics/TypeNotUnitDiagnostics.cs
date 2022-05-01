namespace SharpMeasures.Generators.Diagnostics;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

internal static class TypeNotUnitDiagnostics
{
    public static Diagnostic Create(TypeOfExpressionSyntax typeofSyntax)
        => Diagnostic.Create(DiagnosticRules.TypeNotUnit, typeofSyntax.Type.GetLocation(), typeofSyntax.Type);
}
