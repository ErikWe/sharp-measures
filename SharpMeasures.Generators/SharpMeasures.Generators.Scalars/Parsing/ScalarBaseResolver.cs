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

    private static Optional<ResolvedScalarType> Resolve((ScalarBaseType UnvalidatedScalar, IUnitPopulation UnitPopulation) input, CancellationToken token) => Resolve(input.UnvalidatedScalar, input.UnitPopulation, token);

    private static Optional<ResolvedScalarType> Resolve(ScalarBaseType scalarType, IUnitPopulation unitPopulation, CancellationToken token)
    {
        if (token.IsCancellationRequested)
        {
            return new Optional<ResolvedScalarType>();
        }

        if (unitPopulation.Units.TryGetValue(scalarType.Definition.Unit, out var unit) is false)
        {
            return new Optional<ResolvedScalarType>();
        }

        var includedUnitBaseInstances = ResolveUnitInclusions(unit, scalarType.UnitBaseInstanceInclusions, () => scalarType.UnitBaseInstanceExclusions);
        var includedUnitInstances = ResolveUnitInclusions(unit, scalarType.UnitInstanceInclusions, () => scalarType.UnitInstanceExclusions);

        return new ResolvedScalarType(scalarType.Type, scalarType.TypeLocation, scalarType.Definition.Unit, scalarType.Definition.UseUnitBias, scalarType.Definition.Vector, scalarType.Definition.Reciprocal,
            scalarType.Definition.Square, scalarType.Definition.Cube, scalarType.Definition.SquareRoot, scalarType.Definition.CubeRoot, scalarType.Definition.ImplementSum, scalarType.Definition.ImplementDifference,
            scalarType.Definition.Difference, scalarType.Definition.DefaultUnitInstanceName, scalarType.Definition.DefaultUnitInstanceSymbol, scalarType.Derivations, Array.Empty<IDerivedQuantity>(),
            scalarType.Constants, scalarType.Conversions, includedUnitBaseInstances, includedUnitInstances, scalarType.Definition.GenerateDocumentation);
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
