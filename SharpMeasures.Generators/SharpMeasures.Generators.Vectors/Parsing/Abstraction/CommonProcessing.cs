namespace SharpMeasures.Generators.Vectors.Parsing.Abstraction;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Quantities.Parsing.Contexts.Processing;
using SharpMeasures.Generators.Quantities.Parsing.ConvertibleQuantity;
using SharpMeasures.Generators.Quantities.Parsing.DerivedQuantity;
using SharpMeasures.Generators.Quantities.Parsing.Diagnostics.Processing;
using SharpMeasures.Generators.Quantities.Parsing.ExcludeUnits;
using SharpMeasures.Generators.Quantities.Parsing.IncludeUnits;
using SharpMeasures.Generators.Vectors.Parsing.ConvertibleVector;
using SharpMeasures.Generators.Vectors.Parsing.Diagnostics.Processing;
using SharpMeasures.Generators.Vectors.Parsing.VectorConstant;

using System.Collections.Generic;

internal static class CommonProcessing
{
    public static IResultWithDiagnostics<IReadOnlyList<DerivedQuantityDefinition>> ProcessDerivations(DefinedType type, IEnumerable<RawDerivedQuantityDefinition> rawDefinitions)
    {
        DerivedQuantityProcessingContext processingContext = new(type, QuantityType.Vector);

        return ProcessingFilter.Create(DerivedQuantityProcesser).Filter(processingContext, rawDefinitions);
    }

    public static IResultWithDiagnostics<IReadOnlyList<VectorConstantDefinition>> ProcessConstants(DefinedType type, IEnumerable<RawVectorConstantDefinition> rawDefinitions, NamedType? unit)
    {
        var scalarConstantProcesser = unit is null ? VectorConstantProcesserForUnknownUnit : VectorConstantProcesser(unit.Value);

        QuantityConstantProcessingContext processingContext = new(type);

        return ProcessingFilter.Create(scalarConstantProcesser).Filter(processingContext, rawDefinitions);
    }

    public static IResultWithDiagnostics<IReadOnlyList<ConvertibleVectorDefinition>> ProcessConversions(DefinedType type, NamedType? originalQuantity, bool conversionFromOriginalQuantitySpecified, bool conversionToOriginalQuantitySpecified, IEnumerable<RawConvertibleQuantityDefinition> rawDefinitions)
    {
        ConvertibleQuantityProcessingContext processingContext = new(type, originalQuantity, conversionFromOriginalQuantitySpecified, conversionToOriginalQuantitySpecified);

        return ProcessingFilter.Create(ConvertibleVectorProcesser).Filter(processingContext, rawDefinitions);
    }

    public static IResultWithDiagnostics<IReadOnlyList<IncludeUnitsDefinition>> ProcessIncludeUnitInstances(DefinedType type, IEnumerable<RawIncludeUnitsDefinition> rawDefinitions)
    {
        IncludeUnitsProcessingContext processingContext = new(type);

        return ProcessingFilter.Create(IncludeUnitsProcesser).Filter(processingContext, rawDefinitions);
    }

    public static IResultWithDiagnostics<IReadOnlyList<ExcludeUnitsDefinition>> ProcessExcludeUnitInstances(DefinedType type, IEnumerable<RawExcludeUnitsDefinition> rawDefinitions)
    {
        ExcludeUnitsProcessingContext processingContext = new(type);

        return ProcessingFilter.Create(ExcludeUnitsProcesser).Filter(processingContext, rawDefinitions);
    }

    private static DerivedQuantityProcesser DerivedQuantityProcesser { get; } = new(DerivedQuantityProcessingDiagnostics.Instance);
    private static VectorConstantProcesser VectorConstantProcesser(NamedType unit) => new(new VectorConstantProcessingDiagnostics(unit));
    private static VectorConstantProcesser VectorConstantProcesserForUnknownUnit { get; } = new(new VectorConstantProcessingDiagnostics());
    private static ConvertibleVectorProcesser ConvertibleVectorProcesser { get; } = new(ConvertibleVectorProcessingDiagnostics.Instance);

    private static IncludeUnitsProcesser IncludeUnitsProcesser { get; } = new(IncludeUnitsProcessingDiagnostics.Instance);
    private static ExcludeUnitsProcesser ExcludeUnitsProcesser { get; } = new(ExcludeUnitsProcessingDiagnostics.Instance);
}
