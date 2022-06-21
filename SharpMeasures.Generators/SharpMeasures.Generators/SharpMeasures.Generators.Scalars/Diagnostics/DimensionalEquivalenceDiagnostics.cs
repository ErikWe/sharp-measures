namespace SharpMeasures.Generators.Scalars.Diagnostics;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Quantities.Diagnostics;
using SharpMeasures.Generators.Quantities.Parsing.DimensionalEquivalence;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Scalars.Refinement.DimensionalEquivalence;

internal class DimensionalEquivalenceDiagnostics : ADimensionalEquivalenceProcessingDiagnostics, IDimensionalEquivalenceRefinementDiagnostics
{
    public static DimensionalEquivalenceDiagnostics Instance { get; } = new();

    private DimensionalEquivalenceDiagnostics() { }

    public override Diagnostic NullQuantity(IDimensionalEquivalenceProcessingContext context, RawDimensionalEquivalenceDefinition definition, int index)
    {
        return DiagnosticConstruction.NullTypeNotScalar(definition.Locations.QuantitiesElements[index].AsRoslynLocation());
    }

    public Diagnostic TypeNotScalar(IDimensionalEquivalenceRefinementContext context, DimensionalEquivalenceDefinition definition, int index)
    {
        return DiagnosticConstruction.TypeNotScalar(definition.Locations.QuantitiesElements[index].AsRoslynLocation(), definition.Quantities[index].Name);
    }

    public Diagnostic ScalarNotUnbiased(IDimensionalEquivalenceRefinementContext context, DimensionalEquivalenceDefinition definition, int index)
    {
        return DiagnosticConstruction.TypeNotUnbiasedScalar(definition.Locations.QuantitiesElements[index].AsRoslynLocation(), definition.Quantities[index].Name);
    }

    public Diagnostic ScalarNotBiased(IDimensionalEquivalenceRefinementContext context, DimensionalEquivalenceDefinition definition, int index)
    {
        return DiagnosticConstruction.TypeNotBiasedScalar(definition.Locations.QuantitiesElements[index].AsRoslynLocation(), definition.Quantities[index].Name);
    }
}
