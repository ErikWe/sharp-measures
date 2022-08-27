namespace SharpMeasures.Generators.Units.Parsing.Diagnostics.Validation;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Units.Parsing.DerivableUnit;

internal class DerivableUnitValidationDiagnostics : IDerivableUnitValidationDiagnostics
{
    public static DerivableUnitValidationDiagnostics Instance { get; } = new();

    private DerivableUnitValidationDiagnostics() { }

    public Diagnostic SignatureElementNotUnit(IDerivableUnitValidationContext context, DerivableUnitDefinition definition, int index)
    {
        return DiagnosticConstruction.TypeNotUnit(definition.Locations.SignatureElements[index].AsRoslynLocation(), definition.Signature[index].Name);
    }
}
