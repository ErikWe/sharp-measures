namespace SharpMeasures.Generators.Units.Parsing.Diagnostics;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing.Units;
using SharpMeasures.Generators.Diagnostics;

internal class DerivedUnitDiagnostics : AUnitDiagnostics<RawDerivedUnit>, IDerivedUnitDiagnostics
{
    public static DerivedUnitDiagnostics Instance { get; } = new();

    private DerivedUnitDiagnostics() { }

    public Diagnostic EmptySignature(IDerivedUnitProcessingContext context, RawDerivedUnit definition)
    {
        return DiagnosticConstruction.EmptyUnitDerivationSignature(definition.Locations.SignatureCollection?.AsRoslynLocation());
    }

    public Diagnostic IncompatibleSignatureAndUnitLists(IDerivedUnitProcessingContext context, RawDerivedUnit definition)
    {
        return DiagnosticConstruction.UnitListNotMatchingSignature(definition.Locations.Attribute.AsRoslynLocation(),
            definition.Signature.Count, definition.Units.Count);
    }

    public Diagnostic UnrecognizedSignature(IDerivedUnitProcessingContext context, RawDerivedUnit definition)
    {
        return DiagnosticConstruction.UnrecognizedUnitDerivationSignature(definition.Locations.SignatureCollection?.AsRoslynLocation(), context.Type.Name);
    }

    public Diagnostic NullSignatureElement(IDerivedUnitProcessingContext context, RawDerivedUnit definition, int index)
    {
        return DiagnosticConstruction.TypeNotUnit_Null(definition.Locations.SignatureElements[index].AsRoslynLocation());
    }

    public Diagnostic NullUnitElement(IDerivedUnitProcessingContext context, RawDerivedUnit definition, int index)
    {
        return DiagnosticConstruction.TypeNotUnit_Null(definition.Locations.UnitsElements[index].AsRoslynLocation());
    }

    public Diagnostic EmptyUnitElement(IDerivedUnitProcessingContext context, RawDerivedUnit definition, int index)
        => NullUnitElement(context, definition, index);
}
