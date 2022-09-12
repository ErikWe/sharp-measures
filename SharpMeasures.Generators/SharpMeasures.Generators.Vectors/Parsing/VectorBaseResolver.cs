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
    public static IncrementalValuesProvider<Optional<ResolvedVectorType>> Resolve(IncrementalValuesProvider<Optional<VectorBaseType>> vectorProvider, IncrementalValueProvider<IUnitPopulation> unitPopulationProvider)
    {
        return vectorProvider.Combine(unitPopulationProvider).Select(Resolve);
    }

    private static Optional<ResolvedVectorType> Resolve((Optional<VectorBaseType> UnresolvedVector, IUnitPopulation UnitPopulation) input, CancellationToken token)
    {
        if (token.IsCancellationRequested || input.UnresolvedVector.HasValue is false)
        {
            return new Optional<ResolvedVectorType>();
        }

        return Resolve(input.UnresolvedVector.Value, input.UnitPopulation);
    }

    private static ResolvedVectorType Resolve(VectorBaseType vectorType, IUnitPopulation unitPopulation)
    {
        var unit = unitPopulation.Units[vectorType.Definition.Unit];

        var includedUnits = ResolveUnitInclusions(unit, vectorType.UnitInstanceInclusions, () => vectorType.UnitInstanceExclusions);

        return new(vectorType.Type, vectorType.TypeLocation, vectorType.Definition.Dimension, vectorType.Definition.Unit,
            vectorType.Definition.Scalar, vectorType.Definition.ImplementSum, vectorType.Definition.ImplementDifference,
            vectorType.Definition.Difference, vectorType.Definition.DefaultUnitInstanceName, vectorType.Definition.DefaultUnitInstanceSymbol, vectorType.Derivations,
            Array.Empty<IDerivedQuantity>(), vectorType.Constants, vectorType.Conversions, includedUnits, vectorType.Definition.GenerateDocumentation);
    }

    private static IReadOnlyList<string> ResolveUnitInclusions(IUnitType unit, IEnumerable<IUnitInstanceList> inclusions, Func<IEnumerable<IUnitInstanceList>> exclusionsDelegate)
    {
        if (inclusions.Any())
        {
            return inclusions.SelectMany(static (unitList) => unitList.UnitInstances).ToList();
        }

        HashSet<string> includedUnits = new(unit.UnitInstancesByName.Keys);

        includedUnits.ExceptWith(exclusionsDelegate().SelectMany(static (unitList) => unitList.UnitInstances));

        return includedUnits.ToList();
    }
}
