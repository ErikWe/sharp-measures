namespace SharpMeasures.Generators.Vectors.ForeignVectorParsing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Units;

using System;
using System.Collections.Generic;
using System.Linq;

public static class ForeignVectorSpecializationResolver
{
    public static Optional<IResolvedVectorType> Resolve(IVectorSpecializationType vectorType, IUnitPopulation unitPopulation, IVectorPopulation vectorPopulation)
    {
        if (vectorPopulation.VectorBases.TryGetValue(vectorType.Type.AsNamedType(), out var vectorBase) is false || unitPopulation.Units.TryGetValue(vectorBase.Definition.Unit, out var unit) is false)
        {
            return new Optional<IResolvedVectorType>();
        }

        var definedDerivations = vectorType.Derivations;
        var inheritedDerivations = CollectItems(vectorType, vectorPopulation, static (vector) => vector.Derivations, static (vector) => vector.Definition.InheritDerivations, onlyInherited: true);

        var constants = CollectItems(vectorType, vectorPopulation, static (vector) => vector.Constants, static (vector) => vector.Definition.InheritConstants);
        var conversions = CollectItems(vectorType, vectorPopulation, static (vector) => vector.Conversions, static (vector) => vector.Definition.InheritConversions);

        var includedUnitInstances = ResolveUnitInstanceInclusions(vectorType, vectorPopulation, unit);

        var scalar = RecursivelySearchForDefined(vectorType, vectorPopulation, static (vector) => vector.Definition.Scalar);

        var implementSum = RecursivelySearchForDefined(vectorType, vectorPopulation, static (vector) => vector.Definition.ImplementSum);
        var implementDifference = RecursivelySearchForDefined(vectorType, vectorPopulation, static (vector) => vector.Definition.ImplementDifference);
        var difference = ResolveDifference(vectorType, vectorPopulation);

        var defaultUnitInstanceName = RecursivelySearchForDefined(vectorType, vectorPopulation, static (vector) => vector.Definition.DefaultUnitInstanceName);
        var defaultUnitInstanceSymbol = RecursivelySearchForDefined(vectorType, vectorPopulation, static (vector) => vector.Definition.DefaultUnitInstanceSymbol);

        var generateDocumentation = RecursivelySearchForDefined(vectorType, vectorPopulation, static (vector) => vector.Definition.GenerateDocumentation);

        return new ResolvedVectorType(vectorType.Type, MinimalLocation.None, vectorBase.Definition.Dimension, unit.Type.AsNamedType(), scalar, implementSum!.Value, implementDifference!.Value, difference,
            defaultUnitInstanceName, defaultUnitInstanceSymbol, definedDerivations, inheritedDerivations, constants, conversions, includedUnitInstances, generateDocumentation);
    }

    private static NamedType? ResolveDifference(IVectorSpecializationType vectorType, IVectorPopulation vectorPopulation)
    {
        var difference = RecursivelySearchForMatching(vectorType, vectorPopulation, static (scalar) => scalar.Definition.Difference, static (vector, _) => vector.Definition.Locations.ExplicitlySetDifference);

        if (difference is null && vectorType.Definition.Locations.ExplicitlySetDifference is false)
        {
            difference = vectorType.Type.AsNamedType();
        }

        return difference;
    }

    private static IReadOnlyList<T> CollectItems<T>(IVectorSpecializationType vectorType, IVectorPopulation vectorPopulation, Func<IVectorType, IEnumerable<T>> itemsDelegate, Func<IVectorSpecializationType, bool> shouldInherit, bool onlyInherited = false)
    {
        List<T> items = new();

        recursivelyAddItems(vectorType, onlyInherited);

        return items;

        void recursivelyAddItems(IVectorType vector, bool onlyInherited)
        {
            if (onlyInherited is false)
            {
                items.AddRange(itemsDelegate(vector));
            }

            if (vector is IVectorSpecializationType vectorSpecialization && shouldInherit(vectorSpecialization))
            {
                recursivelyAddItems(vectorPopulation.Vectors[vectorSpecialization.Definition.OriginalQuantity], onlyInherited: false);
            }
        }
    }

    private static IReadOnlyList<string> ResolveUnitInstanceInclusions(IVectorSpecializationType vectorType, IVectorPopulation vectorPopulation, IUnitType unit)
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

    private static T? RecursivelySearchForDefined<T>(IVectorSpecializationType vectorType, IVectorPopulation vectorPopulation, Func<IVectorType, T?> itemDelegate)
    {
        return RecursivelySearchForMatching(vectorType, vectorPopulation, itemDelegate, static (_, item) => item is not null);
    }

    private static T? RecursivelySearchForMatching<T>(IVectorSpecializationType vectorType, IVectorPopulation vectorPopulation, Func<IVectorType, T?> itemDelegate, Func<IVectorType, T?, bool> predicate)
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
