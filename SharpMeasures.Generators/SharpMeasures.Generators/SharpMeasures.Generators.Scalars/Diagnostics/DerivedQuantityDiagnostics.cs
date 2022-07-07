namespace SharpMeasures.Generators.Scalars.Diagnostics;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Quantities.Diagnostics;
using SharpMeasures.Generators.Quantities.Parsing.DerivedQuantity;

internal class DerivedQuantityDiagnostics : ADerivedQuantityDiagnostics
{
    public static DerivedQuantityDiagnostics Instance { get; } = new();

    public override Diagnostic NullSignatureElement(IDerivedQuantityProcessingContext context, RawDerivedQuantityDefinition definition, int index)
    {
        return DiagnosticConstruction.NullTypeNotScalar(definition.Locations.SignatureElements[index].AsRoslynLocation());
    }
}
