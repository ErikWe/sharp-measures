namespace SharpMeasures.Generators.Analyzers.Diagnostics;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;

using SharpMeasures.Generators.Attributes.Parsing.Units;

using System;

internal static class UnitAttributeDiagnostics
{
    private static Type UnitAttributeType { get; } = typeof(GeneratedUnitAttribute);

    public static void Analyze(SymbolAnalysisContext context, INamedTypeSymbol namedTypeSymbol, AttributeData attribute)
    {
        TypeIsNotPartialDiagnostics.AnalyzeNamedType(context, namedTypeSymbol, UnitAttributeType);

        if (GeneratedUnitAttributeParameters.Parse(attribute) is GeneratedUnitAttributeParameters parameters)
        {
            TypeIsNotScalarQuantityDiagnostics.AnalyzeNamedType(context, attribute, parameters);
        }
    }
}