namespace SharpMeasures.Generators.Vectors.Diagnostics;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Quantities.Diagnostics;
using SharpMeasures.Generators.Quantities.Parsing.DimensionalEquivalence;
using SharpMeasures.Generators.Vectors.Refinement;
using SharpMeasures.Generators.Diagnostics;

internal class DimensionalEquivalenceDiagnostics : ADimensionalEquivalenceProcessingDiagnostics, IDimensionalEquivalenceValidationDiagnostics
{
    public static DimensionalEquivalenceDiagnostics Instance { get; } = new();

    private DimensionalEquivalenceDiagnostics() { }

    public override Diagnostic NullQuantity(IDimensionalEquivalenceProcessingContext context, RawDimensionalEquivalenceDefinition definition, int index)
    {
        return DiagnosticConstruction.NullTypeNotVector(definition.Locations.QuantitiesElements[index].AsRoslynLocation());
    }

    public Diagnostic TypeNotVector(IDimensionalEquivalenceValidationContext context, DimensionalEquivalenceDefinition definition, int index)
    {
        return DiagnosticConstruction.TypeNotVector(definition.Locations.QuantitiesElements[index].AsRoslynLocation(), definition.Quantities[index].Name);
    }

    public Diagnostic VectorGroupAlreadySpecified(IDimensionalEquivalenceValidationContext context, DimensionalEquivalenceDefinition definition, int index)
    {
        return DiagnosticConstruction.VectorGroupAlreadySpecified(definition.Locations.QuantitiesElements[index].AsRoslynLocation(), definition.Quantities[index].Name);
    }
}
