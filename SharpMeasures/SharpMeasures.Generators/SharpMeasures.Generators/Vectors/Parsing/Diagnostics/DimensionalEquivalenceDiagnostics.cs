namespace SharpMeasures.Generators.Vectors.Parsing.Diagnostics;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing.Quantities;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Quantities.Parsing.Diagnostics;

internal class DimensionalEquivalenceDiagnostics : ADimensionalEquivalenceDiagnostics
{
    public static DimensionalEquivalenceDiagnostics Instance { get; } = new();

    private DimensionalEquivalenceDiagnostics() { }

    public override Diagnostic NullQuantity(IDimensionalEquivalenceProcessingContext context, RawDimensionalEquivalenceDefinition definition, int index)
    {
        return DiagnosticConstruction.TypeNotVector_Null(definition.Locations.QuantitiesElements[index].AsRoslynLocation());
    }
}
