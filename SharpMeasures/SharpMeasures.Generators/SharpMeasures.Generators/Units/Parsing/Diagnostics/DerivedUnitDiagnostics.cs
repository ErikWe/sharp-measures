namespace SharpMeasures.Generators.Units.Parsing.Diagnostics;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing.Units;
using SharpMeasures.Generators.Diagnostics;

internal class DerivedUnitDiagnostics : AUnitDiagnostics<DerivedUnitDefinition>, IDerivedUnitDiagnostics
{
    public static DerivedUnitDiagnostics Instance { get; } = new();

    private DerivedUnitDiagnostics() { }

    public Diagnostic EmptySignature(IDerivedUnitValidatorContext context, DerivedUnitDefinition definition)
    {
        return DiagnosticConstruction.EmptyUnitDerivationSignature(definition.ParsingData.Locations.SignatureCollection.AsRoslynLocation());
    }

    public Diagnostic IncompatibleSignatureAndUnitLists(IDerivedUnitValidatorContext context, DerivedUnitDefinition definition)
    {
        return DiagnosticConstruction.UnitListNotMatchingSignature(definition.ParsingData.Locations.Attribute.AsRoslynLocation(),
            definition.Signature.Count, definition.Units.Count);
    }

    public Diagnostic UnrecognizedSignature(IDerivedUnitValidatorContext context, DerivedUnitDefinition definition)
    {
        return DiagnosticConstruction.UnrecognizedUnitDerivationSignature(definition.ParsingData.Locations.SignatureCollection.AsRoslynLocation(), context.Type.Name);
    }

    public Diagnostic SignatureElementNull(IDerivedUnitValidatorContext context, DerivedUnitDefinition definition, int index)
    {
        return DiagnosticConstruction.TypeNotUnit_Null(definition.ParsingData.Locations.SignatureElements[index].AsRoslynLocation());
    }

    public Diagnostic UnitElementNullOrEmpty(IDerivedUnitValidatorContext context, DerivedUnitDefinition definition, int index)
    {
        return DiagnosticConstruction.TypeNotUnit_Null(definition.ParsingData.Locations.UnitsElements[index].AsRoslynLocation());
    }
}
