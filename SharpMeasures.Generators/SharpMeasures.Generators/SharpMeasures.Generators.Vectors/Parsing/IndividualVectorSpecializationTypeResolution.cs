namespace SharpMeasures.Generators.Vectors.Parsing;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Unresolved.Scalars;
using SharpMeasures.Generators.Unresolved.Units;
using SharpMeasures.Generators.Unresolved.Units.UnitInstances;
using SharpMeasures.Generators.Vectors;
using SharpMeasures.Generators.Vectors.Parsing.Abstraction;
using SharpMeasures.Generators.Vectors.Parsing.Contexts.Resolution;
using SharpMeasures.Generators.Vectors.Parsing.Diagnostics.Resolution;
using SharpMeasures.Generators.Vectors.Parsing.RegisterVectorGroupMember;
using SharpMeasures.Generators.Vectors.Parsing.SharpMeasuresVectorGroupMember;
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

        var inheritedConversions = ResolveInheritedCollection(intermediateVector, vectorPopulation, static (scalar) => scalar.Definition.InheritConversions,
            static (scalar) => scalar.Conversions, static (scalar) => scalar.Conversions);

        var allConversions = VectorTypePostResolutionFilter.FilterAndCombineConversions(intermediateVector.Type, intermediateVector.Conversions, inheritedConversions);

        var includedUnits = GetIncludedUnits(intermediateVector, intermediateVector.Definition.Unit, vectorPopulation, static (vector) => vector.Definition.InheritUnits,
            static (vector) => vector.UnitInclusions, static (vector) => vector.UnitExclusions, static (vector) => vector.IncludedUnits,
            static (vector) => Array.Empty<IUnresolvedUnitInstance>());

        var inheritedConstants = ResolveInheritedCollection(intermediateVector, vectorPopulation, static (scalar) => scalar.Definition.InheritConstants,
            static (scalar) => scalar.Constants, static (scalar) => scalar.Constants);

        var allConstants = VectorTypePostResolutionFilter.FilterAndCombineConstants(intermediateVector.Type, intermediateVector.Constants, inheritedConstants, includedUnits);

        IndividualVectorType reduced = new(intermediateVector.Type, intermediateVector.TypeLocation, intermediateVector.Definition,
            intermediateVector.RegisteredMembersByDimension, derivations, allConstants.Result, allConversions.Result, includedUnits);

        var allDiagnostics = allConversions.Diagnostics.Concat(allConstants.Diagnostics);

        return OptionalWithDiagnostics.Result(reduced, allDiagnostics);
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

        allDiagnostics = allDiagnostics.Concat(derivations.Diagnostics).Concat(constants.Diagnostics).Concat(conversions.Diagnostics).Concat(unitInclusions.Diagnostics) .Concat(unitExclusions.Diagnostics);

        IntermediateIndividualVectorSpecializationType product = new(unresolvedVector.Type, unresolvedVector.TypeLocation, vector.Result, MockMembersPopulation(unresolvedVector, vector.Result),
            derivations.Result, constants.Result, conversions.Result, unitInclusions.Result.SelectMany((list) => list.Units).ToList(), unitExclusions.Result.SelectMany((list) => list.Units).ToList());

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

    private static IReadOnlyList<T> ResolveInheritedCollection<T>(IIntermediateIndividualVectorSpecializationType vector, IIntermediateIndividualVectorPopulation VectorPopulation,
        Func<IIntermediateIndividualVectorSpecializationType, bool> shouldInherit, Func<IIntermediateIndividualVectorSpecializationType, IEnumerable<T>> specializationTransform,
        Func<IIndividualVectorType, IEnumerable<T>> baseTransform)
        => ResolveCollection(vector, VectorPopulation, shouldInherit, specializationTransform, baseTransform, onlyInherited: true);

    private static IReadOnlyList<T> ResolveCollection<T>(IIntermediateIndividualVectorSpecializationType vector, IIntermediateIndividualVectorPopulation vectorPopulation,
        Func<IIntermediateIndividualVectorSpecializationType, bool> shouldInherit, Func<IIntermediateIndividualVectorSpecializationType, IEnumerable<T>> specializationTransform,
        Func<IIndividualVectorType, IEnumerable<T>> baseTransform, bool onlyInherited = false)
    {
        List<T> items = new();

        recursivelyAdd(vector, onlyInherited);

        return items;

        void recursivelyAdd(IIntermediateIndividualVectorSpecializationType vector, bool onlyInherited = false)
        {
            if (onlyInherited is false)
            {
                items.AddRange(specializationTransform(vector));
            }

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

    private static IReadOnlyDictionary<int, IRegisteredVectorGroupMember> MockMembersPopulation(UnresolvedIndividualVectorSpecializationType unresolvedType, SpecializedSharpMeasuresVectorDefinition vector)
    {
        UnresolvedSharpMeasuresVectorGroupMemberDefinition mockedMember = new(unresolvedType.Type.AsNamedType(), vector.Dimension, false, SharpMeasuresVectorGroupMemberLocations.Empty);
        UnresolvedVectorGroupMemberType mockedType = new(unresolvedType.Type, unresolvedType.TypeLocation, mockedMember, unresolvedType.Constants);

        return new Dictionary<int, IRegisteredVectorGroupMember>(1)
        {
            { vector.Dimension, new RegisterVectorGroupMemberDefinition(mockedType, vector.Dimension, RegisterVectorGroupMemberLocations.Empty) }
        };
    }

    private static SpecializedSharpMeasuresVectorResolver SpecializedSharpMeasuresVectorResolver { get; } = new(SpecializedSharpMeasuresVectorResolutionDiagnostics.Instance);
}
