namespace SharpMeasures.Generators.Units.Parsing.Diagnostics.Resolution;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Units.Parsing.DerivedUnit;
using SharpMeasures.Generators.Unresolved.Units;

internal class DerivedUnitResolutionDiagnostics : IDerivedUnitResolutionDiagnostics
{
    public static DerivedUnitResolutionDiagnostics Instance { get; } = new();

    private DerivedUnitResolutionDiagnostics() { }

    public Diagnostic InvalidUnitListLength(IDerivedUnitResolutionContext context, UnresolvedDerivedUnitDefinition definition, UnresolvedUnitDerivationSignature signature)
    {
        return DiagnosticConstruction.IncompatibleDerivedUnitListSize(definition.Locations.UnitsCollection?.AsRoslynLocation(), signature.Count, definition.Units.Count);
    }

    public Diagnostic UnrecognizedUnit(IDerivedUnitResolutionContext context, UnresolvedDerivedUnitDefinition definition, int index, IUnresolvedUnitType unitType)
    {
        return DiagnosticConstruction.UnrecognizedUnitName(definition.Locations.UnitsElements[index].AsRoslynLocation(), definition.Units[index], unitType.Type.Name);
    }

    public Diagnostic UnitNotDerivable(IDerivedUnitResolutionContext context, UnresolvedDerivedUnitDefinition definition)
    {
        return DiagnosticConstruction.UnitNotDerivable(definition.Locations.AttributeName.AsRoslynLocation(), context.Type.Name);
    }

    public Diagnostic AmbiguousSignatureNotSpecified(IDerivedUnitResolutionContext context, UnresolvedDerivedUnitDefinition definition)
    {
        return DiagnosticConstruction.AmbiguousDerivationSignatureNotSpecified(definition.Locations.AttributeName.AsRoslynLocation(), context.Type.Name);
    }

    public Diagnostic UnrecognizedSignatureID(IDerivedUnitResolutionContext context, UnresolvedDerivedUnitDefinition definition)
    {
        return DiagnosticConstruction.UnrecognizedUnitDerivationID(definition.Locations.DerivationID?.AsRoslynLocation(), definition.DerivationID!, context.Type.Name);
    }
}
