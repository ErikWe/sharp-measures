﻿namespace SharpMeasures.Generators.Vectors.ForeignVectorParsing;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Quantities.Parsing.Contexts.Processing;
using SharpMeasures.Generators.Quantities.Parsing.ConvertibleQuantity;
using SharpMeasures.Generators.Quantities.Parsing.DerivedQuantity;
using SharpMeasures.Generators.Quantities.Parsing.ExcludeUnits;
using SharpMeasures.Generators.Quantities.Parsing.IncludeUnits;
using SharpMeasures.Generators.Vectors.ForeignVectorParsing.Diagnostics;
using SharpMeasures.Generators.Vectors.Parsing.ConvertibleVector;
using SharpMeasures.Generators.Vectors.Parsing.VectorConstant;

using System.Collections.Generic;

internal static class CommonProcessing
{
    public static IResultWithDiagnostics<IReadOnlyList<DerivedQuantityDefinition>> ProcessDerivations(DefinedType type, IEnumerable<RawDerivedQuantityDefinition> rawDefinitions)
    {
        var processingContext = new SimpleProcessingContext(type);

        return ProcessingFilter.Create(DerivedQuantityProcesser).Filter(processingContext, rawDefinitions);
    }

    public static IResultWithDiagnostics<IReadOnlyList<VectorConstantDefinition>> ProcessConstants(DefinedType type, IEnumerable<RawVectorConstantDefinition> rawDefinitions)
    {
        QuantityConstantProcessingContext processingContext = new(type);

        return ProcessingFilter.Create(ScalarConstantProcesser).Filter(processingContext, rawDefinitions);
    }

    public static IResultWithDiagnostics<IReadOnlyList<ConvertibleVectorDefinition>> ProcessConversions(DefinedType type, IEnumerable<RawConvertibleQuantityDefinition> rawDefinitions)
    {
        ConvertibleQuantityProcessingContext processingContext = new(type);

        return ProcessingFilter.Create(ConvertibleScalarProcesser).Filter(processingContext, rawDefinitions);
    }

    public static IResultWithDiagnostics<IReadOnlyList<IncludeUnitsDefinition>> ProcessIncludeUnits(DefinedType type, IEnumerable<RawIncludeUnitsDefinition> rawDefinitions)
    {
        IncludeUnitsProcessingContext processingContext = new(type);

        return ProcessingFilter.Create(IncludeUnitsProcesser).Filter(processingContext, rawDefinitions);
    }

    public static IResultWithDiagnostics<IReadOnlyList<ExcludeUnitsDefinition>> ProcessExcludeUnits(DefinedType type, IEnumerable<RawExcludeUnitsDefinition> rawDefinitions)
    {
        ExcludeUnitsProcessingContext processingContext = new(type);

        return ProcessingFilter.Create(ExcludeUnitsProcesser).Filter(processingContext, rawDefinitions);
    }

    private static DerivedQuantityProcesser DerivedQuantityProcesser { get; } = new(EmptyDerivedQuantityProcessingDiagnostics.Instance);
    private static VectorConstantProcesser ScalarConstantProcesser { get; } = new(EmptyVectorConstantProcessingDiagnostics.Instance);
    private static ConvertibleVectorProcesser ConvertibleScalarProcesser { get; } = new(EmptyConvertibleQuantityProcessingDiagnostics.Instance);

    private static IncludeUnitsProcesser IncludeUnitsProcesser { get; } = new(EmptyIncludeUnitsProcessingDiagnostics.Instance);
    private static ExcludeUnitsProcesser ExcludeUnitsProcesser { get; } = new(EmptyExcludeUnitsProcessingDiagnostics.Instance);
}