namespace SharpMeasures.Generators.Vectors.Parsing;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Raw.Scalars;
using SharpMeasures.Generators.Raw.Units;
using SharpMeasures.Generators.Raw.Units.UnitInstances;
using SharpMeasures.Generators.Vectors;
using SharpMeasures.Generators.Vectors.Groups;
using SharpMeasures.Generators.Vectors.Parsing.Abstraction;
using SharpMeasures.Generators.Vectors.Parsing.Contexts.Resolution;
using SharpMeasures.Generators.Vectors.Parsing.Diagnostics.Resolution;
using SharpMeasures.Generators.Vectors.Parsing.SpecializedSharpMeasuresVectorGroup;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

internal static class VectorGroupSpecializationTypeResolution
{
    public static IOptionalWithDiagnostics<VectorGroupType> Reduce ((IntermediateVectorGroupSpecializationType Intermediate, IIntermediateVectorGroupPopulation Population) vectors, CancellationToken _)
        => Reduce(vectors.Intermediate, vectors.Population);

    public static IOptionalWithDiagnostics<VectorGroupType> Reduce(IntermediateVectorGroupSpecializationType intermediateVector, IIntermediateVectorGroupPopulation vectorPopulation)
    {
        var derivations = ResolveCollection(intermediateVector, vectorPopulation, static (scalar) => scalar.Definition.InheritDerivations,
            static (scalar) => scalar.Derivations, static (scalar) => scalar.Derivations);

        var inheritedConversions = ResolveInheritedCollection(intermediateVector, vectorPopulation, static (scalar) => scalar.Definition.InheritConversions,
            static (scalar) => scalar.Conversions, static (scalar) => scalar.Conversions);

        var allConversions = VectorTypePostResolutionFilter.FilterAndCombineConversions(intermediateVector.Type, intermediateVector.Conversions, inheritedConversions);

        var includedUnits = GetIncludedUnits(intermediateVector, intermediateVector.Definition.Unit, vectorPopulation, static (scalar) => scalar.Definition.InheritUnits,
            static (scalar) => scalar.UnitInclusions, static (scalar) => scalar.UnitExclusions, static (scalar) => scalar.IncludedUnits,
            static (scalar) => Array.Empty<IRawUnitInstance>());

        VectorGroupType reduced = new(intermediateVector.Type, intermediateVector.TypeLocation, intermediateVector.Definition, intermediateVector.MembersByDimension,
            derivations, allConversions.Result, includedUnits);

        var allDiagnostics = allConversions.Diagnostics;

        return OptionalWithDiagnostics.Result(reduced, allDiagnostics);
    }

    public static IOptionalWithDiagnostics<IntermediateVectorGroupSpecializationType> Resolve((UnresolvedVectorGroupSpecializationType Vector,
        IRawUnitPopulation UnitPopulation, IRawScalarPopulation ScalarPopulation, IUnresolvedVectorPopulationWithData VectorPopulation) input, CancellationToken _)
        => Resolve(input.Vector, input.UnitPopulation, input.ScalarPopulation, input.VectorPopulation);

    public static IOptionalWithDiagnostics<IntermediateVectorGroupSpecializationType> Resolve(UnresolvedVectorGroupSpecializationType unresolvedVector,
        IRawUnitPopulation unitPopulation, IRawScalarPopulation scalarPopulation, IUnresolvedVectorPopulationWithData vectorPopulation)
    {
        SpecializedSharpMeasuresVectorGroupResolutionContext vectorResolutionContext = new(unresolvedVector.Type, unitPopulation, scalarPopulation, vectorPopulation);

        var vector = SpecializedSharpMeasuresVectorGroupResolver.Process(vectorResolutionContext, unresolvedVector.Definition);
        var allDiagnostics = vector.Diagnostics;

        if (vector.LacksResult)
        {
            return OptionalWithDiagnostics.Empty<IntermediateVectorGroupSpecializationType>(allDiagnostics);
        }

        var derivations = VectorGroupTypeResolution.ResolveDerivations(unresolvedVector.Type, unresolvedVector.Derivations, scalarPopulation, vectorPopulation);
        var conversions = VectorGroupTypeResolution.ResolveConversions(unresolvedVector.Type, unresolvedVector.Conversions, vectorPopulation);

        var unitInclusions = VectorGroupTypeResolution.ResolveUnitList(unresolvedVector.Type, vector.Result.Unit, unresolvedVector.UnitInclusions);
        var unitExclusions = VectorGroupTypeResolution.ResolveUnitList(unresolvedVector.Type, vector.Result.Unit, unresolvedVector.UnitExclusions);

        var membersByDimension = vectorPopulation.VectorGroupMembersByGroup[unresolvedVector.Type.AsNamedType()].VectorGroupMembersByDimension;

        allDiagnostics = allDiagnostics.Concat(derivations.Diagnostics).Concat(conversions.Diagnostics).Concat(unitInclusions.Diagnostics)
            .Concat(unitExclusions.Diagnostics);

        IntermediateVectorGroupSpecializationType product = new(unresolvedVector.Type, unresolvedVector.TypeLocation, vector.Result, derivations.Result, conversions.Result, membersByDimension,
            unitInclusions.Result.SelectMany((list) => list.Units).ToList(), unitExclusions.Result.SelectMany((list) => list.Units).ToList());

        return OptionalWithDiagnostics.Result(product, allDiagnostics);
    }

    private static IReadOnlyList<IRawUnitInstance> GetIncludedUnits(IIntermediateVectorGroupSpecializationType vector, IRawUnitType unit,
        IIntermediateVectorGroupPopulation vectorPopulation, Func<IIntermediateVectorGroupSpecializationType, bool> shouldInherit,
        Func<IIntermediateVectorGroupSpecializationType, IEnumerable<IRawUnitInstance>> specializationInclusions,
        Func<IIntermediateVectorGroupSpecializationType, IEnumerable<IRawUnitInstance>> specializationExclusions,
        Func<IVectorGroupType, IEnumerable<IRawUnitInstance>> baseInclusions,
        Func<IVectorGroupType, IEnumerable<IRawUnitInstance>> baseExclusions)
    {
        HashSet<IRawUnitInstance> includedUnits = new(unit.UnitsByName.Values);

        recursivelyModify(vector);

        return includedUnits.ToList();

        void recursivelyModify(IIntermediateVectorGroupSpecializationType vector)
        {
            if (shouldInherit(vector))
            {
                if (vectorPopulation.VectorGroupSpecializations.TryGetValue(vector.Definition.OriginalVectorGroup.Type.AsNamedType(), out var originalVector))
                {
                    recursivelyModify(originalVector);
                }
                else if (vectorPopulation.VectorGroupBases.TryGetValue(vector.Definition.OriginalVectorGroup.Type.AsNamedType(), out var baseVector))
                {
                    performModification(baseInclusions(baseVector), baseExclusions(baseVector));
                }
            }

            performModification(specializationInclusions(vector), specializationExclusions(vector));
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

    private static IReadOnlyList<T> ResolveInheritedCollection<T>(IIntermediateVectorGroupSpecializationType vector, IIntermediateVectorGroupPopulation VectorPopulation,
        Func<IIntermediateVectorGroupSpecializationType, bool> shouldInherit, Func<IIntermediateVectorGroupSpecializationType, IEnumerable<T>> specializationTransform,
        Func<IVectorGroupType, IEnumerable<T>> baseTransform)
        => ResolveCollection(vector, VectorPopulation, shouldInherit, specializationTransform, baseTransform, onlyInherited: true);

    private static IReadOnlyList<T> ResolveCollection<T>(IIntermediateVectorGroupSpecializationType vector, IIntermediateVectorGroupPopulation vectorPopulation,
        Func<IIntermediateVectorGroupSpecializationType, bool> shouldInherit, Func<IIntermediateVectorGroupSpecializationType, IEnumerable<T>> specializationTransform,
        Func<IVectorGroupType, IEnumerable<T>> baseTransform, bool onlyInherited = false)
    {
        List<T> items = new();

        recursivelyAdd(vector, onlyInherited);

        return items;

        void recursivelyAdd(IIntermediateVectorGroupSpecializationType vector, bool onlyInherited = false)
        {
            if (onlyInherited is false)
            {
                items.AddRange(specializationTransform(vector));
            }

            if (shouldInherit(vector))
            {
                if (vectorPopulation.VectorGroupSpecializations.TryGetValue(vector.Definition.OriginalVectorGroup.Type.AsNamedType(), out var originalVector))
                {
                    recursivelyAdd(originalVector);
                    return;
                }

                if (vectorPopulation.VectorGroupBases.TryGetValue(vector.Definition.OriginalVectorGroup.Type.AsNamedType(), out var baseVector))
                {
                    items.AddRange(baseTransform(baseVector));
                }
            }
        }
    }

    private static SpecializedSharpMeasuresVectorGroupResolver SpecializedSharpMeasuresVectorGroupResolver { get; } = new(SpecializedSharpMeasuresVectorGroupResolutionDiagnostics.Instance);
}
