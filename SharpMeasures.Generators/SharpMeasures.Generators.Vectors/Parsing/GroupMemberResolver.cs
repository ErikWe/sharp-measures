namespace SharpMeasures.Generators.Vectors.Parsing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Units;
using SharpMeasures.Generators.Vectors.Parsing.Abstraction;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

internal static class GroupMemberResolver
{
    public static IncrementalValuesProvider<ResolvedVectorType> Resolve(IncrementalValuesProvider<GroupMemberType> vectorProvider, IncrementalValueProvider<IUnitPopulation> unitPopulationProvider,
        IncrementalValueProvider<IVectorPopulationWithData> vectorPopulationProvider)
    {
        return vectorProvider.Combine(unitPopulationProvider, vectorPopulationProvider).Select(Resolve);
    }

    private static ResolvedVectorType Resolve((GroupMemberType UnresolvedVector, IUnitPopulation UnitPopulation, IVectorPopulationWithData VectorPopulation) input, CancellationToken _)
    {
        var vectorGroupBase = input.VectorPopulation.GroupBases[input.UnresolvedVector.Definition.VectorGroup];
        var unit = input.UnitPopulation.Units[vectorGroupBase.Definition.Unit];

        var derivations = CollectItems(input.UnresolvedVector, input.VectorPopulation, static (vector) => vector.Derivations, static (vector) => vector.Derivations,
            static (vector) => vector.Definition.InheritDerivationsFromMembers, static (vector) => vector.Definition.InheritDerivations, static (vector) => vector.Definition.InheritDerivations);
        var constants = CollectItems(input.UnresolvedVector, input.VectorPopulation, static (vector) => vector.Constants, static (vector) => Array.Empty<IVectorConstant>(),
            static (vector) => vector.Definition.InheritConstantsFromMembers, static (vector) => false, static (vector) => false);
        var conversions = CollectItems(input.UnresolvedVector, input.VectorPopulation, static (vector) => vector.Conversions, static (vector) => vector.Conversions,
            static (vector) => vector.Definition.InheritConversionsFromMembers, static (vector) => vector.Definition.InheritConversions, static (vector) => vector.Definition.InheritConversions);

        var includedUnits = ResolveUnitInclusions(input.UnresolvedVector, input.VectorPopulation, unit);

        var scalar = RecursivelySearchForDefinedInGroups(input.UnresolvedVector, input.VectorPopulation, static (vector) => vector.Definition.Scalar);

        var implementSum = RecursivelySearchForDefinedInGroups(input.UnresolvedVector, input.VectorPopulation, static (vector) => vector.Definition.ImplementSum);
        var implementDifference = RecursivelySearchForDefinedInGroups(input.UnresolvedVector, input.VectorPopulation, static (vector) => vector.Definition.ImplementDifference);
        var difference = RecursivelySearchForDefinedInGroups(input.UnresolvedVector, input.VectorPopulation, static (vector) => vector.Definition.Difference);

        var defaultUnitName = RecursivelySearchForDefinedInGroups(input.UnresolvedVector, input.VectorPopulation, static (vector) => vector.Definition.DefaultUnitName);
        var defaultUnitSymbol = RecursivelySearchForDefinedInGroups(input.UnresolvedVector, input.VectorPopulation, static (vector) => vector.Definition.DefaultUnitSymbol);

        var generateDocumentation = RecursivelySearchForDefined(input.UnresolvedVector, input.VectorPopulation, static (vector) => vector.Definition.GenerateDocumentation, static (vector) => vector.Definition.GenerateDocumentation);

        return new(input.UnresolvedVector.Type, input.UnresolvedVector.TypeLocation, input.UnresolvedVector.Definition.Dimension, unit.Type.AsNamedType(), scalar, implementSum!.Value, implementDifference!.Value,
            difference, defaultUnitName, defaultUnitSymbol, derivations, constants, conversions, includedUnits, generateDocumentation);
    }

    private static IReadOnlyList<T> CollectItems<T>(GroupMemberType vectorType, IVectorPopulation vectorPopulation, Func<IVectorGroupMemberType, IEnumerable<T>> memberItemsDelegate,
        Func<IVectorGroupType, IEnumerable<T>> groupItemsDelegate, Func<IVectorGroupMemberType, bool> shouldMemberInheritFromMembers, Func<IVectorGroupMemberType, bool> shouldMemberInheritFromGroup,
        Func<IVectorGroupSpecializationType, bool> shouldGroupInherit)
    {
        List<T> items = new();

        recursivelyAddItems(vectorPopulation.Groups[vectorType.Definition.VectorGroup], vectorType, shouldMemberInheritFromMembers(vectorType), shouldMemberInheritFromGroup(vectorType));

        return items;

        void recursivelyAddItems(IVectorGroupType vectorGroup, IVectorGroupMemberType? correspondingMember, bool shouldInheritFromMember, bool shouldInheritFromGroup)
        {
            if (shouldInheritFromGroup)
            {
                items.AddRange(groupItemsDelegate(vectorGroup));
            }

            if (shouldInheritFromMember && correspondingMember is not null)
            {
                items.AddRange(memberItemsDelegate(correspondingMember));
            }

            recurse(vectorGroup, correspondingMember, shouldInheritFromMember, shouldInheritFromGroup);
        }

        void recurse(IVectorGroupType vectorGroup, IVectorGroupMemberType? correspondingMember, bool inheritedFromMember, bool inheritedFromGroup)
        {
            if (vectorGroup is IVectorGroupSpecializationType vectorGroupSpecialization)
            {
                var originalVectorGroup = vectorPopulation.Groups[vectorGroupSpecialization.Definition.OriginalVectorGroup];

                vectorPopulation.GroupMembersByGroup[originalVectorGroup.Type.AsNamedType()].GroupMembersByDimension.TryGetValue(vectorType.Definition.Dimension, out var originalCorrespondingMember);

                bool shouldInheritFromMember = inheritedFromMember && (originalCorrespondingMember is null || shouldMemberInheritFromMembers(originalCorrespondingMember));
                bool shouldInheritFromGroup = originalCorrespondingMember is not null && shouldMemberInheritFromGroup(originalCorrespondingMember) || shouldGroupInherit(vectorGroupSpecialization);

                recursivelyAddItems(originalVectorGroup, originalCorrespondingMember, shouldInheritFromMember, shouldInheritFromGroup);
            }
        }
    }

    private static IReadOnlyList<string> ResolveUnitInclusions(GroupMemberType vectorType, IVectorPopulation vectorPopulation, IUnitType unit)
    {
        HashSet<string> includedUnits = new(unit.UnitsByName.Keys);

        recursivelyModify(vectorPopulation.Groups[vectorType.Definition.VectorGroup], vectorType, vectorType.Definition.InheritUnitsFromMembers, vectorType.Definition.InheritUnits);

        return includedUnits.ToList();

        void recursivelyModify(IVectorGroupType vectorGroup, IVectorGroupMemberType? correspondingMember, bool shouldInheritFromMember, bool shouldInheritFromGroup)
        {
            if (shouldInheritFromGroup)
            {
                modify(vectorGroup.UnitInclusions, vectorGroup.UnitExclusions);
            }

            if (shouldInheritFromMember && correspondingMember is not null)
            {
                modify(correspondingMember.UnitInclusions, correspondingMember.UnitExclusions);
            }

            recurse(vectorGroup, correspondingMember, shouldInheritFromMember, shouldInheritFromGroup);
        }

        void modify(IReadOnlyList<IUnitList> inclusions, IReadOnlyList<IUnitList> exclusions)
        {
            if (inclusions.Any())
            {
                includedUnits.IntersectWith(inclusions.SelectMany(static (unitList) => unitList.Units));

                return;
            }

            includedUnits.ExceptWith(exclusions.SelectMany(static (unitList) => unitList.Units));
        }

        void recurse(IVectorGroupType vectorGroup, IVectorGroupMemberType? correspondingMember, bool inheritedFromMember, bool inheritedFromGroup)
        {
            if (vectorGroup is IVectorGroupSpecializationType vectorGroupSpecialization)
            {
                var originalVectorGroup = vectorPopulation.Groups[vectorGroupSpecialization.Definition.OriginalVectorGroup];

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

    private static T? RecursivelySearchForDefined<T>(GroupMemberType vectorType, IVectorPopulation vectorPopulation, Func<IVectorGroupMemberType, T?> memberItemDelegate,
        Func<IVectorGroupType, T?> groupItemDelegate)
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
                return recursivelySearch(vectorPopulation.Groups[vectorSpecialization.Definition.OriginalVectorGroup]);
            }

            return default;
        }
    }
}
