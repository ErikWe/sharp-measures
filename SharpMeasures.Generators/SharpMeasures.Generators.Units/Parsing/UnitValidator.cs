namespace SharpMeasures.Generators.Units.Parsing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Units.Parsing.Abstractions;
using SharpMeasures.Generators.Units.Parsing.BiasedUnit;
using SharpMeasures.Generators.Units.Parsing.Contexts.Validation;
using SharpMeasures.Generators.Units.Parsing.DerivableUnit;
using SharpMeasures.Generators.Units.Parsing.DerivedUnit;
using SharpMeasures.Generators.Units.Parsing.Diagnostics.Validation;
using SharpMeasures.Generators.Units.Parsing.PrefixedUnit;
using SharpMeasures.Generators.Units.Parsing.ScaledUnit;
using SharpMeasures.Generators.Units.Parsing.SharpMeasuresUnit;
using SharpMeasures.Generators.Units.Parsing.UnitAlias;
using SharpMeasures.Generators.Units.UnitInstances;

using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;

public interface IUnitValidator
{
    public abstract (IncrementalValueProvider<IUnitPopulation>, IUnitGenerator) Validate(IncrementalGeneratorInitializationContext context, IncrementalValueProvider<IScalarPopulation> scalarPopulationProvider);
}

internal class UnitValidator : IUnitValidator
{
    private IncrementalValueProvider<IUnitPopulationWithData> UnitPopulationProvider { get; }

    private IncrementalValuesProvider<UnitType> UnitProvider { get; }

    public UnitValidator(IncrementalValueProvider<IUnitPopulationWithData> unitPopulationProvider, IncrementalValuesProvider<UnitType> unitProvider)
    {
        UnitPopulationProvider = unitPopulationProvider;

        UnitProvider = unitProvider;
    }

    public (IncrementalValueProvider<IUnitPopulation>, IUnitGenerator) Validate(IncrementalGeneratorInitializationContext context, IncrementalValueProvider<IScalarPopulation> scalarPopulationProvider)
    {
        var validatedUnits = UnitProvider.Combine(UnitPopulationProvider, scalarPopulationProvider).Select(Validate).ReportDiagnostics(context);

        var population = validatedUnits.Select(ExtractInterface).Collect().Select(CreatePopulation);

        var reducedPopulation = population.Select(ExtractInterface);

        return (reducedPopulation, new UnitGenerator(population, validatedUnits));
    }

    private static IOptionalWithDiagnostics<UnitType> Validate((UnitType UnvalidatedUnit, IUnitPopulationWithData UnitPopulation, IScalarPopulation ScalarPopulation) input, CancellationToken _)
    {
        var unit = ValidateUnit(input.UnvalidatedUnit, input.UnitPopulation, input.ScalarPopulation);

        if (unit.IsInvalid)
        {
            return unit.AsEmptyOptional<UnitType>();
        }

        var derivations = ValidateDerivations(input.UnvalidatedUnit, input.UnitPopulation);

        var cyclicDependantUnits = GetCyclicDependantUnits(input.UnvalidatedUnit);

        var unitInstanceValidationContext = new DependantUnitValidationContext(input.UnvalidatedUnit.Type, input.UnvalidatedUnit.UnitsByName, cyclicDependantUnits);

        var unitAliases = ValidateUnitAliases(input.UnvalidatedUnit, unitInstanceValidationContext);
        var derivedUnits = ValidateDerivedUnits(input.UnvalidatedUnit, input.UnitPopulation);
        var biasedUnits = ValidateBiasedUnits(input.UnvalidatedUnit, cyclicDependantUnits);
        var prefixedUnits = ValidatePrefixedUnits(input.UnvalidatedUnit, unitInstanceValidationContext);
        var scaledUnits = ValidateScaledUnits(input.UnvalidatedUnit, unitInstanceValidationContext);

        UnitType unitType = new(input.UnvalidatedUnit.Type, input.UnvalidatedUnit.TypeLocation, input.UnvalidatedUnit.Definition, derivations.Result, input.UnvalidatedUnit.FixedUnit, unitAliases.Result,
            derivedUnits.Result, biasedUnits.Result, prefixedUnits.Result, scaledUnits.Result);

        var allDiagnostics = unit.Concat(derivations).Concat(unitAliases).Concat(derivedUnits).Concat(biasedUnits).Concat(prefixedUnits).Concat(scaledUnits);

        return OptionalWithDiagnostics.Result(unitType, allDiagnostics);
    }

    private static IValidityWithDiagnostics ValidateUnit(UnitType unitType, IUnitPopulationWithData unitPopulation, IScalarPopulation scalarPopulation)
    {
        var validationContext = new SharpMeasuresUnitValidationContext(unitType.Type, unitPopulation, scalarPopulation);

        return ValidityFilter.Create(SharpMeasuresUnitValidator).Validate(validationContext, unitType.Definition);
    }

    private static IResultWithDiagnostics<IReadOnlyList<DerivableUnitDefinition>> ValidateDerivations(UnitType unitType, IUnitPopulationWithData unitPopulation)
    {
        var validationContext = new DerivableUnitValidationContext(unitType.Type, unitPopulation);

        return ValidityFilter.Create(DerivableUnitValidator).Filter(validationContext, unitType.UnitDerivations);
    }

    private static HashSet<IDependantUnitInstance> GetCyclicDependantUnits(UnitType unitType)
    {
        HashSet<IDependantUnitInstance> unresolvedUnitInstances = new((unitType.UnitAliases as IEnumerable<IDependantUnitInstance>).Concat(unitType.BiasedUnits).Concat(unitType.PrefixedUnits).Concat(unitType.ScaledUnits));
        HashSet<string> resolvedUnitInstances = new(unitType.DerivedUnits.Select(static (unit) => unit.Name));

        if (unitType.FixedUnit is not null)
        {
            resolvedUnitInstances.Add(unitType.FixedUnit.Name);
        }

        while (true)
        {
            int initialLength = unresolvedUnitInstances.Count;

            foreach (var unresolvedUnitInstance in unresolvedUnitInstances)
            {
                if (resolvedUnitInstances.Contains(unresolvedUnitInstance.DependantOn))
                {
                    resolvedUnitInstances.Add(unresolvedUnitInstance.Name);
                    unresolvedUnitInstances.Remove(unresolvedUnitInstance);
                }
            }

            if (unresolvedUnitInstances.Count == initialLength)
            {
                break;
            }
        }

        return unresolvedUnitInstances;
    }

    private static IResultWithDiagnostics<IReadOnlyList<UnitAliasDefinition>> ValidateUnitAliases(UnitType unitType, IDependantUnitValidationContext validationContext)
    {
        return ValidityFilter.Create(UnitAliasValidator).Filter(validationContext, unitType.UnitAliases);
    }

    private static IResultWithDiagnostics<IReadOnlyList<DerivedUnitDefinition>> ValidateDerivedUnits(UnitType unitType, IUnitPopulationWithData unitPopulation)
    {
        var unnamedUnitDerivation = unitType.UnitDerivations.Count == 1 && unitType.UnitDerivations[0].DerivationID is null ? unitType.UnitDerivations[0] : null;

        var validationContext = new DerivedUnitValidationContext(unitType.Type, unnamedUnitDerivation, unitType.DerivationsByID, unitPopulation);

        return ValidityFilter.Create(DerivedUnitValidator).Filter(validationContext, unitType.DerivedUnits);
    }

    private static IResultWithDiagnostics<IReadOnlyList<BiasedUnitDefinition>> ValidateBiasedUnits(UnitType unitType, HashSet<IDependantUnitInstance> cyclicDependantUnits)
    {
        var validationContext = new BiasedUnitValidationContext(unitType.Type, unitType.Definition.BiasTerm, unitType.UnitsByName, cyclicDependantUnits);

        return ValidityFilter.Create(BiasedUnitValidator).Filter(validationContext, unitType.BiasedUnits);
    }

    private static IResultWithDiagnostics<IReadOnlyList<PrefixedUnitDefinition>> ValidatePrefixedUnits(UnitType unitType, IDependantUnitValidationContext validationContext)
    {
        return ValidityFilter.Create(PrefixedUnitValidator).Filter(validationContext, unitType.PrefixedUnits);
    }

    private static IResultWithDiagnostics<IReadOnlyList<ScaledUnitDefinition>> ValidateScaledUnits(UnitType unitType, IDependantUnitValidationContext validationContext)
    {
        return ValidityFilter.Create(ScaledUnitValidator).Filter(validationContext, unitType.ScaledUnits);
    }

    private static IUnitType ExtractInterface(UnitType unitType, CancellationToken _) => unitType;
    private static IUnitPopulation ExtractInterface(IUnitPopulation population, CancellationToken _) => population;

    private static IUnitPopulationWithData CreatePopulation(ImmutableArray<IUnitType> units, CancellationToken _)
    {
        return UnitPopulation.Build(units);
    }

    private static SharpMeasuresUnitValidator SharpMeasuresUnitValidator { get; } = new(SharpMeasuresUnitValidationDiagnostics.Instance);

    private static DerivableUnitValidator DerivableUnitValidator { get; } = new(DerivableUnitValidationDiagnostics.Instance);

    private static UnitAliasValidator UnitAliasValidator { get; } = new(UnitAliasValidationDiagnostics.Instance);
    private static DerivedUnitValidator DerivedUnitValidator { get; } = new(DerivedUnitValidationDiagnostics.Instance);
    private static BiasedUnitValidator BiasedUnitValidator { get; } = new(BiasedUnitValidationDiagnostics.Instance);
    private static PrefixedUnitValidator PrefixedUnitValidator { get; } = new(PrefixedUnitValidationDiagnostics.Instance);
    private static ScaledUnitValidator ScaledUnitValidator { get; } = new(ScaledUnitValidationDiagnostics.Instance);
}
