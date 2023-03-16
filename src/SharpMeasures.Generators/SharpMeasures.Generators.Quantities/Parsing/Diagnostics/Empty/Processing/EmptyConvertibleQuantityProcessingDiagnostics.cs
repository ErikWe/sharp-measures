namespace SharpMeasures.Generators.Quantities.Parsing.Diagnostics.Empty.Processing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Quantities.Parsing.ConvertibleQuantity;

public sealed class EmptyConvertibleQuantityProcessingDiagnostics : IConvertibleQuantityProcessingDiagnostics
{
    public static EmptyConvertibleQuantityProcessingDiagnostics Instance { get; } = new();

    private EmptyConvertibleQuantityProcessingDiagnostics() { }

    public Diagnostic? ConvertibleToSelf(IConvertibleQuantityProcessingContext context, RawConvertibleQuantityDefinition definition, int index) => null;
    public Diagnostic? DuplicateQuantity(IConvertibleQuantityProcessingContext context, RawConvertibleQuantityDefinition definition, int index) => null;
    public Diagnostic? EmptyQuantityList(IConvertibleQuantityProcessingContext context, RawConvertibleQuantityDefinition definition) => null;
    public Diagnostic? NullQuantity(IConvertibleQuantityProcessingContext context, RawConvertibleQuantityDefinition definition, int index) => null;
    public Diagnostic? UnrecognizedCastOperatorBehaviour(IConvertibleQuantityProcessingContext context, RawConvertibleQuantityDefinition definition) => null;
    public Diagnostic? UnrecognizedConversionDirection(IConvertibleQuantityProcessingContext context, RawConvertibleQuantityDefinition definition) => null;
}
