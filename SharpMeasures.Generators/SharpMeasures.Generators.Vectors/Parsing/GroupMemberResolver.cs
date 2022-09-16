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

    private static Optional<ResolvedVectorType> Resolve(GroupMemberType vectorType, IUnitPopulation unitPopulation, IVectorPopulation vectorPopulation)
    {
        if (vectorPopulation.GroupBases.TryGetValue(vectorType.Definition.VectorGroup, out var groupBase) is false || unitPopulation.Units.TryGetValue(groupBase.Definition.Unit, out var unit) is false)
        {
            return new Optional<ResolvedVectorType>();
        }

        var definedDerivations = vectorType.Derivations;
        var inheritedDerivations = CollectItems(vectorType, vectorPopulation, static (vector) => vector.Derivations, static (vector) => vector.Derivations,
            static (vector) => vector.Definition.InheritDerivationsFromMembers, static (vector) => vector.Definition.InheritDerivations, static (vector) => vector.Definition.InheritDerivations, onlyInherited: true);

        var constants = CollectItems(vectorType, vectorPopulation, static (vector) => vector.Constants, static (vector) => Array.Empty<IVectorConstant>(),
            static (vector) => vector.Definition.InheritConstantsFromMembers, static (vector) => false, static (vector) => false);
        var conversions = CollectItems(vectorType, vectorPopulation, static (vector) => vector.Conversions, static (vector) => vector.Conversions,
            static (vector) => vector.Definition.InheritConversionsFromMembers, static (vector) => vector.Definition.InheritConversions, static (vector) => vector.Definition.InheritConversions);

        var includedUnitInstances = ResolveUnitInstanceInclusions(vectorType, vectorPopulation, unit);

        var scalar = RecursivelySearchForDefinedInGroups(vectorType, vectorPopulation, static (vector) => vector.Definition.Scalar);

        var implementSum = RecursivelySearchForDefinedInGroups(vectorType, vectorPopulation, static (vector) => vector.Definition.ImplementSum);
        var implementDifference = RecursivelySearchForDefinedInGroups(vectorType, vectorPopulation, static (vector) => vector.Definition.ImplementDifference);
        var difference = ResolveDifference(vectorType, vectorPopulation);

        var defaultUnitInstanceName = RecursivelySearchForDefinedInGroups(vectorType, vectorPopulation, static (vector) => vector.Definition.DefaultUnitInstanceName);
        var defaultUnitInstanceSymbol = RecursivelySearchForDefinedInGroups(vectorType, vectorPopulation, static (vector) => vector.Definition.DefaultUnitInstanceSymbol);

        var generateDocumentation = RecursivelySearchForDefined(vectorType, vectorPopulation, static (vector) => vector.Definition.GenerateDocumentation, static (vector) => vector.Definition.GenerateDocumentation);

        return new ResolvedVectorType(vectorType.Type, vectorType.TypeLocation, vectorType.Definition.Dimension, unit.Type.AsNamedType(), scalar, implementSum!.Value, implementDifference!.Value,
            difference, defaultUnitInstanceName, defaultUnitInstanceSymbol, definedDerivations, inheritedDerivations, constants, conversions, includedUnitInstances, generateDocumentation);
    }

    private static NamedType? ResolveDifference(GroupMemberType vectorType, IVectorPopulation vectorPopulation)
    {
        var difference = RecursivelySearchForDefinedInGroups(vectorType, vectorPopulation, static (vector) => vector.Definition.Difference);

        if (difference is null)
        {
            return difference;
        }

        if (vectorPopulation.GroupMembersByGroup[difference.Value].GroupMembersByDimension.TryGetValue(vectorType.Definition.Dimension, out var correspondingMember))
        {
            return correspondingMember.Type.AsNamedType();
        }

        return null;
    }

    private static IReadOnlyList<T> CollectItems<T>(GroupMemberType vectorType, IVectorPopulation vectorPopulation, Func<IVectorGroupMemberType, IEnumerable<T>> memberItemsDelegate, Func<IVectorGroupType, IEnumerable<T>> groupItemsDelegate,
        Func<IVectorGroupMemberType, bool> shouldMemberInheritFromMembers, Func<IVectorGroupMemberType, bool> shouldMemberInheritFromGroup, Func<IVectorGroupSpecializationType, bool> shouldGroupInherit, bool onlyInherited = false)
    {
        List<T> items = new();

        recursivelyAddItems(vectorPopulation.Groups[vectorType.Definition.VectorGroup], vectorType, shouldMemberInheritFromMembers(vectorType), shouldMemberInheritFromGroup(vectorType), onlyInherited);

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
                var originalVectorGroup = vectorPopulation.Groups[vectorGroupSpecialization.Definition.OriginalQuantity];

                vectorPopulation.GroupMembersByGroup[originalVectorGroup.Type.AsNamedType()].GroupMembersByDimension.TryGetValue(vectorType.Definition.Dimension, out var originalCorrespondingMember);

                bool shouldInheritFromMember = inheritedFromMember && (originalCorrespondingMember is null || shouldMemberInheritFromMembers(originalCorrespondingMember));
                bool shouldInheritFromGroup = originalCorrespondingMember is not null && shouldMemberInheritFromGroup(originalCorrespondingMember) || shouldGroupInherit(vectorGroupSpecialization);

                recursivelyAddItems(originalVectorGroup, originalCorrespondingMember, shouldInheritFromMember, shouldInheritFromGroup, onlyInherited: false);
            }
        }
    }

    private static IReadOnlyList<string> ResolveUnitInstanceInclusions(GroupMemberType vectorType, IVectorPopulation vectorPopulation, IUnitType unit)
    {
        HashSet<string> includedUnits = new(unit.UnitInstancesByName.Keys);

        recursivelyModify(vectorPopulation.Groups[vectorType.Definition.VectorGroup], vectorType, vectorType.Definition.InheritUnitsFromMembers, vectorType.Definition.InheritUnits);

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
            if (inclusions.Any())
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
                var originalVectorGroup = vectorPopulation.Groups[vectorGroupSpecialization.Definition.OriginalQuantity];

                vectorPopulation.GroupMembersByGroup[originalVectorGroup.Type.AsNamedType()].GroupMembersByDimension.TryGetValue(vectorType.Definition.Dimension, out var originalCorrespondingMember);

                bool shouldInheritFromMember = inheritedFromMember && (originalCorrespondingMember is null || originalCorrespondingMember.Definition.InheritUnitsFromMembers);
                bool shouldInheritFromGroup = originalCorrespondingMember is not null && originalCorrespondingMember.Definition.InheritUnits || vectorGroupSpecialization.Definition.InheritUnits;

                recursivelyModify(originalVectorGroup, originalCorrespondingMember, shouldInheritFromMember, shouldInheritFromGroup);
            }
        }
    }

    private static T? RecursivelySearchForDefinedInGroups<T>(GroupMemberType vectorType, IVectorPopulation vectorPopulation, Func<IVectorGroupType, T?> itemDelegate)
    {
        return RecursivelySearchForDefined(vectorType, vectorPopulation, static (vector) => default, itemDelegate);
    }

    private static T? RecursivelySearchForDefined<T>(GroupMemberType vectorType, IVectorPopulation vectorPopulation, Func<IVectorGroupMemberType, T?> memberItemDelegate, Func<IVectorGroupType, T?> groupItemDelegate)
    {
        if (memberItemDelegate(vectorType) is T item)
        {
            return item;
        }

        return recursivelySearch(vectorPopulation.Groups[vectorType.Definition.VectorGroup]);

        T? recursivelySearch(IVectorGroupType vector)
        {
            if (groupItemDelegate(vector) is T item)
            {
                return item;
            }

            if (vector is IVectorGroupSpecializationType vectorSpecialization)
            {
                return recursivelySearch(vectorPopulation.Groups[vectorSpecialization.Definition.OriginalQuantity]);
            }

            return default;
        }
    }
}
