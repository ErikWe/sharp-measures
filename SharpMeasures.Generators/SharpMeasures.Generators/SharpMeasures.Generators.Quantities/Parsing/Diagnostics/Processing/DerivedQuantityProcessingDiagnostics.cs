namespace SharpMeasures.Generators.Quantities.Parsing.Diagnostics.Processing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Quantities.Parsing.DerivedQuantity;

public class DerivedQuantityProcessingDiagnostics : IDerivedQuantityProcessingDiagnostics
{
    public static DerivedQuantityProcessingDiagnostics Instance { get; } = new();

    private DerivedQuantityProcessingDiagnostics() { }

    public Diagnostic NullExpression(IDerivedQuantityProcessingContext context, UnprocessedDerivedQuantityDefinition definition)
    {
        return DiagnosticConstruction.NullDerivationExpression(definition.Locations.Expression?.AsRoslynLocation());
    }

    public Diagnostic EmptyExpression(IDerivedQuantityProcessingContext context, UnprocessedDerivedQuantityDefinition definition) => NullExpression(context, definition);

    public Diagnostic NullSignature(IDerivedQuantityProcessingContext context, UnprocessedDerivedQuantityDefinition definition)
    {
        return DiagnosticConstruction.NullDerivationSignature(definition.Locations.SignatureCollection?.AsRoslynLocation());
    }

    public Diagnostic EmptySignature(IDerivedQuantityProcessingContext context, UnprocessedDerivedQuantityDefinition definition)
    {
        return DiagnosticConstruction.EmptyQuantityDerivationSignature(definition.Locations.SignatureCollection?.AsRoslynLocation());
    }

    public Diagnostic NullSignatureElement(IDerivedQuantityProcessingContext context, UnprocessedDerivedQuantityDefinition definition, int index)
    {
        return DiagnosticConstruction.NullTypeNotQuantity(definition.Locations.SignatureElements[index].AsRoslynLocation());
    }
}
