namespace SharpMeasures.Generators.Units.ForeignUnitParsing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Units.ForeignUnitParsing.Diagnostics.Processing;
using SharpMeasures.Generators.Units.Parsing;
using SharpMeasures.Generators.Units.Parsing.Abstractions;
using SharpMeasures.Generators.Units.Parsing.BiasedUnitInstance;
using SharpMeasures.Generators.Units.Parsing.Contexts.Processing;
using SharpMeasures.Generators.Units.Parsing.DerivableUnit;
using SharpMeasures.Generators.Units.Parsing.DerivedUnitInstance;
using SharpMeasures.Generators.Units.Parsing.FixedUnitInstance;
using SharpMeasures.Generators.Units.Parsing.PrefixedUnitInstance;
using SharpMeasures.Generators.Units.Parsing.ScaledUnitInstance;
using SharpMeasures.Generators.Units.Parsing.SharpMeasuresUnit;
using SharpMeasures.Generators.Units.Parsing.UnitInstanceAlias;

using System.Collections.Generic;
using System.Linq;
using System.Threading;

public interface IForeignUnitProcesser
{
    public abstract (IUnitPopulation Population, IForeignUnitValidator Validator) ProcessAndExtend(IUnitPopulation unextendedUnitPopulation, CancellationToken token);
}

internal sealed record class ForeignUnitProcesser : IForeignUnitProcesser
{
    private ForeignUnitParsingResult ParsingResult { get; }

    private EquatableList<UnitType> Units { get; } = new();

    public ForeignUnitProcesser(ForeignUnitParsingResult parsingResult)
    {
        ParsingResult = parsingResult;
    }

    public (IUnitPopulation, IForeignUnitValidator) ProcessAndExtend(IUnitPopulation unextendedUnitPopulation, CancellationToken token)
    {
        if (token.IsCancellationRequested is false)
        {
            foreach (var rawUnit in ParsingResult.Units)
            {
                var unit = Process(rawUnit);

                if (unit.HasValue)
                {
                    Units.Add(unit.Value);
                }
            }
        }

        var result = new ForeignUnitProcessingResult(Units);
        var extendedPopulation = ExtendedUnitPopulation.Build(unextendedUnitPopulation, result);
        var validator = new ForeignUnitValidator(result);

        return (extendedPopulation, validator);
    }

    public static Optional<UnitType> Process(RawUnitType rawUnit)
    {
        var unit = ProcessUnit(rawUnit.Type, rawUnit.Definition);

        if (unit.HasValue is false)
        {
            return new Optional<UnitType>();
        }

        var derivations = ProcessDerivations(rawUnit.Type, rawUnit.UnitDerivations, unit.Value.BiasTerm);

        HashSet<string> reservedUnitInstanceNames = new();
        HashSet<string> reservedUnitInstancePluralForms = new();

        var fixedUnitInstance = rawUnit.FixedUnitInstance is not null
            ? ProcessFixedUnitInstance(rawUnit.Type, rawUnit.FixedUnitInstance, derivations.Count > 0, reservedUnitInstanceNames, reservedUnitInstancePluralForms)
            : new Optional<FixedUnitInstanceDefinition>();

        UnitInstanceProcessingContext unitInstanceProcessingContext = new(rawUnit.Type, reservedUnitInstanceNames, reservedUnitInstancePluralForms);

        var unitInstanceAliases = ProcessUnitInstanceAliases(rawUnit.UnitInstanceAliases, unitInstanceProcessingContext);
        var derivedUnitInstances = ProcessDerivedUnitInstances(rawUnit.DerivedUnitInstances, unitInstanceProcessingContext);
        var biasedUnitInstances = ProcessBiasedUnitInstances(rawUnit.BiasedUnitInstances, unitInstanceProcessingContext);
        var prefixedUnitInstances = ProcessPrefixedUnitInstances(rawUnit.PrefixedUnitInstances, unitInstanceProcessingContext);
        var scaledUnitInstances = ProcessScaledUnitInstances(rawUnit.ScaledUnitInstances, unitInstanceProcessingContext);

        return new UnitType(rawUnit.Type, rawUnit.TypeLocation, unit.Value, derivations, fixedUnitInstance.HasValue ? fixedUnitInstance.Value : null, unitInstanceAliases, derivedUnitInstances, biasedUnitInstances, prefixedUnitInstances, scaledUnitInstances);
    }

    private static Optional<SharpMeasuresUnitDefinition> ProcessUnit(DefinedType type, RawSharpMeasuresUnitDefinition rawDefinition)
    {
        var processingContext = new SimpleProcessingContext(type);

        var unit = ProcessingFilter.Create(Processers.SharpMeasuresUnitProcesser).Filter(processingContext, rawDefinition);

        if (unit.LacksResult)
        {
            return new Optional<SharpMeasuresUnitDefinition>();
        }

        return unit.Result;
    }

    private static IReadOnlyList<DerivableUnitDefinition> ProcessDerivations(DefinedType type, IEnumerable<RawDerivableUnitDefinition> rawDefinition, bool unitIncludesBiasTerm)
    {
        var processingContext = new DerivableUnitProcessingContext(type, unitIncludesBiasTerm, rawDefinition.Skip(1).Any());

        return ProcessingFilter.Create(Processers.DerivableUnitProcesser).Filter(processingContext, rawDefinition).Result;
    }

    private static Optional<FixedUnitInstanceDefinition> ProcessFixedUnitInstance(DefinedType type, RawFixedUnitInstanceDefinition rawDefinitions, bool unitIsDerivable, HashSet<string> reservedUnitInstanceNames, HashSet<string> reservedUnitInstancePluralForms)
    {
        var processingContext = new FixedUnitInstanceProcessingContext(type, reservedUnitInstanceNames, reservedUnitInstancePluralForms, unitIsDerivable);

        var fixedUnit = ProcessingFilter.Create(Processers.FixedUnitInstanceProcesser).Filter(processingContext, rawDefinitions);

        if (fixedUnit.LacksResult)
        {
            return new Optional<FixedUnitInstanceDefinition>();
        }

        return fixedUnit.Result;
    }

    private static IReadOnlyList<UnitInstanceAliasDefinition> ProcessUnitInstanceAliases(IEnumerable<RawUnitInstanceAliasDefinition> rawDefinitions, IUnitInstanceProcessingContext processingContext)
    {
        return ProcessingFilter.Create(Processers.UnitInstanceAliasProcesser).Filter(processingContext, rawDefinitions).Result;
    }

    private static IReadOnlyList<DerivedUnitInstanceDefinition> ProcessDerivedUnitInstances(IEnumerable<RawDerivedUnitInstanceDefinition> rawDefinitions, IUnitInstanceProcessingContext processingContext)
    {
        return ProcessingFilter.Create(Processers.DerivedUnitInstanceProcesser).Filter(processingContext, rawDefinitions).Result;
    }

    private static IReadOnlyList<BiasedUnitInstanceDefinition> ProcessBiasedUnitInstances(IEnumerable<RawBiasedUnitInstanceDefinition> rawDefinitions, IUnitInstanceProcessingContext processingContext)
    {
        return ProcessingFilter.Create(Processers.BiasedUnitInstanceProcesser).Filter(processingContext, rawDefinitions).Result;
    }

    private static IReadOnlyList<PrefixedUnitInstanceDefinition> ProcessPrefixedUnitInstances(IEnumerable<RawPrefixedUnitInstanceDefinition> rawDefinitions, IUnitInstanceProcessingContext processingContext)
    {
        return ProcessingFilter.Create(Processers.PrefixedUnitInstanceProcesser).Filter(processingContext, rawDefinitions).Result;
    }

    private static IReadOnlyList<ScaledUnitInstanceDefinition> ProcessScaledUnitInstances(IEnumerable<RawScaledUnitInstanceDefinition> rawDefinitions, IUnitInstanceProcessingContext processingContext)
    {
        return ProcessingFilter.Create(Processers.ScaledUnitInstanceProcesser).Filter(processingContext, rawDefinitions).Result;
    }

    private static class Processers
    {
        public static SharpMeasuresUnitProcesser SharpMeasuresUnitProcesser { get; } = new(EmptySharpMeasuresUnitProcessingDiagnostics.Instance);

        public static FixedUnitInstanceProcesser FixedUnitInstanceProcesser { get; } = new(EmptyFixedUnitInstanceProcessingDiagnostics.Instance);
        public static DerivableUnitProcesser DerivableUnitProcesser { get; } = new(EmptyDerivableUnitProcessingDiagnostics.Instance);

        public static UnitInstanceAliasProcesser UnitInstanceAliasProcesser { get; } = new(EmptyUnitInstanceAliasProcessingDiagnostics.Instance);
        public static DerivedUnitInstanceProcesser DerivedUnitInstanceProcesser { get; } = new(EmptyDerivedUnitInstanceProcessingDiagnostics.Instance);
        public static BiasedUnitInstanceProcesser BiasedUnitInstanceProcesser { get; } = new(EmptyBiasedUnitInstanceProcessingDiagnostics.Instance);
        public static PrefixedUnitInstanceProcesser PrefixedUnitInstanceProcesser { get; } = new(EmptyPrefixedUnitInstanceProcessingDiagnostics.Instance);
        public static ScaledUnitInstanceProcesser ScaledUnitInstanceProcesser { get; } = new(EmptyScaledUnitInstanceProcessingDiagnostics.Instance);
    }
}
