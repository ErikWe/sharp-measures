namespace SharpMeasures.Generators.Vectors.Parsing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Units;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

internal static class VectorBaseResolver
{
    public static IncrementalValuesProvider<Optional<ResolvedVectorType>> Resolve(IncrementalValuesProvider<Optional<VectorBaseType>> vectorProvider, IncrementalValueProvider<IUnitPopulation> unitPopulationProvider) => vectorProvider.Combine(unitPopulationProvider).Select(Resolve);

    private static Optional<ResolvedVectorType> Resolve((Optional<VectorBaseType> UnresolvedVector, IUnitPopulation UnitPopulation) input, CancellationToken token)
    {
        if (token.IsCancellationRequested || input.UnresolvedVector.HasValue is false)
        {
            return new Optional<ResolvedVectorType>();
        }

        return Resolve(input.UnresolvedVector.Value, input.UnitPopulation);
    }

    public static Optional<ResolvedVectorType> Resolve(VectorBaseType vectorType, IUnitPopulation unitPopulation)
    {
        if (unitPopulation.Units.TryGetValue(vectorType.Definition.Unit, out var unit) is false)
        {
            return new Optional<ResolvedVectorType>();
        }

        var includedUnitInstances = ResolveUnitInstanceInclusions(unit, vectorType.UnitInstanceInclusions, () => vectorType.UnitInstanceExclusions);

        return new ResolvedVectorType(vectorType.Type, vectorType.Definition.Dimension, group: null, vectorType.Definition.Unit, originalQuantity: null, ConversionOperatorBehaviour.None, ConversionOperatorBehaviour.None, vectorType.Definition.Scalar, vectorType.Definition.ImplementSum, vectorType.Definition.ImplementDifference, vectorType.Definition.Difference, vectorType.Definition.DefaultUnitInstanceName, vectorType.Definition.DefaultUnitInstanceSymbol,
            vectorType.Operations, vectorType.VectorOperations, vectorType.Processes, vectorType.Constants, vectorType.Conversions, inheritedOperations: Array.Empty<IQuantityOperation>(), inheritedVectorOperations: Array.Empty<IVectorOperation>(), inheritedProcesses: Array.Empty<IQuantityProcess>(), inheritedConstants: Array.Empty<IVectorConstant>(), inheritedConversions: Array.Empty<IConvertibleQuantity>(), includedUnitInstances);
    }

    private static IReadOnlyList<string> ResolveUnitInstanceInclusions(IUnitType unit, IEnumerable<IUnitInstanceList> unitInstanceInclusions, Func<IEnumerable<IUnitInstanceList>> exclusionsDelegate)
    {
        if (unitInstanceInclusions.Any())
        {
            return unitInstanceInclusions.SelectMany(static (unitList) => unitList.UnitInstances).ToList();
        }

        HashSet<string> includedUnits = new(unit.UnitInstancesByName.Keys);

        includedUnits.ExceptWith(exclusionsDelegate().SelectMany(static (unitList) => unitList.UnitInstances));

        return includedUnits.ToList();
    }
}
