namespace SharpMeasures.Generators.Scalars.Parsing.Diagnostics.PostResolutionFilter;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Quantities.Parsing.Diagnostics.PostResolutionFilter;
using SharpMeasures.Generators.Scalars.Parsing.ScalarConstant;

internal class ScalarConstantPostResolutionFilterDiagnostics : QuantityConstantPostResolutionFilterDiagnostics<ScalarConstantDefinition, ScalarConstantLocations>, IScalarConstantPostResolutionFilterDiagnostics
{
    public static ScalarConstantPostResolutionFilterDiagnostics Instance { get; } = new();

    private ScalarConstantPostResolutionFilterDiagnostics() { }

    public Diagnostic ConstantNameReservedByUnitBase(IScalarConstantPostResolutionFilterContext context, ScalarConstantDefinition definition)
    {
        return DiagnosticConstruction.ConstantSharesNameWithUnit(definition.Locations.Name?.AsRoslynLocation(), definition.Name, context.Type.Name);
    }

    public Diagnostic ConstantMultiplesNameReservedByUnitBase(IScalarConstantPostResolutionFilterContext context, ScalarConstantDefinition definition)
    {
        if (definition.Locations.ExplicitlySetMultiples)
        {
            return DiagnosticConstruction.ConstantSharesNameWithUnit(definition.Locations.Multiples?.AsRoslynLocation(), definition.Multiples!, context.Type.Name);
        }

        return DiagnosticConstruction.ConstantSharesNameWithUnit(definition.Locations.AttributeName.AsRoslynLocation(), definition.Multiples!, context.Type.Name);
    }
}
