namespace SharpMeasures.Generators.Scalars.Parsing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Units;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

internal static class ScalarBaseResolver
{
    public static IncrementalValuesProvider<ResolvedScalarType> Resolve(IncrementalValuesProvider<ScalarBaseType> scalarProvider, IncrementalValueProvider<IUnitPopulation> unitPopulationProvider)
    {
        return scalarProvider.Combine(unitPopulationProvider).Select(Resolve).WhereResult();
    }

    private static Optional<ResolvedScalarType> Resolve((ScalarBaseType UnresolvedScalar, IUnitPopulation UnitPopulation) input, CancellationToken _)
    {
        if (input.UnitPopulation.Units.TryGetValue(input.UnresolvedScalar.Definition.Unit, out var unit) is false)
        {
            return new Optional<ResolvedScalarType>();
        }

        var includedBases = ResolveUnitInclusions(unit, input.UnresolvedScalar.BaseInclusions, () => input.UnresolvedScalar.BaseExclusions);
        var includedUnits = ResolveUnitInclusions(unit, input.UnresolvedScalar.UnitInclusions, () => input.UnresolvedScalar.UnitExclusions);

        return new ResolvedScalarType(input.UnresolvedScalar.Type, input.UnresolvedScalar.TypeLocation, input.UnresolvedScalar.Definition.Unit, input.UnresolvedScalar.Definition.UseUnitBias,
            input.UnresolvedScalar.Definition.Vector, input.UnresolvedScalar.Definition.Reciprocal, input.UnresolvedScalar.Definition.Square, input.UnresolvedScalar.Definition.Cube,
            input.UnresolvedScalar.Definition.SquareRoot, input.UnresolvedScalar.Definition.CubeRoot, input.UnresolvedScalar.Definition.ImplementSum, input.UnresolvedScalar.Definition.ImplementDifference,
            input.UnresolvedScalar.Definition.Difference, input.UnresolvedScalar.Definition.DefaultUnitName, input.UnresolvedScalar.Definition.DefaultUnitSymbol, input.UnresolvedScalar.Derivations,
            input.UnresolvedScalar.Constants, input.UnresolvedScalar.Conversions, includedBases, includedUnits, input.UnresolvedScalar.Definition.GenerateDocumentation);
    }

    private static IReadOnlyList<string> ResolveUnitInclusions(IUnitType unit, IEnumerable<IUnitList> inclusions, Func<IEnumerable<IUnitList>> exclusionsDelegate)
    {
        HashSet<string> includedUnits = new(unit.UnitsByName.Keys);

        if (inclusions.Any())
        {
            includedUnits.IntersectWith(inclusions.SelectMany(static (unitList) => unitList.Units));

            return includedUnits.ToList();
        }

        includedUnits.ExceptWith(exclusionsDelegate().SelectMany(static (unitList) => unitList.Units));

        return includedUnits.ToList();
    }
}
