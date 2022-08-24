namespace SharpMeasures.Generators.Scalars.Parsing.Diagnostics.Processing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Quantities.Parsing.ConvertibleQuantity;
using SharpMeasures.Generators.Quantities.Parsing.Diagnostics.Processing;

internal class ConvertibleScalarProcessingDiagnostics : AConvertibleQuantityProcessingDiagnostics
{
    public static ConvertibleScalarProcessingDiagnostics Instance { get; } = new();

    private ConvertibleScalarProcessingDiagnostics() { }

    public override Diagnostic NullQuantity(IConvertibleQuantityProcessingContext context, RawConvertibleQuantityDefinition definition, int index)
    {
        return DiagnosticConstruction.NullTypeNotScalar(definition.Locations.QuantitiesElements[index].AsRoslynLocation());
    }
}
