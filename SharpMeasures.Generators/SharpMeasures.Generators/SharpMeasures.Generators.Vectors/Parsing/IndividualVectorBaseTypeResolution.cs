namespace SharpMeasures.Generators.Vectors.Parsing;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Raw.Scalars;
using SharpMeasures.Generators.Raw.Units;
using SharpMeasures.Generators.Raw.Units.UnitInstances;
using SharpMeasures.Generators.Vectors;
using SharpMeasures.Generators.Vectors.Parsing.Abstraction;
using SharpMeasures.Generators.Vectors.Parsing.Contexts.Resolution;
using SharpMeasures.Generators.Vectors.Parsing.Diagnostics.Resolution;
using SharpMeasures.Generators.Vectors.Parsing.SharpMeasuresVector;
using SharpMeasures.Generators.Vectors.Parsing.SharpMeasuresVectorGroupMember;
using SharpMeasures.Generators.Vectors.Parsing.VectorConstant;
using SharpMeasures.Generators.Vectors.Parsing.ConvertibleVector;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using SharpMeasures.Generators.Raw.Vectors.Groups;

internal static class IndividualVectorBaseTypeResolution
{
    public static IOptionalWithDiagnostics<IndividualVectorType> Resolve((UnresolvedIndividualVectorBaseType Vector, IRawUnitPopulation UnitPopulation,
        IRawScalarPopulation ScalarPopulation, IUnresolvedVectorPopulationWithData VectorPopulation) input, CancellationToken _)
        => Resolve(input.Vector, input.UnitPopulation, input.ScalarPopulation, input.VectorPopulation);

    public static IOptionalWithDiagnostics<IndividualVectorType> Resolve(UnresolvedIndividualVectorBaseType unresolvedVector, IRawUnitPopulation unitPopulation,
        IRawScalarPopulation scalarPopulation, IUnresolvedVectorPopulationWithData vectorPopulation)
    {
        SharpMeasuresVectorResolutionContext vectorResolutionContext = new(unresolvedVector.Type, unitPopulation, scalarPopulation, vectorPopulation);

        var vector = SharpMeasuresVectorResolver.Process(vectorResolutionContext, unresolvedVector.Definition);
        var allDiagnostics = vector.Diagnostics;

        if (vector.LacksResult)
        {
            return OptionalWithDiagnostics.Empty<IndividualVectorType>(allDiagnostics);
        }

        var derivations = IndividualVectorTypeResolution.ResolveDerivations(unresolvedVector.Type, unresolvedVector.Derivations, scalarPopulation, vectorPopulation);
        var constants = IndividualVectorTypeResolution.ResolveConstants(unresolvedVector.Type, unresolvedVector.Constants, vector.Result.Unit, vector.Result.Dimension);
        var conversions = IndividualVectorTypeResolution.ResolveConversions(unresolvedVector.Type, unresolvedVector.Conversions, vectorPopulation);

        var unitInclusions = IndividualVectorTypeResolution.ResolveUnitList(unresolvedVector.Type, vector.Result.Unit, unresolvedVector.UnitInclusions);
        var unitExclusions = IndividualVectorTypeResolution.ResolveUnitList(unresolvedVector.Type, vector.Result.Unit, unresolvedVector.UnitExclusions);

        var includedUnits = GetIncludedUnits(vector.Result.Unit, unitInclusions.Result, unitExclusions.Result);

        var filteredConstants = VectorTypePostResolutionFilter.FilterAndCombineConstants(unresolvedVector.Type, constants.Result, Array.Empty<VectorConstantDefinition>(), includedUnits);
        var filteredConversions = VectorTypePostResolutionFilter.FilterAndCombineConversions(unresolvedVector.Type, conversions.Result, Array.Empty<ConvertibleVectorDefinition>());

        allDiagnostics = allDiagnostics.Concat(derivations.Diagnostics).Concat(constants.Diagnostics).Concat(conversions.Diagnostics).Concat(unitInclusions.Diagnostics).Concat(unitExclusions.Diagnostics)
            .Concat(filteredConstants.Diagnostics).Concat(filteredConversions.Diagnostics);

        IndividualVectorType product = new(unresolvedVector.Type, unresolvedVector.TypeLocation, vector.Result, MockMembersPopulation(unresolvedVector, vector.Result), derivations.Result,
            filteredConstants.Result, filteredConversions.Result, includedUnits);

        return OptionalWithDiagnostics.Result(product, allDiagnostics);
    }

    private static IReadOnlyList<IRawUnitInstance> GetIncludedUnits(IRawUnitType unit, IEnumerable<IUnitList> inclusions, IEnumerable<IUnitList> exclusions)
    {
        if (inclusions.Any())
        {
            return inclusions.SelectMany(static (unitList) => unitList).ToList();
        }

        HashSet<IRawUnitInstance> includedUnits = new(unit.UnitsByName.Values);

        foreach (var exclusion in exclusions.SelectMany(static (unitList) => unitList))
        {
            includedUnits.Remove(exclusion);
        }

        return includedUnits.ToList();
    }

    private static IReadOnlyDictionary<int, IRawVectorGroupMemberType> MockMembersPopulation(UnresolvedIndividualVectorBaseType unresolvedType, SharpMeasuresVectorDefinition vector)
    {
        UnresolvedSharpMeasuresVectorGroupMemberDefinition mockedMember = new(unresolvedType.Type.AsNamedType(), vector.Dimension, false, SharpMeasuresVectorGroupMemberLocations.Empty);
        UnresolvedVectorGroupMemberType mockedType = new(unresolvedType.Type, unresolvedType.TypeLocation, mockedMember, unresolvedType.Constants);

        return new Dictionary<int, IRawVectorGroupMemberType>(1)
        {
            { vector.Dimension, mockedType }
        };
    }

    private static SharpMeasuresVectorResolver SharpMeasuresVectorResolver { get; } = new(SharpMeasuresVectorResolutionDiagnostics.Instance);
}
