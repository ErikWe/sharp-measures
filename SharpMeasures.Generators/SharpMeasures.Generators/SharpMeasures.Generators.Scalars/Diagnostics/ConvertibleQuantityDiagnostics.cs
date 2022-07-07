namespace SharpMeasures.Generators.Scalars.Diagnostics;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Quantities.Diagnostics;
using SharpMeasures.Generators.Quantities.Parsing.ConvertibleQuantity;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Scalars.Refinement.DimensionalEquivalence;

internal class ConvertibleQuantityDiagnostics : AConvertibleQuantityDiagnostics, IConvertibleQuantityRefinementDiagnostics
{
    public static ConvertibleQuantityDiagnostics Instance { get; } = new();

    private ConvertibleQuantityDiagnostics() { }

    public override Diagnostic NullQuantity(IConvertibleQuantityProcessingContext context, RawConvertibleQuantityDefinition definition, int index)
    {
        return DiagnosticConstruction.NullTypeNotScalar(definition.Locations.QuantitiesElements[index].AsRoslynLocation());
    }

    public Diagnostic TypeNotScalar(IConvertibleQuantityRefinementContext context, ConvertibleQuantityDefinition definition, int index)
    {
        return DiagnosticConstruction.TypeNotScalar(definition.Locations.QuantitiesElements[index].AsRoslynLocation(), definition.Quantities[index].Name);
    }

    public Diagnostic ScalarNotUnbiased(IConvertibleQuantityRefinementContext context, ConvertibleQuantityDefinition definition, int index)
    {
        return DiagnosticConstruction.TypeNotUnbiasedScalar(definition.Locations.QuantitiesElements[index].AsRoslynLocation(), definition.Quantities[index].Name);
    }

    public Diagnostic ScalarNotBiased(IConvertibleQuantityRefinementContext context, ConvertibleQuantityDefinition definition, int index)
    {
        return DiagnosticConstruction.TypeNotBiasedScalar(definition.Locations.QuantitiesElements[index].AsRoslynLocation(), definition.Quantities[index].Name);
    }
}
