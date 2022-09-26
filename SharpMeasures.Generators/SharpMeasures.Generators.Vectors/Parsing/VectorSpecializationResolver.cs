namespace SharpMeasures.Generators.Vectors.Parsing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Units;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

internal static class VectorSpecializationResolver
{
    public static IncrementalValuesProvider<Optional<ResolvedVectorType>> Resolve(IncrementalValuesProvider<Optional<VectorSpecializationType>> vectorProvider, IncrementalValueProvider<IUnitPopulation> unitPopulationProvider, IncrementalValueProvider<IVectorPopulation> vectorPopulationProvider)
    {
        return vectorProvider.Combine(unitPopulationProvider, vectorPopulationProvider).Select(Resolve);
    }

    private static Optional<ResolvedVectorType> Resolve((Optional<VectorSpecializationType> UnresolvedVector, IUnitPopulation UnitPopulation, IVectorPopulation VectorPopulation) input, CancellationToken token)
    {
        if (token.IsCancellationRequested || input.UnresolvedVector.HasValue is false)
        {
            return new Optional<ResolvedVectorType>();
        }

        return Resolve(input.UnresolvedVector.Value, input.UnitPopulation, input.VectorPopulation);
    }

    public static Optional<ResolvedVectorType> Resolve(VectorSpecializationType vectorType, IUnitPopulation unitPopulation, IVectorPopulation vectorPopulation)
    {
        if (vectorPopulation.VectorBases.TryGetValue(vectorType.Type.AsNamedType(), out var vectorBase) is false || unitPopulation.Units.TryGetValue(vectorBase.Definition.Unit, out var unit) is false)
        {
            return new Optional<ResolvedVectorType>();
        }

        var allConstants = CollectItems(vectorType, vectorPopulation, static (vector) => vector.Constants, static (vector) => vector.Definition.InheritConstants);

        var inheritedDerivations = CollectItems(vectorType, vectorPopulation, static (vector) => vector.Derivations, static (vector) => vector.Definition.InheritDerivations, onlyInherited: true);
        var inheritedConversions = CollectItems(vectorType, vectorPopulation, static (vector) => vector.Conversions, static (vector) => vector.Definition.InheritConversions, onlyInherited: true);

        var includedUnitInstances = ResolveUnitInstanceInclusions(vectorType, vectorPopulation, unit);

        var scalar = RecursivelySearchForDefined(vectorType, vectorPopulation, static (vector) => vector.Definition.Scalar);

        var implementSum = RecursivelySearchForDefined(vectorType, vectorPopulation, static (vector) => vector.Definition.ImplementSum);
        var implementDifference = RecursivelySearchForDefined(vectorType, vectorPopulation, static (vector) => vector.Definition.ImplementDifference);
        var difference = ResolveDifference(vectorType, vectorPopulation);

        var defaultUnitInstanceName = RecursivelySearchForDefined(vectorType, vectorPopulation, static (vector) => vector.Definition.DefaultUnitInstanceName);
        var defaultUnitInstanceSymbol = RecursivelySearchForDefined(vectorType, vectorPopulation, static (vector) => vector.Definition.DefaultUnitInstanceSymbol);

        var generateDocumentation = RecursivelySearchForDefined(vectorType, vectorPopulation, static (vector) => vector.Definition.GenerateDocumentation);

        return new ResolvedVectorType(vectorType.Type, vectorBase.Definition.Dimension, group: null, unit.Type.AsNamedType(), vectorType.Definition.OriginalQuantity, vectorType.Definition.ForwardsCastOperatorBehaviour, vectorType.Definition.BackwardsCastOperatorBehaviour, scalar,
            implementSum!.Value, implementDifference!.Value, difference, defaultUnitInstanceName, defaultUnitInstanceSymbol, vectorType.Derivations, allConstants, vectorType.Conversions, inheritedDerivations, inheritedConversions, includedUnitInstances, generateDocumentation);
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

    private static IReadOnlyList<T> CollectItems<T>(VectorSpecializationType vectorType, IVectorPopulation vectorPopulation, Func<IVectorType, IEnumerable<T>> itemsDelegate, Func<IVectorSpecializationType, bool> shouldInherit, bool onlyInherited = false)
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

    private static T? RecursivelySearchForDefined<T>(VectorSpecializationType vectorType, IVectorPopulation vectorPopulation, Func<IVectorType, T?> itemDelegate) => RecursivelySearchForMatching(vectorType, vectorPopulation, itemDelegate, static (_, item) => item is not null);
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
