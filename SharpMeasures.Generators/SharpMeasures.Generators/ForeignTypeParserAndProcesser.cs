namespace SharpMeasures.Generators;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Scalars.ForeignScalarParsing;
using SharpMeasures.Generators.Units;
using SharpMeasures.Generators.Units.ForeignUnitParsing;
using SharpMeasures.Generators.Vectors;
using SharpMeasures.Generators.Vectors.ForeignVectorParsing;

using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;

internal class ForeignTypeParserAndProcesser
{
    public static IncrementalValueProvider<ForeignTypes> Parse(IncrementalValueProvider<ImmutableArray<INamedTypeSymbol>> foreignSymbols, IncrementalValueProvider<IUnitPopulation> unitPopulation, IncrementalValueProvider<IScalarPopulation> scalarPopulation, IncrementalValueProvider<IVectorPopulation> vectorPopulation)
    {
        return foreignSymbols.Combine(unitPopulation, scalarPopulation, vectorPopulation).Select(Parse);
    }

    private static ForeignTypes Parse((ImmutableArray<INamedTypeSymbol> ForeignSymbols, IUnitPopulation UnitPopulation, IScalarPopulation ScalarPopulation, IVectorPopulation VectorPopulation) input, CancellationToken _)
    {
        List<INamedTypeSymbol> foreignSymbolsList = new(input.ForeignSymbols.Length);

        foreignSymbolsList.AddRange(input.ForeignSymbols);

        ForeignTypeParserAndProcesser parser = new(foreignSymbolsList);

        return parser.Parse();
    }

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

    public ForeignTypeParserAndProcesser(List<INamedTypeSymbol> foreignSymbols)
    {
        ForeignSymbols = foreignSymbols;
    }

    private ForeignTypes Parse()
    {
        IterativelyParse();

        return new(Units, ScalarBases, ScalarSpecializations, GroupBases, GroupSpecializations, GroupMembers, VectorBases, VectorSpecializations);
    }

    private void IterativelyParse()
    {
        for (int i = 0; i < ForeignSymbols.Count; i++)
        {
            var foreignSymbol = ForeignSymbols[i];
            var foreignSymbolType = NamedType.FromSymbol(foreignSymbol);

            ForeignSymbols.RemoveAt(i);
            i -= 1;

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

        if (ForeignSymbols.Count > 0)
        {
            IterativelyParse();
        }
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
        (var scalar, var scalarSymbols) = ForeignScalarBaseParser.Parse(foreignSymbol);

        if (scalar.HasValue is false)
        {
            return false;
        }

        ScalarBases.Add(scalar.Value);
        ForeignSymbols.AddRange(scalarSymbols);

        return true;
    }

    private bool ParseSpecializedScalar(INamedTypeSymbol foreignSymbol)
    {
        (var scalar, var scalarSymbols) = ForeignScalarSpecializationParser.Parse(foreignSymbol);

        if (scalar.HasValue is false)
        {
            return false;
        }

        ScalarSpecializations.Add(scalar.Value);
        ForeignSymbols.AddRange(scalarSymbols);

        return true;
    }

    private bool ParseVector(INamedTypeSymbol foreignSymbol)
    {
        (var vector, var vectorSymbols) = ForeignVectorBaseParser.Parse(foreignSymbol);

        if (vector.HasValue is false)
        {
            return false;
        }

        VectorBases.Add(vector.Value);
        ForeignSymbols.AddRange(vectorSymbols);

        return true;
    }

    private bool ParseSpecializedVector(INamedTypeSymbol foreignSymbol)
    {
        (var vector, var vectorSymbols) = ForeignVectorSpecializationParser.Parse(foreignSymbol);

        if (vector.HasValue is false)
        {
            return false;
        }

        VectorSpecializations.Add(vector.Value);
        ForeignSymbols.AddRange(vectorSymbols);

        return true;
    }

    private bool ParseGroup(INamedTypeSymbol foreignSymbol)
    {
        (var group, var groupSymbols) = ForeignGroupBaseParser.Parse(foreignSymbol);

        if (group.HasValue is false)
        {
            return false;
        }

        GroupBases.Add(group.Value);
        ForeignSymbols.AddRange(groupSymbols);

        return true;
    }

    private bool ParseSpecializedGroup(INamedTypeSymbol foreignSymbol)
    {
        (var group, var groupSymbols) = ForeignGroupSpecializationParser.Parse(foreignSymbol);

        if (group.HasValue is false)
        {
            return false;
        }

        GroupSpecializations.Add(group.Value);
        ForeignSymbols.AddRange(groupSymbols);

        return true;
    }

    private bool ParseGroupMember(INamedTypeSymbol foreignSymbol)
    {
        (var member, var memberSymbols) = ForeignGroupMemberParser.Parse(foreignSymbol);

        if (member.HasValue is false)
        {
            return false;
        }

        GroupMembers.Add(member.Value);
        ForeignSymbols.AddRange(memberSymbols);

        return true;
    }
}
