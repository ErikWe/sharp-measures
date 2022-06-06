namespace SharpMeasures.Generators.Scalars.Processing.Diagnostics;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing.Quantities;
using SharpMeasures.Generators.Diagnostics;

internal static class DimensionalEquivalenceDiagnostics
{
    public static Diagnostic TypeNotScalar(DimensionalEquivalence input, int index)
    {
        return DiagnosticConstruction.TypeNotScalar(input.Locations.QuantitiesElements[index].AsRoslynLocation(), input.Quantities[index].Name);
    }

    public static Diagnostic QuantityNotUnbiased(DimensionalEquivalence input, int index)
    {
        return DiagnosticConstruction.ScalarNotUnbiased(input.Locations.QuantitiesElements[index].AsRoslynLocation(), input.Quantities[index].Name);
    }

    public static Diagnostic QuantityNotBiased(DimensionalEquivalence input, int index)
    {
        return DiagnosticConstruction.ScalarNotBiased(input.Locations.QuantitiesElements[index].AsRoslynLocation(), input.Quantities[index].Name);
    }
}
