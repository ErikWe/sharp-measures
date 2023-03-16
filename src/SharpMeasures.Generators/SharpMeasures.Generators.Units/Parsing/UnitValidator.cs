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
using SharpMeasures.Generators.Units.Parsing.PrefixedUnitInstance;
using SharpMeasures.Generators.Units.Parsing.ScaledUnitInstance;
using SharpMeasures.Generators.Units.Parsing.SharpMeasuresUnit;
using SharpMeasures.Generators.Units.Parsing.UnitInstanceAlias;

using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;

public sealed class UnitValidator
{
    public static (UnitValidationResult ValidationResult, IncrementalValueProvider<IUnitPopulation> Population) Validate(IncrementalGeneratorInitializationContext context, UnitProcessingResult processingResult, IncrementalValueProvider<IUnitPopulation> unitPopulationProvider, IncrementalValueProvider<IScalarPopulation> scalarPopulationProvider)
    {
        UnitValidator validator = new(UnitValidationDiagnosticsStrategies.Default);

        var validatedUnits = processingResult.UnitProvider.Combine(processingResult.ProcessingDataProvider, unitPopulationProvider, scalarPopulationProvider).Select(validate).ReportDiagnostics(context);

        var population = validatedUnits.Select(ExtractInterface).CollectResults().Select(CreatePopulation);

        return (new UnitValidationResult(validatedUnits), population);

        IOptionalWithDiagnostics<UnitType> validate((Optional<UnitType> UnvalidatedUnit, UnitProcessingData ProcessingData, IUnitPopulation UnitPopulation, IScalarPopulation ScalarPopulation) input, CancellationToken token)
        {
            if (token.IsCancellationRequested || input.UnvalidatedUnit.HasValue is false)
            {
                return OptionalWithDiagnostics.Empty<UnitType>();
            }

            return validator.Validate(input.UnvalidatedUnit.Value, input.ProcessingData, input.UnitPopulation, input.ScalarPopulation);
        }
    }

    private IUnitValidationDiagnosticsStrategy DiagnosticsStrategy { get; }

    internal UnitValidator(IUnitValidationDiagnosticsStrategy diagnosticsStrategy)
    {
        DiagnosticsStrategy = diagnosticsStrategy;
    }

    internal IOptionalWithDiagnostics<UnitType> Validate(UnitType unitType, UnitProcessingData processingData, IUnitPopulation unitPopulation, IScalarPopulation scalarPopulation)
    {
        var unit = ValidateUnit(unitType, processingData, scalarPopulation);

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

        UnitType product = new(unitType.Type, unitType.Definition, derivations.Result, unitType.FixedUnitInstance, unitInstanceAliases.Result, derivedUnitInstances.Result, biasedUnitInstances.Result, prefixedUnitInstances.Result, scaledUnitInstances.Result);
        var allDiagnostics = unit.Concat(derivations).Concat(unitInstanceAliases).Concat(derivedUnitInstances).Concat(biasedUnitInstances).Concat(prefixedUnitInstances).Concat(scaledUnitInstances);

        return OptionalWithDiagnostics.Result(product, allDiagnostics);
    }

    private IValidityWithDiagnostics ValidateUnit(UnitType unitType, UnitProcessingData processingData, IScalarPopulation scalarPopulation)
    {
        var validationContext = new SharpMeasuresUnitValidationContext(unitType.Type, processingData, scalarPopulation);

        return ValidityFilter.Create(SharpMeasuresUnitValidator).Validate(validationContext, unitType.Definition);
    }

    private IResultWithDiagnostics<IReadOnlyList<DerivableUnitDefinition>> ValidateDerivations(UnitType unitType, IUnitPopulation unitPopulation)
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
            var initialLength = unresolvedUnitInstances.Count;

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

    private IResultWithDiagnostics<IReadOnlyList<UnitInstanceAliasDefinition>> ValidateUnitInstanceAliases(UnitType unitType, IModifiedUnitInstanceValidationContext validationContext)
    {
        return ValidityFilter.Create(UnitInstanceAliasValidator).Filter(validationContext, unitType.UnitInstanceAliases);
    }

    private IResultWithDiagnostics<IReadOnlyList<DerivedUnitInstanceDefinition>> ValidateDerivedUnitInstances(UnitType unitType, IUnitPopulation unitPopulation)
    {
        var unnamedUnitDerivation = unitType.UnitDerivations.Count == 1 && unitType.UnitDerivations[0].DerivationID is null ? unitType.UnitDerivations[0] : null;

        var validationContext = new DerivedUniInstancetValidationContext(unitType.Type, unnamedUnitDerivation, unitType.DerivationsByID, unitPopulation);

        return ValidityFilter.Create(DerivedUnitInstanceValidator).Filter(validationContext, unitType.DerivedUnitInstances);
    }

    private IResultWithDiagnostics<IReadOnlyList<BiasedUnitInstanceDefinition>> ValidateBiasedUnitInstances(UnitType unitType, HashSet<IModifiedUnitInstance> cyclicDependantUnits)
    {
        var validationContext = new BiasedUnitInstanceValidationContext(unitType.Type, unitType.Definition.BiasTerm, unitType.UnitInstancesByName, cyclicDependantUnits);

        return ValidityFilter.Create(BiasedUnitInstanceValidator).Filter(validationContext, unitType.BiasedUnitInstances);
    }

    private IResultWithDiagnostics<IReadOnlyList<PrefixedUnitInstanceDefinition>> ValidatePrefixedUnitInstances(UnitType unitType, IModifiedUnitInstanceValidationContext validationContext)
    {
        return ValidityFilter.Create(PrefixedUnitInstanceValidator).Filter(validationContext, unitType.PrefixedUnitInstances);
    }

    private IResultWithDiagnostics<IReadOnlyList<ScaledUnitInstanceDefinition>> ValidateScaledUnitInstances(UnitType unitType, IModifiedUnitInstanceValidationContext validationContext)
    {
        return ValidityFilter.Create(ScaledUnitInstanceValidator).Filter(validationContext, unitType.ScaledUnitInstances);
    }

    private static Optional<IUnitType> ExtractInterface(Optional<UnitType> unitType, CancellationToken _) => unitType.HasValue ? unitType.Value : new Optional<IUnitType>();
    private static IUnitPopulation CreatePopulation(ImmutableArray<IUnitType> units, CancellationToken _) => UnitPopulation.BuildWithoutProcessingData(units);

    private SharpMeasuresUnitValidator SharpMeasuresUnitValidator => new(DiagnosticsStrategy.SharpMeasuresUnitDiagnostics);

    private DerivableUnitValidator DerivableUnitValidator => new(DiagnosticsStrategy.DerivableUnitDiagnostics);

    private UnitInstanceAliasValidator UnitInstanceAliasValidator => new(DiagnosticsStrategy.UnitInstanceAliasDiagnostics);
    private DerivedUnitInstanceValidator DerivedUnitInstanceValidator => new(DiagnosticsStrategy.DerivedUnitInstanceDiagnostics);
    private BiasedUnitInstanceValidator BiasedUnitInstanceValidator => new(DiagnosticsStrategy.BiasedUnitInstanceDiagnostics);
    private PrefixedUnitInstanceValidator PrefixedUnitInstanceValidator => new(DiagnosticsStrategy.PrefixedUnitInstanceDiagnostics);
    private ScaledUnitInstanceValidator ScaledUnitInstanceValidator => new(DiagnosticsStrategy.ScaledUnitInstanceDiagnostics);
}
