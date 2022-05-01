namespace SharpMeasures.Generators.Diagnostics.UnitDefinitions;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using System.Globalization;

internal static class UnitListNotMatchingSignatureDiagnostics
{
    public static Diagnostic Create(AttributeArgumentSyntax argumentSyntax, int signatureLength, int unitsLength)
        => Diagnostic.Create(DiagnosticRules.UnitListNotMatchingSignature, argumentSyntax.GetLocation(), signatureLength.ToString(CultureInfo.InvariantCulture),
            unitsLength.ToString(CultureInfo.InvariantCulture));
}
