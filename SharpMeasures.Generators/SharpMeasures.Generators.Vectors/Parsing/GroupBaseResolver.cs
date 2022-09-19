﻿namespace SharpMeasures.Generators.Vectors.Parsing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Units;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

internal static class GroupBaseResolver
{
    public static IncrementalValuesProvider<Optional<ResolvedGroupType>> Resolve(IncrementalValuesProvider<Optional<GroupBaseType>> groupProvider, IncrementalValueProvider<IUnitPopulation> unitPopulationProvider, IncrementalValueProvider<IVectorPopulation> vectorPopulationProvider)
    {
        return groupProvider.Combine(unitPopulationProvider, vectorPopulationProvider).Select(Resolve);
    }

    private static Optional<ResolvedGroupType> Resolve((Optional<GroupBaseType> UnresolvedGroup, IUnitPopulation UnitPopulation, IVectorPopulation VectorPopulation) input, CancellationToken token)
    {
        if (token.IsCancellationRequested || input.UnresolvedGroup.HasValue is false)
        {
            return new Optional<ResolvedGroupType>();
        }

        return Resolve(input.UnresolvedGroup.Value, input.UnitPopulation, input.VectorPopulation);
    }

    private static Optional<ResolvedGroupType> Resolve(GroupBaseType groupType, IUnitPopulation unitPopulation, IVectorPopulation vectorPopulation)
    {
        if (unitPopulation.Units.TryGetValue(groupType.Definition.Unit, out var unit) is false)
        {
            return new Optional<ResolvedGroupType>();
        }

        var membersByDimension = ResolveMembers(groupType, vectorPopulation);

        var includedUnitInstances = ResolveUnitInstanceInclusions(unit, groupType.UnitInstanceInclusions, () => groupType.UnitInstanceExclusions);

        return new ResolvedGroupType(groupType.Type, groupType.TypeLocation, groupType.Definition.Unit, groupType.Definition.Scalar, groupType.Definition.ImplementSum, groupType.Definition.ImplementDifference, groupType.Definition.Difference, groupType.Definition.DefaultUnitInstanceName,
            groupType.Definition.DefaultUnitInstanceSymbol, membersByDimension, groupType.Derivations, Array.Empty<IDerivedQuantity>(), groupType.Conversions, includedUnitInstances, groupType.Definition.GenerateDocumentation);
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

    private static IReadOnlyDictionary<int, NamedType> ResolveMembers(GroupBaseType groupType, IVectorPopulation vectorPopulation) => vectorPopulation.GroupMembersByGroup[groupType.Type.AsNamedType()].GroupMembersByDimension.Transform(static (vector) => vector.Type.AsNamedType());
}
