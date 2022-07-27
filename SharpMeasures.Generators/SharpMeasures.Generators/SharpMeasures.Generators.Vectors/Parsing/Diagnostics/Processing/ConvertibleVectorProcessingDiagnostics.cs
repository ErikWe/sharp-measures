namespace SharpMeasures.Generators.Vectors.Parsing.Diagnostics.Processing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Quantities.Parsing.ConvertibleQuantity;
using SharpMeasures.Generators.Quantities.Parsing.Diagnostics.Processing;

internal class ConvertibleVectorProcessingDiagnostics : AConvertibleQuantityProcessingDiagnostics
{
    public static ConvertibleVectorProcessingDiagnostics Instance { get; } = new();

    private ConvertibleVectorProcessingDiagnostics() { }

    public override Diagnostic NullQuantity(IConvertibleQuantityProcessingContext context, RawConvertibleQuantityDefinition definition, int index)
    {
        return DiagnosticConstruction.NullTypeNotVector(definition.Locations.QuantitiesElements[index].AsRoslynLocation());
    }
}
