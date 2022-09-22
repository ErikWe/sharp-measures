namespace SharpMeasures.Generators.Scalars.ForeignScalarParsing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Quantities.Parsing.Contexts.Processing;
using SharpMeasures.Generators.Quantities.Parsing.ConvertibleQuantity;
using SharpMeasures.Generators.Quantities.Parsing.DerivedQuantity;
using SharpMeasures.Generators.Quantities.Parsing.ExcludeUnits;
using SharpMeasures.Generators.Quantities.Parsing.IncludeUnits;
using SharpMeasures.Generators.Scalars.ForeignScalarParsing.Diagnostics.Processing;
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

        if (scalar.HasValue is false)
        {
            return new Optional<TProductType>();
        }

        var derivations = ProcessDerivations(rawScalar.Type, rawScalar.Derivations);
        var constants = ProcessConstants(rawScalar.Type, rawScalar.Constants);
        var conversions = ProcessConversions(rawScalar.Type, GetOriginalQuantity(scalar.Value), ConversionFromOriginalQuantitySpecified(scalar.Value), ConversionToOriginalQuantitySpecified(scalar.Value), rawScalar.Conversions);

        var includeUnitInstanceBases = ProcessIncludeUnitBases(rawScalar.Type, rawScalar.UnitBaseInstanceInclusions);
        var excludeUnitInstanceBases = ProcessExcludeUnitBases(rawScalar.Type, rawScalar.UnitBaseInstanceExclusions);

        var includeUnitInstances = ProcessIncludeUnits(rawScalar.Type, rawScalar.UnitInstanceInclusions);
        var excludeUnitInstances = ProcessExcludeUnits(rawScalar.Type, rawScalar.UnitInstanceExclusions);

        if (includeUnitInstanceBases.Count > 0 && excludeUnitInstanceBases.Count > 0)
        {
            excludeUnitInstanceBases = Array.Empty<ExcludeUnitBasesDefinition>();
        }

        if (includeUnitInstances.Count > 0 && excludeUnitInstances.Count > 0)
        {
            excludeUnitInstances = Array.Empty<ExcludeUnitsDefinition>();
        }

        return ProduceResult(rawScalar.Type, rawScalar.TypeLocation, scalar.Value, derivations, constants, conversions, includeUnitInstanceBases, excludeUnitInstanceBases, includeUnitInstances, excludeUnitInstances);
    }

    protected abstract TProductType ProduceResult(DefinedType type, MinimalLocation typeLocation, TProductDefinition definition, IReadOnlyList<DerivedQuantityDefinition> derivations, IReadOnlyList<ScalarConstantDefinition> constants, IReadOnlyList<ConvertibleScalarDefinition> conversions,
        IReadOnlyList<IncludeUnitBasesDefinition> baseInclusions, IReadOnlyList<ExcludeUnitBasesDefinition> baseExclusions, IReadOnlyList<IncludeUnitsDefinition> unitInstanceInclusions, IReadOnlyList<ExcludeUnitsDefinition> unitInstanceExclusions);

    protected abstract Optional<TProductDefinition> ProcessScalar(DefinedType type, TRawDefinition rawDefinition);

    protected abstract NamedType? GetOriginalQuantity(TProductDefinition scalar);
    protected abstract bool ConversionFromOriginalQuantitySpecified(TProductDefinition scalar);
    protected abstract bool ConversionToOriginalQuantitySpecified(TProductDefinition scalar);

    private static IReadOnlyList<DerivedQuantityDefinition> ProcessDerivations(DefinedType type, IEnumerable<RawDerivedQuantityDefinition> rawDefinitions)
    {
        DerivedQuantityProcessingContext processingContext = new(type, Quantities.QuantityType.Scalar);

        return ProcessingFilter.Create(DerivedQuantityProcesser).Filter(processingContext, rawDefinitions).Result;
    }

    private static IReadOnlyList<ScalarConstantDefinition> ProcessConstants(DefinedType type, IEnumerable<RawScalarConstantDefinition> rawDefinitions)
    {
        QuantityConstantProcessingContext processingContext = new(type);

        return ProcessingFilter.Create(ScalarConstantProcesser).Filter(processingContext, rawDefinitions).Result;
    }

    private static IReadOnlyList<ConvertibleScalarDefinition> ProcessConversions(DefinedType type, NamedType? originalQuantity, bool conversionFromOriginalQuantitySpecified, bool conversionToOriginalQuantitySpecified, IEnumerable<RawConvertibleQuantityDefinition> rawDefinitions)
    {
        ConvertibleQuantityProcessingContext processingContext = new(type, originalQuantity, conversionFromOriginalQuantitySpecified, conversionToOriginalQuantitySpecified);

        return ProcessingFilter.Create(ConvertibleScalarProcesser).Filter(processingContext, rawDefinitions).Result;
    }

    private static IReadOnlyList<IncludeUnitBasesDefinition> ProcessIncludeUnitBases(DefinedType type, IEnumerable<RawIncludeUnitBasesDefinition> rawDefinitions)
    {
        IncludeUnitBasesProcessingContext processingContext = new(type);

        return ProcessingFilter.Create(IncludeUnitBasesProcesser).Filter(processingContext, rawDefinitions).Result;
    }

    private static IReadOnlyList<ExcludeUnitBasesDefinition> ProcessExcludeUnitBases(DefinedType type, IEnumerable<RawExcludeUnitBasesDefinition> rawDefinitions)
    {
        ExcludUniteBasesProcessingContext processingContext = new(type);

        return ProcessingFilter.Create(ExcludeUnitBasesProcesser).Filter(processingContext, rawDefinitions).Result;
    }

    private static IReadOnlyList<IncludeUnitsDefinition> ProcessIncludeUnits(DefinedType type, IEnumerable<RawIncludeUnitsDefinition> rawDefinitions)
    {
        IncludeUnitsProcessingContext processingContext = new(type);

        return ProcessingFilter.Create(IncludeUnitsProcesser).Filter(processingContext, rawDefinitions).Result;
    }

    private static IReadOnlyList<ExcludeUnitsDefinition> ProcessExcludeUnits(DefinedType type, IEnumerable<RawExcludeUnitsDefinition> rawDefinitions)
    {
        ExcludeUnitsProcessingContext processingContext = new(type);

        return ProcessingFilter.Create(ExcludeUnitsProcesser).Filter(processingContext, rawDefinitions).Result;
    }

    private static DerivedQuantityProcesser DerivedQuantityProcesser { get; } = new(EmptyDerivedQuantityProcessingDiagnostics.Instance);
    private static ScalarConstantProcesser ScalarConstantProcesser { get; } = new(EmptyScalarConstantProcessingDiagnostics.Instance);
    private static ConvertibleScalarProcesser ConvertibleScalarProcesser { get; } = new(EmptyConvertibleQuantityProcessingDiagnostics.Instance);

    private static IncludeUnitBasesProcesser IncludeUnitBasesProcesser { get; } = new(EmptyIncludeUnitBasesProcessingDiagnostics.Instance);
    private static ExcludeUnitBasesProcesser ExcludeUnitBasesProcesser { get; } = new(EmptyExcludeUnitBasesProcessingDiagnostics.Instance);
    private static IncludeUnitsProcesser IncludeUnitsProcesser { get; } = new(EmptyIncludeUnitsProcessingDiagnostics.Instance);
    private static ExcludeUnitsProcesser ExcludeUnitsProcesser { get; } = new(EmptyExcludeUnitsProcessingDiagnostics.Instance);
}
