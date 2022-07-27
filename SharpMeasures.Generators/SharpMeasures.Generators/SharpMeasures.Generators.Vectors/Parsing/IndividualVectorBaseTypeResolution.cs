namespace SharpMeasures.Generators.Vectors.Parsing;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Vectors;
using SharpMeasures.Generators.Unresolved.Scalars;
using SharpMeasures.Generators.Unresolved.Units;
using SharpMeasures.Generators.Unresolved.Units.UnitInstances;
using SharpMeasures.Generators.Unresolved.Vectors;
using SharpMeasures.Generators.Vectors.Parsing.RegisterVectorGroupMember;
using SharpMeasures.Generators.Vectors.Parsing.Contexts.Resolution;
using SharpMeasures.Generators.Vectors.Parsing.Diagnostics.Resolution;
using SharpMeasures.Generators.Vectors.Parsing.SharpMeasuresVector;

using System.Collections.Generic;
using System.Linq;
using System.Threading;

internal static class IndividualVectorBaseTypeResolution
{
    public static IOptionalWithDiagnostics<IndividualVectorType> Resolve((UnresolvedIndividualVectorBaseType Vector, IUnresolvedUnitPopulation UnitPopulation,
        IUnresolvedScalarPopulation ScalarPopulation, IUnresolvedVectorPopulation VectorPopulation) input, CancellationToken _)
        => Resolve(input.Vector, input.UnitPopulation, input.ScalarPopulation, input.VectorPopulation);

    public static IOptionalWithDiagnostics<IndividualVectorType> Resolve(UnresolvedIndividualVectorBaseType unresolvedVector, IUnresolvedUnitPopulation unitPopulation,
        IUnresolvedScalarPopulation scalarPopulation, IUnresolvedVectorPopulation vectorPopulation)
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

        allDiagnostics = allDiagnostics.Concat(derivations.Diagnostics).Concat(conversions.Diagnostics).Concat(unitInclusions.Diagnostics) .Concat(unitExclusions.Diagnostics);

        var membersByDimension = (new IRegisteredVectorGroupMember[]
        {
            new RegisterVectorGroupMemberDefinition(unresolvedVector, vector.Result.Dimension, RegisterVectorGroupMemberLocations.Empty)
        }).ToDictionary(static (vector) => vector.Dimension);

        IndividualVectorType product = new(unresolvedVector.Type, unresolvedVector.TypeLocation, vector.Result, membersByDimension, derivations.Result, constants.Result,
            conversions.Result, includedUnits);

        return OptionalWithDiagnostics.Result(product, allDiagnostics);
    }

    private static IReadOnlyList<IUnresolvedUnitInstance> GetIncludedUnits(IUnresolvedUnitType unit, IEnumerable<IUnitList> inclusions, IEnumerable<IUnitList> exclusions)
    {
        if (inclusions.Any())
        {
            return inclusions.SelectMany(static (unitList) => unitList).ToList();
        }

        HashSet<IUnresolvedUnitInstance> includedUnits = new(unit.UnitsByName.Values);

        foreach (var exclusion in exclusions.SelectMany(static (unitList) => unitList))
        {
            includedUnits.Remove(exclusion);
        }

        return includedUnits.ToList();
    }

    private static SharpMeasuresVectorResolver SharpMeasuresVectorResolver { get; } = new(SharpMeasuresVectorResolutionDiagnostics.Instance);
}
