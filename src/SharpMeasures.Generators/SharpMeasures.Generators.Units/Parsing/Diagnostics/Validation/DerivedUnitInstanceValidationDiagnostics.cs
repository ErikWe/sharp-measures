namespace SharpMeasures.Generators.Units.Parsing.Diagnostics.Validation;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Units.Parsing.DerivedUnitInstance;

internal sealed class DerivedUnitInstanceValidationDiagnostics : IDerivedUnitInstanceValidationDiagnostics
{
    public static DerivedUnitInstanceValidationDiagnostics Instance { get; } = new();

    private DerivedUnitInstanceValidationDiagnostics() { }

    public Diagnostic InvalidUnitListLength(IDerivedUnitInstanceValidationContext context, DerivedUnitInstanceDefinition definition, int signatureLength)
    {
        return DiagnosticConstruction.IncompatibleDerivedUnitListSize(definition.Locations.UnitsCollection?.AsRoslynLocation(), signatureLength, definition.Units.Count);
    }

    public Diagnostic UnrecognizedUnitInstance(IDerivedUnitInstanceValidationContext context, DerivedUnitInstanceDefinition definition, int index, NamedType unitType)
    {
        return DiagnosticConstruction.UnrecognizedUnitInstanceName(definition.Locations.UnitsElements[index].AsRoslynLocation(), definition.Units[index], unitType.Name);
    }

    public Diagnostic UnitNotDerivable(IDerivedUnitInstanceValidationContext context, DerivedUnitInstanceDefinition definition)
    {
        return DiagnosticConstruction.UnitNotDerivable(definition.Locations.AttributeName.AsRoslynLocation(), context.Type.Name);
    }

    public Diagnostic AmbiguousSignatureNotSpecified(IDerivedUnitInstanceValidationContext context, DerivedUnitInstanceDefinition definition)
    {
        return DiagnosticConstruction.AmbiguousDerivationSignatureNotSpecified(definition.Locations.AttributeName.AsRoslynLocation(), context.Type.Name);
    }

    public Diagnostic UnrecognizedDerivationID(IDerivedUnitInstanceValidationContext context, DerivedUnitInstanceDefinition definition)
    {
        return DiagnosticConstruction.UnrecognizedUnitDerivationID(definition.Locations.DerivationID?.AsRoslynLocation(), definition.DerivationID!, context.Type.Name);
    }
}
