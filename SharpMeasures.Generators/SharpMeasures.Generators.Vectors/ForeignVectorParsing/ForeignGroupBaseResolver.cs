namespace SharpMeasures.Generators.Vectors.ForeignVectorParsing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Units;

using System;
using System.Collections.Generic;
using System.Linq;

internal static class ForeignGroupBaseResolver
{
    public static Optional<ResolvedGroupType> Resolve(GroupBaseType groupType, IUnitPopulation unitPopulation, IVectorPopulation vectorPopulation)
    {
        if (unitPopulation.Units.TryGetValue(groupType.Definition.Unit, out var unit) is false)
        {
            return new Optional<ResolvedGroupType>();
        }

        var membersByDimension = ResolveMembers(groupType, vectorPopulation);

        var includedUnitInstances = ResolveUnitInstanceInclusions(unit, groupType.UnitInstanceInclusions, () => groupType.UnitInstanceExclusions);

        return new ResolvedGroupType(groupType.Type, MinimalLocation.None, groupType.Definition.Unit, originalQuantity: null, ConversionOperatorBehaviour.None, ConversionOperatorBehaviour.None, groupType.Definition.Scalar, groupType.Definition.ImplementSum, groupType.Definition.ImplementDifference, groupType.Definition.Difference, groupType.Definition.DefaultUnitInstanceName,
            groupType.Definition.DefaultUnitInstanceSymbol, membersByDimension, groupType.Derivations, groupType.Conversions, inheritedDerivations: Array.Empty<IDerivedQuantity>(), inheritedConversions: Array.Empty<IConvertibleQuantity>(), includedUnitInstances, groupType.Definition.GenerateDocumentation);
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

    private static IReadOnlyDictionary<int, NamedType> ResolveMembers(IVectorGroupBaseType groupType, IVectorPopulation vectorPopulation) => vectorPopulation.GroupMembersByGroup[groupType.Type.AsNamedType()].GroupMembersByDimension.Transform(static (vector) => vector.Type.AsNamedType());
}
