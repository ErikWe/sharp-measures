namespace SharpMeasures.Generators.Scalars.Parsing.Diagnostics;

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
        return DiagnosticConstruction.TypeNotScalar_Null(definition.Locations.QuantitiesElements[index].AsRoslynLocation());
    }
}
