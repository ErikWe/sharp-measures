namespace SharpMeasures.Generators.Quantities.Parsing.Diagnostics.Validation;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Quantities.Parsing.QuantityConstant;

public sealed class QuantityConstantValidationDiagnostics<TDefinition, TLocations> : IQuantityConstantValidationDiagnostics<TDefinition, TLocations>
    where TDefinition : AQuantityConstantDefinition<TLocations>
    where TLocations : AQuantityConstantLocations<TLocations>
{
    public static QuantityConstantValidationDiagnostics<TDefinition, TLocations> Instance { get; } = new();

    private QuantityConstantValidationDiagnostics() { }

    public Diagnostic UnrecognizedUnitInstanceName(IQuantityConstantValidationContext context, TDefinition definition)
    {
        return DiagnosticConstruction.UnrecognizedUnitInstanceName(definition.Locations.UnitInstanceName?.AsRoslynLocation(), definition.UnitInstanceName, context.UnitType.Type.Name);
    }

    public Diagnostic DuplicateName(IQuantityConstantValidationContext context, TDefinition definition)
    {
        return DiagnosticConstruction.DuplicateConstantName(definition.Locations.Name?.AsRoslynLocation(), definition.Name, context.Type.Name);
    }

    public Diagnostic NameReservedByMultiples(IQuantityConstantValidationContext context, TDefinition definition)
    {
        return DiagnosticConstruction.ConstantNameReservedByConstantMultiples(definition.Locations.Name?.AsRoslynLocation(), definition.Name, context.Type.Name);
    }

    public Diagnostic DuplicateMultiples(IQuantityConstantValidationContext context, TDefinition definition)
    {
        if (definition.Locations.ExplicitlySetMultiples)
        {
            return DiagnosticConstruction.DuplicateConstantMultiplesName(definition.Locations.Multiples?.AsRoslynLocation(), definition.Multiples!, context.Type.Name);
        }

        return DiagnosticConstruction.DuplicateConstantMultiplesName(definition.Locations.AttributeName.AsRoslynLocation(), definition.Multiples!, context.Type.Name);
    }

    public Diagnostic MultiplesReservedByName(IQuantityConstantValidationContext context, TDefinition definition)
    {
        if (definition.Locations.ExplicitlySetMultiples)
        {
            return DiagnosticConstruction.ConstantMultiplesNameReversedByConstantName(definition.Locations.Multiples?.AsRoslynLocation(), definition.Multiples!, context.Type.Name);
        }

        return DiagnosticConstruction.ConstantMultiplesNameReversedByConstantName(definition.Locations.AttributeName.AsRoslynLocation(), definition.Multiples!, context.Type.Name);
    }

    public Diagnostic NameReservedByUnitInstancePluralForm(IQuantityConstantValidationContext context, TDefinition definition)
    {
        return DiagnosticConstruction.ConstantSharesNameWithUnitInstance(definition.Locations.Name?.AsRoslynLocation(), definition.Name, context.Type.Name);
    }

    public Diagnostic MultiplesReservedByUnitInstancePluralForm(IQuantityConstantValidationContext context, TDefinition definition)
    {
        if (definition.Locations.ExplicitlySetMultiples)
        {
            return DiagnosticConstruction.ConstantSharesNameWithUnitInstance(definition.Locations.Multiples?.AsRoslynLocation(), definition.Multiples!, context.Type.Name);
        }

        return DiagnosticConstruction.ConstantSharesNameWithUnitInstance(definition.Locations.AttributeName.AsRoslynLocation(), definition.Multiples!, context.Type.Name);
    }
}
