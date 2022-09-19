namespace SharpMeasures.Generators.Units.ForeignUnitParsing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Units.ForeignUnitParsing.Diagnostics.Validation;
using SharpMeasures.Generators.Units.Parsing;
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
using System.Linq;

public interface IForeignUnitValidator
{
    public abstract IUnitPopulation ValidateAndExtend(IUnitPopulation unitPopulation, IScalarPopulation scalarPopulation, IUnitPopulation unextendedUnitPopulation);
}

internal sealed record class ForeignUnitValidator : IForeignUnitValidator
{
    private ForeignUnitProcessingResult ProcessingResult { get; }

    private EquatableList<UnitType> Units { get; } = new();

    public ForeignUnitValidator(ForeignUnitProcessingResult processingResult)
    {
        ProcessingResult = processingResult;
    }

    public IUnitPopulation ValidateAndExtend(IUnitPopulation unitPopulation, IScalarPopulation scalarPopulation, IUnitPopulation unextendedUnitPopulation)
    {
        foreach (var processedUnit in ProcessingResult.Units)
        {
            var unit = Validate(processedUnit, unitPopulation, scalarPopulation);

            if (unit.HasValue)
            {
                Units.Add(unit.Value);
            }
        }

        var result = new ForeignUnitProcessingResult(Units);
        var extendedPopulation = ExtendedUnitPopulation.Build(unextendedUnitPopulation, result);

        return extendedPopulation;
    }

    private static Optional<UnitType> Validate(UnitType unitType, IUnitPopulation unitPopulation, IScalarPopulation scalarPopulation)
    {
        var unitValidity = ValidateUnit(unitType, scalarPopulation);

        if (unitValidity is false)
        {
            return new Optional<UnitType>();
        }

        var derivations = ValidateDerivations(unitType, unitPopulation);

        var cyclicallyModifiedUnitInstances = GetCyclicallyModifiedUnitInstances(unitType);

        var unitInstanceValidationContext = new ModifiedUnitValidationContext(unitType.Type, unitType.UnitInstancesByName, cyclicallyModifiedUnitInstances);

        var unitInstanceAliases = ValidateUnitInstanceAliases(unitType, unitInstanceValidationContext);
        var derivedUnitInstances = ValidateDerivedUnitInstances(unitType, unitPopulation);
        var biasedUnitInstances = ValidateBiasedUnitInstances(unitType, cyclicallyModifiedUnitInstances);
        var prefixedUnitInstances = ValidatePrefixedUnitInstances(unitType, unitInstanceValidationContext);
        var scaledUnitInstances = ValidateScaledUnitInstances(unitType, unitInstanceValidationContext);

        return new UnitType(unitType.Type, unitType.TypeLocation, unitType.Definition, derivations, unitType.FixedUnitInstance, unitInstanceAliases, derivedUnitInstances, biasedUnitInstances, prefixedUnitInstances, scaledUnitInstances);
    }

    private static bool ValidateUnit(UnitType unitType, IScalarPopulation scalarPopulation)
    {
        UnitProcessingData processingData = new(new Dictionary<NamedType, IUnitType>());

        var validationContext = new SharpMeasuresUnitValidationContext(unitType.Type, processingData, scalarPopulation);

        return ValidityFilter.Create(SharpMeasuresUnitValidator).Validate(validationContext, unitType.Definition).IsValid;
    }

    private static IReadOnlyList<DerivableUnitDefinition> ValidateDerivations(UnitType unitType, IUnitPopulation unitPopulation)
    {
        var validationContext = new DerivableUnitValidationContext(unitType.Type, unitPopulation);

        return ValidityFilter.Create(DerivableUnitValidator).Filter(validationContext, unitType.UnitDerivations).Result;
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

    private static IReadOnlyList<UnitInstanceAliasDefinition> ValidateUnitInstanceAliases(UnitType unitType, IModifiedUnitInstanceValidationContext validationContext)
    {
        return ValidityFilter.Create(UnitInstanceAliasValidator).Filter(validationContext, unitType.UnitInstanceAliases).Result;
    }

    private static IReadOnlyList<DerivedUnitInstanceDefinition> ValidateDerivedUnitInstances(UnitType unitType, IUnitPopulation unitPopulation)
    {
        var unnamedUnitDerivation = unitType.UnitDerivations.Count == 1 && unitType.UnitDerivations[0].DerivationID is null ? unitType.UnitDerivations[0] : null;

        var validationContext = new DerivedUniInstancetValidationContext(unitType.Type, unnamedUnitDerivation, unitType.DerivationsByID, unitPopulation);

        return ValidityFilter.Create(DerivedUnitInstanceValidator).Filter(validationContext, unitType.DerivedUnitInstances).Result;
    }

    private static IReadOnlyList<BiasedUnitInstanceDefinition> ValidateBiasedUnitInstances(UnitType unitType, HashSet<IModifiedUnitInstance> cyclicDependantUnits)
    {
        var validationContext = new BiasedUnitInstanceValidationContext(unitType.Type, unitType.Definition.BiasTerm, unitType.UnitInstancesByName, cyclicDependantUnits);

        return ValidityFilter.Create(BiasedUnitInstanceValidator).Filter(validationContext, unitType.BiasedUnitInstances).Result;
    }

    private static IReadOnlyList<PrefixedUnitInstanceDefinition> ValidatePrefixedUnitInstances(UnitType unitType, IModifiedUnitInstanceValidationContext validationContext)
    {
        return ValidityFilter.Create(PrefixedUnitInstanceValidator).Filter(validationContext, unitType.PrefixedUnitInstances).Result;
    }

    private static IReadOnlyList<ScaledUnitInstanceDefinition> ValidateScaledUnitInstances(UnitType unitType, IModifiedUnitInstanceValidationContext validationContext)
    {
        return ValidityFilter.Create(ScaledUnitInstanceValidator).Filter(validationContext, unitType.ScaledUnitInstances).Result;
    }

    private static SharpMeasuresUnitValidator SharpMeasuresUnitValidator { get; } = new(EmptySharpMeasuresUnitValidationDiagnostics.Instance);

    private static DerivableUnitValidator DerivableUnitValidator { get; } = new(EmptyDerivableUnitValidationDiagnostics.Instance);

    private static UnitInstanceAliasValidator UnitInstanceAliasValidator { get; } = new(EmptyUnitInstanceAliasValidationDiagnostics.Instance);
    private static DerivedUnitInstanceValidator DerivedUnitInstanceValidator { get; } = new(EmptyDerivedUnitInstanceValidationDiagnostics.Instance);
    private static BiasedUnitInstanceValidator BiasedUnitInstanceValidator { get; } = new(EmptyBiasedUnitInstanceValidationDiagnostics.Instance);
    private static PrefixedUnitInstanceValidator PrefixedUnitInstanceValidator { get; } = new(EmptyPrefixedUnitInstanceValidationDiagnostics.Instance);
    private static ScaledUnitInstanceValidator ScaledUnitInstanceValidator { get; } = new(EmptyScaledUnitInstanceValidationDiagnostics.Instance);
}
