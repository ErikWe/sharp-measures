namespace SharpMeasures.Generators.Units.Diagnostics;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Units.Parsing.DerivedUnit;
using SharpMeasures.Generators.Units.Refinement.DerivedUnit;

internal class DerivedUnitDiagnostics : AUnitDiagnostics<RawDerivedUnitDefinition>, IDerivedUnitProcessingDiagnostics, IDerivedUnitValidatorDiagnostics
{
    public static DerivedUnitDiagnostics Instance { get; } = new();

    private DerivedUnitDiagnostics() { }

    public Diagnostic EmptySignature(IDerivedUnitProcessingContext context, RawDerivedUnitDefinition definition)
    {
        return DiagnosticConstruction.EmptyUnitDerivationSignature(definition.Locations.SignatureCollection?.AsRoslynLocation());
    }

    public Diagnostic IncompatibleSignatureAndUnitLists(IDerivedUnitProcessingContext context, RawDerivedUnitDefinition definition)
    {
        return DiagnosticConstruction.UnitListNotMatchingSignature(definition.Locations.Attribute.AsRoslynLocation(),
            definition.Signature.Count, definition.Units.Count);
    }

    public Diagnostic UnrecognizedSignature(IDerivedUnitProcessingContext context, RawDerivedUnitDefinition definition)
    {
        return DiagnosticConstruction.UnrecognizedUnitDerivationSignature(definition.Locations.SignatureCollection?.AsRoslynLocation(), context.Type.Name);
    }

    public Diagnostic NullSignatureElement(IDerivedUnitProcessingContext context, RawDerivedUnitDefinition definition, int index)
    {
        return DiagnosticConstruction.NullTypeNotUnit(definition.Locations.SignatureElements[index].AsRoslynLocation());
    }

    public Diagnostic NullUnitElement(IDerivedUnitProcessingContext context, RawDerivedUnitDefinition definition, int index)
    {
        return DiagnosticConstruction.NullUnrecognizedUnitName(definition.Locations.UnitsElements[index].AsRoslynLocation(), definition.Signature[index]!.Value.Name);
    }

    public Diagnostic EmptyUnitElement(IDerivedUnitProcessingContext context, RawDerivedUnitDefinition definition, int index)
    {
        return NullUnitElement(context, definition, index);
    }

    public Diagnostic UnrecognizedUnit(IDerivedUnitValidatorContext context, DerivedUnitDefinition definition, int index)
    {
        return DiagnosticConstruction.UnrecognizedUnitName(definition.Locations.UnitsElements[index].AsRoslynLocation(), definition.Units[index],
            definition.Signature[index].Name);
    }
}
