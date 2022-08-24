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

        var includedUnits = ResolveUnitInclusions(input.UnresolvedVector, input.VectorPopulation, unit);

        var scalar = RecursivelySearchForDefined(input.UnresolvedVector, input.VectorPopulation, static (vector) => vector.Definition.Scalar);

        var implementSum = RecursivelySearchForDefined(input.UnresolvedVector, input.VectorPopulation, static (vector) => vector.Definition.ImplementSum);
        var implementDifference = RecursivelySearchForDefined(input.UnresolvedVector, input.VectorPopulation, static (vector) => vector.Definition.ImplementDifference);
        var difference = RecursivelySearchForDefined(input.UnresolvedVector, input.VectorPopulation, static (vector) => vector.Definition.Difference);

        var defaultUnitName = RecursivelySearchForDefined(input.UnresolvedVector, input.VectorPopulation, static (vector) => vector.Definition.DefaultUnitName);
        var defaultUnitSymbol = RecursivelySearchForDefined(input.UnresolvedVector, input.VectorPopulation, static (vector) => vector.Definition.DefaultUnitSymbol);

        var generateDocumentation = RecursivelySearchForDefined(input.UnresolvedVector, input.VectorPopulation, static (vector) => vector.Definition.GenerateDocumentation);

        return new(input.UnresolvedVector.Type, input.UnresolvedVector.TypeLocation, vectorBase.Definition.Dimension, unit.Type.AsNamedType(), scalar, implementSum!.Value, implementDifference!.Value, difference,
            defaultUnitName, defaultUnitSymbol, derivations, constants, conversions, includedUnits, generateDocumentation);
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
                recursivelyAddItems(vectorPopulation.Vectors[vectorSpecialization.Definition.OriginalVector]);
            }
        }
    }

    private static IReadOnlyList<string> ResolveUnitInclusions(VectorSpecializationType vectorType, IVectorPopulation vectorPopulation, IUnitType unit)
    {
        HashSet<string> includedUnits = new(unit.UnitsByName.Keys);

        recurisvelyModify(vectorType);

        return includedUnits.ToList();

        void recurisvelyModify(IVectorType vector)
        {
            modify(vector);

            recurse(vector);
        }

        void modify(IVectorType vector)
        {
            if (vector.UnitInclusions.Any())
            {
                includedUnits.IntersectWith(vector.UnitInclusions.SelectMany(static (unitList) => unitList.Units));

                return;
            }

            includedUnits.ExceptWith(vector.UnitExclusions.SelectMany(static (unitList) => unitList.Units));
        }

        void recurse(IVectorType vector)
        {
            if (vector is IVectorSpecializationType scalarSpecialization && scalarSpecialization.Definition.InheritUnits)
            {
                recurisvelyModify(vectorPopulation.Vectors[scalarSpecialization.Definition.OriginalVector]);
            }
        }
    }

    private static T? RecursivelySearchForDefined<T>(VectorSpecializationType vectorType, IVectorPopulation vectorPopulation, Func<IVectorType, T?> itemDelegate)
    {
        return recursivelySearch(vectorType);

        T? recursivelySearch(IVectorType vector)
        {
            if (itemDelegate(vector) is T item)
            {
                return item;
            }

            if (vector is IVectorSpecializationType vectorSpecialization)
            {
                return recursivelySearch(vectorPopulation.Vectors[vectorSpecialization.Definition.OriginalVector]);
            }

            return default;
        }
    }
}
