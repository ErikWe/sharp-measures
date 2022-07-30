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
using SharpMeasures.Generators.Vectors.Parsing.RegisterVectorGroupMember;
using SharpMeasures.Generators.Vectors.Parsing.SpecializedSharpMeasuresVector;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

internal static class IndividualVectorSpecializationTypeResolution
{
    public static IOptionalWithDiagnostics<IndividualVectorType> Reduce
        ((IntermediateIndividualVectorSpecializationType Intermediate, IIntermediateIndividualVectorPopulation Population) vectors, CancellationToken _)
        => Reduce(vectors.Intermediate, vectors.Population);

    public static IOptionalWithDiagnostics<IndividualVectorType> Reduce(IntermediateIndividualVectorSpecializationType intermediateVector,
        IIntermediateIndividualVectorPopulation vectorPopulation)
    {
        var derivations = ResolveCollection(intermediateVector, vectorPopulation, static (vector) => vector.Definition.InheritDerivations,
            static (vector) => vector.Derivations, static (vector) => vector.Derivations);

        var constants = ResolveCollection(intermediateVector, vectorPopulation, static (vector) => vector.Definition.InheritConstants,
            static (vector) => vector.Constants, static (vector) => vector.Constants);

        var conversions = ResolveCollection(intermediateVector, vectorPopulation, static (vector) => vector.Definition.InheritConversions,
            static (vector) => vector.Conversions, static (vector) => vector.Conversions);

        var includedUnits = GetIncludedUnits(intermediateVector, intermediateVector.Definition.Unit, vectorPopulation, static (vector) => vector.Definition.InheritUnits,
            static (vector) => vector.UnitInclusions, static (vector) => vector.UnitExclusions, static (vector) => vector.IncludedUnits,
            static (vector) => Array.Empty<IUnresolvedUnitInstance>());

        IndividualVectorType reduced = new(intermediateVector.Type, intermediateVector.TypeLocation, intermediateVector.Definition,
            intermediateVector.RegisteredMembersByDimension, derivations, constants, conversions, includedUnits);

        return OptionalWithDiagnostics.Result(reduced);
    }

    public static IOptionalWithDiagnostics<IntermediateIndividualVectorSpecializationType> Resolve((UnresolvedIndividualVectorSpecializationType Vector,
        IUnresolvedUnitPopulation UnitPopulation, IUnresolvedScalarPopulation ScalarPopulation, IUnresolvedVectorPopulationWithData VectorPopulation) input, CancellationToken _)
        => Resolve(input.Vector, input.UnitPopulation, input.ScalarPopulation, input.VectorPopulation);

    public static IOptionalWithDiagnostics<IntermediateIndividualVectorSpecializationType> Resolve(UnresolvedIndividualVectorSpecializationType unresolvedVector,
        IUnresolvedUnitPopulation unitPopulation, IUnresolvedScalarPopulation scalarPopulation, IUnresolvedVectorPopulationWithData vectorPopulation)
    {
        SpecializedSharpMeasuresVectorResolutionContext vectorResolutionContext = new(unresolvedVector.Type, unitPopulation, scalarPopulation, vectorPopulation);

        var vector = SpecializedSharpMeasuresVectorResolver.Process(vectorResolutionContext, unresolvedVector.Definition);
        var allDiagnostics = vector.Diagnostics;

        if (vector.LacksResult)
        {
            return OptionalWithDiagnostics.Empty<IntermediateIndividualVectorSpecializationType>(allDiagnostics);
        }

        var derivations = IndividualVectorTypeResolution.ResolveDerivations(unresolvedVector.Type, unresolvedVector.Derivations, scalarPopulation, vectorPopulation);
        var constants = IndividualVectorTypeResolution.ResolveConstants(unresolvedVector.Type, unresolvedVector.Constants, vector.Result.Unit, vector.Result.Dimension);
        var conversions = IndividualVectorTypeResolution.ResolveConversions(unresolvedVector.Type, unresolvedVector.Conversions, vectorPopulation);

        var unitInclusions = IndividualVectorTypeResolution.ResolveUnitList(unresolvedVector.Type, vector.Result.Unit, unresolvedVector.UnitInclusions);
        var unitExclusions = IndividualVectorTypeResolution.ResolveUnitList(unresolvedVector.Type, vector.Result.Unit, unresolvedVector.UnitExclusions);

        allDiagnostics = allDiagnostics.Concat(derivations.Diagnostics).Concat(conversions.Diagnostics).Concat(unitInclusions.Diagnostics) .Concat(unitExclusions.Diagnostics);

        var membersByDimension = (new IRegisteredVectorGroupMember[]
        {
            new RegisterVectorGroupMemberDefinition(unresolvedVector, vector.Result.Dimension, RegisterVectorGroupMemberLocations.Empty)
        }).ToDictionary(static (vector) => vector.Dimension);

        IntermediateIndividualVectorSpecializationType product = new(unresolvedVector.Type, unresolvedVector.TypeLocation, vector.Result, membersByDimension,
            derivations.Result, constants.Result, conversions.Result, unitInclusions.Result.SelectMany((list) => list.Units).ToList(),
            unitExclusions.Result.SelectMany((list) => list.Units).ToList());

        return OptionalWithDiagnostics.Result(product, allDiagnostics);
    }

    private static IReadOnlyList<IUnresolvedUnitInstance> GetIncludedUnits(IIntermediateIndividualVectorSpecializationType vector, IUnresolvedUnitType unit,
        IIntermediateIndividualVectorPopulation vectorPopulation, Func<IIntermediateIndividualVectorSpecializationType, bool> shouldInherit,
        Func<IIntermediateIndividualVectorSpecializationType, IEnumerable<IUnresolvedUnitInstance>> specializationInclusions,
        Func<IIntermediateIndividualVectorSpecializationType, IEnumerable<IUnresolvedUnitInstance>> specializationExclusions,
        Func<IIndividualVectorType, IEnumerable<IUnresolvedUnitInstance>> baseInclusions,
        Func<IIndividualVectorType, IEnumerable<IUnresolvedUnitInstance>> baseExclusions)
    {
        HashSet<IUnresolvedUnitInstance> includedUnits = new(unit.UnitsByName.Values);

        recursivelyModify(vector);

        return includedUnits.ToList();

        void recursivelyModify(IIntermediateIndividualVectorSpecializationType vector)
        {
            if (shouldInherit(vector))
            {
                if (vectorPopulation.VectorSpecializations.TryGetValue(vector.Definition.OriginalIndividualVector.Type.AsNamedType(), out var originalVector))
                {
                    recursivelyModify(originalVector);
                }
                else if (vectorPopulation.VectorBases.TryGetValue(vector.Definition.OriginalIndividualVector.Type.AsNamedType(), out var baseVector))
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

    private static IReadOnlyList<T> ResolveCollection<T>(IIntermediateIndividualVectorSpecializationType vector, IIntermediateIndividualVectorPopulation vectorPopulation,
        Func<IIntermediateIndividualVectorSpecializationType, bool> shouldInherit,
        Func<IIntermediateIndividualVectorSpecializationType, IEnumerable<T>> specializationTransform, Func<IIndividualVectorType, IEnumerable<T>> baseTransform)
    {
        List<T> items = new();

        recursivelyAdd(vector);

        return items;

        void recursivelyAdd(IIntermediateIndividualVectorSpecializationType vector)
        {
            items.AddRange(specializationTransform(vector));

            if (shouldInherit(vector))
            {
                if (vectorPopulation.VectorSpecializations.TryGetValue(vector.Definition.OriginalIndividualVector.Type.AsNamedType(), out var originalVector))
                {
                    recursivelyAdd(originalVector);
                    return;
                }

                if (vectorPopulation.VectorBases.TryGetValue(vector.Definition.OriginalIndividualVector.Type.AsNamedType(), out var baseVector))
                {
                    items.AddRange(baseTransform(baseVector));
                }
            }
        }
    }

    private static SpecializedSharpMeasuresVectorResolver SpecializedSharpMeasuresVectorResolver { get; } = new(SpecializedSharpMeasuresVectorResolutionDiagnostics.Instance);
}
