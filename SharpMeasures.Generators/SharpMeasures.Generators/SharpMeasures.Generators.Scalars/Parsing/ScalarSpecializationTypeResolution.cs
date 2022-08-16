namespace SharpMeasures.Generators.Scalars.Parsing;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Scalars.Parsing.Abstraction;
using SharpMeasures.Generators.Scalars.Parsing.Contexts.Resolution;
using SharpMeasures.Generators.Scalars.Parsing.Diagnostics.Resolution;
using SharpMeasures.Generators.Scalars.Parsing.SpecializedSharpMeasuresScalar;
using SharpMeasures.Generators.Raw.Units;
using SharpMeasures.Generators.Raw.Units.UnitInstances;
using SharpMeasures.Generators.Raw.Vectors;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

internal static class ScalarSpecializationTypeResolution
{
    public static IOptionalWithDiagnostics<ScalarType> Reduce ((IntermediateScalarSpecializationType Intermediate, IIntermediateScalarPopulation Population) scalars, CancellationToken _)
        => Reduce(scalars.Intermediate, scalars.Population);

    public static IOptionalWithDiagnostics<ScalarType> Reduce(IntermediateScalarSpecializationType intermediateScalar, IIntermediateScalarPopulation scalarPopulation)
    {
        var derivations = ResolveCollection(intermediateScalar, scalarPopulation, static (scalar) => scalar.Definition.InheritDerivations,
            static (scalar) => scalar.Derivations, static (scalar) => scalar.Derivations);

        var inheritedConversions = ResolveInheritedCollection(intermediateScalar, scalarPopulation, static (scalar) => scalar.Definition.InheritConversions,
            static (scalar) => scalar.Conversions, static (scalar) => scalar.Conversions);

        var allConversions = ScalarTypePostResolutionFilter.FilterAndCombineConversions(intermediateScalar.Type, intermediateScalar.Conversions, inheritedConversions);

        var includedBases = GetIncludedUnits(intermediateScalar, intermediateScalar.Definition.Unit, scalarPopulation, static (scalar) => scalar.Definition.InheritBases,
            static (scalar) => scalar.BaseInclusions, static (scalar) => scalar.BaseExclusions, static (scalar) => scalar.IncludedBases,
            static (scalar) => Array.Empty<IRawUnitInstance>());

        var includedUnits = GetIncludedUnits(intermediateScalar, intermediateScalar.Definition.Unit, scalarPopulation, static (scalar) => scalar.Definition.InheritUnits,
            static (scalar) => scalar.UnitInclusions, static (scalar) => scalar.UnitExclusions, static (scalar) => scalar.IncludedUnits,
            static (scalar) => Array.Empty<IRawUnitInstance>());

        var inheritedConstants = ResolveInheritedCollection(intermediateScalar, scalarPopulation, static (scalar) => scalar.Definition.InheritConstants,
            static (scalar) => scalar.Constants, static (scalar) => scalar.Constants);

        var allConstants = ScalarTypePostResolutionFilter.FilterAndCombineConstants(intermediateScalar.Type, intermediateScalar.Constants, inheritedConstants, includedBases, includedUnits);

        ScalarType reduced = new(intermediateScalar.Type, intermediateScalar.TypeLocation, intermediateScalar.Definition, derivations, allConstants.Result, allConversions.Result, includedBases, includedUnits);
        var allDiagnostics = allConversions.Diagnostics.Concat(allConstants.Diagnostics);

        return OptionalWithDiagnostics.Result(reduced, allDiagnostics);
    }

    public static IOptionalWithDiagnostics<IntermediateScalarSpecializationType> Resolve((UnresolvedScalarSpecializationType Scalar, IRawUnitPopulation UnitPopulation,
        IUnresolvedScalarPopulationWithData ScalarPopulation, IRawVectorPopulation VectorPopulation) input, CancellationToken _)
        => Resolve(input.Scalar, input.UnitPopulation, input.ScalarPopulation, input.VectorPopulation);

    public static IOptionalWithDiagnostics<IntermediateScalarSpecializationType> Resolve(UnresolvedScalarSpecializationType unresolvedScalar,
        IRawUnitPopulation unitPopulation, IUnresolvedScalarPopulationWithData scalarPopulation, IRawVectorPopulation vectorPopulation)
    {
        SpecializedSharpMeasuresScalarResolutionContext scalarResolutionContext = new(unresolvedScalar.Type, unitPopulation, scalarPopulation, vectorPopulation);

        var scalar = SpecializedSharpMeasuresScalarResolver.Process(scalarResolutionContext, unresolvedScalar.Definition);
        var allDiagnostics = scalar.Diagnostics;

        if (scalar.LacksResult)
        {
            return OptionalWithDiagnostics.Empty<IntermediateScalarSpecializationType>(allDiagnostics);
        }

        var derivations = ScalarTypeResolution.ResolveDerivations(unresolvedScalar.Type, unresolvedScalar.Derivations, scalarPopulation, vectorPopulation);
        var constants = ScalarTypeResolution.ResolveConstants(unresolvedScalar.Type, unresolvedScalar.Constants, scalar.Result.Unit);
        var conversions = ScalarTypeResolution.ResolveConversions(unresolvedScalar.Type, unresolvedScalar.Conversions, scalarPopulation, scalar.Result.UseUnitBias);

        var baseInclusions = ScalarTypeResolution.ResolveUnitList(unresolvedScalar.Type, scalar.Result.Unit, unresolvedScalar.BaseInclusions);
        var baseExclusions = ScalarTypeResolution.ResolveUnitList(unresolvedScalar.Type, scalar.Result.Unit, unresolvedScalar.BaseExclusions);

        var unitInclusions = ScalarTypeResolution.ResolveUnitList(unresolvedScalar.Type, scalar.Result.Unit, unresolvedScalar.UnitInclusions);
        var unitExclusions = ScalarTypeResolution.ResolveUnitList(unresolvedScalar.Type, scalar.Result.Unit, unresolvedScalar.UnitExclusions);

        allDiagnostics = allDiagnostics.Concat(derivations.Diagnostics).Concat(constants.Diagnostics).Concat(conversions.Diagnostics).Concat(baseInclusions.Diagnostics)
            .Concat(baseExclusions.Diagnostics).Concat(unitInclusions.Diagnostics).Concat(unitExclusions.Diagnostics);

        IntermediateScalarSpecializationType product = new(unresolvedScalar.Type, unresolvedScalar.TypeLocation, scalar.Result, derivations.Result, constants.Result,
            conversions.Result, baseInclusions.Result.SelectMany((list) => list.Units).ToList(), baseExclusions.Result.SelectMany((list) => list.Units).ToList(),
            unitInclusions.Result.SelectMany((list) => list.Units).ToList(), unitExclusions.Result.SelectMany((list) => list.Units).ToList());

        return OptionalWithDiagnostics.Result(product, allDiagnostics);
    }

    private static IReadOnlyList<IRawUnitInstance> GetIncludedUnits(IIntermediateScalarSpecializationType scalar, IRawUnitType unit,
        IIntermediateScalarPopulation scalarPopulation, Func<IIntermediateScalarSpecializationType, bool> shouldInherit,
        Func<IIntermediateScalarSpecializationType, IEnumerable<IRawUnitInstance>> specializationInclusions,
        Func<IIntermediateScalarSpecializationType, IEnumerable<IRawUnitInstance>> specializationExclusions,
        Func<IScalarType, IEnumerable<IRawUnitInstance>> baseInclusions,
        Func<IScalarType, IEnumerable<IRawUnitInstance>> baseExclusions)
    {
        HashSet<IRawUnitInstance> includedUnits = new(unit.UnitsByName.Values);

        recursivelyModify(scalar);

        return includedUnits.ToList();

        void recursivelyModify(IIntermediateScalarSpecializationType scalar)
        {
            if (shouldInherit(scalar))
            {
                if (scalarPopulation.ScalarSpecializations.TryGetValue(scalar.Definition.OriginalScalar.Type.AsNamedType(), out var originalScalar))
                {
                    recursivelyModify(originalScalar);
                }
                else if (scalarPopulation.ScalarBases.TryGetValue(scalar.Definition.OriginalScalar.Type.AsNamedType(), out var baseScalar))
                {
                    performModification(baseInclusions(baseScalar), baseExclusions(baseScalar));
                }
            }

            performModification(specializationInclusions(scalar), specializationExclusions(scalar));
        }

        void performModification(IEnumerable<IRawUnitInstance> inclusions, IEnumerable<IRawUnitInstance> exclusions)
        {
            if (inclusions.Any())
            {
                includedUnits.IntersectWith(inclusions);
            }
            else
            {
                includedUnits.ExceptWith(exclusions);
            }
        }
    }

    private static IReadOnlyList<T> ResolveInheritedCollection<T>(IIntermediateScalarSpecializationType scalar, IIntermediateScalarPopulation scalarPopulation,
        Func<IIntermediateScalarSpecializationType, bool> shouldInherit, Func<IIntermediateScalarSpecializationType, IEnumerable<T>> specializationTransform,
        Func<IScalarType, IEnumerable<T>> baseTransform)
        => ResolveCollection(scalar, scalarPopulation, shouldInherit, specializationTransform, baseTransform, onlyInherited: true);

    private static IReadOnlyList<T> ResolveCollection<T>(IIntermediateScalarSpecializationType scalar, IIntermediateScalarPopulation scalarPopulation,
        Func<IIntermediateScalarSpecializationType, bool> shouldInherit, Func<IIntermediateScalarSpecializationType, IEnumerable<T>> specializationTransform,
        Func<IScalarType, IEnumerable<T>> baseTransform, bool onlyInherited = false)
    {
        List<T> items = new();

        recursivelyAdd(scalar, onlyInherited);

        return items;

        void recursivelyAdd(IIntermediateScalarSpecializationType scalar, bool onlyInherited = false)
        {
            if (onlyInherited is false)
            {
                items.AddRange(specializationTransform(scalar));
            }

            if (shouldInherit(scalar))
            {
                if (scalarPopulation.ScalarSpecializations.TryGetValue(scalar.Definition.OriginalScalar.Type.AsNamedType(), out var originalScalar))
                {
                    recursivelyAdd(originalScalar);
                    return;
                }

                if (scalarPopulation.ScalarBases.TryGetValue(scalar.Definition.OriginalScalar.Type.AsNamedType(), out var baseScalar))
                {
                    items.AddRange(baseTransform(baseScalar));
                }
            }
        }
    }

    private static SpecializedSharpMeasuresScalarResolver SpecializedSharpMeasuresScalarResolver { get; } = new(SpecializedSharpMeasuresScalarResolutionDiagnostics.Instance);
}
