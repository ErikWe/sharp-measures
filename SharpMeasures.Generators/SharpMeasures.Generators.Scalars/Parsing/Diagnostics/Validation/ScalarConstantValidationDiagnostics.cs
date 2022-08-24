namespace SharpMeasures.Generators.Scalars.Parsing.Diagnostics.Validation;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Quantities.Parsing.Diagnostics.Validation;
using SharpMeasures.Generators.Scalars.Parsing.ScalarConstant;

internal class ScalarConstantValidationDiagnostics : QuantityConstantValidationDiagnostics<ScalarConstantDefinition, ScalarConstantLocations>, IScalarConstantValidationDiagnostics
{
    new public static ScalarConstantValidationDiagnostics Instance { get; } = new();

    private ScalarConstantValidationDiagnostics() { }

    public Diagnostic ConstantNameReservedByUnitBase(IScalarConstantValidationContext context, ScalarConstantDefinition definition)
    {
        return DiagnosticConstruction.ConstantSharesNameWithUnit(definition.Locations.Name?.AsRoslynLocation(), definition.Name, context.Type.Name);
    }

    public Diagnostic ConstantMultiplesNameReservedByUnitBase(IScalarConstantValidationContext context, ScalarConstantDefinition definition)
    {
        if (definition.Locations.ExplicitlySetMultiples)
        {
            return DiagnosticConstruction.ConstantSharesNameWithUnit(definition.Locations.Multiples?.AsRoslynLocation(), definition.Multiples!, context.Type.Name);
        }

        return DiagnosticConstruction.ConstantSharesNameWithUnit(definition.Locations.AttributeName.AsRoslynLocation(), definition.Multiples!, context.Type.Name);
    }
}
