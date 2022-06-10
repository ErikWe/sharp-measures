namespace SharpMeasures.Generators.Vectors.Diagnostics;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Quantities.Diagnostics;
using SharpMeasures.Generators.Quantities.Parsing.DimensionalEquivalence;
using SharpMeasures.Generators.Diagnostics;

internal class DimensionalEquivalenceDiagnostics : ADimensionalEquivalenceProcessingDiagnostics
{
    public static DimensionalEquivalenceDiagnostics Instance { get; } = new();

    private DimensionalEquivalenceDiagnostics() { }

    public override Diagnostic NullQuantity(IDimensionalEquivalenceProcessingContext context, RawDimensionalEquivalenceDefinition definition, int index)
    {
        return DiagnosticConstruction.NullTypeNotVector(definition.Locations.QuantitiesElements[index].AsRoslynLocation());
    }
}
