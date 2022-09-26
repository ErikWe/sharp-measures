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
    public static IncrementalValuesProvider<Optional<ResolvedScalarType>> Resolve(IncrementalValuesProvider<Optional<ScalarBaseType>> scalarProvider, IncrementalValueProvider<IUnitPopulation> unitPopulationProvider) => scalarProvider.Combine(unitPopulationProvider).Select(Resolve);

    private static Optional<ResolvedScalarType> Resolve((Optional<ScalarBaseType> UnvalidatedScalar, IUnitPopulation UnitPopulation) input, CancellationToken token)
    {
        if (token.IsCancellationRequested || input.UnvalidatedScalar.HasValue is false)
        {
            return new Optional<ResolvedScalarType>();
        }

        return Resolve(input.UnvalidatedScalar.Value, input.UnitPopulation);
    }

    public static Optional<ResolvedScalarType> Resolve(ScalarBaseType scalarType, IUnitPopulation unitPopulation)
    {
        if (unitPopulation.Units.TryGetValue(scalarType.Definition.Unit, out var unit) is false)
        {
            return new Optional<ResolvedScalarType>();
        }

        var includedUnitBaseInstances = ResolveUnitInstanceInclusions(unit, scalarType.UnitBaseInstanceInclusions, () => scalarType.UnitBaseInstanceExclusions);
        var includedUnitInstances = ResolveUnitInstanceInclusions(unit, scalarType.UnitInstanceInclusions, () => scalarType.UnitInstanceExclusions);

        return new ResolvedScalarType(scalarType.Type, scalarType.Definition.Unit, scalarType.Definition.UseUnitBias, originalQuantity: null, ConversionOperatorBehaviour.None, ConversionOperatorBehaviour.None, scalarType.Definition.Vector, scalarType.Definition.ImplementSum,
            scalarType.Definition.ImplementDifference, scalarType.Definition.Difference, scalarType.Definition.DefaultUnitInstanceName, scalarType.Definition.DefaultUnitInstanceSymbol, scalarType.Derivations, scalarType.Constants, scalarType.Conversions,
            inheritedDerivations: Array.Empty<IDerivedQuantity>(), inheritedConversions: Array.Empty<IConvertibleQuantity>(), includedUnitBaseInstances, includedUnitInstances, scalarType.Definition.GenerateDocumentation);
    }

    private static IReadOnlyList<string> ResolveUnitInstanceInclusions(IUnitType unit, IEnumerable<IUnitInstanceList> inclusions, Func<IEnumerable<IUnitInstanceList>> exclusionsDelegate)
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
