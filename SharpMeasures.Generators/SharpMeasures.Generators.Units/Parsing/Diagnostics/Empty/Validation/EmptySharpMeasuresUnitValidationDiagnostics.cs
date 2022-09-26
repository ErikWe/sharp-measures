namespace SharpMeasures.Generators.Units.Parsing.Diagnostics.Empty.Validation;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Units.Parsing.SharpMeasuresUnit;

internal sealed class EmptySharpMeasuresUnitValidationDiagnostics : ISharpMeasuresUnitValidationDiagnostics
{
    public static EmptySharpMeasuresUnitValidationDiagnostics Instance { get; } = new();

    private EmptySharpMeasuresUnitValidationDiagnostics() { }

    Diagnostic? ISharpMeasuresUnitValidationDiagnostics.QuantityNotScalar(ISharpMeasuresUnitValidationContext context, SharpMeasuresUnitDefinition definition) => null;
    Diagnostic? ISharpMeasuresUnitValidationDiagnostics.QuantityBiased(ISharpMeasuresUnitValidationContext context, SharpMeasuresUnitDefinition definition) => null;
}
