namespace SharpMeasures.Generators.Units.Parsing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Units.Parsing.Abstractions;
using SharpMeasures.Generators.Units.Parsing.BiasedUnitInstance;
using SharpMeasures.Generators.Units.Parsing.Contexts.Validation;
using SharpMeasures.Generators.Units.Parsing.DerivableUnit;
using SharpMeasures.Generators.Units.Parsing.DerivedUnitInstance;
using SharpMeasures.Generators.Units.Parsing.Diagnostics.Validation;
using SharpMeasures.Generators.Units.Parsing.PrefixedUnitInstance;
using SharpMeasures.Generators.Units.Parsing.ScaledUnitInstance;
using SharpMeasures.Generators.Units.Parsing.SharpMeasuresUnit;
using SharpMeasures.Generators.Units.Parsing.UnitInstanceAlias;

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

    private static IOptionalWithDiagnostics<UnitType> Validate((UnitType UnvalidatedUnit, IUnitPopulationWithData UnitPopulation, IScalarPopulation ScalarPopulation) input, CancellationToken token)
        => Validate(input.UnvalidatedUnit, input.UnitPopulation, input.ScalarPopulation, token);

    private static IOptionalWithDiagnostics<UnitType> Validate(UnitType unitType, IUnitPopulationWithData unitPopulation, IScalarPopulation scalarPopulation, CancellationToken token)
    {
        if (token.IsCancellationRequested)
        {
            return OptionalWithDiagnostics.Empty<UnitType>();
        }

        var unit = ValidateUnit(unitType, unitPopulation, scalarPopulation);

        if (unit.IsInvalid)
        {
            return unit.AsEmptyOptional<UnitType>();
        }

        var derivations = ValidateDerivations(unitType, unitPopulation);

        var cyclicallyModifiedUnitInstances = GetCyclicallyModifiedUnitInstances(unitType);

        var unitInstanceValidationContext = new ModifiedUnitValidationContext(unitType.Type, unitType.UnitInstancesByName, cyclicallyModifiedUnitInstances);

        var unitInstanceAliases = ValidateUnitInstanceAliases(unitType, unitInstanceValidationContext);
        var derivedUnitInstances = ValidateDerivedUnitInstances(unitType, unitPopulation);
        var biasedUnitInstances = ValidateBiasedUnitInstances(unitType, cyclicallyModifiedUnitInstances);
        var prefixedUnitInstances = ValidatePrefixedUnitInstances(unitType, unitInstanceValidationContext);
        var scaledUnitInstances = ValidateScaledUnitInstances(unitType, unitInstanceValidationContext);

        UnitType product = new(unitType.Type, unitType.TypeLocation, unitType.Definition, derivations.Result, unitType.FixedUnitInstance, unitInstanceAliases.Result,
            derivedUnitInstances.Result, biasedUnitInstances.Result, prefixedUnitInstances.Result, scaledUnitInstances.Result);

        var allDiagnostics = unit.Concat(derivations).Concat(unitInstanceAliases).Concat(derivedUnitInstances).Concat(biasedUnitInstances).Concat(prefixedUnitInstances).Concat(scaledUnitInstances);

        return OptionalWithDiagnostics.Result(product, allDiagnostics);
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

    private static HashSet<IModifiedUnitInstance> GetCyclicallyModifiedUnitInstances(UnitType unitType)
    {
        HashSet<IModifiedUnitInstance> unresolvedUnitInstances = new((unitType.UnitInstanceAliases as IEnumerable<IModifiedUnitInstance>).Concat(unitType.BiasedUnitInstances).Concat(unitType.PrefixedUnitInstances).Concat(unitType.ScaledUnitInstances));
        HashSet<string> resolvedUnitInstances = new(unitType.DerivedUnitInstances.Select(static (unit) => unit.Name));

        if (unitType.FixedUnitInstance is not null)
        {
            resolvedUnitInstances.Add(unitType.FixedUnitInstance.Name);
        }

        while (true)
        {
            int initialLength = unresolvedUnitInstances.Count;

            List<IModifiedUnitInstance> toRemove = new();

            foreach (var unresolvedUnitInstance in unresolvedUnitInstances)
            {
                if (resolvedUnitInstances.Contains(unresolvedUnitInstance.OriginalUnitInstance))
                {
                    toRemove.Add(unresolvedUnitInstance);
                }
            }

            foreach (var unitInstance in toRemove)
            {
                resolvedUnitInstances.Add(unitInstance.Name);
                unresolvedUnitInstances.Remove(unitInstance);
            }

            if (unresolvedUnitInstances.Count == initialLength)
            {
                break;
            }
        }

        return unresolvedUnitInstances;
    }

    private static IResultWithDiagnostics<IReadOnlyList<UnitInstanceAliasDefinition>> ValidateUnitInstanceAliases(UnitType unitType, IModifiedUnitInstanceValidationContext validationContext)
    {
        return ValidityFilter.Create(UnitInstanceAliasValidator).Filter(validationContext, unitType.UnitInstanceAliases);
    }

    private static IResultWithDiagnostics<IReadOnlyList<DerivedUnitInstanceDefinition>> ValidateDerivedUnitInstances(UnitType unitType, IUnitPopulationWithData unitPopulation)
    {
        var unnamedUnitDerivation = unitType.UnitDerivations.Count == 1 && unitType.UnitDerivations[0].DerivationID is null ? unitType.UnitDerivations[0] : null;

        var validationContext = new DerivedUniInstancetValidationContext(unitType.Type, unnamedUnitDerivation, unitType.DerivationsByID, unitPopulation);

        return ValidityFilter.Create(DerivedUnitInstanceValidator).Filter(validationContext, unitType.DerivedUnitInstances);
    }

    private static IResultWithDiagnostics<IReadOnlyList<BiasedUnitInstanceDefinition>> ValidateBiasedUnitInstances(UnitType unitType, HashSet<IModifiedUnitInstance> cyclicDependantUnits)
    {
        var validationContext = new BiasedUnitInstanceValidationContext(unitType.Type, unitType.Definition.BiasTerm, unitType.UnitInstancesByName, cyclicDependantUnits);

        return ValidityFilter.Create(BiasedUnitInstanceValidator).Filter(validationContext, unitType.BiasedUnitInstances);
    }

    private static IResultWithDiagnostics<IReadOnlyList<PrefixedUnitInstanceDefinition>> ValidatePrefixedUnitInstances(UnitType unitType, IModifiedUnitInstanceValidationContext validationContext)
    {
        return ValidityFilter.Create(PrefixedUnitInstanceValidator).Filter(validationContext, unitType.PrefixedUnitInstances);
    }

    private static IResultWithDiagnostics<IReadOnlyList<ScaledUnitInstanceDefinition>> ValidateScaledUnitInstances(UnitType unitType, IModifiedUnitInstanceValidationContext validationContext)
    {
        return ValidityFilter.Create(ScaledUnitInstanceValidator).Filter(validationContext, unitType.ScaledUnitInstances);
    }

    private static IUnitType ExtractInterface(UnitType unitType, CancellationToken _) => unitType;
    private static IUnitPopulation ExtractInterface(IUnitPopulation population, CancellationToken _) => population;

    private static IUnitPopulationWithData CreatePopulation(ImmutableArray<IUnitType> units, CancellationToken _)
    {
        return UnitPopulation.Build(units);
    }

    private static SharpMeasuresUnitValidator SharpMeasuresUnitValidator { get; } = new(SharpMeasuresUnitValidationDiagnostics.Instance);

    private static DerivableUnitValidator DerivableUnitValidator { get; } = new(DerivableUnitValidationDiagnostics.Instance);

    private static UnitInstanceAliasValidator UnitInstanceAliasValidator { get; } = new(UnitInstanceAliasValidationDiagnostics.Instance);
    private static DerivedUnitInstanceValidator DerivedUnitInstanceValidator { get; } = new(DerivedUnitInstanceValidationDiagnostics.Instance);
    private static BiasedUnitInstanceValidator BiasedUnitInstanceValidator { get; } = new(BiasedUnitInstanceValidationDiagnostics.Instance);
    private static PrefixedUnitInstanceValidator PrefixedUnitInstanceValidator { get; } = new(PrefixedUnitInstanceValidationDiagnostics.Instance);
    private static ScaledUnitInstanceValidator ScaledUnitInstanceValidator { get; } = new(ScaledUnitInstanceValidationDiagnostics.Instance);
}
