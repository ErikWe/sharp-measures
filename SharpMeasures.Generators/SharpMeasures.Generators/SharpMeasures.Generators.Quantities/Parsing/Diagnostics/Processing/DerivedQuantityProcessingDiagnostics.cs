namespace SharpMeasures.Generators.Quantities.Parsing.Diagnostics.Processing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Quantities.Parsing.DerivedQuantity;

public class DerivedQuantityProcessingDiagnostics : IDerivedQuantityProcessingDiagnostics
{
    public static DerivedQuantityProcessingDiagnostics Instance { get; } = new();

    private DerivedQuantityProcessingDiagnostics() { }

    public Diagnostic NullExpression(IDerivedQuantityProcessingContext context, RawDerivedQuantityDefinition definition)
    {
        return DiagnosticConstruction.NullDerivationExpression(definition.Locations.Expression?.AsRoslynLocation());
    }

    public Diagnostic EmptyExpression(IDerivedQuantityProcessingContext context, RawDerivedQuantityDefinition definition) => NullExpression(context, definition);

    public Diagnostic NullSignature(IDerivedQuantityProcessingContext context, RawDerivedQuantityDefinition definition)
    {
        return DiagnosticConstruction.NullDerivationSignature(definition.Locations.SignatureCollection?.AsRoslynLocation());
    }

    public Diagnostic EmptySignature(IDerivedQuantityProcessingContext context, RawDerivedQuantityDefinition definition)
    {
        return DiagnosticConstruction.EmptyQuantityDerivationSignature(definition.Locations.SignatureCollection?.AsRoslynLocation());
    }

    public Diagnostic NullSignatureElement(IDerivedQuantityProcessingContext context, RawDerivedQuantityDefinition definition, int index)
    {
        return DiagnosticConstruction.NullTypeNotQuantity(definition.Locations.SignatureElements[index].AsRoslynLocation());
    }
}
