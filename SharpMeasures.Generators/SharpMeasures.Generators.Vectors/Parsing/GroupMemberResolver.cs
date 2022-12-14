namespace SharpMeasures.Generators.Vectors.Parsing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Units;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

internal static class GroupMemberResolver
{
    public static IncrementalValuesProvider<Optional<ResolvedVectorType>> Resolve(IncrementalValuesProvider<Optional<GroupMemberType>> vectorProvider, IncrementalValueProvider<IUnitPopulation> unitPopulationProvider, IncrementalValueProvider<IVectorPopulation> vectorPopulationProvider)
    {
        return vectorProvider.Combine(unitPopulationProvider, vectorPopulationProvider).Select(Resolve);
    }

    private static Optional<ResolvedVectorType> Resolve((Optional<GroupMemberType> UnresolvedVector, IUnitPopulation UnitPopulation, IVectorPopulation VectorPopulation) input, CancellationToken token)
    {
        if (token.IsCancellationRequested || input.UnresolvedVector.HasValue is false)
        {
            return new Optional<ResolvedVectorType>();
        }

        return Resolve(input.UnresolvedVector.Value, input.UnitPopulation, input.VectorPopulation);
    }

    public static Optional<ResolvedVectorType> Resolve(GroupMemberType vectorType, IUnitPopulation unitPopulation, IVectorPopulation vectorPopulation)
    {
        if (vectorPopulation.GroupBases.TryGetValue(vectorType.Definition.VectorGroup, out var groupBase) is false || unitPopulation.Units.TryGetValue(groupBase.Definition.Unit, out var unit) is false)
        {
            return new Optional<ResolvedVectorType>();
        }

        var inheritedOperations = CollectItems(vectorType, vectorPopulation, static (vector) => vector.Operations, static (group) => group.Operations, static (vector) => vector.Definition.InheritOperationsFromMembers, static (vector) => vector.Definition.InheritOperations, static (group) => group.Definition.InheritOperations, onlyInherited: true);
        var inheritedVectorOperations = CollectItems(vectorType, vectorPopulation, static (vector) => vector.VectorOperations, static (group) => group.VectorOperations, static (vector) => vector.Definition.InheritOperationsFromMembers, static (vector) => vector.Definition.InheritOperations, static (group) => group.Definition.InheritOperations, onlyInherited: true);
        var inheritedProcesses = CollectItems(vectorType, vectorPopulation, static (vector) => vector.Processes, static (group) => Array.Empty<IQuantityProcess>(), static (vector) => vector.Definition.InheritProcessesFromMembers, static (group) => false, static (group) => false, onlyInherited: true);
        var inheritedConstants = CollectItems(vectorType, vectorPopulation, static (vector) => vector.Constants, static (group) => Array.Empty<IVectorConstant>(), static (vector) => vector.Definition.InheritConstantsFromMembers, static (vector) => false, static (group) => false, onlyInherited: true);
        var inheritedConversions = CollectItems(vectorType, vectorPopulation, static (vector) => vector.Conversions, static (group) => group.Conversions, static (vector) => vector.Definition.InheritConversionsFromMembers, static (vector) => vector.Definition.InheritConversions, static (group) => group.Definition.InheritConversions, onlyInherited: true);

        var includedUnitInstances = ResolveUnitInstanceInclusions(vectorType, vectorPopulation, unit);

        var scalar = RecursivelySearchForDefinedInGroups(vectorType, vectorPopulation, static (vector) => vector.Definition.Scalar);

        var implementSum = RecursivelySearchForDefinedInGroups(vectorType, vectorPopulation, static (vector) => vector.Definition.ImplementSum);
        var implementDifference = RecursivelySearchForDefinedInGroups(vectorType, vectorPopulation, static (vector) => vector.Definition.ImplementDifference);
        var difference = ResolveDifference(vectorType, vectorPopulation);

        var defaultUnitInstanceName = RecursivelySearchForDefinedInGroups(vectorType, vectorPopulation, static (vector) => vector.Definition.DefaultUnitInstanceName);
        var defaultUnitInstanceSymbol = RecursivelySearchForDefinedInGroups(vectorType, vectorPopulation, static (vector) => vector.Definition.DefaultUnitInstanceSymbol);

        (var forwardsCastBehaviour, var backwardsCastBehaviour) = GetSpecializationCastBehaviour(vectorType, vectorPopulation);

        return new ResolvedVectorType(vectorType.Type, vectorType.Definition.Dimension, vectorType.Definition.VectorGroup, unit.Type.AsNamedType(), originalQuantity: null, forwardsCastBehaviour, backwardsCastBehaviour, scalar, implementSum!.Value, implementDifference!.Value,
            difference, defaultUnitInstanceName, defaultUnitInstanceSymbol, vectorType.Operations, vectorType.VectorOperations, vectorType.Processes, vectorType.Constants, vectorType.Conversions, inheritedOperations, inheritedVectorOperations, inheritedProcesses, inheritedConstants, inheritedConversions, includedUnitInstances);
    }

    private static (ConversionOperatorBehaviour ForwardsCastBehaviour, ConversionOperatorBehaviour BackwardsCastBehaviour) GetSpecializationCastBehaviour(GroupMemberType vectorType, IVectorPopulation vectorPopulation)
    {
        if (vectorPopulation.Groups.TryGetValue(vectorType.Definition.VectorGroup, out var group) && group is IVectorGroupSpecializationType groupSpecialization)
        {
            return (groupSpecialization.Definition.ForwardsCastOperatorBehaviour, groupSpecialization.Definition.BackwardsCastOperatorBehaviour);
        }

        return (ConversionOperatorBehaviour.None, ConversionOperatorBehaviour.None);
    }

    private static NamedType? ResolveDifference(GroupMemberType vectorType, IVectorPopulation vectorPopulation)
    {
        var difference = RecursivelySearchForDefinedInGroups(vectorType, vectorPopulation, static (vector) => vector.Definition.Difference);

        if (difference is null)
        {
            return difference;
        }

        if (vectorPopulation.GroupMembersByGroup.TryGetValue(difference.Value, out var groupMembers) && groupMembers.GroupMembersByDimension.TryGetValue(vectorType.Definition.Dimension, out var correspondingMember))
        {
            return correspondingMember.Type.AsNamedType();
        }

        return null;
    }

    private static IReadOnlyList<T> CollectItems<T>(GroupMemberType vectorType, IVectorPopulation vectorPopulation, Func<IVectorGroupMemberType, IEnumerable<T>> memberItemsDelegate, Func<IVectorGroupType, IEnumerable<T>> groupItemsDelegate,
        Func<IVectorGroupMemberType, bool> shouldMemberInheritFromMembers, Func<IVectorGroupMemberType, bool> shouldMemberInheritFromGroup, Func<IVectorGroupSpecializationType, bool> shouldGroupInherit, bool onlyInherited = false)
    {
        if (vectorPopulation.Groups.TryGetValue(vectorType.Definition.VectorGroup, out var group) is false)
        {
            return Array.Empty<T>();
        }

        List<T> items = new();

        recursivelyAddItems(group, vectorType, shouldMemberInheritFromMembers(vectorType), shouldMemberInheritFromGroup(vectorType), onlyInherited);

        return items;

        void recursivelyAddItems(IVectorGroupType vectorGroup, IVectorGroupMemberType? correspondingMember, bool shouldInheritFromMember, bool shouldInheritFromGroup, bool onlyInherited)
        {
            if (shouldInheritFromGroup)
            {
                items.AddRange(groupItemsDelegate(vectorGroup));
            }

            if (shouldInheritFromMember && correspondingMember is not null && onlyInherited is false)
            {
                items.AddRange(memberItemsDelegate(correspondingMember));
            }

            recurse(vectorGroup, correspondingMember, shouldInheritFromMember, shouldInheritFromGroup);
        }

        void recurse(IVectorGroupType vectorGroup, IVectorGroupMemberType? correspondingMember, bool inheritedFromMember, bool inheritedFromGroup)
        {
            if (vectorGroup is IVectorGroupSpecializationType vectorGroupSpecialization)
            {
                if (vectorPopulation.Groups.TryGetValue(vectorGroupSpecialization.Definition.OriginalQuantity, out var originalVectorGroup) is false)
                {
                    return;
                }

                if (vectorPopulation.GroupMembersByGroup.TryGetValue(originalVectorGroup.Type.AsNamedType(), out var originalGroup) is false)
                {
                    return;
                }

                if (originalGroup.GroupMembersByDimension.TryGetValue(vectorType.Definition.Dimension, out var originalCorrespondingMember) is false)
                {
                    return;
                }

                var shouldInheritFromMember = inheritedFromMember && (originalCorrespondingMember is null || shouldMemberInheritFromMembers(originalCorrespondingMember));
                var shouldInheritFromGroup = (originalCorrespondingMember is not null && shouldMemberInheritFromGroup(originalCorrespondingMember)) || shouldGroupInherit(vectorGroupSpecialization);

                recursivelyAddItems(originalVectorGroup, originalCorrespondingMember, shouldInheritFromMember, shouldInheritFromGroup, onlyInherited: false);
            }
        }
    }

    private static IReadOnlyList<string> ResolveUnitInstanceInclusions(GroupMemberType vectorType, IVectorPopulation vectorPopulation, IUnitType unit)
    {
        if (vectorPopulation.Groups.TryGetValue(vectorType.Definition.VectorGroup, out var group) is false)
        {
            return Array.Empty<string>();
        }

        HashSet<string> includedUnits = new(unit.UnitInstancesByName.Keys);

        recursivelyModify(group, vectorType, vectorType.Definition.InheritUnitsFromMembers, vectorType.Definition.InheritUnits);

        return includedUnits.ToList();

        void recursivelyModify(IVectorGroupType vectorGroup, IVectorGroupMemberType? correspondingMember, bool shouldInheritFromMember, bool shouldInheritFromGroup)
        {
            if (shouldInheritFromGroup)
            {
                modify(vectorGroup.UnitInstanceInclusions, vectorGroup.UnitInstanceExclusions);
            }

            if (shouldInheritFromMember && correspondingMember is not null)
            {
                modify(correspondingMember.UnitInstanceInclusions, correspondingMember.UnitInstanceExclusions);
            }

            recurse(vectorGroup, correspondingMember, shouldInheritFromMember, shouldInheritFromGroup);
        }

        void modify(IReadOnlyList<IUnitInstanceList> inclusions, IReadOnlyList<IUnitInstanceList> exclusions)
        {
            if (inclusions.Count > 0)
            {
                includedUnits.IntersectWith(inclusions.SelectMany(static (unitList) => unitList.UnitInstances));

                return;
            }

            includedUnits.ExceptWith(exclusions.SelectMany(static (unitList) => unitList.UnitInstances));
        }

        void recurse(IVectorGroupType vectorGroup, IVectorGroupMemberType? correspondingMember, bool inheritedFromMember, bool inheritedFromGroup)
        {
            if (vectorGroup is IVectorGroupSpecializationType vectorGroupSpecialization)
            {
                if (vectorPopulation.Groups.TryGetValue(vectorGroupSpecialization.Definition.OriginalQuantity, out var originalVectorGroup) is false)
                {
                    return;
                }

                if (vectorPopulation.GroupMembersByGroup.TryGetValue(originalVectorGroup.Type.AsNamedType(), out var originalGroup) is false)
                {
                    return;
                }

                if (originalGroup.GroupMembersByDimension.TryGetValue(vectorType.Definition.Dimension, out var originalCorrespondingMember) is false)
                {
                    return;
                }

                var shouldInheritFromMember = inheritedFromMember && (originalCorrespondingMember is null || originalCorrespondingMember.Definition.InheritUnitsFromMembers);
                var shouldInheritFromGroup = (originalCorrespondingMember is not null && originalCorrespondingMember.Definition.InheritUnits) || vectorGroupSpecialization.Definition.InheritUnits;

                recursivelyModify(originalVectorGroup, originalCorrespondingMember, shouldInheritFromMember, shouldInheritFromGroup);
            }
        }
    }

    private static T? RecursivelySearchForDefinedInGroups<T>(GroupMemberType vectorType, IVectorPopulation vectorPopulation, Func<IVectorGroupType, T?> itemDelegate) => RecursivelySearchForDefined(vectorType, vectorPopulation, static (vector) => default, itemDelegate);
    private static T? RecursivelySearchForDefined<T>(GroupMemberType vectorType, IVectorPopulation vectorPopulation, Func<IVectorGroupMemberType, T?> memberItemDelegate, Func<IVectorGroupType, T?> groupItemDelegate)
    {
        if (memberItemDelegate(vectorType) is T item)
        {
            return item;
        }

        if (vectorPopulation.Groups.TryGetValue(vectorType.Definition.VectorGroup, out var group) is false)
        {
            return default;
        }

        return recursivelySearch(group);

        T? recursivelySearch(IVectorGroupType vectorGroup)
        {
            if (groupItemDelegate(vectorGroup) is T item)
            {
                return item;
            }

            if (vectorGroup is IVectorGroupSpecializationType vectorGroupSpecialization)
            {
                if (vectorPopulation.Groups.TryGetValue(vectorGroupSpecialization.Definition.OriginalQuantity, out var originalGroup))
                {
                    return recursivelySearch(originalGroup);
                }
            }

            return default;
        }
    }
}
