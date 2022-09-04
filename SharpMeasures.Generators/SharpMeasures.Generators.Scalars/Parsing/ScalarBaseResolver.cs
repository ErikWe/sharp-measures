namespace SharpMeasures.Generators.Scalars.Parsing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Quantities;
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

    private static Optional<ResolvedScalarType> Resolve((ScalarBaseType UnvalidatedScalar, IUnitPopulation UnitPopulation) input, CancellationToken _)
    {
        if (input.UnitPopulation.Units.TryGetValue(input.UnvalidatedScalar.Definition.Unit, out var unit) is false)
        {
            return new Optional<ResolvedScalarType>();
        }

        var includedBases = ResolveUnitInclusions(unit, input.UnvalidatedScalar.UnitBaseInstanceInclusions, () => input.UnvalidatedScalar.UnitBaseInstanceExclusions);
        var includedUnits = ResolveUnitInclusions(unit, input.UnvalidatedScalar.UnitInstanceInclusions, () => input.UnvalidatedScalar.UnitInstanceExclusions);

        return new ResolvedScalarType(input.UnvalidatedScalar.Type, input.UnvalidatedScalar.TypeLocation, input.UnvalidatedScalar.Definition.Unit, input.UnvalidatedScalar.Definition.UseUnitBias,
            input.UnvalidatedScalar.Definition.Vector, input.UnvalidatedScalar.Definition.Reciprocal, input.UnvalidatedScalar.Definition.Square, input.UnvalidatedScalar.Definition.Cube,
            input.UnvalidatedScalar.Definition.SquareRoot, input.UnvalidatedScalar.Definition.CubeRoot, input.UnvalidatedScalar.Definition.ImplementSum, input.UnvalidatedScalar.Definition.ImplementDifference,
            input.UnvalidatedScalar.Definition.Difference, input.UnvalidatedScalar.Definition.DefaultUnitInstanceName, input.UnvalidatedScalar.Definition.DefaultUnitInstanceSymbol, input.UnvalidatedScalar.Derivations,
            input.UnvalidatedScalar.Constants, input.UnvalidatedScalar.Conversions, includedBases, includedUnits, input.UnvalidatedScalar.Definition.GenerateDocumentation);
    }

    private static IReadOnlyList<string> ResolveUnitInclusions(IUnitType unit, IEnumerable<IUnitInstanceList> inclusions, Func<IEnumerable<IUnitInstanceList>> exclusionsDelegate)
    {
        HashSet<string> includedUnitInstances = new(unit.UnitInstancesByName.Keys);

        if (inclusions.Any())
        {
            includedUnitInstances.IntersectWith(inclusions.SelectMany(static (unitList) => unitList.UnitInstances));

            return includedUnitInstances.ToList();
        }

        includedUnitInstances.ExceptWith(exclusionsDelegate().SelectMany(static (unitList) => unitList.UnitInstances));

        return includedUnitInstances.ToList();
    }
}
