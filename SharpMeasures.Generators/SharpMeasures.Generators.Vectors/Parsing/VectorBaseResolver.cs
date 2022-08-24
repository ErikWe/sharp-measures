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
    public static IncrementalValuesProvider<ResolvedVectorType> Resolve(IncrementalValuesProvider<VectorBaseType> vectorProvider, IncrementalValueProvider<IUnitPopulation> unitPopulationProvider)
    {
        return vectorProvider.Combine(unitPopulationProvider).Select(Resolve);
    }

    private static ResolvedVectorType Resolve((VectorBaseType UnresolvedVector, IUnitPopulation UnitPopulation) input, CancellationToken _)
    {
        var unit = input.UnitPopulation.Units[input.UnresolvedVector.Definition.Unit];

        var includedUnits = ResolveUnitInclusions(unit, input.UnresolvedVector.UnitInclusions, () => input.UnresolvedVector.UnitExclusions);

        return new(input.UnresolvedVector.Type, input.UnresolvedVector.TypeLocation, input.UnresolvedVector.Definition.Dimension, input.UnresolvedVector.Definition.Unit,
            input.UnresolvedVector.Definition.Scalar, input.UnresolvedVector.Definition.ImplementSum, input.UnresolvedVector.Definition.ImplementDifference,
            input.UnresolvedVector.Definition.Difference, input.UnresolvedVector.Definition.DefaultUnitName, input.UnresolvedVector.Definition.DefaultUnitSymbol, input.UnresolvedVector.Derivations,
            input.UnresolvedVector.Constants, input.UnresolvedVector.Conversions, includedUnits, input.UnresolvedVector.Definition.GenerateDocumentation);
    }

    private static IReadOnlyList<string> ResolveUnitInclusions(IUnitType unit, IEnumerable<IUnitList> inclusions, Func<IEnumerable<IUnitList>> exclusionsDelegate)
    {
        if (inclusions.Any())
        {
            return inclusions.SelectMany(static (unitList) => unitList.Units).ToList();
        }

        HashSet<string> includedUnits = new(unit.UnitsByName.Keys);

        includedUnits.ExceptWith(exclusionsDelegate().SelectMany(static (unitList) => unitList.Units));

        return includedUnits.ToList();
    }
}
