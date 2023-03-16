namespace SharpMeasures.Generators.Units.Parsing.Diagnostics.Empty.Validation;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators;
using SharpMeasures.Generators.Units.Parsing.DerivedUnitInstance;

internal sealed class EmptyDerivedUnitInstanceValidationDiagnostics : IDerivedUnitInstanceValidationDiagnostics
{
    public static EmptyDerivedUnitInstanceValidationDiagnostics Instance { get; } = new();

    private EmptyDerivedUnitInstanceValidationDiagnostics() { }

    public Diagnostic? UnitNotDerivable(IDerivedUnitInstanceValidationContext context, DerivedUnitInstanceDefinition definition) => null;
    public Diagnostic? AmbiguousSignatureNotSpecified(IDerivedUnitInstanceValidationContext context, DerivedUnitInstanceDefinition definition) => null;
    public Diagnostic? UnrecognizedDerivationID(IDerivedUnitInstanceValidationContext context, DerivedUnitInstanceDefinition definition) => null;
    public Diagnostic? InvalidUnitListLength(IDerivedUnitInstanceValidationContext context, DerivedUnitInstanceDefinition definition, int signatureLength) => null;
    public Diagnostic? UnrecognizedUnitInstance(IDerivedUnitInstanceValidationContext context, DerivedUnitInstanceDefinition definition, int index, NamedType unitType) => null;
}
