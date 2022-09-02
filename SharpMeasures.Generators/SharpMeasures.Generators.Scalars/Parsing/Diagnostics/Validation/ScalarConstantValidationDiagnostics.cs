namespace SharpMeasures.Generators.Scalars.Parsing.Diagnostics.Validation;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Quantities.Parsing.Diagnostics.Validation;
using SharpMeasures.Generators.Scalars.Parsing.ScalarConstant;

internal class ScalarConstantValidationDiagnostics : QuantityConstantValidationDiagnostics<ScalarConstantDefinition, ScalarConstantLocations>, IScalarConstantValidationDiagnostics
{
    new public static ScalarConstantValidationDiagnostics Instance { get; } = new();

    private ScalarConstantValidationDiagnostics() { }

    public Diagnostic ConstantNameReservedByUnitInstanceName(IScalarConstantValidationContext context, ScalarConstantDefinition definition)
    {
        return DiagnosticConstruction.ConstantSharesNameWithUnitInstance(definition.Locations.Name?.AsRoslynLocation(), definition.Name, context.Type.Name);
    }

    public Diagnostic ConstantMultiplesNameReservedByUnitInstanceName(IScalarConstantValidationContext context, ScalarConstantDefinition definition)
    {
        if (definition.Locations.ExplicitlySetMultiples)
        {
            return DiagnosticConstruction.ConstantSharesNameWithUnitInstance(definition.Locations.Multiples?.AsRoslynLocation(), definition.Multiples!, context.Type.Name);
        }

        return DiagnosticConstruction.ConstantSharesNameWithUnitInstance(definition.Locations.AttributeName.AsRoslynLocation(), definition.Multiples!, context.Type.Name);
    }
}
