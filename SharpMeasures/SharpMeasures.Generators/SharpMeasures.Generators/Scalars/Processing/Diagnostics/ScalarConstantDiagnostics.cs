namespace SharpMeasures.Generators.Scalars.Processing.Diagnostics;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Attributes.Parsing.Scalars;

internal static class ScalarConstantDiagnostics
{
    public static Diagnostic UnrecognizedUnit(ScalarConstantProcessingContext context, ScalarConstant definition)
    {
        return DiagnosticConstruction.UnrecognizedUnitName(definition.Locations.Unit?.AsRoslynLocation(), definition.Unit, context.Unit.UnitType.Name);
    }

    public static Diagnostic ConstantSharesNameWithUnit(ScalarConstantProcessingContext context, ScalarConstant definition)
    {
        return DiagnosticConstruction.ConstantSharesNameWithUnit(definition.Locations.Name?.AsRoslynLocation(), definition.Name, context.Type.Name);
    }

    public static Diagnostic ConstantMultiplesSharesNameWithUnitPlural(ScalarConstantProcessingContext context, ScalarConstant definition)
    {
        if (definition.Locations.ExplicitlySetMultiplesName)
        {
            return DiagnosticConstruction.ConstantSharesNameWithUnit(definition.Locations.MultiplesName?.AsRoslynLocation(), definition.MultiplesName!,
                context.Type.Name);
        }

        return DiagnosticConstruction.ConstantSharesNameWithUnit(definition.Locations.Attribute.AsRoslynLocation(), definition.MultiplesName!,
            context.Type.Name);
    }
}
