namespace SharpMeasures.Generators.Vectors.Parsing;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Vectors;
using SharpMeasures.Generators.Unresolved.Scalars;
using SharpMeasures.Generators.Unresolved.Units;
using SharpMeasures.Generators.Unresolved.Units.UnitInstances;
using SharpMeasures.Generators.Unresolved.Vectors;
using SharpMeasures.Generators.Vectors.Parsing.Contexts.Resolution;
using SharpMeasures.Generators.Vectors.Parsing.Diagnostics.Resolution;
using SharpMeasures.Generators.Vectors.Parsing.SharpMeasuresVectorGroup;

using System.Collections.Generic;
using System.Linq;
using System.Threading;

internal static class VectorGroupBaseTypeResolution
{
    public static IOptionalWithDiagnostics<VectorGroupType> Resolve((UnresolvedVectorGroupBaseType Vector, IUnresolvedUnitPopulation UnitPopulation,
        IUnresolvedScalarPopulation ScalarPopulation, IUnresolvedVectorPopulation VectorPopulation) input, CancellationToken _)
        => Resolve(input.Vector, input.UnitPopulation, input.ScalarPopulation, input.VectorPopulation);

    public static IOptionalWithDiagnostics<VectorGroupType> Resolve(UnresolvedVectorGroupBaseType unresolvedVector, IUnresolvedUnitPopulation unitPopulation,
        IUnresolvedScalarPopulation scalarPopulation, IUnresolvedVectorPopulation vectorPopulation)
    {
        SharpMeasuresVectorGroupResolutionContext vectorResolutionContext = new(unresolvedVector.Type, unitPopulation, scalarPopulation, vectorPopulation);

        var vector = SharpMeasuresVectorGroupResolver.Process(vectorResolutionContext, unresolvedVector.Definition);
        var allDiagnostics = vector.Diagnostics;

        if (vector.LacksResult)
        {
            return OptionalWithDiagnostics.Empty<VectorGroupType>(allDiagnostics);
        }

        var members = VectorGroupTypeResolution.ResolveMembers(unresolvedVector.Type, unresolvedVector.RegisteredMembersByDimension.Values, unresolvedVector, vectorPopulation);

        var derivations = VectorGroupTypeResolution.ResolveDerivations(unresolvedVector.Type, unresolvedVector.Derivations, scalarPopulation, vectorPopulation);
        var conversions = VectorGroupTypeResolution.ResolveConversions(unresolvedVector.Type, unresolvedVector.Conversions, vectorPopulation);

        var unitInclusions = VectorGroupTypeResolution.ResolveUnitList(unresolvedVector.Type, vector.Result.Unit, unresolvedVector.UnitInclusions);
        var unitExclusions = VectorGroupTypeResolution.ResolveUnitList(unresolvedVector.Type, vector.Result.Unit, unresolvedVector.UnitExclusions);

        var includedUnits = GetIncludedUnits(vector.Result.Unit, unitInclusions.Result, unitExclusions.Result);

        allDiagnostics = allDiagnostics.Concat(members.Diagnostics).Concat(derivations.Diagnostics).Concat(conversions.Diagnostics).Concat(unitInclusions.Diagnostics)
            .Concat(unitExclusions.Diagnostics);

        var membersDictionary = members.Result.ToDictionary(static (member) => member.Dimension, static (member) => member as IRegisteredVectorGroupMember);

        VectorGroupType product = new(unresolvedVector.Type, unresolvedVector.TypeLocation, vector.Result, membersDictionary, derivations.Result,
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

    private static SharpMeasuresVectorGroupResolver SharpMeasuresVectorGroupResolver { get; } = new(SharpMeasuresVectorGroupResolutionDiagnostics.Instance);
}
