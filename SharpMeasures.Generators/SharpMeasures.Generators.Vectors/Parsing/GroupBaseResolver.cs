namespace SharpMeasures.Generators.Vectors.Parsing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Units;
using SharpMeasures.Generators.Vectors.Parsing.Abstraction;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

internal static class GroupBaseResolver
{
    public static IncrementalValuesProvider<Optional<ResolvedGroupType>> Resolve(IncrementalValuesProvider<Optional<GroupBaseType>> vectorProvider, IncrementalValueProvider<IUnitPopulation> unitPopulationProvider, IncrementalValueProvider<IVectorPopulationWithData> vectorPopulationProvider)
    {
        return vectorProvider.Combine(unitPopulationProvider, vectorPopulationProvider).Select(Resolve);
    }

    private static Optional<ResolvedGroupType> Resolve((Optional<GroupBaseType> UnresolvedVector, IUnitPopulation UnitPopulation, IVectorPopulationWithData VectorPopulation) input, CancellationToken token)
    {
        if (token.IsCancellationRequested || input.UnresolvedVector.HasValue is false)
        {
            return new Optional<ResolvedGroupType>();
        }

        return Resolve(input.UnresolvedVector.Value, input.UnitPopulation, input.VectorPopulation);
    }

    private static ResolvedGroupType Resolve(GroupBaseType vectorType, IUnitPopulation unitPopulation, IVectorPopulationWithData vectorPopulation)
    {
        var unit = unitPopulation.Units[vectorType.Definition.Unit];

        var membersByDimension = ResolveMembers(vectorType, vectorPopulation);

        var includedUnitInstances = ResolveUnitInstanceInclusions(unit, vectorType.UnitInstanceInclusions, () => vectorType.UnitInstanceExclusions);

        return new(vectorType.Type, vectorType.TypeLocation, vectorType.Definition.Unit, vectorType.Definition.Scalar, vectorType.Definition.ImplementSum,
            vectorType.Definition.ImplementDifference, vectorType.Definition.Difference, vectorType.Definition.DefaultUnitInstanceName, vectorType.Definition.DefaultUnitInstanceSymbol,
            membersByDimension, vectorType.Derivations, Array.Empty<IDerivedQuantity>(), vectorType.Conversions, includedUnitInstances, vectorType.Definition.GenerateDocumentation);
    }

    private static IReadOnlyList<string> ResolveUnitInstanceInclusions(IUnitType unit, IEnumerable<IUnitInstanceList> inclusions, Func<IEnumerable<IUnitInstanceList>> exclusionsDelegate)
    {
        if (inclusions.Any())
        {
            return inclusions.SelectMany(static (unitList) => unitList.UnitInstances).ToList();
        }

        HashSet<string> includedUnits = new(unit.UnitInstancesByName.Keys);

        includedUnits.ExceptWith(exclusionsDelegate().SelectMany(static (unitList) => unitList.UnitInstances));

        return includedUnits.ToList();
    }

    private static IReadOnlyDictionary<int, NamedType> ResolveMembers(GroupBaseType vectorType, IVectorPopulation vectorPopulation)
    {
        return vectorPopulation.GroupMembersByGroup[vectorType.Type.AsNamedType()].GroupMembersByDimension.Transform(static (vector) => vector.Type.AsNamedType());
    }
}
