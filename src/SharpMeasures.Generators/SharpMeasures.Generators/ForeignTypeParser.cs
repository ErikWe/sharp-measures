namespace SharpMeasures.Generators;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Scalars.ForeignScalarParsing;
using SharpMeasures.Generators.Units.ForeignUnitParsing;
using SharpMeasures.Generators.Vectors.ForeignVectorParsing;

using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading;

internal sealed class ForeignTypeParser
{
    public static (IncrementalValueProvider<ForeignUnitParsingResult> UnitProvider, IncrementalValueProvider<ForeignScalarParsingResult> ScalarProvider, IncrementalValueProvider<ForeignVectorParsingResult> VectorProvider) Parse(IncrementalValueProvider<ImmutableArray<INamedTypeSymbol>> foreignSymbols)
    {
        var combinedProvider = foreignSymbols.Select(Parse);

        return (combinedProvider.Select(ExtractUnits), combinedProvider.Select(ExtractScalars), combinedProvider.Select(ExtractVectors));
    }

    private static (ForeignUnitParsingResult Units, ForeignScalarParsingResult Scalars, ForeignVectorParsingResult Vectors) Parse(ImmutableArray<INamedTypeSymbol> foreignSymbols, CancellationToken token)
    {
        return new ForeignTypeParser(foreignSymbols).Parse(token);
    }

    private ForeignUnitParser UnitParser { get; } = new();
    private ForeignScalarParser ScalarParser { get; } = new();
    private ForeignVectorParser VectorParser { get; } = new();

    private List<INamedTypeSymbol> ForeignSymbols { get; } = new();

    private HashSet<INamedTypeSymbol> ParsedTypes { get; } = new(SymbolEqualityComparer.Default);

    private ForeignTypeParser(ImmutableArray<INamedTypeSymbol> foreignSymbols)
    {
        ForeignSymbols.AddRange(foreignSymbols);
    }

    private (ForeignUnitParsingResult, ForeignScalarParsingResult, ForeignVectorParsingResult) Parse(CancellationToken token)
    {
        if (token.IsCancellationRequested is false)
        {
            IterativelyParse();
        }

        return (UnitParser.Finalize(), ScalarParser.Finalize(), VectorParser.Finalize());
    }

    private void IterativelyParse()
    {
        for (var i = 0; i < ForeignSymbols.Count; i++)
        {
            var foreignSymbol = ForeignSymbols[i];

            ForeignSymbols.RemoveAt(i);
            i -= 1;

            if (ParsedTypes.Contains(foreignSymbol))
            {
                continue;
            }

            ParsedTypes.Add(foreignSymbol);

            if (ParseUnit(foreignSymbol))
            {
                continue;
            }

            if (ParseScalar(foreignSymbol))
            {
                continue;
            }

            if (ParseVector(foreignSymbol))
            {
                continue;
            }
        }

        if (ForeignSymbols.Count > 0)
        {
            IterativelyParse();
        }
    }

    private bool ParseUnit(INamedTypeSymbol foreignSymbol)
    {
        (var success, var referencedSymbols) = UnitParser.TryParse(foreignSymbol);

        if (success)
        {
            ForeignSymbols.AddRange(referencedSymbols);
        }

        return success;
    }

    private bool ParseScalar(INamedTypeSymbol foreignSymbol)
    {
        (var success, var referencedSymbols) = ScalarParser.TryParse(foreignSymbol);

        if (success)
        {
            ForeignSymbols.AddRange(referencedSymbols);
        }

        return success;
    }

    private bool ParseVector(INamedTypeSymbol foreignSymbol)
    {
        (var success, var referencedSymbols) = VectorParser.TryParse(foreignSymbol);

        if (success)
        {
            ForeignSymbols.AddRange(referencedSymbols);
        }

        return success;
    }

    private static ForeignUnitParsingResult ExtractUnits<T1, T2>((ForeignUnitParsingResult Units, T1, T2) results, CancellationToken _) => results.Units;
    private static ForeignScalarParsingResult ExtractScalars<T1, T2>((T1, ForeignScalarParsingResult Scalars, T2) results, CancellationToken _) => results.Scalars;
    private static ForeignVectorParsingResult ExtractVectors<T1, T2>((T1, T2, ForeignVectorParsingResult Vectors) results, CancellationToken _) => results.Vectors;
}
