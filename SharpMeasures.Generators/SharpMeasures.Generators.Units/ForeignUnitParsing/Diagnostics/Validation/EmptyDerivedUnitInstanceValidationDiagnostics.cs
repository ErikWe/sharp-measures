namespace SharpMeasures.Generators.Units.ForeignUnitParsing.Diagnostics.Validation;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators;
using SharpMeasures.Generators.Units.Parsing.DerivedUnitInstance;

internal sealed class EmptyDerivedUnitInstanceValidationDiagnostics : IDerivedUnitInstanceValidationDiagnostics
{
    public static EmptyDerivedUnitInstanceValidationDiagnostics Instance { get; } = new();

    private EmptyDerivedUnitInstanceValidationDiagnostics() { }

    Diagnostic? IDerivedUnitInstanceValidationDiagnostics.UnitNotDerivable(IDerivedUnitInstanceValidationContext context, DerivedUnitInstanceDefinition definition) => null;
    Diagnostic? IDerivedUnitInstanceValidationDiagnostics.AmbiguousSignatureNotSpecified(IDerivedUnitInstanceValidationContext context, DerivedUnitInstanceDefinition definition) => null;
    Diagnostic? IDerivedUnitInstanceValidationDiagnostics.UnrecognizedDerivationID(IDerivedUnitInstanceValidationContext context, DerivedUnitInstanceDefinition definition) => null;
    Diagnostic? IDerivedUnitInstanceValidationDiagnostics.InvalidUnitListLength(IDerivedUnitInstanceValidationContext context, DerivedUnitInstanceDefinition definition, int signatureLength) => null;
    Diagnostic? IDerivedUnitInstanceValidationDiagnostics.UnrecognizedUnitInstance(IDerivedUnitInstanceValidationContext context, DerivedUnitInstanceDefinition definition, int index, NamedType unitType) => null;
}
