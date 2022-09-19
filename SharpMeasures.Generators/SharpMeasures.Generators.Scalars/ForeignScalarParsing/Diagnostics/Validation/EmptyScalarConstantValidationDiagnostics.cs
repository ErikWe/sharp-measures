namespace SharpMeasures.Generators.Scalars.ForeignScalarParsing.Diagnostics.Validation;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Quantities.Parsing.QuantityConstant;
using SharpMeasures.Generators.Scalars.Parsing.ScalarConstant;

internal sealed class EmptyScalarConstantValidationDiagnostics : IScalarConstantValidationDiagnostics
{
    public static EmptyScalarConstantValidationDiagnostics Instance { get; } = new();

    private EmptyScalarConstantValidationDiagnostics() { }

    Diagnostic? IScalarConstantValidationDiagnostics.NameReservedByUnitInstanceName(IScalarConstantValidationContext context, ScalarConstantDefinition definition) => null;
    Diagnostic? IScalarConstantValidationDiagnostics.MultiplesNameReservedByUnitInstanceName(IScalarConstantValidationContext context, ScalarConstantDefinition definition) => null;
    Diagnostic? IQuantityConstantValidationDiagnostics<ScalarConstantDefinition, ScalarConstantLocations>.UnrecognizedUnitInstanceName(IQuantityConstantValidationContext context, ScalarConstantDefinition definition) => null;
    Diagnostic? IQuantityConstantValidationDiagnostics<ScalarConstantDefinition, ScalarConstantLocations>.DuplicateName(IQuantityConstantValidationContext context, ScalarConstantDefinition definition) => null;
    Diagnostic? IQuantityConstantValidationDiagnostics<ScalarConstantDefinition, ScalarConstantLocations>.NameReservedByMultiples(IQuantityConstantValidationContext context, ScalarConstantDefinition definition) => null;
    Diagnostic? IQuantityConstantValidationDiagnostics<ScalarConstantDefinition, ScalarConstantLocations>.DuplicateMultiples(IQuantityConstantValidationContext context, ScalarConstantDefinition definition) => null;
    Diagnostic? IQuantityConstantValidationDiagnostics<ScalarConstantDefinition, ScalarConstantLocations>.MultiplesReservedByName(IQuantityConstantValidationContext context, ScalarConstantDefinition definition) => null;
    Diagnostic? IQuantityConstantValidationDiagnostics<ScalarConstantDefinition, ScalarConstantLocations>.NameReservedByUnitInstancePluralForm(IQuantityConstantValidationContext context, ScalarConstantDefinition definition) => null;
    Diagnostic? IQuantityConstantValidationDiagnostics<ScalarConstantDefinition, ScalarConstantLocations>.MultiplesReservedByUnitInstancePluralForm(IQuantityConstantValidationContext context, ScalarConstantDefinition definition) => null;
}
