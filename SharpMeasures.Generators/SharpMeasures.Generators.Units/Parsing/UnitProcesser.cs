namespace SharpMeasures.Generators.Units.Parsing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
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
using System.Collections.Immutable;
using System.Linq;
using System.Threading;

public sealed class UnitProcesser
{
    public static (UnitProcessingResult ProcessingResult, IncrementalValueProvider<IUnitPopulation> Population) Process(IncrementalGeneratorInitializationContext context, UnitParsingResult parsingResult)
    {
        UnitProcesser processer = new(UnitProcessingDiagnosticsStrategies.Default);

        var units = parsingResult.UnitProvider.Select(process).ReportDiagnostics(context);

        var populationAndProcessingData = units.Select(ExtractInterface).CollectResults().Select(CreatePopulation);

        var population = populationAndProcessingData.Select(ExtractPopulation);
        var processingData = populationAndProcessingData.Select(ExtractProcessingData);

        return (new UnitProcessingResult(units, processingData), population);

        IOptionalWithDiagnostics<UnitType> process(Optional<RawUnitType> rawUnit, CancellationToken token)
        {
            if (token.IsCancellationRequested || rawUnit.HasValue is false)
            {
                return OptionalWithDiagnostics.Empty<UnitType>();
            }

            return processer.Process(rawUnit.Value);
        }
    }

    private IUnitProcessingDiagnosticsStrategy DiagnosticsStrategy { get; }

    internal UnitProcesser(IUnitProcessingDiagnosticsStrategy diagnosticsStrategy)
    {
        DiagnosticsStrategy = diagnosticsStrategy;
    }

    internal IOptionalWithDiagnostics<UnitType> Process(RawUnitType rawUnit)
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

        UnitType unitType = new(rawUnit.Type, unit.Result, derivations.Result, fixedUnitInstance.NullableReferenceResult(), unitInstanceAliases.Result, derivedUnitInstances.Result, biasedUnitInstances.Result, prefixedUnitInstances.Result, scaledUnitInstances.Result);

        var allDiagnostics = unit.Concat(derivations).Concat(fixedUnitInstance).Concat(unitInstanceAliases).Concat(derivedUnitInstances).Concat(biasedUnitInstances).Concat(prefixedUnitInstances).Concat(scaledUnitInstances);

        return OptionalWithDiagnostics.Result(unitType, allDiagnostics);
    }

    private IOptionalWithDiagnostics<SharpMeasuresUnitDefinition> ProcessUnit(DefinedType type, RawSharpMeasuresUnitDefinition rawDefinition)
    {
        var processingContext = new SimpleProcessingContext(type);

        return ProcessingFilter.Create(SharpMeasuresUnitProcesser).Filter(processingContext, rawDefinition);
    }

    private IResultWithDiagnostics<IReadOnlyList<DerivableUnitDefinition>> ProcessDerivations(DefinedType type, IEnumerable<RawDerivableUnitDefinition> rawDefinition, bool unitIncludesBiasTerm)
    {
        var processingContext = new DerivableUnitProcessingContext(type, unitIncludesBiasTerm, rawDefinition.Skip(1).Any());

        return ProcessingFilter.Create(DerivableUnitProcesser).Filter(processingContext, rawDefinition);
    }

    private IOptionalWithDiagnostics<FixedUnitInstanceDefinition> ProcessFixedUnitInstance(DefinedType type, RawFixedUnitInstanceDefinition rawDefinitions, bool unitIsDerivable, HashSet<string> reservedUnitInstanceNames, HashSet<string> reservedUnitInstancePluralForms)
    {
        var processingContext = new FixedUnitInstanceProcessingContext(type, reservedUnitInstanceNames, reservedUnitInstancePluralForms, unitIsDerivable);

        return ProcessingFilter.Create(FixedUnitInstanceProcesser).Filter(processingContext, rawDefinitions);
    }

    private IResultWithDiagnostics<IReadOnlyList<UnitInstanceAliasDefinition>> ProcessUnitInstanceAliases(IEnumerable<RawUnitInstanceAliasDefinition> rawDefinitions, IUnitInstanceProcessingContext processingContext)
    {
        return ProcessingFilter.Create(UnitInstanceAliasProcesser).Filter(processingContext, rawDefinitions);
    }

    private IResultWithDiagnostics<IReadOnlyList<DerivedUnitInstanceDefinition>> ProcessDerivedUnitInstances(IEnumerable<RawDerivedUnitInstanceDefinition> rawDefinitions, IUnitInstanceProcessingContext processingContext)
    {
        return ProcessingFilter.Create(DerivedUnitInstanceProcesser).Filter(processingContext, rawDefinitions);
    }

    private IResultWithDiagnostics<IReadOnlyList<BiasedUnitInstanceDefinition>> ProcessBiasedUnitInstances(IEnumerable<RawBiasedUnitInstanceDefinition> rawDefinitions, IUnitInstanceProcessingContext processingContext)
    {
        return ProcessingFilter.Create(BiasedUnitInstanceProcesser).Filter(processingContext, rawDefinitions);
    }

    private IResultWithDiagnostics<IReadOnlyList<PrefixedUnitInstanceDefinition>> ProcessPrefixedUnitInstances(IEnumerable<RawPrefixedUnitInstanceDefinition> rawDefinitions, IUnitInstanceProcessingContext processingContext)
    {
        return ProcessingFilter.Create(PrefixedUnitInstanceProcesser).Filter(processingContext, rawDefinitions);
    }

    private IResultWithDiagnostics<IReadOnlyList<ScaledUnitInstanceDefinition>> ProcessScaledUnitInstances(IEnumerable<RawScaledUnitInstanceDefinition> rawDefinitions, IUnitInstanceProcessingContext processingContext)
    {
        return ProcessingFilter.Create(ScaledUnitInstanceProcesser).Filter(processingContext, rawDefinitions);
    }

    private static Optional<IUnitType> ExtractInterface(Optional<UnitType> unitType, CancellationToken _) => unitType.HasValue ? unitType.Value : new Optional<IUnitType>();
    private static IUnitPopulation ExtractPopulation<T>((IUnitPopulation Population, T) input, CancellationToken _) => input.Population;
    private static UnitProcessingData ExtractProcessingData<T>((T, UnitProcessingData ProcessingData) input, CancellationToken _) => input.ProcessingData;

    private static (IUnitPopulation, UnitProcessingData) CreatePopulation(ImmutableArray<IUnitType> units, CancellationToken _) => UnitPopulation.Build(units);

    private SharpMeasuresUnitProcesser SharpMeasuresUnitProcesser => new(DiagnosticsStrategy.SharpMeasuresUnitDiagnostics);

    private DerivableUnitProcesser DerivableUnitProcesser => new(DiagnosticsStrategy.DerivableUnitDiagnostics);

    private FixedUnitInstanceProcesser FixedUnitInstanceProcesser => new(DiagnosticsStrategy.FixedUnitInstanceDiagnostics);
    private UnitInstanceAliasProcesser UnitInstanceAliasProcesser => new(DiagnosticsStrategy.UnitInstanceAliasDiagnostics);
    private DerivedUnitInstanceProcesser DerivedUnitInstanceProcesser => new(DiagnosticsStrategy.DerivedUnitInstanceDiagnostics);
    private BiasedUnitInstanceProcesser BiasedUnitInstanceProcesser => new(DiagnosticsStrategy.BiasedUnitInstanceDiagnostics);
    private PrefixedUnitInstanceProcesser PrefixedUnitInstanceProcesser => new(DiagnosticsStrategy.PrefixedUnitInstanceDiagnostics);
    private ScaledUnitInstanceProcesser ScaledUnitInstanceProcesser => new(DiagnosticsStrategy.ScaledUnitInstanceDiagnostics);
}
