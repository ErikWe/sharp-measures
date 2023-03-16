namespace SharpMeasures.Generators.Vectors.Parsing.Abstraction;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Quantities.Parsing.Contexts.Processing;
using SharpMeasures.Generators.Quantities.Parsing.ConvertibleQuantity;
using SharpMeasures.Generators.Quantities.Parsing.ExcludeUnits;
using SharpMeasures.Generators.Quantities.Parsing.IncludeUnits;
using SharpMeasures.Generators.Quantities.Parsing.QuantityOperation;
using SharpMeasures.Generators.Quantities.Parsing.QuantityProcess;
using SharpMeasures.Generators.Vectors.Parsing.Contexts.Processing;
using SharpMeasures.Generators.Vectors.Parsing.ConvertibleVector;
using SharpMeasures.Generators.Vectors.Parsing.VectorConstant;
using SharpMeasures.Generators.Vectors.Parsing.VectorOperation;

using System.Collections.Generic;

internal static class CommonProcessing
{
    public static IResultWithDiagnostics<IReadOnlyList<QuantityOperationDefinition>> ProcessOperations(DefinedType type, IEnumerable<RawQuantityOperationDefinition> rawDefinitions, HashSet<(string, NamedType)> reservedSignatures, IVectorProcessingDiagnosticsStrategy diagnosticsStrategy)
    {
        Contexts.Processing.QuantityOperationProcessingContext processingContext = new(type, reservedSignatures);

        return ProcessingFilter.Create(new QuantityOperationProcesser(diagnosticsStrategy.QuantityOperationDiagnostics)).Filter(processingContext, rawDefinitions);
    }

    public static IResultWithDiagnostics<IReadOnlyList<VectorOperationDefinition>> ProcessVectorOperations(DefinedType type, IEnumerable<RawVectorOperationDefinition> rawDefinitions, HashSet<(string, NamedType)> reservedSignatures, IVectorProcessingDiagnosticsStrategy diagnosticsStrategy)
    {
        VectorOperationProcessingContext processingContext = new(type, reservedSignatures);

        return ProcessingFilter.Create(new VectorOperationProcesser(diagnosticsStrategy.VectorOperationDiagnostics)).Filter(processingContext, rawDefinitions);
    }

    public static IResultWithDiagnostics<IReadOnlyList<QuantityProcessDefinition>> ProcessProcesses(DefinedType type, IEnumerable<RawQuantityProcessDefinition> rawDefinitions, IVectorProcessingDiagnosticsStrategy diagnosticsStrategy)
    {
        QuantityProcessProcessingContext processingContext = new(type);

        return ProcessingFilter.Create(new QuantityProcessProcesser(diagnosticsStrategy.ProcessedQuantityDiagnostics)).Filter(processingContext, rawDefinitions);
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
