namespace SharpMeasures.Generators;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Scalars.ForeignScalarParsing;
using SharpMeasures.Generators.Units.ForeignUnitParsing;
using SharpMeasures.Generators.Vectors.ForeignVectorParsing;

using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;

internal sealed class ForeignTypeParser
{
    public static (IncrementalValueProvider<IForeignUnitProcesser> UnitProvider, IncrementalValueProvider<IForeignScalarProcesser> ScalarProvider, IncrementalValueProvider<IForeignVectorProcesser> VectorProvider) Parse(IncrementalValueProvider<ImmutableArray<INamedTypeSymbol>> foreignSymbols)
    {
        var combinedProvider = foreignSymbols.Select(Parse);

        return (combinedProvider.Select(ExtractUnits), combinedProvider.Select(ExtractScalars), combinedProvider.Select(ExtractVectors));
    }

    private static (IForeignUnitProcesser Units, IForeignScalarProcesser Scalars, IForeignVectorProcesser Vectors) Parse(ImmutableArray<INamedTypeSymbol> foreignSymbols, CancellationToken _)
    {
        return new ForeignTypeParser(foreignSymbols).Parse();
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

    private (IForeignUnitProcesser, IForeignScalarProcesser, IForeignVectorProcesser) Parse()
    {
        IterativelyParse();

        return (UnitParser.Finalize(), ScalarParser.Finalize(), VectorParser.Finalize());
    }

    private void IterativelyParse()
    {
        for (int i = 0; i < ForeignSymbols.Count; i++)
        {
            var foreignSymbol = ForeignSymbols[i];

            ForeignSymbols.RemoveAt(i);
            i -= 1;

            if (ParsedTypes.Contains(foreignSymbol))
            {
                continue;
            }

            ParsedTypes.Add(foreignSymbol);

            if (ParseUnit(foreignSymbol)) continue;
            if (ParseScalar(foreignSymbol)) continue;
            if (ParseVector(foreignSymbol)) continue;
        }

        if (ForeignSymbols.Count > 0)
        {
            IterativelyParse();
        }
    }

    private bool ParseUnit(INamedTypeSymbol foreignSymbol)
    {
        var referencedSymbols = UnitParser.TryParse(foreignSymbol);

        if (referencedSymbols.HasValue is false)
        {
            return false;
        }

        ForeignSymbols.AddRange(referencedSymbols.Value);

        return true;
    }

    private bool ParseScalar(INamedTypeSymbol foreignSymbol)
    {
        var referencedSymbols = ScalarParser.TryParse(foreignSymbol);

        if (referencedSymbols.HasValue is false)
        {
            return false;
        }

        ForeignSymbols.AddRange(referencedSymbols.Value);

        return true;
    }

    private bool ParseVector(INamedTypeSymbol foreignSymbol)
    {
        var referencedSymbols = VectorParser.TryParse(foreignSymbol);

        if (referencedSymbols.HasValue is false)
        {
            return false;
        }

        ForeignSymbols.AddRange(referencedSymbols.Value);

        return true;
    }

    private static IForeignUnitProcesser ExtractUnits((IForeignUnitProcesser Units, IForeignScalarProcesser, IForeignVectorProcesser) results, CancellationToken _) => results.Units;
    private static IForeignScalarProcesser ExtractScalars((IForeignUnitProcesser, IForeignScalarProcesser Scalars, IForeignVectorProcesser) results, CancellationToken _) => results.Scalars;
    private static IForeignVectorProcesser ExtractVectors((IForeignUnitProcesser, IForeignScalarProcesser, IForeignVectorProcesser Vectors) results, CancellationToken _) => results.Vectors;
}
