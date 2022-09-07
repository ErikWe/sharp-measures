namespace SharpMeasures.Generators.Scalars.Parsing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Scalars.Parsing.Abstraction;
using SharpMeasures.Generators.Units;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

internal static class ScalarSpecializationResolver
{
    public static IncrementalValuesProvider<ResolvedScalarType> Resolve(IncrementalValuesProvider<ScalarSpecializationType> scalarProvider, IncrementalValueProvider<IUnitPopulation> unitPopulationProvider,
        IncrementalValueProvider<IScalarPopulationWithData> scalarPopulationProvider)
    {
        return scalarProvider.Combine(unitPopulationProvider, scalarPopulationProvider).Select(Resolve);
    }

    private static ResolvedScalarType Resolve((ScalarSpecializationType UnresolvedScalar, IUnitPopulation UnitPopulation, IScalarPopulationWithData ScalarPopulation) input, CancellationToken _)
    {
        var scalarBase = input.ScalarPopulation.ScalarBases[input.UnresolvedScalar.Type.AsNamedType()];
        var unit = input.UnitPopulation.Units[scalarBase.Definition.Unit];

        var definedDerivations = input.UnresolvedScalar.Derivations;
        var inheritedDerivations = CollectItems(input.UnresolvedScalar, input.ScalarPopulation, static (scalar) => scalar.Derivations, static (scalar) => scalar.Definition.InheritDerivations, onlyInherited: true);

        var constants = CollectItems(input.UnresolvedScalar, input.ScalarPopulation, static (scalar) => scalar.Constants, static (scalar) => scalar.Definition.InheritConstants);
        var conversions = CollectItems(input.UnresolvedScalar, input.ScalarPopulation, static (scalar) => scalar.Conversions, static (scalar) => scalar.Definition.InheritConversions);

        var includedUnitBaseInstances = ResolveUnitInclusions(input.UnresolvedScalar, input.ScalarPopulation, unit, static (scalar) => scalar.UnitBaseInstanceInclusions, static (scalar) => scalar.UnitBaseInstanceExclusions, static (scalar) => scalar.Definition.InheritBases);
        var includedUnitInstances = ResolveUnitInclusions(input.UnresolvedScalar, input.ScalarPopulation, unit, static (scalar) => scalar.UnitInstanceInclusions, static (scalar) => scalar.UnitInstanceExclusions, static (scalar) => scalar.Definition.InheritUnits);

        var vector = RecursivelySearchForDefined(input.UnresolvedScalar, input.ScalarPopulation, static (scalar) => scalar.Definition.Vector);

        var reciprocal = RecursivelySearchForDefined(input.UnresolvedScalar, input.ScalarPopulation, static (scalar) => scalar.Definition.Reciprocal);
        var square = RecursivelySearchForDefined(input.UnresolvedScalar, input.ScalarPopulation, static (scalar) => scalar.Definition.Square);
        var cube = RecursivelySearchForDefined(input.UnresolvedScalar, input.ScalarPopulation, static (scalar) => scalar.Definition.Cube);
        var squareRoot = RecursivelySearchForDefined(input.UnresolvedScalar, input.ScalarPopulation, static (scalar) => scalar.Definition.SquareRoot);
        var cubeRoot = RecursivelySearchForDefined(input.UnresolvedScalar, input.ScalarPopulation, static (scalar) => scalar.Definition.CubeRoot);

        var implementSum = RecursivelySearchForDefined(input.UnresolvedScalar, input.ScalarPopulation, static (scalar) => scalar.Definition.ImplementSum);
        var implementDifference = RecursivelySearchForDefined(input.UnresolvedScalar, input.ScalarPopulation, static (scalar) => scalar.Definition.ImplementDifference);
        var difference = ResolveDifference(input.UnresolvedScalar, input.ScalarPopulation);

        var defaultUnitInstanceName = RecursivelySearchForDefined(input.UnresolvedScalar, input.ScalarPopulation, static (scalar) => scalar.Definition.DefaultUnitInstanceName);
        var defaultUnitInstanceSymbol = RecursivelySearchForDefined(input.UnresolvedScalar, input.ScalarPopulation, static (scalar) => scalar.Definition.DefaultUnitInstanceSymbol);

        var generateDocumentation = RecursivelySearchForDefined(input.UnresolvedScalar, input.ScalarPopulation, static (scalar) => scalar.Definition.GenerateDocumentation);

        return new(input.UnresolvedScalar.Type, input.UnresolvedScalar.TypeLocation, unit.Type.AsNamedType(), scalarBase.Definition.UseUnitBias, vector, reciprocal, square, cube, squareRoot, cubeRoot,
            implementSum!.Value, implementDifference!.Value, difference, defaultUnitInstanceName, defaultUnitInstanceSymbol, definedDerivations, inheritedDerivations, constants, conversions, includedUnitBaseInstances, includedUnitInstances, generateDocumentation);
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

            if (scalar is IScalarSpecializationType scalarSpecialization && shouldInherit(scalarSpecialization))
            {
                recursivelyAddItems(scalarPopulation.Scalars[scalarSpecialization.Definition.OriginalQuantity], onlyInherited: false);
            }
        }
    }

    private static IReadOnlyList<string> ResolveUnitInclusions(ScalarSpecializationType scalarType, IScalarPopulation scalarPopulation, IUnitType unit, Func<IScalarType, IEnumerable<IUnitInstanceList>> inclusionsDelegate,
        Func<IScalarType, IEnumerable<IUnitInstanceList>> exclusionsDelegate, Func<IScalarSpecializationType, bool> shouldInherit)
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
            if (scalar is IScalarSpecializationType scalarSpecialization && shouldInherit(scalarSpecialization))
            {
                recursivelyModify(scalarPopulation.Scalars[scalarSpecialization.Definition.OriginalQuantity]);
            }
        }
    }

    private static T? RecursivelySearchForDefined<T>(ScalarSpecializationType scalarType, IScalarPopulation scalarPopulation, Func<IScalarType, T?> itemDelegate)
    {
        return RecursivelySearchForMatching(scalarType, scalarPopulation, itemDelegate, static (_, item) => item is not null);
    }

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

            if (scalar is IScalarSpecializationType scalarSpecialization)
            {
                return recursivelySearch(scalarPopulation.Scalars[scalarSpecialization.Definition.OriginalQuantity]);
            }

            return default;
        }
    }
}
