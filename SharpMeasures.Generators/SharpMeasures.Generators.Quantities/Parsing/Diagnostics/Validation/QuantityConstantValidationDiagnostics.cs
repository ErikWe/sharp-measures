namespace SharpMeasures.Generators.Quantities.Parsing.Diagnostics.Validation;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Quantities.Parsing.QuantityConstant;

using System.Diagnostics.CodeAnalysis;

public class QuantityConstantValidationDiagnostics<TDefinition, TLocations> : IQuantityConstantValidationDiagnostics<TDefinition, TLocations>
    where TDefinition : AQuantityConstantDefinition<TLocations>
    where TLocations : AQuantityConstantLocations<TLocations>
{
    [SuppressMessage("Design", "CA1000", Justification = "Property")]
    public static QuantityConstantValidationDiagnostics<TDefinition, TLocations> Instance { get; } = new();

    protected QuantityConstantValidationDiagnostics() { }

    public Diagnostic UnrecognizedUnit(IQuantityConstantValidationContext context, TDefinition definition)
    {
        return DiagnosticConstruction.UnrecognizedUnitName(definition.Locations.Unit?.AsRoslynLocation(), definition.Unit, context.UnitType.Type.Name);
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
        return DiagnosticConstruction.DuplicateConstantMultiplesName(definition.Locations.Multiples?.AsRoslynLocation(), definition.Multiples!, context.Type.Name);
    }

    public Diagnostic MultiplesReservedByName(IQuantityConstantValidationContext context, TDefinition definition)
    {
        return DiagnosticConstruction.ConstantMultiplesNameReversedByConstantName(definition.Locations.Multiples?.AsRoslynLocation(), definition.Multiples!, context.Type.Name);
    }

    public Diagnostic NameReservedByUnitPlural(IQuantityConstantValidationContext context, TDefinition definition)
    {
        return DiagnosticConstruction.ConstantSharesNameWithUnit(definition.Locations.Name?.AsRoslynLocation(), definition.Name, context.Type.Name);
    }

    public Diagnostic MultiplesReservedByUnitPlural(IQuantityConstantValidationContext context, TDefinition definition)
    {
        return DiagnosticConstruction.ConstantSharesNameWithUnit(definition.Locations.Multiples?.AsRoslynLocation(), definition.Multiples!, context.Type.Name);
    }
}
