namespace SharpMeasures.Generators.Scalars.Parsing.Diagnostics.Empty.Validation;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Quantities.Parsing.QuantityConstant;
using SharpMeasures.Generators.Scalars.Parsing.ScalarConstant;

internal sealed class EmptyScalarConstantValidationDiagnostics : IScalarConstantValidationDiagnostics
{
    public static EmptyScalarConstantValidationDiagnostics Instance { get; } = new();

    private EmptyScalarConstantValidationDiagnostics() { }

    public Diagnostic? NameReservedByUnitInstanceName(IScalarConstantValidationContext context, ScalarConstantDefinition definition) => null;
    public Diagnostic? MultiplesNameReservedByUnitInstanceName(IScalarConstantValidationContext context, ScalarConstantDefinition definition) => null;
    public Diagnostic? UnrecognizedUnitInstanceName(IQuantityConstantValidationContext context, ScalarConstantDefinition definition) => null;
    public Diagnostic? DuplicateName(IQuantityConstantValidationContext context, ScalarConstantDefinition definition) => null;
    public Diagnostic? NameReservedByMultiples(IQuantityConstantValidationContext context, ScalarConstantDefinition definition) => null;
    public Diagnostic? DuplicateMultiples(IQuantityConstantValidationContext context, ScalarConstantDefinition definition) => null;
    public Diagnostic? MultiplesReservedByName(IQuantityConstantValidationContext context, ScalarConstantDefinition definition) => null;
    public Diagnostic? NameReservedByUnitInstancePluralForm(IQuantityConstantValidationContext context, ScalarConstantDefinition definition) => null;
    public Diagnostic? MultiplesReservedByUnitInstancePluralForm(IQuantityConstantValidationContext context, ScalarConstantDefinition definition) => null;
}
