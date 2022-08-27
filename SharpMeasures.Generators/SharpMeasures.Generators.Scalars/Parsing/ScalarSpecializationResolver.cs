namespace SharpMeasures.Generators.Scalars.Parsing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Scalars;
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

        var derivations = CollectItems(input.UnresolvedScalar, input.ScalarPopulation, static (scalar) => scalar.Derivations, static (scalar) => scalar.Definition.InheritDerivations);
        var constants = CollectItems(input.UnresolvedScalar, input.ScalarPopulation, static (scalar) => scalar.Constants, static (scalar) => scalar.Definition.InheritConstants);
        var conversions = CollectItems(input.UnresolvedScalar, input.ScalarPopulation, static (scalar) => scalar.Conversions, static (scalar) => scalar.Definition.InheritConversions);

        var includedBases = ResolveUnitInclusions(input.UnresolvedScalar, input.ScalarPopulation, unit, static (scalar) => scalar.BaseInclusions, static (scalar) => scalar.BaseExclusions, static (scalar) => scalar.Definition.InheritBases);
        var includedUnits = ResolveUnitInclusions(input.UnresolvedScalar, input.ScalarPopulation, unit, static (scalar) => scalar.UnitInclusions, static (scalar) => scalar.UnitExclusions, static (scalar) => scalar.Definition.InheritUnits);

        var vector = RecursivelySearchForDefined(input.UnresolvedScalar, input.ScalarPopulation, static (scalar) => scalar.Definition.Vector);

        var reciprocal = RecursivelySearchForDefined(input.UnresolvedScalar, input.ScalarPopulation, static (scalar) => scalar.Definition.Reciprocal);
        var square = RecursivelySearchForDefined(input.UnresolvedScalar, input.ScalarPopulation, static (scalar) => scalar.Definition.Square);
        var cube = RecursivelySearchForDefined(input.UnresolvedScalar, input.ScalarPopulation, static (scalar) => scalar.Definition.Cube);
        var squareRoot = RecursivelySearchForDefined(input.UnresolvedScalar, input.ScalarPopulation, static (scalar) => scalar.Definition.SquareRoot);
        var cubeRoot = RecursivelySearchForDefined(input.UnresolvedScalar, input.ScalarPopulation, static (scalar) => scalar.Definition.CubeRoot);

        var implementSum = RecursivelySearchForDefined(input.UnresolvedScalar, input.ScalarPopulation, static (scalar) => scalar.Definition.ImplementSum);
        var implementDifference = RecursivelySearchForDefined(input.UnresolvedScalar, input.ScalarPopulation, static (scalar) => scalar.Definition.ImplementDifference);
        var difference = RecursivelySearchForDefined(input.UnresolvedScalar, input.ScalarPopulation, static (scalar) => scalar.Definition.Difference);

        var defaultUnitName = RecursivelySearchForDefined(input.UnresolvedScalar, input.ScalarPopulation, static (scalar) => scalar.Definition.DefaultUnitName);
        var defaultUnitSymbol = RecursivelySearchForDefined(input.UnresolvedScalar, input.ScalarPopulation, static (scalar) => scalar.Definition.DefaultUnitSymbol);

        var generateDocumentation = RecursivelySearchForDefined(input.UnresolvedScalar, input.ScalarPopulation, static (scalar) => scalar.Definition.GenerateDocumentation);

        return new(input.UnresolvedScalar.Type, input.UnresolvedScalar.TypeLocation, unit.Type.AsNamedType(), scalarBase.Definition.UseUnitBias, vector, reciprocal, square, cube, squareRoot, cubeRoot,
            implementSum!.Value, implementDifference!.Value, difference, defaultUnitName, defaultUnitSymbol, derivations, constants, conversions, includedBases, includedUnits, generateDocumentation);
    }

    private static IReadOnlyList<T> CollectItems<T>(ScalarSpecializationType scalarType, IScalarPopulation scalarPopulation, Func<IScalarType, IEnumerable<T>> itemsDelegate, Func<IScalarSpecializationType, bool> shouldInherit)
    {
        List<T> items = new();

        recursivelyAddItems(scalarType);

        return items;

        void recursivelyAddItems(IScalarType scalar)
        {
            items.AddRange(itemsDelegate(scalar));

            if (scalar is IScalarSpecializationType scalarSpecialization && shouldInherit(scalarSpecialization))
            {
                recursivelyAddItems(scalarPopulation.Scalars[scalarSpecialization.Definition.OriginalScalar]);
            }
        }
    }

    private static IReadOnlyList<string> ResolveUnitInclusions(ScalarSpecializationType scalarType, IScalarPopulation scalarPopulation, IUnitType unit, Func<IScalarType, IEnumerable<IUnitList>> inclusionsDelegate,
        Func<IScalarType, IEnumerable<IUnitList>> exclusionsDelegate, Func<IScalarSpecializationType, bool> shouldInherit)
    {
        HashSet<string> includedUnits = new(unit.UnitsByName.Keys);

        recursivelyModify(scalarType);

        return includedUnits.ToList();

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
                includedUnits.IntersectWith(inclusions.SelectMany(static (unitList) => unitList.Units));

                return;
            }

            includedUnits.ExceptWith(exclusionsDelegate(scalar).SelectMany(static (unitList) => unitList.Units));
        }

        void recurse(IScalarType scalar)
        {
            if (scalar is IScalarSpecializationType scalarSpecialization && shouldInherit(scalarSpecialization))
            {
                recursivelyModify(scalarPopulation.Scalars[scalarSpecialization.Definition.OriginalScalar]);
            }
        }
    }

    private static T? RecursivelySearchForDefined<T>(ScalarSpecializationType scalarType, IScalarPopulation scalarPopulation, Func<IScalarType, T?> itemDelegate)
    {
        return recursivelySearch(scalarType);

        T? recursivelySearch(IScalarType scalar)
        {
            if (itemDelegate(scalar) is T item)
            {
                return item;
            }

            if (scalar is IScalarSpecializationType scalarSpecialization)
            {
                return recursivelySearch(scalarPopulation.Scalars[scalarSpecialization.Definition.OriginalScalar]);
            }

            return default;
        }
    }
}
