namespace SharpMeasures.Generators.Scalars.Parsing.Diagnostics.Validation;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Quantities.Parsing.Diagnostics.Validation;
using SharpMeasures.Generators.Quantities.Parsing.QuantityConstant;
using SharpMeasures.Generators.Scalars.Parsing.ScalarConstant;

internal sealed class ScalarConstantValidationDiagnostics : IScalarConstantValidationDiagnostics
{
    public static ScalarConstantValidationDiagnostics Instance { get; } = new();

    private static QuantityConstantValidationDiagnostics<ScalarConstantDefinition, ScalarConstantLocations> QuantityInstance { get; } = QuantityConstantValidationDiagnostics<ScalarConstantDefinition, ScalarConstantLocations>.Instance;

    private ScalarConstantValidationDiagnostics() { }

    public Diagnostic? UnrecognizedUnitInstanceName(IQuantityConstantValidationContext context, ScalarConstantDefinition definition) => QuantityInstance.UnrecognizedUnitInstanceName(context, definition);
    public Diagnostic? DuplicateName(IQuantityConstantValidationContext context, ScalarConstantDefinition definition) => QuantityInstance.DuplicateName(context, definition);
    public Diagnostic? NameReservedByMultiples(IQuantityConstantValidationContext context, ScalarConstantDefinition definition) => QuantityInstance.NameReservedByMultiples(context, definition);
    public Diagnostic? DuplicateMultiples(IQuantityConstantValidationContext context, ScalarConstantDefinition definition) => QuantityInstance.DuplicateMultiples(context, definition);
    public Diagnostic? MultiplesReservedByName(IQuantityConstantValidationContext context, ScalarConstantDefinition definition) => QuantityInstance.MultiplesReservedByName(context, definition);
    public Diagnostic? NameReservedByUnitInstancePluralForm(IQuantityConstantValidationContext context, ScalarConstantDefinition definition) => QuantityInstance.NameReservedByUnitInstancePluralForm(context, definition);
    public Diagnostic? MultiplesReservedByUnitInstancePluralForm(IQuantityConstantValidationContext context, ScalarConstantDefinition definition) => QuantityInstance.MultiplesReservedByUnitInstancePluralForm(context, definition);

    public Diagnostic NameReservedByUnitInstanceName(IScalarConstantValidationContext context, ScalarConstantDefinition definition)
    {
        return DiagnosticConstruction.ConstantSharesNameWithUnitInstance(definition.Locations.Name?.AsRoslynLocation(), definition.Name, context.Type.Name);
    }

    public Diagnostic MultiplesNameReservedByUnitInstanceName(IScalarConstantValidationContext context, ScalarConstantDefinition definition)
    {
        if (definition.Locations.ExplicitlySetMultiples)
        {
            return DiagnosticConstruction.ConstantSharesNameWithUnitInstance(definition.Locations.Multiples?.AsRoslynLocation(), definition.Multiples!, context.Type.Name);
        }

        return DiagnosticConstruction.ConstantSharesNameWithUnitInstance(definition.Locations.AttributeName.AsRoslynLocation(), definition.Multiples!, context.Type.Name);
    }
}
