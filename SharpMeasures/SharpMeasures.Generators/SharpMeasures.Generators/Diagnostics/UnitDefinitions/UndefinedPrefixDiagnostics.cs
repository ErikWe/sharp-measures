namespace SharpMeasures.Generators.Diagnostics.UnitDefinitions;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

internal static class UndefinedPrefixDiagnostics
{
    public static Diagnostic Create<TPrefix>(AttributeArgumentSyntax argumentSyntax)
        => Diagnostic.Create(DiagnosticRules.UndefinedPrefix, argumentSyntax.GetLocation(), argumentSyntax.ToString(), typeof(TPrefix).ToString());
}
