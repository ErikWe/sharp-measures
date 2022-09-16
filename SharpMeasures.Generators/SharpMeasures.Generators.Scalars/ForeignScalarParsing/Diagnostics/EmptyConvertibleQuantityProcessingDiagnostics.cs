namespace SharpMeasures.Generators.Scalars.ForeignScalarParsing.Diagnostics;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Quantities.Parsing.ConvertibleQuantity;

internal class EmptyConvertibleQuantityProcessingDiagnostics : IConvertibleQuantityProcessingDiagnostics
{
    public static EmptyConvertibleQuantityProcessingDiagnostics Instance { get; } = new();

    Diagnostic? IConvertibleQuantityProcessingDiagnostics.ConvertibleToSelf(IConvertibleQuantityProcessingContext context, RawConvertibleQuantityDefinition definition, int index) => null;
    Diagnostic? IConvertibleQuantityProcessingDiagnostics.DuplicateQuantity(IConvertibleQuantityProcessingContext context, RawConvertibleQuantityDefinition definition, int index) => null;
    Diagnostic? IConvertibleQuantityProcessingDiagnostics.EmptyQuantityList(IConvertibleQuantityProcessingContext context, RawConvertibleQuantityDefinition definition) => null;
    Diagnostic? IConvertibleQuantityProcessingDiagnostics.NullQuantity(IConvertibleQuantityProcessingContext context, RawConvertibleQuantityDefinition definition, int index) => null;
    Diagnostic? IConvertibleQuantityProcessingDiagnostics.UnrecognizedCastOperatorBehaviour(IConvertibleQuantityProcessingContext context, RawConvertibleQuantityDefinition definition) => null;
    Diagnostic? IConvertibleQuantityProcessingDiagnostics.UnrecognizedConversionDirection(IConvertibleQuantityProcessingContext context, RawConvertibleQuantityDefinition definition) => null;
}
