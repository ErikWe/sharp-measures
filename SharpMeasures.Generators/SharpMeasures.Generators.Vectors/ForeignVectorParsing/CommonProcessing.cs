namespace SharpMeasures.Generators.Vectors.ForeignVectorParsing;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Quantities.Parsing.Contexts.Processing;
using SharpMeasures.Generators.Quantities.Parsing.ConvertibleQuantity;
using SharpMeasures.Generators.Quantities.Parsing.DerivedQuantity;
using SharpMeasures.Generators.Quantities.Parsing.ExcludeUnits;
using SharpMeasures.Generators.Quantities.Parsing.IncludeUnits;
using SharpMeasures.Generators.Vectors.ForeignVectorParsing.Diagnostics.Processing;
using SharpMeasures.Generators.Vectors.Parsing.ConvertibleVector;
using SharpMeasures.Generators.Vectors.Parsing.VectorConstant;

using System.Collections.Generic;

internal static class CommonProcessing
{
    public static IReadOnlyList<DerivedQuantityDefinition> ProcessDerivations(DefinedType type, IEnumerable<RawDerivedQuantityDefinition> rawDefinitions)
    {
        DerivedQuantityProcessingContext processingContext = new(type, Quantities.QuantityType.Vector);

        return ProcessingFilter.Create(DerivedQuantityProcesser).Filter(processingContext, rawDefinitions).Result;
    }

    public static IReadOnlyList<VectorConstantDefinition> ProcessConstants(DefinedType type, IEnumerable<RawVectorConstantDefinition> rawDefinitions)
    {
        QuantityConstantProcessingContext processingContext = new(type);

        return ProcessingFilter.Create(ScalarConstantProcesser).Filter(processingContext, rawDefinitions).Result;
    }

    public static IReadOnlyList<ConvertibleVectorDefinition> ProcessConversions(DefinedType type, NamedType? originalQuantity, bool conversionFromOriginalQuantitySpecified, bool conversionToOriginalQuantitySpecified, IEnumerable<RawConvertibleQuantityDefinition> rawDefinitions)
    {
        ConvertibleQuantityProcessingContext processingContext = new(type, originalQuantity, conversionFromOriginalQuantitySpecified, conversionToOriginalQuantitySpecified);

        return ProcessingFilter.Create(ConvertibleScalarProcesser).Filter(processingContext, rawDefinitions).Result;
    }

    public static IReadOnlyList<IncludeUnitsDefinition> ProcessIncludeUnits(DefinedType type, IEnumerable<RawIncludeUnitsDefinition> rawDefinitions)
    {
        IncludeUnitsProcessingContext processingContext = new(type);

        return ProcessingFilter.Create(IncludeUnitsProcesser).Filter(processingContext, rawDefinitions).Result;
    }

    public static IReadOnlyList<ExcludeUnitsDefinition> ProcessExcludeUnits(DefinedType type, IEnumerable<RawExcludeUnitsDefinition> rawDefinitions)
    {
        ExcludeUnitsProcessingContext processingContext = new(type);

        return ProcessingFilter.Create(ExcludeUnitsProcesser).Filter(processingContext, rawDefinitions).Result;
    }

    private static DerivedQuantityProcesser DerivedQuantityProcesser { get; } = new(EmptyDerivedQuantityProcessingDiagnostics.Instance);
    private static VectorConstantProcesser ScalarConstantProcesser { get; } = new(EmptyVectorConstantProcessingDiagnostics.Instance);
    private static ConvertibleVectorProcesser ConvertibleScalarProcesser { get; } = new(EmptyConvertibleQuantityProcessingDiagnostics.Instance);

    private static IncludeUnitsProcesser IncludeUnitsProcesser { get; } = new(EmptyIncludeUnitsProcessingDiagnostics.Instance);
    private static ExcludeUnitsProcesser ExcludeUnitsProcesser { get; } = new(EmptyExcludeUnitsProcessingDiagnostics.Instance);
}
