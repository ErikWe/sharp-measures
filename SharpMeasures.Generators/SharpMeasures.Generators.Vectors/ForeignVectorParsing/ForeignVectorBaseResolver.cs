namespace SharpMeasures.Generators.Vectors.ForeignVectorParsing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Units;

using System;
using System.Collections.Generic;
using System.Linq;

internal static class ForeignVectorBaseResolver
{
    public static Optional<ResolvedVectorType> Resolve(VectorBaseType vectorType, IUnitPopulation unitPopulation)
    {
        if (unitPopulation.Units.TryGetValue(vectorType.Definition.Unit, out var unit) is false)
        {
            return new Optional<ResolvedVectorType>();
        }

        var includedUnitInstances = ResolveUnitInstanceInclusions(unit, vectorType.UnitInstanceInclusions, () => vectorType.UnitInstanceExclusions);

        return new ResolvedVectorType(vectorType.Type, MinimalLocation.None, vectorType.Definition.Dimension, vectorType.Definition.Unit, vectorType.Definition.Scalar, vectorType.Definition.ImplementSum,
            vectorType.Definition.ImplementDifference, vectorType.Definition.Difference, vectorType.Definition.DefaultUnitInstanceName, vectorType.Definition.DefaultUnitInstanceSymbol, vectorType.Derivations,
            Array.Empty<IDerivedQuantity>(), vectorType.Constants, vectorType.Conversions, includedUnitInstances, vectorType.Definition.GenerateDocumentation);
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
