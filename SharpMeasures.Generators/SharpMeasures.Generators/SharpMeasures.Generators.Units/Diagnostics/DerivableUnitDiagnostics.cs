namespace SharpMeasures.Generators.Units.Diagnostics;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Units.Parsing.DerivableUnit;
using SharpMeasures.Generators.Units.Refinement.DerivableUnit;

internal class DerivableUnitDiagnostics : IDerivableUnitProcessingDiagnostics, IDerivableUnitRefinementDiagnostics
{
    public static DerivableUnitDiagnostics Instance { get; } = new();

    private DerivableUnitDiagnostics() { }

    public Diagnostic NullExpression(IDerivableUnitProcessingContext context, RawDerivableUnitDefinition definition)
    {
        return DiagnosticConstruction.NullUnitDerivationExpression(definition.Locations.Expression?.AsRoslynLocation());
    }

    public Diagnostic EmptyExpression(IDerivableUnitProcessingContext context, RawDerivableUnitDefinition definition) => NullExpression(context, definition);

    public Diagnostic EmptySignature(IDerivableUnitProcessingContext context, RawDerivableUnitDefinition definition)
    {
        return DiagnosticConstruction.EmptyUnitDerivationSignature(definition.Locations.SignatureCollection?.AsRoslynLocation());
    }

    public Diagnostic NullSignatureElement(IDerivableUnitProcessingContext context, RawDerivableUnitDefinition definition, int index)
    {
        return DiagnosticConstruction.NullTypeNotUnit(definition.Locations.SignatureElements[index].AsRoslynLocation());
    }

    public Diagnostic DuplicateSignature(IDerivableUnitProcessingContext context, RawDerivableUnitDefinition definition)
    {
        return DiagnosticConstruction.DuplicateUnitDerivationSignature(definition.Locations.SignatureCollection?.AsRoslynLocation(), context.Type.Name);
    }

    public Diagnostic SignatureElementNotUnit(IDerivableUnitRefinementContext context, DerivableUnitDefinition definition, int index)
    {
        return DiagnosticConstruction.TypeNotUnit(definition.Locations.SignatureElements[index].AsRoslynLocation(), definition.Signature[index].Name);
    }
}
