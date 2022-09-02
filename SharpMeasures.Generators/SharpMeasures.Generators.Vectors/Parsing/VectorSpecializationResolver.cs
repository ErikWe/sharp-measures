namespace SharpMeasures.Generators.Vectors.Parsing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Units;
using SharpMeasures.Generators.Vectors.Parsing.Abstraction;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

internal static class VectorSpecializationResolver
{
    public static IncrementalValuesProvider<ResolvedVectorType> Resolve(IncrementalValuesProvider<VectorSpecializationType> vectorProvider, IncrementalValueProvider<IUnitPopulation> unitPopulationProvider,
        IncrementalValueProvider<IVectorPopulationWithData> vectorPopulationProvider)
    {
        return vectorProvider.Combine(unitPopulationProvider, vectorPopulationProvider).Select(Resolve);
    }

    private static ResolvedVectorType Resolve((VectorSpecializationType UnresolvedVector, IUnitPopulation UnitPopulation, IVectorPopulationWithData VectorPopulation) input, CancellationToken _)
    {
        var vectorBase = input.VectorPopulation.VectorBases[input.UnresolvedVector.Type.AsNamedType()];
        var unit = input.UnitPopulation.Units[vectorBase.Definition.Unit];

        var derivations = CollectItems(input.UnresolvedVector, input.VectorPopulation, static (vector) => vector.Derivations, static (vector) => vector.Definition.InheritDerivations);
        var constants = CollectItems(input.UnresolvedVector, input.VectorPopulation, static (vector) => vector.Constants, static (vector) => vector.Definition.InheritConstants);
        var conversions = CollectItems(input.UnresolvedVector, input.VectorPopulation, static (vector) => vector.Conversions, static (vector) => vector.Definition.InheritConversions);

        var includedUnitInstances = ResolveUnitInstanceInclusions(input.UnresolvedVector, input.VectorPopulation, unit);

        var scalar = RecursivelySearchForDefined(input.UnresolvedVector, input.VectorPopulation, static (vector) => vector.Definition.Scalar);

        var implementSum = RecursivelySearchForDefined(input.UnresolvedVector, input.VectorPopulation, static (vector) => vector.Definition.ImplementSum);
        var implementDifference = RecursivelySearchForDefined(input.UnresolvedVector, input.VectorPopulation, static (vector) => vector.Definition.ImplementDifference);
        var difference = ResolveDifference(input.UnresolvedVector, input.VectorPopulation);

        var defaultUnitInstanceName = RecursivelySearchForDefined(input.UnresolvedVector, input.VectorPopulation, static (vector) => vector.Definition.DefaultUnitInstanceName);
        var defaultUnitInstanceSymbol = RecursivelySearchForDefined(input.UnresolvedVector, input.VectorPopulation, static (vector) => vector.Definition.DefaultUnitInstanceSymbol);

        var generateDocumentation = RecursivelySearchForDefined(input.UnresolvedVector, input.VectorPopulation, static (vector) => vector.Definition.GenerateDocumentation);

        return new(input.UnresolvedVector.Type, input.UnresolvedVector.TypeLocation, vectorBase.Definition.Dimension, unit.Type.AsNamedType(), scalar, implementSum!.Value, implementDifference!.Value, difference,
            defaultUnitInstanceName, defaultUnitInstanceSymbol, derivations, constants, conversions, includedUnitInstances, generateDocumentation);
    }

    private static NamedType? ResolveDifference(VectorSpecializationType vectorType, IVectorPopulation vectorPopulation)
    {
        var difference = RecursivelySearchForMatching(vectorType, vectorPopulation, static (scalar) => scalar.Definition.Difference, static (vector, _) => vector.Definition.Locations.ExplicitlySetDifference);

        if (difference is null && vectorType.Definition.Locations.ExplicitlySetDifference is false)
        {
            difference = vectorType.Type.AsNamedType();
        }

        return difference;
    }

    private static IReadOnlyList<T> CollectItems<T>(VectorSpecializationType vectorType, IVectorPopulation vectorPopulation, Func<IVectorType, IEnumerable<T>> itemsDelegate, Func<IVectorSpecializationType, bool> shouldInherit)
    {
        List<T> items = new();

        recursivelyAddItems(vectorType);

        return items;

        void recursivelyAddItems(IVectorType vector)
        {
            items.AddRange(itemsDelegate(vector));

            if (vector is IVectorSpecializationType vectorSpecialization && shouldInherit(vectorSpecialization))
            {
                recursivelyAddItems(vectorPopulation.Vectors[vectorSpecialization.Definition.OriginalQuantity]);
            }
        }
    }

    private static IReadOnlyList<string> ResolveUnitInstanceInclusions(VectorSpecializationType vectorType, IVectorPopulation vectorPopulation, IUnitType unit)
    {
        HashSet<string> includedUnits = new(unit.UnitInstancesByName.Keys);

        recurisvelyModify(vectorType);

        return includedUnits.ToList();

        void recurisvelyModify(IVectorType vector)
        {
            modify(vector);

            recurse(vector);
        }

        void modify(IVectorType vector)
        {
            if (vector.UnitInstanceInclusions.Any())
            {
                includedUnits.IntersectWith(vector.UnitInstanceInclusions.SelectMany(static (unitList) => unitList.UnitInstances));

                return;
            }

            includedUnits.ExceptWith(vector.UnitInstanceExclusions.SelectMany(static (unitList) => unitList.UnitInstances));
        }

        void recurse(IVectorType vector)
        {
            if (vector is IVectorSpecializationType scalarSpecialization && scalarSpecialization.Definition.InheritUnits)
            {
                recurisvelyModify(vectorPopulation.Vectors[scalarSpecialization.Definition.OriginalQuantity]);
            }
        }
    }

    private static T? RecursivelySearchForDefined<T>(VectorSpecializationType vectorType, IVectorPopulation vectorPopulation, Func<IVectorType, T?> itemDelegate)
    {
        return RecursivelySearchForMatching(vectorType, vectorPopulation, itemDelegate, static (_, item) => item is not null);
    }

    private static T? RecursivelySearchForMatching<T>(VectorSpecializationType vectorType, IVectorPopulation vectorPopulation, Func<IVectorType, T?> itemDelegate, Func<IVectorType, T?, bool> predicate)
    {
        return recursivelySearch(vectorType);

        T? recursivelySearch(IVectorType vector)
        {
            var item = itemDelegate(vector);

            if (predicate(vector, item))
            {
                return item;
            }

            if (vector is IVectorSpecializationType vectorSpecialization)
            {
                return recursivelySearch(vectorPopulation.Vectors[vectorSpecialization.Definition.OriginalQuantity]);
            }

            return default;
        }
    }
}
