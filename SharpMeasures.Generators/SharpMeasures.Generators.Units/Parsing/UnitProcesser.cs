namespace SharpMeasures.Generators.Units.Parsing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Units.Parsing.Abstractions;
using SharpMeasures.Generators.Units.Parsing.BiasedUnitInstance;
using SharpMeasures.Generators.Units.Parsing.Contexts.Processing;
using SharpMeasures.Generators.Units.Parsing.DerivableUnit;
using SharpMeasures.Generators.Units.Parsing.DerivedUnitInstance;
using SharpMeasures.Generators.Units.Parsing.Diagnostics.Processing;
using SharpMeasures.Generators.Units.Parsing.FixedUnitInstance;
using SharpMeasures.Generators.Units.Parsing.PrefixedUnitInstance;
using SharpMeasures.Generators.Units.Parsing.ScaledUnitInstance;
using SharpMeasures.Generators.Units.Parsing.SharpMeasuresUnit;
using SharpMeasures.Generators.Units.Parsing.UnitInstanceAlias;

using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;

public interface IUnitProcesser
{
    public abstract (IncrementalValueProvider<IUnitPopulation> Population, IUnitValidator Validator) Process(IncrementalGeneratorInitializationContext context);
}

public class UnitProcesser : IUnitProcesser
{
    private IncrementalValuesProvider<Optional<RawUnitType>> UnitProvider { get; }

    internal UnitProcesser(IncrementalValuesProvider<Optional<RawUnitType>> unitProvider)
    {
        UnitProvider = unitProvider;
    }

    public (IncrementalValueProvider<IUnitPopulation>, IUnitValidator) Process(IncrementalGeneratorInitializationContext context)
    {
        var units = UnitProvider.Select(Process).ReportDiagnostics(context);

        var population = units.Select(ExtractInterface).CollectResults().Select(CreatePopulation);

        var reducedPopulation = population.Select(ExtractInterface);

        return (reducedPopulation, new UnitValidator(population, units));
    }

    private static IOptionalWithDiagnostics<UnitType> Process(Optional<RawUnitType> rawUnit, CancellationToken token)
    {
        if (token.IsCancellationRequested || rawUnit.HasValue is false)
        {
            return OptionalWithDiagnostics.Empty<UnitType>();
        }

        return Process(rawUnit.Value);
    }

    private static IOptionalWithDiagnostics<UnitType> Process(RawUnitType rawUnit)
    {
        var unit = ProcessUnit(rawUnit.Type, rawUnit.Definition);

        if (unit.LacksResult)
        {
            return unit.AsEmptyOptional<UnitType>();
        }

        var derivations = ProcessDerivations(rawUnit.Type, rawUnit.UnitDerivations, unit.Result.BiasTerm);

        HashSet<string> reservedUnitInstanceNames = new();
        HashSet<string> reservedUnitInstancePluralForms = new();

        var fixedUnitInstance = rawUnit.FixedUnitInstance is not null
            ? ProcessFixedUnitInstance(rawUnit.Type, rawUnit.FixedUnitInstance, derivations.Result.Count > 0, reservedUnitInstanceNames, reservedUnitInstancePluralForms)
            : OptionalWithDiagnostics.EmptyWithoutDiagnostics<FixedUnitInstanceDefinition>();

        UnitInstanceProcessingContext unitInstanceProcessingContext = new(rawUnit.Type, reservedUnitInstanceNames, reservedUnitInstancePluralForms);

        var unitInstanceAliases = ProcessUnitInstanceAliases(rawUnit.UnitInstanceAliases, unitInstanceProcessingContext);
        var derivedUnitInstances = ProcessDerivedUnitInstances(rawUnit.DerivedUnitInstances, unitInstanceProcessingContext);
        var biasedUnitInstances = ProcessBiasedUnitInstances(rawUnit.BiasedUnitInstances, unitInstanceProcessingContext);
        var prefixedUnitInstances = ProcessPrefixedUnitInstances(rawUnit.PrefixedUnitInstances, unitInstanceProcessingContext);
        var scaledUnitInstances = ProcessScaledUnitInstances(rawUnit.ScaledUnitInstances, unitInstanceProcessingContext);

        UnitType unitType = new(rawUnit.Type, rawUnit.TypeLocation, unit.Result, derivations.Result, fixedUnitInstance.NullableReferenceResult(), unitInstanceAliases.Result, derivedUnitInstances.Result, biasedUnitInstances.Result, prefixedUnitInstances.Result, scaledUnitInstances.Result);

        var allDiagnostics = unit.Concat(derivations).Concat(fixedUnitInstance).Concat(unitInstanceAliases).Concat(derivedUnitInstances).Concat(biasedUnitInstances).Concat(prefixedUnitInstances).Concat(scaledUnitInstances);

        return OptionalWithDiagnostics.Result(unitType, allDiagnostics);
    }

    private static IOptionalWithDiagnostics<SharpMeasuresUnitDefinition> ProcessUnit(DefinedType type, RawSharpMeasuresUnitDefinition rawDefinition)
    {
        var processingContext = new SimpleProcessingContext(type);

        return ProcessingFilter.Create(Processers.SharpMeasuresUnitProcesser).Filter(processingContext, rawDefinition);
    }

    private static IResultWithDiagnostics<IReadOnlyList<DerivableUnitDefinition>> ProcessDerivations(DefinedType type, IEnumerable<RawDerivableUnitDefinition> rawDefinition, bool unitIncludesBiasTerm)
    {
        var processingContext = new DerivableUnitProcessingContext(type, unitIncludesBiasTerm, rawDefinition.Skip(1).Any());

        return ProcessingFilter.Create(Processers.DerivableUnitProcesser).Filter(processingContext, rawDefinition);
    }

    private static IOptionalWithDiagnostics<FixedUnitInstanceDefinition> ProcessFixedUnitInstance(DefinedType type, RawFixedUnitInstanceDefinition rawDefinitions, bool unitIsDerivable, HashSet<string> reservedUnitInstanceNames, HashSet<string> reservedUnitInstancePluralForms)
    {
        var processingContext = new FixedUnitInstanceProcessingContext(type, reservedUnitInstanceNames, reservedUnitInstancePluralForms, unitIsDerivable);

        return ProcessingFilter.Create(Processers.FixedUnitInstanceProcesser).Filter(processingContext, rawDefinitions);
    }

    private static IResultWithDiagnostics<IReadOnlyList<UnitInstanceAliasDefinition>> ProcessUnitInstanceAliases(IEnumerable<RawUnitInstanceAliasDefinition> rawDefinitions, IUnitInstanceProcessingContext processingContext)
    {
        return ProcessingFilter.Create(Processers.UnitInstanceAliasProcesser).Filter(processingContext, rawDefinitions);
    }

    private static IResultWithDiagnostics<IReadOnlyList<DerivedUnitInstanceDefinition>> ProcessDerivedUnitInstances(IEnumerable<RawDerivedUnitInstanceDefinition> rawDefinitions, IUnitInstanceProcessingContext processingContext)
    {
        return ProcessingFilter.Create(Processers.DerivedUnitInstanceProcesser).Filter(processingContext, rawDefinitions);
    }

    private static IResultWithDiagnostics<IReadOnlyList<BiasedUnitInstanceDefinition>> ProcessBiasedUnitInstances(IEnumerable<RawBiasedUnitInstanceDefinition> rawDefinitions, IUnitInstanceProcessingContext processingContext)
    {
        return ProcessingFilter.Create(Processers.BiasedUnitInstanceProcesser).Filter(processingContext, rawDefinitions);
    }

    private static IResultWithDiagnostics<IReadOnlyList<PrefixedUnitInstanceDefinition>> ProcessPrefixedUnitInstances(IEnumerable<RawPrefixedUnitInstanceDefinition> rawDefinitions, IUnitInstanceProcessingContext processingContext)
    {
        return ProcessingFilter.Create(Processers.PrefixedUnitInstanceProcesser).Filter(processingContext, rawDefinitions);
    }

    private static IResultWithDiagnostics<IReadOnlyList<ScaledUnitInstanceDefinition>> ProcessScaledUnitInstances(IEnumerable<RawScaledUnitInstanceDefinition> rawDefinitions, IUnitInstanceProcessingContext processingContext)
    {
        return ProcessingFilter.Create(Processers.ScaledUnitInstanceProcesser).Filter(processingContext, rawDefinitions);
    }

    private static Optional<IUnitType> ExtractInterface(Optional<UnitType> unitType, CancellationToken _) => unitType.HasValue ? unitType.Value : new Optional<IUnitType>();
    private static IUnitPopulation ExtractInterface(IUnitPopulation population, CancellationToken _) => population;

    private static IUnitPopulationWithData CreatePopulation(ImmutableArray<IUnitType> units, CancellationToken _)
    {
        return UnitPopulation.Build(units);
    }

    private static class Processers
    {
        public static SharpMeasuresUnitProcesser SharpMeasuresUnitProcesser { get; } = new(SharpMeasuresUnitProcessingDiagnostics.Instance);

        public static FixedUnitInstanceProcesser FixedUnitInstanceProcesser { get; } = new(FixedUnitInstanceProcessingDiagnostics.Instance);
        public static DerivableUnitProcesser DerivableUnitProcesser { get; } = new(DerivableUnitProcessingDiagnostics.Instance);

        public static UnitInstanceAliasProcesser UnitInstanceAliasProcesser { get; } = new(UnitInstanceAliasProcessingDiagnostics.Instance);
        public static DerivedUnitInstanceProcesser DerivedUnitInstanceProcesser { get; } = new(DerivedUnitInstanceProcessingDiagnostics.Instance);
        public static BiasedUnitInstanceProcesser BiasedUnitInstanceProcesser { get; } = new(BiasedUnitInstanceProcessingDiagnostics.Instance);
        public static PrefixedUnitInstanceProcesser PrefixedUnitInstanceProcesser { get; } = new(PrefixedUnitInstanceProcessingDiagnostics.Instance);
        public static ScaledUnitInstanceProcesser ScaledUnitInstanceProcesser { get; } = new(ScaledUnitInstanceProcessingDiagnostics.Instance);
    }
}
