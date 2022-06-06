namespace SharpMeasures.Generators.Units.Parsing.Diagnostics;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing.Units;
using SharpMeasures.Generators.Diagnostics;

internal class DerivableUnitDiagnostics : IDerivableUnitDiagnostics
{
    public static DerivableUnitDiagnostics Instance { get; } = new();

    private DerivableUnitDiagnostics() { }

    public Diagnostic NullExpression(IDerivableUnitProcessingContext context, RawDerivableUnit definition)
    {
        return DiagnosticConstruction.InvalidUnitDerivationExpression_Null(definition.Locations.Expression?.AsRoslynLocation());
    }

    public Diagnostic EmptyExpression(IDerivableUnitProcessingContext context, RawDerivableUnit definition) => NullExpression(context, definition);

    public Diagnostic EmptySignature(IDerivableUnitProcessingContext context, RawDerivableUnit definition)
    {
        return DiagnosticConstruction.EmptyUnitDerivationSignature(definition.Locations.SignatureCollection?.AsRoslynLocation());
    }

    public Diagnostic NullSignatureElement(IDerivableUnitProcessingContext context, RawDerivableUnit definition, int index)
    {
        return DiagnosticConstruction.TypeNotUnit_Null(definition.Locations.SignatureElements[index].AsRoslynLocation());
    }

    public Diagnostic DuplicateSignature(IDerivableUnitProcessingContext context, RawDerivableUnit definition)
    {
        return DiagnosticConstruction.DuplicateUnitDerivationSignature(definition.Locations.SignatureCollection?.AsRoslynLocation(), context.Type.Name);
    }
}
