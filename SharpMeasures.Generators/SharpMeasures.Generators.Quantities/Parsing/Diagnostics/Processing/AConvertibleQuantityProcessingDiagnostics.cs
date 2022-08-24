namespace SharpMeasures.Generators.Quantities.Parsing.Diagnostics.Processing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Quantities.Parsing.ConvertibleQuantity;

public abstract class AConvertibleQuantityProcessingDiagnostics : IConvertibleQuantityProcessingDiagnostics
{
    public Diagnostic UnrecognizedCastOperatorBehaviour(IConvertibleQuantityProcessingContext context, RawConvertibleQuantityDefinition definition)
    {
        return DiagnosticConstruction.UnrecognizedConversionOperationBehaviour(definition.Locations.CastOperatorBehaviour?.AsRoslynLocation(),
            definition.CastOperatorBehaviour);
    }

    public Diagnostic EmptyQuantityList(IConvertibleQuantityProcessingContext context, RawConvertibleQuantityDefinition definition)
    {
        if (definition.Locations.ExplicitlySetQuantities)
        {
            return DiagnosticConstruction.EmptyQuantityList(definition.Locations.QuantitiesCollection?.AsRoslynLocation());
        }

        return DiagnosticConstruction.EmptyQuantityList(definition.Locations.Attribute.AsRoslynLocation());
    }

    public Diagnostic DuplicateQuantity(IConvertibleQuantityProcessingContext context, RawConvertibleQuantityDefinition definition, int index)
    {
        return DiagnosticConstruction.DuplicateQuantityListing(definition.Locations.QuantitiesElements[index].AsRoslynLocation(), definition.Quantities[index]!.Value.Name);
    }

    public abstract Diagnostic? NullQuantity(IConvertibleQuantityProcessingContext context, RawConvertibleQuantityDefinition definition, int index);

    public Diagnostic ConvertibleToSelf(IConvertibleQuantityProcessingContext context, RawConvertibleQuantityDefinition definition, int index)
    {
        return DiagnosticConstruction.QuantityConvertibleToSelf(definition.Locations.QuantitiesElements[index].AsRoslynLocation(), definition.Quantities[index]!.Value.Name);
    }
}
