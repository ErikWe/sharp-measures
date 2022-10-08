namespace SharpMeasures.Generators.Scalars.Parsing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Units;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

internal static class ScalarSpecializationResolver
{
    public static IncrementalValuesProvider<Optional<ResolvedScalarType>> Resolve(IncrementalValuesProvider<Optional<ScalarSpecializationType>> scalarProvider, IncrementalValueProvider<IUnitPopulation> unitPopulationProvider, IncrementalValueProvider<IScalarPopulation> scalarPopulationProvider)
    {
        return scalarProvider.Combine(unitPopulationProvider, scalarPopulationProvider).Select(Resolve);
    }

    private static Optional<ResolvedScalarType> Resolve((Optional<ScalarSpecializationType> UnresolvedScalar, IUnitPopulation UnitPopulation, IScalarPopulation ScalarPopulation) input, CancellationToken token)
    {
        if (token.IsCancellationRequested || input.UnresolvedScalar.HasValue is false)
        {
            return new Optional<ResolvedScalarType>();
        }

        return Resolve(input.UnresolvedScalar.Value, input.UnitPopulation, input.ScalarPopulation);
    }

    public static Optional<ResolvedScalarType> Resolve(ScalarSpecializationType scalarType, IUnitPopulation unitPopulation, IScalarPopulation scalarPopulation)
    {
        if (scalarPopulation.ScalarBases.TryGetValue(scalarType.Type.AsNamedType(), out var scalarBase) is false || unitPopulation.Units.TryGetValue(scalarBase.Definition.Unit, out var unit) is false)
        {
            return new Optional<ResolvedScalarType>();
        }

        var inheritedOperations = CollectItems(scalarType, scalarPopulation, static (scalar) => scalar.Operations, static (scalar) => scalar.Definition.InheritOperations, onlyInherited: true);
        var inheritedProcesses = CollectItems(scalarType, scalarPopulation, static (scalar) => scalar.Processes, static (scalar) => scalar.Definition.InheritProcesses, onlyInherited: true);
        var inheritedConstants = CollectItems(scalarType, scalarPopulation, static (scalar) => scalar.Constants, static (scalar) => scalar.Definition.InheritConstants, onlyInherited: true);
        var inheritedConversions = CollectItems(scalarType, scalarPopulation, static (scalar) => scalar.Conversions, static (scalar) => scalar.Definition.InheritConversions, onlyInherited: true);

        var includedUnitInstanceBases = ResolveUnitInstanceInclusions(scalarType, scalarPopulation, unit, static (scalar) => scalar.UnitBaseInstanceInclusions, static (scalar) => scalar.UnitBaseInstanceExclusions, static (scalar) => scalar.Definition.InheritBases);
        var includedUnitInstances = ResolveUnitInstanceInclusions(scalarType, scalarPopulation, unit, static (scalar) => scalar.UnitInstanceInclusions, static (scalar) => scalar.UnitInstanceExclusions, static (scalar) => scalar.Definition.InheritUnits);

        var vector = RecursivelySearchForDefined(scalarType, scalarPopulation, static (scalar) => scalar.Definition.Vector);

        var implementSum = RecursivelySearchForDefined(scalarType, scalarPopulation, static (scalar) => scalar.Definition.ImplementSum);
        var implementDifference = RecursivelySearchForDefined(scalarType, scalarPopulation, static (scalar) => scalar.Definition.ImplementDifference);
        var difference = ResolveDifference(scalarType, scalarPopulation);

        var defaultUnitInstanceName = RecursivelySearchForDefined(scalarType, scalarPopulation, static (scalar) => scalar.Definition.DefaultUnitInstanceName);
        var defaultUnitInstanceSymbol = RecursivelySearchForDefined(scalarType, scalarPopulation, static (scalar) => scalar.Definition.DefaultUnitInstanceSymbol);

        var generateDocumentation = RecursivelySearchForDefined(scalarType, scalarPopulation, static (scalar) => scalar.Definition.GenerateDocumentation);

        return new ResolvedScalarType(scalarType.Type, unit.Type.AsNamedType(), scalarBase.Definition.UseUnitBias, scalarType.Definition.OriginalQuantity, scalarType.Definition.ForwardsCastOperatorBehaviour, scalarType.Definition.BackwardsCastOperatorBehaviour, vector, implementSum!.Value,
            implementDifference!.Value, difference, defaultUnitInstanceName, defaultUnitInstanceSymbol, scalarType.Operations, scalarType.Processes, scalarType.Constants, scalarType.Conversions, inheritedOperations, inheritedProcesses, inheritedConstants, inheritedConversions, includedUnitInstanceBases, includedUnitInstances, generateDocumentation);
    }

    private static NamedType? ResolveDifference(ScalarSpecializationType scalarType, IScalarPopulation scalarPopulation)
    {
        var difference = RecursivelySearchForMatching(scalarType, scalarPopulation, static (scalar) => scalar.Definition.Difference, static (scalar, _) => scalar.Definition.Locations.ExplicitlySetDifference);

        if (difference is null && scalarType.Definition.Locations.ExplicitlySetDifference is false)
        {
            difference = scalarType.Type.AsNamedType();
        }

        return difference;
    }

    private static IReadOnlyList<T> CollectItems<T>(ScalarSpecializationType scalarType, IScalarPopulation scalarPopulation, Func<IScalarType, IEnumerable<T>> itemsDelegate, Func<IScalarSpecializationType, bool> shouldInherit, bool onlyInherited = false)
    {
        List<T> items = new();

        recursivelyAddItems(scalarType, onlyInherited);

        return items;

        void recursivelyAddItems(IScalarType scalar, bool onlyInherited)
        {
            if (onlyInherited is false)
            {
                items.AddRange(itemsDelegate(scalar));
            }

            if (scalar is IScalarSpecializationType scalarSpecialization && shouldInherit(scalarSpecialization) && scalarPopulation.Scalars.TryGetValue(scalarSpecialization.Definition.OriginalQuantity, out var originalScalar))
            {
                recursivelyAddItems(originalScalar, onlyInherited: false);
            }
        }
    }

    private static IReadOnlyList<string> ResolveUnitInstanceInclusions(ScalarSpecializationType scalarType, IScalarPopulation scalarPopulation, IUnitType unit, Func<IScalarType, IEnumerable<IUnitInstanceList>> inclusionsDelegate, Func<IScalarType, IEnumerable<IUnitInstanceList>> exclusionsDelegate, Func<IScalarSpecializationType, bool> shouldInherit)
    {
        HashSet<string> includedUnitInstances = new(unit.UnitInstancesByName.Keys);

        recursivelyModify(scalarType);

        return includedUnitInstances.ToList();

        void recursivelyModify(IScalarType scalar)
        {
            modify(scalar);

            recurse(scalar);
        }

        void modify(IScalarType scalar)
        {
            var inclusions = inclusionsDelegate(scalar);

            if (inclusions.Any())
            {
                includedUnitInstances.IntersectWith(inclusions.SelectMany(static (unitList) => unitList.UnitInstances));

                return;
            }

            includedUnitInstances.ExceptWith(exclusionsDelegate(scalar).SelectMany(static (unitList) => unitList.UnitInstances));
        }

        void recurse(IScalarType scalar)
        {
            if (scalar is IScalarSpecializationType scalarSpecialization && shouldInherit(scalarSpecialization) && scalarPopulation.Scalars.TryGetValue(scalarSpecialization.Definition.OriginalQuantity, out var originalScalar))
            {
                recursivelyModify(originalScalar);
            }
        }
    }

    private static T? RecursivelySearchForDefined<T>(ScalarSpecializationType scalarType, IScalarPopulation scalarPopulation, Func<IScalarType, T?> itemDelegate) => RecursivelySearchForMatching(scalarType, scalarPopulation, itemDelegate, static (_, item) => item is not null);
    private static T? RecursivelySearchForMatching<T>(ScalarSpecializationType scalarType, IScalarPopulation scalarPopulation, Func<IScalarType, T?> itemDelegate, Func<IScalarType, T?, bool> predicate)
    {
        return recursivelySearch(scalarType);

        T? recursivelySearch(IScalarType scalar)
        {
            var item = itemDelegate(scalar);
            
            if (predicate(scalar, item))
            {
                return item;
            }

            if (scalar is IScalarSpecializationType scalarSpecialization && scalarPopulation.Scalars.TryGetValue(scalarSpecialization.Definition.OriginalQuantity, out var originalScalar))
            {
                return recursivelySearch(originalScalar);
            }

            return default;
        }
    }
}
