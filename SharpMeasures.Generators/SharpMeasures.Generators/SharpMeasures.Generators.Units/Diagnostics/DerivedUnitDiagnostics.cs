namespace SharpMeasures.Generators.Units.Diagnostics;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Units.Parsing.DerivedUnit;
using SharpMeasures.Generators.Units.Refinement.DerivedUnit;

internal class DerivedUnitDiagnostics : AUnitDiagnostics<RawDerivedUnitDefinition>, IDerivedUnitProcessingDiagnostics, IDerivedUnitValidatorDiagnostics
{
    public static DerivedUnitDiagnostics Instance { get; } = new();

    private DerivedUnitDiagnostics() { }

    public Diagnostic UnitNotDerivable(IDerivedUnitProcessingContext context, RawDerivedUnitDefinition definition)
    {
        return DiagnosticConstruction.UnitNotDerivable(definition.Locations.AttributeName.AsRoslynLocation(), context.Type.Name);
    }

    public Diagnostic AmbiguousSignatureNotSpecified(IDerivedUnitProcessingContext context, RawDerivedUnitDefinition definition)
    {
        return DiagnosticConstruction.AmbiguousDerivationSignatureNotSpecified(definition.Locations.AttributeName.AsRoslynLocation(), context.Type.Name);
    }

    public Diagnostic InvalidUnitListLength(IDerivedUnitProcessingContext context, RawDerivedUnitDefinition definition, DerivableSignature signature)
    {
        return DiagnosticConstruction.IncompatibleDerivedUnitListSize(definition.Locations.UnitsCollection?.AsRoslynLocation(), signature.Count,
            definition.Units.Count);
    }

    public Diagnostic UnrecognizedSignatureID(IDerivedUnitProcessingContext context, RawDerivedUnitDefinition definition)
    {
        return DiagnosticConstruction.UnrecognizedUnitDerivationID(definition.Locations.SignatureID?.AsRoslynLocation(), definition.SignatureID!, context.Type.Name);
    }

    public Diagnostic NullUnitElement(IDerivedUnitProcessingContext context, RawDerivedUnitDefinition definition, int index, DerivableSignature signature)
    {
        return DiagnosticConstruction.NullUnrecognizedUnitName(definition.Locations.UnitsElements[index].AsRoslynLocation(),
            signature[index].Name);
    }

    public Diagnostic EmptyUnitElement(IDerivedUnitProcessingContext context, RawDerivedUnitDefinition definition, int index, DerivableSignature signature)
        => NullUnitElement(context, definition, index, signature);

    public Diagnostic UnrecognizedUnit(IDerivedUnitValidatorContext context, DerivedUnitDefinition definition, int index)
    {
        return DiagnosticConstruction.UnrecognizedUnitName(definition.Locations.UnitsElements[index].AsRoslynLocation(), definition.Units[index],
            definition.Signature[index].Name);
    }
}
