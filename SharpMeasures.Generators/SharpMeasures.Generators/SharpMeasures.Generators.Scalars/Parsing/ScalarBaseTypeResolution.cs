namespace SharpMeasures.Generators.Scalars.Parsing;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Scalars.Parsing.Abstraction;
using SharpMeasures.Generators.Scalars.Parsing.Contexts.Resolution;
using SharpMeasures.Generators.Scalars.Parsing.ConvertibleScalar;
using SharpMeasures.Generators.Scalars.Parsing.Diagnostics.Resolution;
using SharpMeasures.Generators.Scalars.Parsing.ScalarConstant;
using SharpMeasures.Generators.Scalars.Parsing.SharpMeasuresScalar;
using SharpMeasures.Generators.Unresolved.Units;
using SharpMeasures.Generators.Unresolved.Units.UnitInstances;
using SharpMeasures.Generators.Unresolved.Vectors;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

internal static class ScalarBaseTypeResolution
{
    public static IOptionalWithDiagnostics<ScalarType> Resolve((UnresolvedScalarBaseType Scalar, IUnresolvedUnitPopulation UnitPopulation,
        IUnresolvedScalarPopulationWithData ScalarPopulation, IUnresolvedVectorPopulation VectorPopulation) input, CancellationToken _)
        => Resolve(input.Scalar, input.UnitPopulation, input.ScalarPopulation, input.VectorPopulation);

    public static IOptionalWithDiagnostics<ScalarType> Resolve(UnresolvedScalarBaseType unresolvedScalar, IUnresolvedUnitPopulation unitPopulation,
        IUnresolvedScalarPopulationWithData scalarPopulation, IUnresolvedVectorPopulation vectorPopulation)
    {
        SharpMeasuresScalarResolutionContext scalarResolutionContext = new(unresolvedScalar.Type, unitPopulation, scalarPopulation, vectorPopulation);

        var scalar = SharpMeasuresScalarResolver.Process(scalarResolutionContext, unresolvedScalar.Definition);
        var allDiagnostics = scalar.Diagnostics;

        if (scalar.LacksResult)
        {
            return OptionalWithDiagnostics.Empty<ScalarType>(allDiagnostics);
        }

        var derivations = ScalarTypeResolution.ResolveDerivations(unresolvedScalar.Type, unresolvedScalar.Derivations, scalarPopulation, vectorPopulation);
        var constants = ScalarTypeResolution.ResolveConstants(unresolvedScalar.Type, unresolvedScalar.Constants, scalar.Result.Unit);
        var conversions = ScalarTypeResolution.ResolveConversions(unresolvedScalar.Type, unresolvedScalar.Conversions, scalarPopulation, scalar.Result.UseUnitBias);

        var baseInclusions = ScalarTypeResolution.ResolveUnitList(unresolvedScalar.Type, scalar.Result.Unit, unresolvedScalar.BaseInclusions);
        var baseExclusions = ScalarTypeResolution.ResolveUnitList(unresolvedScalar.Type, scalar.Result.Unit, unresolvedScalar.BaseExclusions);

        var unitInclusions = ScalarTypeResolution.ResolveUnitList(unresolvedScalar.Type, scalar.Result.Unit, unresolvedScalar.UnitInclusions);
        var unitExclusions = ScalarTypeResolution.ResolveUnitList(unresolvedScalar.Type, scalar.Result.Unit, unresolvedScalar.UnitExclusions);

        var includedBases = GetIncludedUnits(scalar.Result.Unit, baseInclusions.Result, baseExclusions.Result);
        var includedUnits = GetIncludedUnits(scalar.Result.Unit, unitInclusions.Result, unitExclusions.Result);

        var filteredConstants = ScalarTypePostResolutionFilter.FilterAndCombineConstants(unresolvedScalar.Type, constants.Result, Array.Empty<ScalarConstantDefinition>(), includedBases, includedUnits);
        var filteredConversions = ScalarTypePostResolutionFilter.FilterAndCombineConversions(unresolvedScalar.Type, conversions.Result, Array.Empty<ConvertibleScalarDefinition>());

        allDiagnostics = allDiagnostics.Concat(derivations.Diagnostics).Concat(constants.Diagnostics).Concat(conversions.Diagnostics).Concat(baseInclusions.Diagnostics)
            .Concat(baseExclusions.Diagnostics).Concat(unitInclusions.Diagnostics).Concat(unitExclusions.Diagnostics).Concat(filteredConstants.Diagnostics).Concat(filteredConversions.Diagnostics);

        ScalarType product = new(unresolvedScalar.Type, unresolvedScalar.TypeLocation, scalar.Result, derivations.Result, filteredConstants.Result, filteredConversions.Result, includedBases, includedUnits);

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

    private static SharpMeasuresScalarResolver SharpMeasuresScalarResolver { get; } = new(SharpMeasuresScalarResolutionDiagnostics.Instance);
}
