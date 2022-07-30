namespace SharpMeasures.Generators.Units.Parsing.Diagnostics.Processing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Units.Parsing.DerivableUnit;

internal class DerivableUnitProcessingDiagnostics : IDerivableUnitProcessingDiagnostics
{
    public static DerivableUnitProcessingDiagnostics Instance { get; } = new();

    private DerivableUnitProcessingDiagnostics() { }

    public Diagnostic MultipleDerivationsButNotNamed(IDerivableUnitProcessingContext context, RawDerivableUnitDefinition definition)
    {
        return DiagnosticConstruction.MultipleDerivationSignaturesButNotNamed(definition.Locations.AttributeName.AsRoslynLocation(), context.Type.Name);
    }

    public Diagnostic DuplicateDerivationID(IDerivableUnitProcessingContext context, RawDerivableUnitDefinition definition)
    {
        return DiagnosticConstruction.DuplicateUnitDerivationID(definition.Locations.DerivationID?.AsRoslynLocation(), definition.DerivationID!, context.Type.Name);
    }

    public Diagnostic NullExpression(IDerivableUnitProcessingContext context, RawDerivableUnitDefinition definition)
    {
        return DiagnosticConstruction.NullDerivationExpression(definition.Locations.Expression?.AsRoslynLocation());
    }

    public Diagnostic EmptyExpression(IDerivableUnitProcessingContext context, RawDerivableUnitDefinition definition) => NullExpression(context, definition);

    public Diagnostic NullSignature(IDerivableUnitProcessingContext context, RawDerivableUnitDefinition definition)
    {
        return DiagnosticConstruction.NullDerivationSignature(definition.Locations.SignatureCollection?.AsRoslynLocation());
    }

    public Diagnostic EmptySignature(IDerivableUnitProcessingContext context, RawDerivableUnitDefinition definition)
    {
        return DiagnosticConstruction.EmptyUnitDerivationSignature(definition.Locations.SignatureCollection?.AsRoslynLocation());
    }

    public Diagnostic NullSignatureElement(IDerivableUnitProcessingContext context, RawDerivableUnitDefinition definition, int index)
    {
        return DiagnosticConstruction.NullTypeNotUnit(definition.Locations.SignatureElements[index].AsRoslynLocation());
    }
}
