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
    public static IncrementalValuesProvider<ResolvedGroupType> Resolve(IncrementalValuesProvider<GroupSpecializationType> vectorProvider, IncrementalValueProvider<IUnitPopulation> unitPopulationProvider,
        IncrementalValueProvider<IVectorPopulationWithData> vectorPopulationProvider)
    {
        return vectorProvider.Combine(unitPopulationProvider, vectorPopulationProvider).Select(Resolve);
    }

    private static ResolvedGroupType Resolve((GroupSpecializationType UnresolvedVector, IUnitPopulation UnitPopulation, IVectorPopulationWithData VectorPopulation) input, CancellationToken _)
    {
        var vectorBase = input.VectorPopulation.GroupBases[input.UnresolvedVector.Type.AsNamedType()];
        var unit = input.UnitPopulation.Units[vectorBase.Definition.Unit];

        var membersByDimension = ResolveMembers(input.UnresolvedVector, input.VectorPopulation);

        var derivations = CollectItems(input.UnresolvedVector, input.VectorPopulation, static (vector) => vector.Derivations, static (vector) => vector.Definition.InheritDerivations);
        var conversions = CollectItems(input.UnresolvedVector, input.VectorPopulation, static (vector) => vector.Conversions, static (vector) => vector.Definition.InheritConversions);

        var includedUnits = ResolveUnitInclusions(input.UnresolvedVector, input.VectorPopulation, unit);

        var scalar = RecursivelySearchForDefined(input.UnresolvedVector, input.VectorPopulation, static (vector) => vector.Definition.Scalar);

        var implementSum = RecursivelySearchForDefined(input.UnresolvedVector, input.VectorPopulation, static (vector) => vector.Definition.ImplementSum);
        var implementDifference = RecursivelySearchForDefined(input.UnresolvedVector, input.VectorPopulation, static (vector) => vector.Definition.ImplementDifference);
        var difference = RecursivelySearchForDefined(input.UnresolvedVector, input.VectorPopulation, static (vector) => vector.Definition.Difference);

        var defaultUnitName = RecursivelySearchForDefined(input.UnresolvedVector, input.VectorPopulation, static (vector) => vector.Definition.DefaultUnitName);
        var defaultUnitSymbol = RecursivelySearchForDefined(input.UnresolvedVector, input.VectorPopulation, static (vector) => vector.Definition.DefaultUnitSymbol);

        var generateDocumentation = RecursivelySearchForDefined(input.UnresolvedVector, input.VectorPopulation, static (vector) => vector.Definition.GenerateDocumentation);

        return new(input.UnresolvedVector.Type, input.UnresolvedVector.TypeLocation, unit.Type.AsNamedType(), scalar, implementSum!.Value, implementDifference!.Value, difference,
            defaultUnitName, defaultUnitSymbol, membersByDimension, derivations, conversions, includedUnits, generateDocumentation);
    }

    private static IReadOnlyDictionary<int, NamedType> ResolveMembers(GroupSpecializationType vectorType, IVectorPopulation vectorPopulation)
    {
        return vectorPopulation.GroupMembersByGroup[vectorType.Type.AsNamedType()].GroupMembersByDimension.Transform(static (vector) => vector.Type.AsNamedType());
    }

    private static IReadOnlyList<T> CollectItems<T>(GroupSpecializationType vectorType, IVectorPopulation vectorPopulation, Func<IVectorGroupType, IEnumerable<T>> itemsDelegate, Func<IVectorGroupSpecializationType, bool> shouldInherit)
    {
        List<T> items = new();

        recursivelyAddItems(vectorType);

        return items;

        void recursivelyAddItems(IVectorGroupType vector)
        {
            items.AddRange(itemsDelegate(vector));

            if (vector is IVectorGroupSpecializationType vectorSpecialization && shouldInherit(vectorSpecialization))
            {
                recursivelyAddItems(vectorPopulation.Groups[vectorSpecialization.Definition.OriginalVectorGroup]);
            }
        }
    }

    private static IReadOnlyList<string> ResolveUnitInclusions(GroupSpecializationType vectorType, IVectorPopulation vectorPopulation, IUnitType unit)
    {
        HashSet<string> includedUnits = new(unit.UnitsByName.Keys);

        recurisvelyModify(vectorType);

        return includedUnits.ToList();

        void recurisvelyModify(IVectorGroupType vector)
        {
            modify(vector);

            recurse(vector);
        }

        void modify(IVectorGroupType vector)
        {
            if (vector.UnitInclusions.Any())
            {
                includedUnits.IntersectWith(vector.UnitInclusions.SelectMany(static (unitList) => unitList.Units));

                return;
            }

            includedUnits.ExceptWith(vector.UnitExclusions.SelectMany(static (unitList) => unitList.Units));
        }

        void recurse(IVectorGroupType vector)
        {
            if (vector is IVectorGroupSpecializationType scalarSpecialization && scalarSpecialization.Definition.InheritUnits)
            {
                recurisvelyModify(vectorPopulation.Groups[scalarSpecialization.Definition.OriginalVectorGroup]);
            }
        }
    }

    private static T? RecursivelySearchForDefined<T>(GroupSpecializationType vectorType, IVectorPopulation vectorPopulation, Func<IVectorGroupType, T?> itemDelegate)
    {
        return recursivelySearch(vectorType);

        T? recursivelySearch(IVectorGroupType vector)
        {
            if (itemDelegate(vector) is T item)
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
