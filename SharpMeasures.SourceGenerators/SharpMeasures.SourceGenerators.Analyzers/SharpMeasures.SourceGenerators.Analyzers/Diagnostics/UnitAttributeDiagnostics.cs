﻿namespace ErikWe.SharpMeasures.SourceGenerators.Analyzers.Diagnostics;

using ErikWe.SharpMeasures.Attributes;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;

using System;
using System.Collections.Immutable;

internal static class UnitAttributeDiagnostics
{
    private static string AttributeFullName { get; } = typeof(UnitAttribute).FullName;

    public static void Analyze(SymbolAnalysisContext context, INamedTypeSymbol namedTypeSymbol, AttributeData attribute)
    {
        TypeIsNotPartialDiagnostics.AnalyzeNamedType(context, namedTypeSymbol, AttributeFullName);
    }
}