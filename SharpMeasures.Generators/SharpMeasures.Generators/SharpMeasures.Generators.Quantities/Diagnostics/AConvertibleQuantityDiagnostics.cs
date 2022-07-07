namespace SharpMeasures.Generators.Quantities.Diagnostics;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Quantities.Parsing.ConvertibleQuantity;

public abstract class AConvertibleQuantityDiagnostics : IConvertibleQuantityProcessingDiagnostics
{
    public Diagnostic UnrecognizedCastOperatorBehaviour(IConvertibleQuantityProcessingContext context, RawConvertibleQuantityDefinition definition)
    {
        return DiagnosticConstruction.UnrecognizedConversionOperationBehaviour(definition.Locations.CastOperatorBehaviour?.AsRoslynLocation(),
            definition.CastOperatorBehaviour);
    }

    public Diagnostic EmptyQuantityList(IConvertibleQuantityProcessingContext context, RawConvertibleQuantityDefinition definition)
    {
        return DiagnosticConstruction.EmptyQuantityList(definition.Locations.QuantitiesCollection?.AsRoslynLocation());
    }

    public Diagnostic DuplicateQuantity(IConvertibleQuantityProcessingContext context, RawConvertibleQuantityDefinition definition, int index)
    {
        return DiagnosticConstruction.DuplicateQuantityListing(definition.Locations.QuantitiesElements[index].AsRoslynLocation(), definition.Quantities[index]?.Name!);
    }

    public abstract Diagnostic? NullQuantity(IConvertibleQuantityProcessingContext context, RawConvertibleQuantityDefinition definition, int index);
}
