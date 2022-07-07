namespace SharpMeasures.Generators.Vectors.Diagnostics;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Quantities.Diagnostics;
using SharpMeasures.Generators.Quantities.Parsing.ConvertibleQuantity;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Vectors.Refinement.DimensionalEquivalence;

internal class DimensionalEquivalenceDiagnostics : ADimensionalEquivalenceProcessingDiagnostics, IDimensionalEquivalenceValidationDiagnostics
{
    public static DimensionalEquivalenceDiagnostics Instance { get; } = new();

    private DimensionalEquivalenceDiagnostics() { }

    public override Diagnostic NullQuantity(IConvertibleQuantityProcessingContext context, RawConvertibleQuantityDefinition definition, int index)
    {
        return DiagnosticConstruction.NullTypeNotVector(definition.Locations.QuantitiesElements[index].AsRoslynLocation());
    }

    public Diagnostic TypeNotVector(IDimensionalEquivalenceValidationContext context, ConvertibleQuantityDefinition definition, int index)
    {
        return DiagnosticConstruction.TypeNotVector(definition.Locations.QuantitiesElements[index].AsRoslynLocation(), definition.Quantities[index].Name);
    }

    public Diagnostic VectorGroupAlreadySpecified(IDimensionalEquivalenceValidationContext context, ConvertibleQuantityDefinition definition, int index)
    {
        return DiagnosticConstruction.VectorGroupAlreadySpecified(definition.Locations.QuantitiesElements[index].AsRoslynLocation(), definition.Quantities[index].Name);
    }
}
