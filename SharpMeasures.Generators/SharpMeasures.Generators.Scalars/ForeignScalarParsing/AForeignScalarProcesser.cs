namespace SharpMeasures.Generators.Scalars.ForeignScalarParsing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Quantities.Parsing.Contexts.Processing;
using SharpMeasures.Generators.Quantities.Parsing.ConvertibleQuantity;
using SharpMeasures.Generators.Quantities.Parsing.DerivedQuantity;
using SharpMeasures.Generators.Quantities.Parsing.ExcludeUnits;
using SharpMeasures.Generators.Quantities.Parsing.IncludeUnits;
using SharpMeasures.Generators.Scalars.ForeignScalarParsing.Diagnostics;
using SharpMeasures.Generators.Scalars.Parsing.Abstraction;
using SharpMeasures.Generators.Scalars.Parsing.Contexts.Processing;
using SharpMeasures.Generators.Scalars.Parsing.ConvertibleScalar;
using SharpMeasures.Generators.Scalars.Parsing.ExcludeUnitBases;
using SharpMeasures.Generators.Scalars.Parsing.IncludeUnitBases;
using SharpMeasures.Generators.Scalars.Parsing.ScalarConstant;

using System;
using System.Collections.Generic;

internal abstract class AForeignScalarProcesser<TRawType, TRawDefinition, TProductType, TProductDefinition>
    where TRawType : ARawScalarType<TRawDefinition>
    where TProductDefinition : IScalar
{
    public Optional<TProductType> Process(TRawType rawScalar)
    {
        var scalar = ProcessScalar(rawScalar.Type, rawScalar.Definition);

        if (scalar.LacksResult)
        {
            return new Optional<TProductType>();
        }

        var derivations = ProcessDerivations(rawScalar.Type, rawScalar.Derivations);
        var constants = ProcessConstants(rawScalar.Type, rawScalar.Constants);
        var conversions = ProcessConversions(rawScalar.Type, rawScalar.Conversions);

        var includeUnitInstanceBases = ProcessIncludeUnitBases(rawScalar.Type, rawScalar.UnitBaseInstanceInclusions);
        var excludeUnitInstanceBases = ProcessExcludeUnitBases(rawScalar.Type, rawScalar.UnitBaseInstanceExclusions);

        var includeUnitInstances = ProcessIncludeUnits(rawScalar.Type, rawScalar.UnitInstanceInclusions);
        var excludeUnitInstances = ProcessExcludeUnits(rawScalar.Type, rawScalar.UnitInstanceExclusions);

        if (includeUnitInstanceBases.HasResult && includeUnitInstanceBases.Result.Count > 0 && excludeUnitInstanceBases.HasResult && excludeUnitInstanceBases.Result.Count > 0)
        {
            excludeUnitInstanceBases = ResultWithDiagnostics.Construct(Array.Empty<ExcludeUnitBasesDefinition>() as IReadOnlyList<ExcludeUnitBasesDefinition>);
        }

        if (includeUnitInstances.HasResult && includeUnitInstances.Result.Count > 0 && excludeUnitInstances.HasResult && excludeUnitInstances.Result.Count > 0)
        {
            excludeUnitInstances = ResultWithDiagnostics.Construct(Array.Empty<ExcludeUnitsDefinition>() as IReadOnlyList<ExcludeUnitsDefinition>);
        }

        return ProduceResult(rawScalar.Type, rawScalar.TypeLocation, scalar.Result, derivations.Result, constants.Result, conversions.Result, includeUnitInstanceBases.Result, excludeUnitInstanceBases.Result, includeUnitInstances.Result, excludeUnitInstances.Result);
    }

    protected abstract TProductType ProduceResult(DefinedType type, MinimalLocation typeLocation, TProductDefinition definition, IReadOnlyList<DerivedQuantityDefinition> derivations,
        IReadOnlyList<ScalarConstantDefinition> constants, IReadOnlyList<ConvertibleScalarDefinition> conversions, IReadOnlyList<IncludeUnitBasesDefinition> baseInclusions,
        IReadOnlyList<ExcludeUnitBasesDefinition> baseExclusions, IReadOnlyList<IncludeUnitsDefinition> unitInstanceInclusions, IReadOnlyList<ExcludeUnitsDefinition> unitInstanceExclusions);

    protected abstract IOptionalWithDiagnostics<TProductDefinition> ProcessScalar(DefinedType type, TRawDefinition rawDefinition);

    private static IResultWithDiagnostics<IReadOnlyList<DerivedQuantityDefinition>> ProcessDerivations(DefinedType type, IEnumerable<RawDerivedQuantityDefinition> rawDefinitions)
    {
        var processingContext = new SimpleProcessingContext(type);

        return ProcessingFilter.Create(DerivedQuantityProcesser).Filter(processingContext, rawDefinitions);
    }

    private static IResultWithDiagnostics<IReadOnlyList<ScalarConstantDefinition>> ProcessConstants(DefinedType type, IEnumerable<RawScalarConstantDefinition> rawDefinitions)
    {
        QuantityConstantProcessingContext processingContext = new(type);

        return ProcessingFilter.Create(ScalarConstantProcesser).Filter(processingContext, rawDefinitions);
    }

    private static IResultWithDiagnostics<IReadOnlyList<ConvertibleScalarDefinition>> ProcessConversions(DefinedType type, IEnumerable<RawConvertibleQuantityDefinition> rawDefinitions)
    {
        ConvertibleQuantityProcessingContext processingContext = new(type);

        return ProcessingFilter.Create(ConvertibleScalarProcesser).Filter(processingContext, rawDefinitions);
    }

    private static IResultWithDiagnostics<IReadOnlyList<IncludeUnitBasesDefinition>> ProcessIncludeUnitBases(DefinedType type, IEnumerable<RawIncludeUnitBasesDefinition> rawDefinitions)
    {
        IncludeUnitBasesProcessingContext processingContext = new(type);

        return ProcessingFilter.Create(IncludeUnitBasesProcesser).Filter(processingContext, rawDefinitions);
    }

    private static IResultWithDiagnostics<IReadOnlyList<ExcludeUnitBasesDefinition>> ProcessExcludeUnitBases(DefinedType type, IEnumerable<RawExcludeUnitBasesDefinition> rawDefinitions)
    {
        ExcludUniteBasesProcessingContext processingContext = new(type);

        return ProcessingFilter.Create(ExcludeUnitBasesProcesser).Filter(processingContext, rawDefinitions);
    }

    private static IResultWithDiagnostics<IReadOnlyList<IncludeUnitsDefinition>> ProcessIncludeUnits(DefinedType type, IEnumerable<RawIncludeUnitsDefinition> rawDefinitions)
    {
        IncludeUnitsProcessingContext processingContext = new(type);

        return ProcessingFilter.Create(IncludeUnitsProcesser).Filter(processingContext, rawDefinitions);
    }

    private static IResultWithDiagnostics<IReadOnlyList<ExcludeUnitsDefinition>> ProcessExcludeUnits(DefinedType type, IEnumerable<RawExcludeUnitsDefinition> rawDefinitions)
    {
        ExcludeUnitsProcessingContext processingContext = new(type);

        return ProcessingFilter.Create(ExcludeUnitsProcesser).Filter(processingContext, rawDefinitions);
    }

    private static DerivedQuantityProcesser DerivedQuantityProcesser { get; } = new(EmptyDerivedQuantityProcessingDiagnostics.Instance);
    private static ScalarConstantProcesser ScalarConstantProcesser { get; } = new(EmptyScalarConstantProcessingDiagnostics.Instance);
    private static ConvertibleScalarProcesser ConvertibleScalarProcesser { get; } = new(EmptyConvertibleQuantityProcessingDiagnostics.Instance);

    private static IncludeUnitBasesProcesser IncludeUnitBasesProcesser { get; } = new(EmptyIncludeUnitBasesProcessingDiagnostics.Instance);
    private static ExcludeUnitBasesProcesser ExcludeUnitBasesProcesser { get; } = new(EmptyExcludeUnitBasesProcessingDiagnostics.Instance);
    private static IncludeUnitsProcesser IncludeUnitsProcesser { get; } = new(EmptyIncludeUnitsProcessingDiagnostics.Instance);
    private static ExcludeUnitsProcesser ExcludeUnitsProcesser { get; } = new(EmptyExcludeUnitsProcessingDiagnostics.Instance);
}
