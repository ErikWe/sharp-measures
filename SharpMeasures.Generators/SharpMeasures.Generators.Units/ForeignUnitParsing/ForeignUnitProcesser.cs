namespace SharpMeasures.Generators.Units.ForeignUnitParsing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Units.ForeignUnitParsing.Diagnostics;
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

internal static class ForeignUnitProcesser
{
    public static Optional<IUnitType> Process(RawUnitType rawUnit)
    {
        var unit = ProcessUnit(rawUnit.Type, rawUnit.Definition);

        if (unit.LacksResult)
        {
            return new Optional<IUnitType>();
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

        return unitType;
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

    private static class Processers
    {
        public static SharpMeasuresUnitProcesser SharpMeasuresUnitProcesser { get; } = new(EmptySharpMeasuresUnitProcessingDiagnostics.Instance);

        public static FixedUnitInstanceProcesser FixedUnitInstanceProcesser { get; } = new(EmptyFixedUnitInstanceProcessingDiagnostics.Instance);
        public static DerivableUnitProcesser DerivableUnitProcesser { get; } = new(EmptyDerivableUnitProcessingDiagnostics.Instance);

        public static UnitInstanceAliasProcesser UnitInstanceAliasProcesser { get; } = new(EmptyModifiedUnitInstanceProcessingDiagnostics<RawUnitInstanceAliasDefinition, UnitInstanceAliasLocations>.Instance);
        public static DerivedUnitInstanceProcesser DerivedUnitInstanceProcesser { get; } = new(EmptyDerivedUnitInstanceProcessingDiagnostics.Instance);
        public static BiasedUnitInstanceProcesser BiasedUnitInstanceProcesser { get; } = new(EmptyBiasedUnitInstanceProcessingDiagnostics.Instance);
        public static PrefixedUnitInstanceProcesser PrefixedUnitInstanceProcesser { get; } = new(EmptyPrefixedUnitInstanceProcessingDiagnostics.Instance);
        public static ScaledUnitInstanceProcesser ScaledUnitInstanceProcesser { get; } = new(EmptyScaledUnitInstanceProcessingDiagnostics.Instance);
    }
}
