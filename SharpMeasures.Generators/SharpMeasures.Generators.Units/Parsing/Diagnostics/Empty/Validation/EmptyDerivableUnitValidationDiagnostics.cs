namespace SharpMeasures.Generators.Units.Parsing.Diagnostics.Empty.Validation;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Units.Parsing.DerivableUnit;

internal sealed class EmptyDerivableUnitValidationDiagnostics : IDerivableUnitValidationDiagnostics
{
    public static EmptyDerivableUnitValidationDiagnostics Instance { get; } = new();

    private EmptyDerivableUnitValidationDiagnostics() { }

    Diagnostic? IDerivableUnitValidationDiagnostics.SignatureElementNotUnit(IDerivableUnitValidationContext context, DerivableUnitDefinition definition, int index) => null;
}
