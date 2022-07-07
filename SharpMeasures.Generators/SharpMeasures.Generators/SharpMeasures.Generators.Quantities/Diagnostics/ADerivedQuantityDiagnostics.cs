namespace SharpMeasures.Generators.Quantities.Diagnostics;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Quantities.Parsing.DerivedQuantity;

public abstract class ADerivedQuantityDiagnostics : IDerivedQuantityProcessingDiagnostics
{
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

    public abstract Diagnostic NullSignatureElement(IDerivedQuantityProcessingContext context, RawDerivedQuantityDefinition definition, int index);
}
