namespace SharpMeasures.Generators.Scalars.Parsing.Abstraction;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Quantities.Parsing.Contexts.Processing;
using SharpMeasures.Generators.Quantities.Parsing.ConvertibleQuantity;
using SharpMeasures.Generators.Quantities.Parsing.DerivedQuantity;
using SharpMeasures.Generators.Quantities.Parsing.Diagnostics.Processing;
using SharpMeasures.Generators.Quantities.Parsing.ExcludeUnits;
using SharpMeasures.Generators.Quantities.Parsing.IncludeUnits;
using SharpMeasures.Generators.Scalars.Parsing.Contexts.Processing;
using SharpMeasures.Generators.Scalars.Parsing.ConvertibleScalar;
using SharpMeasures.Generators.Scalars.Parsing.Diagnostics;
using SharpMeasures.Generators.Scalars.Parsing.Diagnostics.Processing;
using SharpMeasures.Generators.Scalars.Parsing.ExcludeUnitBases;
using SharpMeasures.Generators.Scalars.Parsing.IncludeUnitBases;
using SharpMeasures.Generators.Scalars.Parsing.ScalarConstant;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

internal abstract class AScalarProcesser<TRawType, TRawDefinition, TProductType, TProductDefinition>
    where TRawType : ARawScalarType<TRawDefinition>
    where TProductDefinition : IScalar
{
    public IOptionalWithDiagnostics<TProductType> Process(Optional<TRawType> rawScalar, CancellationToken token)
    {
        if (token.IsCancellationRequested || rawScalar.HasValue is false)
        {
            return OptionalWithDiagnostics.Empty<TProductType>();
        }

        return Process(rawScalar.Value);
    }

    public IOptionalWithDiagnostics<TProductType> Process(TRawType rawScalar)
    {
        var scalar = ProcessScalar(rawScalar.Type, rawScalar.Definition);

        if (scalar.LacksResult)
        {
            return scalar.AsEmptyOptional<TProductType>();
        }

        var unit = GetUnit(scalar.Result);

        var derivations = ProcessDerivations(rawScalar.Type, rawScalar.Derivations);
        var constants = ProcessConstants(rawScalar.Type, rawScalar.Constants, unit);
        var conversions = ProcessConversions(rawScalar.Type, GetOriginalQuantity(scalar.Result), ConversionFromOriginalQuantitySpecified(scalar.Result), ConversionToOriginalQuantitySpecified(scalar.Result), rawScalar.Conversions);

        var includeUnitInstanceBases = ProcessIncludeUnitBases(rawScalar.Type, rawScalar.UnitBaseInstanceInclusions);
        var excludeUnitInstanceBases = ProcessExcludeUnitBases(rawScalar.Type, rawScalar.UnitBaseInstanceExclusions);

        var includeUnitInstances = ProcessIncludeUnits(rawScalar.Type, rawScalar.UnitInstanceInclusions);
        var excludeUnitInstances = ProcessExcludeUnits(rawScalar.Type, rawScalar.UnitInstanceExclusions);

        var allDiagnostics = scalar.Diagnostics.Concat(derivations).Concat(constants).Concat(conversions).Concat(includeUnitInstanceBases).Concat(excludeUnitInstanceBases).Concat(includeUnitInstances).Concat(excludeUnitInstances);

        if (includeUnitInstanceBases.HasResult && includeUnitInstanceBases.Result.Count > 0 && excludeUnitInstanceBases.HasResult && excludeUnitInstanceBases.Result.Count > 0)
        {
            allDiagnostics = allDiagnostics.Concat(new[] { ScalarTypeDiagnostics.ContradictoryAttributes<IncludeUnitBasesAttribute, ExcludeUnitBasesAttribute>(rawScalar.TypeLocation) });
            excludeUnitInstanceBases = ResultWithDiagnostics.Construct(Array.Empty<ExcludeUnitBasesDefinition>() as IReadOnlyList<ExcludeUnitBasesDefinition>);
        }

        if (includeUnitInstances.HasResult && includeUnitInstances.Result.Count > 0 && excludeUnitInstances.HasResult && excludeUnitInstances.Result.Count > 0)
        {
            allDiagnostics = allDiagnostics.Concat(new[] { ScalarTypeDiagnostics.ContradictoryAttributes<IncludeUnitsAttribute, ExcludeUnitsAttribute>(rawScalar.TypeLocation) });
            excludeUnitInstances = ResultWithDiagnostics.Construct(Array.Empty<ExcludeUnitsDefinition>() as IReadOnlyList<ExcludeUnitsDefinition>);
        }

        TProductType product = ProduceResult(rawScalar.Type, rawScalar.TypeLocation, scalar.Result, derivations.Result, constants.Result, conversions.Result, includeUnitInstanceBases.Result, excludeUnitInstanceBases.Result, includeUnitInstances.Result, excludeUnitInstances.Result);

        return OptionalWithDiagnostics.Result(product, allDiagnostics);
    }

    protected abstract TProductType ProduceResult(DefinedType type, MinimalLocation typeLocation, TProductDefinition definition, IReadOnlyList<DerivedQuantityDefinition> derivations, IReadOnlyList<ScalarConstantDefinition> constants, IReadOnlyList<ConvertibleScalarDefinition> conversions,
        IReadOnlyList<IncludeUnitBasesDefinition> baseInclusions, IReadOnlyList<ExcludeUnitBasesDefinition> baseExclusions, IReadOnlyList<IncludeUnitsDefinition> unitInstanceInclusions, IReadOnlyList<ExcludeUnitsDefinition> unitInstanceExclusions);

    protected abstract IOptionalWithDiagnostics<TProductDefinition> ProcessScalar(DefinedType type, TRawDefinition rawDefinition);

    protected abstract NamedType? GetUnit(TProductDefinition scalar);
    protected abstract NamedType? GetOriginalQuantity(TProductDefinition scalar);
    protected abstract bool ConversionFromOriginalQuantitySpecified(TProductDefinition scalar);
    protected abstract bool ConversionToOriginalQuantitySpecified(TProductDefinition scalar);

    private static IResultWithDiagnostics<IReadOnlyList<DerivedQuantityDefinition>> ProcessDerivations(DefinedType type, IEnumerable<RawDerivedQuantityDefinition> rawDefinitions)
    {
        DerivedQuantityProcessingContext processingContext = new(type, QuantityType.Scalar);

        return ProcessingFilter.Create(DerivedQuantityProcesser).Filter(processingContext, rawDefinitions);
    }

    private static IResultWithDiagnostics<IReadOnlyList<ScalarConstantDefinition>> ProcessConstants(DefinedType type, IEnumerable<RawScalarConstantDefinition> rawDefinitions, NamedType? unit)
    {
        var scalarConstantProcesser = unit is null ? ScalarConstantProcesserForUnknownUnit : ScalarConstantProcesser(unit.Value);

        QuantityConstantProcessingContext processingContext = new(type);

        return ProcessingFilter.Create(scalarConstantProcesser).Filter(processingContext, rawDefinitions);
    }

    private static IResultWithDiagnostics<IReadOnlyList<ConvertibleScalarDefinition>> ProcessConversions(DefinedType type, NamedType? originalQuantity, bool conversionFromOriginalQuantitySpecified, bool conversionToOriginalQuantitySpecified, IEnumerable<RawConvertibleQuantityDefinition> rawDefinitions)
    {
        ConvertibleQuantityProcessingContext processingContext = new(type, originalQuantity, conversionFromOriginalQuantitySpecified, conversionToOriginalQuantitySpecified);

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

    private static DerivedQuantityProcesser DerivedQuantityProcesser { get; } = new(DerivedQuantityProcessingDiagnostics.Instance);
    private static ScalarConstantProcesser ScalarConstantProcesser(NamedType unit) => new(new ScalarConstantProcessingDiagnostics(unit));
    private static ScalarConstantProcesser ScalarConstantProcesserForUnknownUnit { get; } = new(new ScalarConstantProcessingDiagnostics());
    private static ConvertibleScalarProcesser ConvertibleScalarProcesser { get; } = new(ConvertibleScalarProcessingDiagnostics.Instance);

    private static IncludeUnitBasesProcesser IncludeUnitBasesProcesser { get; } = new(IncludeUnitBasesProcessingDiagnostics.Instance);
    private static ExcludeUnitBasesProcesser ExcludeUnitBasesProcesser { get; } = new(ExcludeUnitBasesProcessingDiagnostics.Instance);
    private static IncludeUnitsProcesser IncludeUnitsProcesser { get; } = new(IncludeUnitsProcessingDiagnostics.Instance);
    private static ExcludeUnitsProcesser ExcludeUnitsProcesser { get; } = new(ExcludeUnitsProcessingDiagnostics.Instance);
}
