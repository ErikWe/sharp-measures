namespace SharpMeasures.Generators.Vectors.Parsing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Units;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

internal static class GroupSpecializationResolver
{
    public static IncrementalValuesProvider<Optional<ResolvedGroupType>> Resolve(IncrementalValuesProvider<Optional<GroupSpecializationType>> groupProvider, IncrementalValueProvider<IUnitPopulation> unitPopulationProvider, IncrementalValueProvider<IVectorPopulation> vectorPopulationProvider)
    {
        return groupProvider.Combine(unitPopulationProvider, vectorPopulationProvider).Select(Resolve);
    }

    private static Optional<ResolvedGroupType> Resolve((Optional<GroupSpecializationType> UnresolvedGroup, IUnitPopulation UnitPopulation, IVectorPopulation VectorPopulation) input, CancellationToken token)
    {
        if (token.IsCancellationRequested || input.UnresolvedGroup.HasValue is false)
        {
            return new Optional<ResolvedGroupType>();
        }

        return Resolve(input.UnresolvedGroup.Value, input.UnitPopulation, input.VectorPopulation);
    }

    private static Optional<ResolvedGroupType> Resolve(GroupSpecializationType groupType, IUnitPopulation unitPopulation, IVectorPopulation vectorPopulation)
    {
        if (vectorPopulation.GroupBases.TryGetValue(groupType.Type.AsNamedType(), out var groupBase) is false || unitPopulation.Units.TryGetValue(groupBase.Definition.Unit, out var unit) is false)
        {
            return new Optional<ResolvedGroupType>();
        }

        var membersByDimension = ResolveMembers(groupType, vectorPopulation);

        var inheritedDerivations = CollectItems(groupType, vectorPopulation, static (vector) => vector.Derivations, static (vector) => vector.Definition.InheritDerivations, onlyInherited: true);
        var inheritedConversions = CollectItems(groupType, vectorPopulation, static (vector) => vector.Conversions, static (vector) => vector.Definition.InheritConversions, onlyInherited: true);

        var includedUnitInstances = ResolveUnitInstanceInclusions(groupType, vectorPopulation, unit);

        var scalar = RecursivelySearchForDefined(groupType, vectorPopulation, static (vector) => vector.Definition.Scalar);

        var implementSum = RecursivelySearchForDefined(groupType, vectorPopulation, static (vector) => vector.Definition.ImplementSum);
        var implementDifference = RecursivelySearchForDefined(groupType, vectorPopulation, static (vector) => vector.Definition.ImplementDifference);
        var difference = ResolveDifference(groupType, vectorPopulation);

        var defaultUnitInstanceName = RecursivelySearchForDefined(groupType, vectorPopulation, static (vector) => vector.Definition.DefaultUnitInstanceName);
        var defaultUnitInstanceSymbol = RecursivelySearchForDefined(groupType, vectorPopulation, static (vector) => vector.Definition.DefaultUnitInstanceSymbol);

        var generateDocumentation = RecursivelySearchForDefined(groupType, vectorPopulation, static (vector) => vector.Definition.GenerateDocumentation);

        return new ResolvedGroupType(groupType.Type, groupType.TypeLocation, unit.Type.AsNamedType(), groupType.Definition.OriginalQuantity, groupType.Definition.ForwardsCastOperatorBehaviour, groupType.Definition.BackwardsCastOperatorBehaviour, scalar, implementSum!.Value, implementDifference!.Value,
            difference, defaultUnitInstanceName, defaultUnitInstanceSymbol, membersByDimension, groupType.Derivations, groupType.Conversions, inheritedDerivations, inheritedConversions, includedUnitInstances, generateDocumentation);
    }

    private static IReadOnlyDictionary<int, NamedType> ResolveMembers(GroupSpecializationType groupType, IVectorPopulation vectorPopulation) => vectorPopulation.GroupMembersByGroup[groupType.Type.AsNamedType()].GroupMembersByDimension.Transform(static (vector) => vector.Type.AsNamedType());

    private static NamedType? ResolveDifference(GroupSpecializationType groupType, IVectorPopulation vectorPopulation)
    {
        var difference = RecursivelySearchForMatching(groupType, vectorPopulation, static (scalar) => scalar.Definition.Difference, static (vector, _) => vector.Definition.Locations.ExplicitlySetDifference);

        if (difference is null && groupType.Definition.Locations.ExplicitlySetDifference is false)
        {
            difference = groupType.Type.AsNamedType();
        }

        return difference;
    }

    private static IReadOnlyList<T> CollectItems<T>(GroupSpecializationType groupType, IVectorPopulation vectorPopulation, Func<IVectorGroupType, IEnumerable<T>> itemsDelegate, Func<IVectorGroupSpecializationType, bool> shouldInherit, bool onlyInherited = false)
    {
        List<T> items = new();

        recursivelyAddItems(groupType, onlyInherited);

        return items;

        void recursivelyAddItems(IVectorGroupType vector, bool onlyInherited)
        {
            if (onlyInherited is false)
            {
                items.AddRange(itemsDelegate(vector));
            }

            if (vector is IVectorGroupSpecializationType vectorSpecialization && shouldInherit(vectorSpecialization))
            {
                recursivelyAddItems(vectorPopulation.Groups[vectorSpecialization.Definition.OriginalQuantity], onlyInherited: false);
            }
        }
    }

    private static IReadOnlyList<string> ResolveUnitInstanceInclusions(GroupSpecializationType groupType, IVectorPopulation vectorPopulation, IUnitType unit)
    {
        HashSet<string> includedUnits = new(unit.UnitInstancesByName.Keys);

        recurisvelyModify(groupType);

        return includedUnits.ToList();

        void recurisvelyModify(IVectorGroupType vector)
        {
            modify(vector);

            recurse(vector);
        }

        void modify(IVectorGroupType vector)
        {
            if (vector.UnitInstanceInclusions.Any())
            {
                includedUnits.IntersectWith(vector.UnitInstanceInclusions.SelectMany(static (unitList) => unitList.UnitInstances));

                return;
            }

            includedUnits.ExceptWith(vector.UnitInstanceExclusions.SelectMany(static (unitList) => unitList.UnitInstances));
        }

        void recurse(IVectorGroupType vector)
        {
            if (vector is IVectorGroupSpecializationType scalarSpecialization && scalarSpecialization.Definition.InheritUnits)
            {
                recurisvelyModify(vectorPopulation.Groups[scalarSpecialization.Definition.OriginalQuantity]);
            }
        }
    }

    private static T? RecursivelySearchForDefined<T>(GroupSpecializationType groupType, IVectorPopulation vectorPopulation, Func<IVectorGroupType, T?> itemDelegate) => RecursivelySearchForMatching(groupType, vectorPopulation, itemDelegate, static (_, item) => item is not null);
    private static T? RecursivelySearchForMatching<T>(GroupSpecializationType groupType, IVectorPopulation vectorPopulation, Func<IVectorGroupType, T?> itemDelegate, Func<IVectorGroupType, T?, bool> predicate)
    {
        return recursivelySearch(groupType);

        T? recursivelySearch(IVectorGroupType vector)
        {
            var item = itemDelegate(vector);

            if (predicate(vector, item))
            {
                return item;
            }

            if (vector is IVectorGroupSpecializationType groupSpecialization)
            {
                return recursivelySearch(vectorPopulation.Groups[groupSpecialization.Definition.OriginalQuantity]);
            }

            return default;
        }
    }
}
