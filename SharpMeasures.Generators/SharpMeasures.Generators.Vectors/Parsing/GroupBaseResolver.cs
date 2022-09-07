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
    public static IncrementalValuesProvider<ResolvedGroupType> Resolve(IncrementalValuesProvider<GroupBaseType> vectorProvider, IncrementalValueProvider<IUnitPopulation> unitPopulationProvider, IncrementalValueProvider<IVectorPopulationWithData> vectorPopulationProvider)
    {
        return vectorProvider.Combine(unitPopulationProvider, vectorPopulationProvider).Select(Resolve);
    }

    private static ResolvedGroupType Resolve((GroupBaseType UnresolvedVector, IUnitPopulation UnitPopulation, IVectorPopulationWithData VectorPopulation) input, CancellationToken _)
    {
        var unit = input.UnitPopulation.Units[input.UnresolvedVector.Definition.Unit];

        var membersByDimension = ResolveMembers(input.UnresolvedVector, input.VectorPopulation);

        var includedUnitInstances = ResolveUnitInclusions(unit, input.UnresolvedVector.UnitInclusions, () => input.UnresolvedVector.UnitExclusions);

        return new(input.UnresolvedVector.Type, input.UnresolvedVector.TypeLocation, input.UnresolvedVector.Definition.Unit, input.UnresolvedVector.Definition.Scalar, input.UnresolvedVector.Definition.ImplementSum,
            input.UnresolvedVector.Definition.ImplementDifference, input.UnresolvedVector.Definition.Difference, input.UnresolvedVector.Definition.DefaultUnitInstanceName, input.UnresolvedVector.Definition.DefaultUnitInstanceSymbol,
            membersByDimension, input.UnresolvedVector.Derivations, Array.Empty<IDerivedQuantity>(), input.UnresolvedVector.Conversions, includedUnitInstances, input.UnresolvedVector.Definition.GenerateDocumentation);
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

    private static IReadOnlyDictionary<int, NamedType> ResolveMembers(GroupBaseType vectorType, IVectorPopulation vectorPopulation)
    {
        return vectorPopulation.GroupMembersByGroup[vectorType.Type.AsNamedType()].GroupMembersByDimension.Transform(static (vector) => vector.Type.AsNamedType());
    }
}
