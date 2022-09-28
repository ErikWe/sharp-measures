namespace SharpMeasures.Generators.Vectors.Parsing.Abstraction;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Quantities.Parsing.Contexts.Processing;
using SharpMeasures.Generators.Quantities.Parsing.ConvertibleQuantity;
using SharpMeasures.Generators.Quantities.Parsing.DerivedQuantity;
using SharpMeasures.Generators.Quantities.Parsing.ExcludeUnits;
using SharpMeasures.Generators.Quantities.Parsing.IncludeUnits;
using SharpMeasures.Generators.Quantities.Parsing.ProcessedQuantity;
using SharpMeasures.Generators.Vectors.Parsing.ConvertibleVector;
using SharpMeasures.Generators.Vectors.Parsing.VectorConstant;

using System.Collections.Generic;

internal static class CommonProcessing
{
    public static IResultWithDiagnostics<IReadOnlyList<DerivedQuantityDefinition>> ProcessDerivations(DefinedType type, IEnumerable<RawDerivedQuantityDefinition> rawDefinitions, IVectorProcessingDiagnosticsStrategy diagnosticsStrategy)
    {
        DerivedQuantityProcessingContext processingContext = new(type, QuantityType.Vector);

        return ProcessingFilter.Create(new DerivedQuantityProcesser(diagnosticsStrategy.DerivedQuantityDiagnostics)).Filter(processingContext, rawDefinitions);
    }

    public static IResultWithDiagnostics<IReadOnlyList<ProcessedQuantityDefinition>> ProcessProcesses(DefinedType type, IEnumerable<RawProcessedQuantityDefinition> rawDefinitions, IVectorProcessingDiagnosticsStrategy diagnosticsStrategy)
    {
        ProcessedQuantityProcessingContext processingContext = new(type);

        return ProcessingFilter.Create(new ProcessedQuantityProcesser(diagnosticsStrategy.ProcessedQuantityDiagnostics)).Filter(processingContext, rawDefinitions);
    }

    public static IResultWithDiagnostics<IReadOnlyList<VectorConstantDefinition>> ProcessConstants(DefinedType type, IEnumerable<RawVectorConstantDefinition> rawDefinitions, NamedType? unit, IVectorProcessingDiagnosticsStrategy diagnosticsStrategy)
    {
        var vectorConstantProcesser = unit is null ? new VectorConstantProcesser(diagnosticsStrategy.VectorConstantDiagnosticsForUnknownUnit) : new VectorConstantProcesser(diagnosticsStrategy.VectorConstantDiagnostics(unit.Value));

        QuantityConstantProcessingContext processingContext = new(type);

        return ProcessingFilter.Create(vectorConstantProcesser).Filter(processingContext, rawDefinitions);
    }

    public static IResultWithDiagnostics<IReadOnlyList<ConvertibleVectorDefinition>> ProcessConversions(DefinedType type, NamedType? originalQuantity, bool conversionFromOriginalQuantitySpecified, bool conversionToOriginalQuantitySpecified, IEnumerable<RawConvertibleQuantityDefinition> rawDefinitions, IVectorProcessingDiagnosticsStrategy diagnosticsStrategy)
    {
        ConvertibleQuantityProcessingContext processingContext = new(type, originalQuantity, conversionFromOriginalQuantitySpecified, conversionToOriginalQuantitySpecified);

        return ProcessingFilter.Create(new ConvertibleVectorProcesser(diagnosticsStrategy.ConvertibleVectorDiagnostics)).Filter(processingContext, rawDefinitions);
    }

    public static IResultWithDiagnostics<IReadOnlyList<IncludeUnitsDefinition>> ProcessIncludeUnitInstances(DefinedType type, IEnumerable<RawIncludeUnitsDefinition> rawDefinitions, IVectorProcessingDiagnosticsStrategy diagnosticsStrategy)
    {
        IncludeUnitsProcessingContext processingContext = new(type);

        return ProcessingFilter.Create(new IncludeUnitsProcesser(diagnosticsStrategy.IncludeUnitsDiagnostics)).Filter(processingContext, rawDefinitions);
    }

    public static IResultWithDiagnostics<IReadOnlyList<ExcludeUnitsDefinition>> ProcessExcludeUnitInstances(DefinedType type, IEnumerable<RawExcludeUnitsDefinition> rawDefinitions, IVectorProcessingDiagnosticsStrategy diagnosticsStrategy)
    {
        ExcludeUnitsProcessingContext processingContext = new(type);

        return ProcessingFilter.Create(new ExcludeUnitsProcesser(diagnosticsStrategy.ExcludeUnitsDiagnostics)).Filter(processingContext, rawDefinitions);
    }
}
