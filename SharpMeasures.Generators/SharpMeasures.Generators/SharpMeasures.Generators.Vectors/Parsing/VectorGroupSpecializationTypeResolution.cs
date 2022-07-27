namespace SharpMeasures.Generators.Vectors.Parsing;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Unresolved.Scalars;
using SharpMeasures.Generators.Unresolved.Units;
using SharpMeasures.Generators.Unresolved.Units.UnitInstances;
using SharpMeasures.Generators.Unresolved.Vectors;
using SharpMeasures.Generators.Vectors;
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
    public static IOptionalWithDiagnostics<VectorGroupType> Reduce
        ((IntermediateVectorGroupSpecializationType Intermediate, IIntermediateVectorGroupPopulation Population) vectors, CancellationToken _)
        => Reduce(vectors.Intermediate, vectors.Population);

    public static IOptionalWithDiagnostics<VectorGroupType> Reduce(IntermediateVectorGroupSpecializationType intermediateVector,
        IIntermediateVectorGroupPopulation vectorPopulation)
    {
        var derivations = ResolveCollection(intermediateVector, vectorPopulation, static (scalar) => scalar.Definition.InheritDerivations,
            static (scalar) => scalar.Derivations, static (scalar) => scalar.Derivations);

        var conversions = ResolveCollection(intermediateVector, vectorPopulation, static (scalar) => scalar.Definition.InheritConversions,
            static (scalar) => scalar.Conversions, static (scalar) => scalar.Conversions);

        var includedUnits = GetIncludedUnits(intermediateVector, intermediateVector.Definition.Unit, vectorPopulation, static (scalar) => scalar.Definition.InheritUnits,
            static (scalar) => scalar.UnitInclusions, static (scalar) => scalar.UnitExclusions, static (scalar) => scalar.IncludedUnits,
            static (scalar) => Array.Empty<IUnresolvedUnitInstance>());

        VectorGroupType reduced = new(intermediateVector.Type, intermediateVector.TypeLocation, intermediateVector.Definition, intermediateVector.RegisteredMembersByDimension,
            derivations, conversions, includedUnits);

        return OptionalWithDiagnostics.Result(reduced);
    }

    public static IOptionalWithDiagnostics<IntermediateVectorGroupSpecializationType> Resolve((UnresolvedVectorGroupSpecializationType Vector,
        IUnresolvedUnitPopulation UnitPopulation, IUnresolvedScalarPopulation ScalarPopulation, IUnresolvedVectorPopulation VectorPopulation) input, CancellationToken _)
        => Resolve(input.Vector, input.UnitPopulation, input.ScalarPopulation, input.VectorPopulation);

    public static IOptionalWithDiagnostics<IntermediateVectorGroupSpecializationType> Resolve(UnresolvedVectorGroupSpecializationType unresolvedVector,
        IUnresolvedUnitPopulation unitPopulation, IUnresolvedScalarPopulation scalarPopulation, IUnresolvedVectorPopulation vectorPopulation)
    {
        SpecializedSharpMeasuresVectorGroupResolutionContext vectorResolutionContext = new(unresolvedVector.Type, unitPopulation, scalarPopulation, vectorPopulation);

        var vector = SpecializedSharpMeasuresVectorGroupResolver.Process(vectorResolutionContext, unresolvedVector.Definition);
        var allDiagnostics = vector.Diagnostics;

        if (vector.LacksResult)
        {
            return OptionalWithDiagnostics.Empty<IntermediateVectorGroupSpecializationType>(allDiagnostics);
        }

        var members = VectorGroupTypeResolution.ResolveMembers(unresolvedVector.Type, unresolvedVector.RegisteredMembersByDimension.Values, unresolvedVector, vectorPopulation);

        var derivations = VectorGroupTypeResolution.ResolveDerivations(unresolvedVector.Type, unresolvedVector.Derivations, scalarPopulation, vectorPopulation);
        var conversions = VectorGroupTypeResolution.ResolveConversions(unresolvedVector.Type, unresolvedVector.Conversions, vectorPopulation);

        var unitInclusions = VectorGroupTypeResolution.ResolveUnitList(unresolvedVector.Type, vector.Result.Unit, unresolvedVector.UnitInclusions);
        var unitExclusions = VectorGroupTypeResolution.ResolveUnitList(unresolvedVector.Type, vector.Result.Unit, unresolvedVector.UnitExclusions);

        allDiagnostics = allDiagnostics.Concat(members.Diagnostics).Concat(derivations.Diagnostics).Concat(conversions.Diagnostics).Concat(unitInclusions.Diagnostics)
            .Concat(unitExclusions.Diagnostics);

        var membersDictionary = members.Result.ToDictionary(static (member) => member.Dimension, static (member) => member as IRegisteredVectorGroupMember);

        IntermediateVectorGroupSpecializationType product = new(unresolvedVector.Type, unresolvedVector.TypeLocation, vector.Result, membersDictionary, derivations.Result,
            conversions.Result, unitInclusions.Result.SelectMany((list) => list.Units).ToList(), unitExclusions.Result.SelectMany((list) => list.Units).ToList());

        return OptionalWithDiagnostics.Result(product, allDiagnostics);
    }

    private static IReadOnlyList<IUnresolvedUnitInstance> GetIncludedUnits(IIntermediateVectorGroupSpecializationType vector, IUnresolvedUnitType unit,
        IIntermediateVectorGroupPopulation vectorPopulation, Func<IIntermediateVectorGroupSpecializationType, bool> shouldInherit,
        Func<IIntermediateVectorGroupSpecializationType, IEnumerable<IUnresolvedUnitInstance>> specializationInclusions,
        Func<IIntermediateVectorGroupSpecializationType, IEnumerable<IUnresolvedUnitInstance>> specializationExclusions,
        Func<IVectorGroupType, IEnumerable<IUnresolvedUnitInstance>> baseInclusions,
        Func<IVectorGroupType, IEnumerable<IUnresolvedUnitInstance>> baseExclusions)
    {
        HashSet<IUnresolvedUnitInstance> includedUnits = new(unit.UnitsByName.Values);

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

        void performModification(IEnumerable<IUnresolvedUnitInstance> inclusions, IEnumerable<IUnresolvedUnitInstance> exclusions)
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

    private static IReadOnlyList<T> ResolveCollection<T>(IIntermediateVectorGroupSpecializationType vector, IIntermediateVectorGroupPopulation vectorPopulation,
        Func<IIntermediateVectorGroupSpecializationType, bool> shouldInherit, Func<IIntermediateVectorGroupSpecializationType, IEnumerable<T>> specializationTransform,
        Func<IVectorGroupType, IEnumerable<T>> baseTransform)
    {
        List<T> items = new();

        recursivelyAdd(vector);

        return items;

        void recursivelyAdd(IIntermediateVectorGroupSpecializationType vector)
        {
            items.AddRange(specializationTransform(vector));

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

    private static SpecializedSharpMeasuresVectorGroupResolver SpecializedSharpMeasuresVectorGroupResolver { get; }
        = new(SpecializedSharpMeasuresVectorGroupResolutionDiagnostics.Instance);
}
