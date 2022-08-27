namespace SharpMeasures.Generators.Units.Parsing.Diagnostics.Validation;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Units.Parsing.DerivedUnit;

internal class DerivedUnitValidationDiagnostics : IDerivedUnitValidationDiagnostics
{
    public static DerivedUnitValidationDiagnostics Instance { get; } = new();

    private DerivedUnitValidationDiagnostics() { }

    public Diagnostic InvalidUnitListLength(IDerivedUnitValidationContext context, DerivedUnitDefinition definition, int signatureLength)
    {
        return DiagnosticConstruction.IncompatibleDerivedUnitListSize(definition.Locations.UnitsCollection?.AsRoslynLocation(), signatureLength, definition.Units.Count);
    }

    public Diagnostic UnrecognizedUnit(IDerivedUnitValidationContext context, DerivedUnitDefinition definition, int index, NamedType unitType)
    {
        return DiagnosticConstruction.UnrecognizedUnitName(definition.Locations.UnitsElements[index].AsRoslynLocation(), definition.Units[index], unitType.Name);
    }

    public Diagnostic UnitNotDerivable(IDerivedUnitValidationContext context, DerivedUnitDefinition definition)
    {
        return DiagnosticConstruction.UnitNotDerivable(definition.Locations.AttributeName.AsRoslynLocation(), context.Type.Name);
    }

    public Diagnostic AmbiguousSignatureNotSpecified(IDerivedUnitValidationContext context, DerivedUnitDefinition definition)
    {
        return DiagnosticConstruction.AmbiguousDerivationSignatureNotSpecified(definition.Locations.AttributeName.AsRoslynLocation(), context.Type.Name);
    }

    public Diagnostic UnrecognizedDerivationID(IDerivedUnitValidationContext context, DerivedUnitDefinition definition)
    {
        return DiagnosticConstruction.UnrecognizedUnitDerivationID(definition.Locations.DerivationID?.AsRoslynLocation(), definition.DerivationID!, context.Type.Name);
    }
}
