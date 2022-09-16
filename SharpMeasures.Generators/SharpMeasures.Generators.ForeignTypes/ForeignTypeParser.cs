namespace SharpMeasures.Generators.ForeignTypes;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Units;
using SharpMeasures.Generators.Units.ForeignUnitParsing;
using SharpMeasures.Generators.Vectors;

using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;

public class ForeignTypeParser
{
    public static (IncrementalValueProvider<IUnitPopulation>, IncrementalValueProvider<IScalarPopulation>, IncrementalValueProvider<IVectorPopulation>) Parse(IncrementalValueProvider<ImmutableArray<INamedTypeSymbol>> foreignSymbols, IncrementalValueProvider<IUnitPopulation> unitPopulation, IncrementalValueProvider<IScalarPopulation> scalarPopulation, IncrementalValueProvider<IVectorPopulation> vectorPopulation)
    {
        var extendedPopulations = foreignSymbols.Combine(unitPopulation, scalarPopulation, vectorPopulation).Select(Parse);

        var extendedUnitPopulation = extendedPopulations.Select(ExtractUnitPopulation);
        var extendedScalarPopulation = extendedPopulations.Select(ExtractScalarPopulation);
        var extendedVectorPopulation = extendedPopulations.Select(ExtractVectorPopulation);

        return (extendedUnitPopulation, extendedScalarPopulation, extendedVectorPopulation);
    }

    private static (IUnitPopulation, IScalarPopulation, IVectorPopulation) Parse((ImmutableArray<INamedTypeSymbol> ForeignSymbols, IUnitPopulation UnitPopulation, IScalarPopulation ScalarPopulation, IVectorPopulation VectorPopulation) input, CancellationToken _)
    {
        List<INamedTypeSymbol> foreignSymbolsList = new(input.ForeignSymbols.Length);

        foreignSymbolsList.AddRange(input.ForeignSymbols);

        ForeignTypeParser parser = new(foreignSymbolsList);

        parser.Parse();

        return parser.Extend(input.UnitPopulation, input.ScalarPopulation, input.VectorPopulation);
    }

    private static IUnitPopulation ExtractUnitPopulation((IUnitPopulation UnitPopulation, IScalarPopulation, IVectorPopulation) input, CancellationToken _) => input.UnitPopulation;
    private static IScalarPopulation ExtractScalarPopulation((IUnitPopulation, IScalarPopulation ScalarPopulation, IVectorPopulation) input, CancellationToken _) => input.ScalarPopulation;
    private static IVectorPopulation ExtractVectorPopulation((IUnitPopulation, IScalarPopulation, IVectorPopulation VectorPopulation) input, CancellationToken _) => input.VectorPopulation;

    private List<INamedTypeSymbol> ForeignSymbols { get; }

    private HashSet<NamedType> ParsedTypes { get; } = new();

    private List<IUnitType> Units { get; } = new();
    
    private List<IScalarBaseType> ScalarBases { get; } = new();
    private List<IScalarSpecializationType> ScalarSpecializations { get; } = new();

    private List<IVectorGroupBaseType> GroupBases { get; } = new();
    private List<IVectorGroupSpecializationType> GroupSpecializations { get; } = new();
    private List<IVectorGroupMemberType> GroupMembers { get; } = new();

    private List<IVectorBaseType> VectorBases { get; } = new();
    private List<IVectorSpecializationType> VectorSpecializations { get; } = new();

    private ForeignTypeParser(List<INamedTypeSymbol> foreignSymbols)
    {
        ForeignSymbols = foreignSymbols;
    }

    private void Parse()
    {
        for (int i = 0; i < ForeignSymbols.Count; i++)
        {
            var foreignSymbol = ForeignSymbols[i];
            var foreignSymbolType = NamedType.FromSymbol(foreignSymbol);

            ForeignSymbols.RemoveAt(i);

            if (ParsedTypes.Contains(foreignSymbolType))
            {
                continue;
            }

            ParsedTypes.Add(foreignSymbolType);

            if (ParseUnit(foreignSymbol)) continue;
            if (ParseScalar(foreignSymbol)) continue;
            if (ParseSpecializedScalar(foreignSymbol)) continue;
            if (ParseVector(foreignSymbol)) continue;
            if (ParseSpecializedVector(foreignSymbol)) continue;
            if (ParseGroup(foreignSymbol)) continue;
            if (ParseSpecializedGroup(foreignSymbol)) continue;
            if (ParseGroupMember(foreignSymbol)) continue;
        }
    }

    private (IUnitPopulation UnitPopulation, IScalarPopulation ScalarPopulation, IVectorPopulation VectorPopulation) Extend(IUnitPopulation unitPopulation, IScalarPopulation scalarPopulation, IVectorPopulation vectorPopulation)
    {

    }

    private bool ParseUnit(INamedTypeSymbol foreignSymbol)
    {
        (var unit, var unitSymbols) = ForeignUnitParser.Parse(foreignSymbol);

        if (unit.HasValue is false)
        {
            return false;
        }

        Units.Add(unit.Value);
        ForeignSymbols.AddRange(unitSymbols);

        return true;
    }

    private bool ParseScalar(INamedTypeSymbol foreignSymbol)
    {
        return false;
    }

    private bool ParseSpecializedScalar(INamedTypeSymbol foreignSymbol)
    {
        return false;
    }

    private bool ParseVector(INamedTypeSymbol foreignSymbol)
    {
        return false;
    }

    private bool ParseSpecializedVector(INamedTypeSymbol foreignSymbol)
    {
        return false;
    }

    private bool ParseGroup(INamedTypeSymbol foreignSymbol)
    {
        return false;
    }

    private bool ParseSpecializedGroup(INamedTypeSymbol foreignSymbol)
    {
        return false;
    }

    private bool ParseGroupMember(INamedTypeSymbol foreignSymbol)
    {
        return false;
    }
}
