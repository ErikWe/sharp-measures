namespace SharpMeasures.Generators.Vectors.Parsing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Units;
using SharpMeasures.Generators.Vectors.Parsing.Abstraction;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

internal static class GroupSpecializationResolver
{
    public static IncrementalValuesProvider<Optional<ResolvedGroupType>> Resolve(IncrementalValuesProvider<Optional<GroupSpecializationType>> vectorProvider, IncrementalValueProvider<IUnitPopulation> unitPopulationProvider,
        IncrementalValueProvider<IVectorPopulationWithData> vectorPopulationProvider)
    {
        return vectorProvider.Combine(unitPopulationProvider, vectorPopulationProvider).Select(Resolve);
    }

    private static Optional<ResolvedGroupType> Resolve((Optional<GroupSpecializationType> UnresolvedVector, IUnitPopulation UnitPopulation, IVectorPopulationWithData VectorPopulation) input, CancellationToken token)
    {
        if (token.IsCancellationRequested || input.UnresolvedVector.HasValue is false)
        {
            return new Optional<ResolvedGroupType>();
        }

        return Resolve(input.UnresolvedVector.Value, input.UnitPopulation, input.VectorPopulation);
    }

    private static ResolvedGroupType Resolve(GroupSpecializationType vectorType, IUnitPopulation unitPopulation, IVectorPopulationWithData vectorPopulation)
    {
        var vectorBase = vectorPopulation.GroupBases[vectorType.Type.AsNamedType()];
        var unit = unitPopulation.Units[vectorBase.Definition.Unit];

        var membersByDimension = ResolveMembers(vectorType, vectorPopulation);

        var definedDerivations = vectorType.Derivations;
        var inheritedDerivations = CollectItems(vectorType, vectorPopulation, static (vector) => vector.Derivations, static (vector) => vector.Definition.InheritDerivations, onlyInherited: true);

        var conversions = CollectItems(vectorType, vectorPopulation, static (vector) => vector.Conversions, static (vector) => vector.Definition.InheritConversions);

        var includedUnitInstances = ResolveUnitInstanceInclusions(vectorType, vectorPopulation, unit);

        var scalar = RecursivelySearchForDefined(vectorType, vectorPopulation, static (vector) => vector.Definition.Scalar);

        var implementSum = RecursivelySearchForDefined(vectorType, vectorPopulation, static (vector) => vector.Definition.ImplementSum);
        var implementDifference = RecursivelySearchForDefined(vectorType, vectorPopulation, static (vector) => vector.Definition.ImplementDifference);
        var difference = ResolveDifference(vectorType, vectorPopulation);

        var defaultUnitInstanceName = RecursivelySearchForDefined(vectorType, vectorPopulation, static (vector) => vector.Definition.DefaultUnitInstanceName);
        var defaultUnitInstanceSymbol = RecursivelySearchForDefined(vectorType, vectorPopulation, static (vector) => vector.Definition.DefaultUnitInstanceSymbol);

        var generateDocumentation = RecursivelySearchForDefined(vectorType, vectorPopulation, static (vector) => vector.Definition.GenerateDocumentation);

        return new(vectorType.Type, vectorType.TypeLocation, unit.Type.AsNamedType(), scalar, implementSum!.Value, implementDifference!.Value, difference,
            defaultUnitInstanceName, defaultUnitInstanceSymbol, membersByDimension, definedDerivations, inheritedDerivations, conversions, includedUnitInstances, generateDocumentation);
    }

    private static IReadOnlyDictionary<int, NamedType> ResolveMembers(GroupSpecializationType vectorType, IVectorPopulation vectorPopulation)
    {
        return vectorPopulation.GroupMembersByGroup[vectorType.Type.AsNamedType()].GroupMembersByDimension.Transform(static (vector) => vector.Type.AsNamedType());
    }

    private static NamedType? ResolveDifference(GroupSpecializationType vectorType, IVectorPopulation vectorPopulation)
    {
        var difference = RecursivelySearchForMatching(vectorType, vectorPopulation, static (scalar) => scalar.Definition.Difference, static (vector, _) => vector.Definition.Locations.ExplicitlySetDifference);

        if (difference is null && vectorType.Definition.Locations.ExplicitlySetDifference is false)
        {
            difference = vectorType.Type.AsNamedType();
        }

        return difference;
    }

    private static IReadOnlyList<T> CollectItems<T>(GroupSpecializationType vectorType, IVectorPopulation vectorPopulation, Func<IVectorGroupType, IEnumerable<T>> itemsDelegate, Func<IVectorGroupSpecializationType, bool> shouldInherit, bool onlyInherited = false)
    {
        List<T> items = new();

        recursivelyAddItems(vectorType, onlyInherited);

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

    private static IReadOnlyList<string> ResolveUnitInstanceInclusions(GroupSpecializationType vectorType, IVectorPopulation vectorPopulation, IUnitType unit)
    {
        HashSet<string> includedUnits = new(unit.UnitInstancesByName.Keys);

        recurisvelyModify(vectorType);

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

    private static T? RecursivelySearchForDefined<T>(GroupSpecializationType vectorType, IVectorPopulation vectorPopulation, Func<IVectorGroupType, T?> itemDelegate)
    {
        return RecursivelySearchForMatching(vectorType, vectorPopulation, itemDelegate, static (_, item) => item is not null);
    }

    private static T? RecursivelySearchForMatching<T>(GroupSpecializationType vectorType, IVectorPopulation vectorPopulation, Func<IVectorGroupType, T?> itemDelegate, Func<IVectorGroupType, T?, bool> predicate)
    {
        return recursivelySearch(vectorType);

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
