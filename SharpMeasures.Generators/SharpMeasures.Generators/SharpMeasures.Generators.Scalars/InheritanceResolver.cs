namespace SharpMeasures.Generators.Scalars;

using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Units;
using SharpMeasures.Generators.Units.UnitInstances;
using SharpMeasures.Generators.Unresolved.Scalars;
using SharpMeasures.Generators.Unresolved.Units.UnitInstances;

using System;
using System.Collections.Generic;
using System.Linq;

internal static class InheritanceResolver
{
    public static InheritanceData Resolve<TScalarType>(TScalarType scalarType, IUnitPopulation unitPopulation, IScalarPopulation scalarPopulation)
        where TScalarType : IScalarType
    {
        var baseScalar = scalarPopulation.BaseScalarByScalarType[scalarType.Type.AsNamedType()].Definition;
        var unit = unitPopulation.Units[baseScalar.Unit.Type.AsNamedType()];

        var defaultUnit = GetDefaultUnit(scalarType, scalarPopulation);
        var defaultUnitSymbol = GetDefaultUnitSymbol(scalarType, scalarPopulation);

        var implementSum = GetImplementSum(scalarType, scalarPopulation);
        var implementDifference = GetImplementDifference(scalarType, scalarPopulation);

        var difference = GetDifference(scalarType, scalarPopulation);

        var includedBases = GetIncludedUnits(unit, scalarType, scalarPopulation, static (scalar) => scalar.BaseInclusions,
            static (scalar) => scalar.BaseExclusions, static (scalar) => scalar.Definition.InheritBases);

        var includedUnits = GetIncludedUnits(unit, scalarType, scalarPopulation, static (scalar) => scalar.UnitInclusions,
            static (scalar) => scalar.UnitExclusions, static (scalar) => scalar.Definition.InheritUnits);

        var derivations = GetDerivations(scalarType, scalarPopulation);
        var constants = GetConstants(scalarType, scalarPopulation);
        var convertibles = GetConvertibles(scalarType, scalarPopulation);

        var reciprocal = GetReciprocal(scalarType, scalarPopulation);
        var square = GetSquare(scalarType, scalarPopulation);
        var cube = GetCube(scalarType, scalarPopulation);
        var squareRoot = GetSquareRoot(scalarType, scalarPopulation);
        var cubeRoot = GetCubeRoot(scalarType, scalarPopulation);

        return new(baseScalar, defaultUnit, defaultUnitSymbol, implementSum, implementDifference, difference, includedBases, includedUnits, derivations, constants,
            convertibles, reciprocal, square, cube, squareRoot, cubeRoot);
    }

    private static IUnresolvedUnitInstance? GetDefaultUnit(IScalarType scalar, IScalarPopulation scalarPopulation)
    {
        return RecursivelySearchForDefined(scalar, scalarPopulation, static (scalar) => scalar.Definition.DefaultUnit);
    }

    private static string? GetDefaultUnitSymbol(IScalarType scalar, IScalarPopulation scalarPopulation)
    {
        return RecursivelySearchForDefined(scalar, scalarPopulation, static (scalar) => scalar.Definition.DefaultUnitSymbol);
    }

    private static bool GetImplementSum(IScalarType scalar, IScalarPopulation scalarPopulation)
    {
        return RecursivelySearchForDefined(scalar, scalarPopulation, static (scalar) => scalar.Definition.ImplementSum, true)!.Value;
    }

    private static bool GetImplementDifference(IScalarType scalar, IScalarPopulation scalarPopulation)
    {
        return RecursivelySearchForDefined(scalar, scalarPopulation, static (scalar) => scalar.Definition.ImplementDifference, true)!.Value;
    }

    private static IUnresolvedScalarType GetDifference(IScalarType scalar, IScalarPopulation scalarPopulation)
    {
        return RecursivelySearchForDefined(scalar, scalarPopulation, static (scalar) => scalar.Definition.Difference)!;
    }

    private static IReadOnlyList<IUnitInstance> GetIncludedUnits(IUnitType unit, IScalarType scalar, IScalarPopulation scalarPopulation,
        Func<IScalarType, IEnumerable<IUnitList>> inclusionsDelegate, Func<IScalarType, IEnumerable<IUnitList>> exclusionsDelegate,
        Func<ISpecializedScalarType, bool> shouldInheritDelegate)
    {
        HashSet<IUnitInstance> includedUnits = new();

        recursivelyModifyDictionary(scalar);

        return includedUnits.ToList();

        void recursivelyModifyDictionary(IScalarType scalar)
        {
            if (scalar is ISpecializedScalarType specializedScalarType && shouldInheritDelegate(specializedScalarType))
            {
                if (scalarPopulation.Scalars.TryGetValue(specializedScalarType.Definition.OriginalScalar.Type.AsNamedType(), out var originalScalar))
                {
                    recursivelyModifyDictionary(originalScalar);
                }
            }
            else
            {
                includedUnits.UnionWith(unit.UnitsByName.Values);
            }

            var inclusions = inclusionsDelegate(scalar).SelectMany(static (list) => list);

            if (inclusions.Any())
            {
                includedUnits.IntersectWith(inclusions.Select((inclusion) => unit.UnitsByName[inclusion.Name]));
            }
            else
            {
                var exclusions = exclusionsDelegate(scalar).SelectMany(static (list) => list);

                includedUnits.ExceptWith(exclusions.Select((exclusion) => unit.UnitsByName[exclusion.Name]));
            }
        }
    }

    private static IReadOnlyList<IDerivedQuantity> GetDerivations(IScalarType scalar, IScalarPopulation scalarPopulation)
    {
        return RecursivelyAdd(scalar, scalarPopulation, static (scalar) => scalar.Definition.InheritDerivations, static (scalar) => scalar.Derivations);
    }

    private static IReadOnlyList<IScalarConstant> GetConstants(IScalarType scalar, IScalarPopulation scalarPopulation)
    {
        return RecursivelyAdd(scalar, scalarPopulation, static (scalar) => scalar.Definition.InheritConstants, static (scalar) => scalar.Constants);
    }

    private static IReadOnlyList<IConvertibleScalar> GetConvertibles(IScalarType scalar, IScalarPopulation scalarPopulation)
    {
        return RecursivelyAdd(scalar, scalarPopulation, static (scalar) => scalar.Definition.InheritConvertibleQuantities, static (scalar) => scalar.ConvertibleScalars);
    }

    private static IUnresolvedScalarType? GetReciprocal(IScalarType scalar, IScalarPopulation scalarPopulation)
    {
        return RecursivelySearchForDefined(scalar, scalarPopulation, static (scalar) => scalar.Definition.Reciprocal);
    }

    private static IUnresolvedScalarType? GetSquare(IScalarType scalar, IScalarPopulation scalarPopulation)
    {
        return RecursivelySearchForDefined(scalar, scalarPopulation, static (scalar) => scalar.Definition.Square);
    }

    private static IUnresolvedScalarType? GetCube(IScalarType scalar, IScalarPopulation scalarPopulation)
    {
        return RecursivelySearchForDefined(scalar, scalarPopulation, static (scalar) => scalar.Definition.Cube);
    }

    private static IUnresolvedScalarType? GetSquareRoot(IScalarType scalar, IScalarPopulation scalarPopulation)
    {
        return RecursivelySearchForDefined(scalar, scalarPopulation, static (scalar) => scalar.Definition.SquareRoot);
    }

    private static IUnresolvedScalarType? GetCubeRoot(IScalarType scalar, IScalarPopulation scalarPopulation)
    {
        return RecursivelySearchForDefined(scalar, scalarPopulation, static (scalar) => scalar.Definition.CubeRoot);
    }

    private static IReadOnlyList<T> RecursivelyAdd<T>(IScalarType scalar, IScalarPopulation scalarPopulation, Func<ISpecializedScalarType, bool> predicate,
        Func<IScalarType, IEnumerable<T>> transform)
    {
        List<T> items = new();

        recursivelyAddItems(scalar);

        return items;

        void recursivelyAddItems(IScalarType scalar)
        {
            items.AddRange(transform(scalar));

            if (scalar is ISpecializedScalarType specializedScalar && predicate(specializedScalar)
                && scalarPopulation.Scalars.TryGetValue(specializedScalar.Definition.OriginalScalar.Type.AsNamedType(), out var originalScalar))
            {
                recursivelyAddItems(originalScalar);
            }
        }
    }

    private static T? RecursivelySearch<T>(IScalarType scalar, IScalarPopulation scalarPopulation, Func<IScalarType, bool> predicate, Func<IScalarType, T?> transform)
    {
        if (predicate(scalar))
        {
            return transform(scalar);
        }

        if (scalar is ISpecializedScalarType specializedScalar
            && scalarPopulation.Scalars.TryGetValue(specializedScalar.Definition.OriginalQuantity.Type.AsNamedType(), out var originalScalar))
        {
            return RecursivelySearchForDefined(originalScalar, scalarPopulation, transform);
        }

        return default;
    }

    private static T RecursivelySearch<T>(IScalarType scalar, IScalarPopulation scalarPopulation, Func<IScalarType, bool> predicate, Func<IScalarType, T?> transform,
        T defaultValue)
    {
        return RecursivelySearch(scalar, scalarPopulation, predicate, transform) ?? defaultValue;
    }

    private static T? RecursivelySearchForDefined<T>(IScalarType scalar, IScalarPopulation scalarPopulation, Func<IScalarType, T?> transform)
    {
        return RecursivelySearch<T>(scalar, scalarPopulation, (scalar) => transform(scalar) is not null, transform);
    }

    private static T RecursivelySearchForDefined<T>(IScalarType scalar, IScalarPopulation scalarPopulation, Func<IScalarType, T?> transform, T defaultValue)
    {
        return RecursivelySearchForDefined(scalar, scalarPopulation, transform) ?? defaultValue;
    }
}
