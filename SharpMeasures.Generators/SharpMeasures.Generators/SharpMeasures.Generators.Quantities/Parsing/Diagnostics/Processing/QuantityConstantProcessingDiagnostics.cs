namespace SharpMeasures.Generators.Quantities.Parsing.Diagnostics.Processing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Quantities.Parsing.QuantityConstant;

public class QuantityConstantProcessingDiagnostics<TDefinition, TLocations> : IQuantityConstantProcessingDiagnostics<TDefinition, TLocations>
    where TDefinition : ARawQuantityConstantDefinition<TDefinition, TLocations>
    where TLocations : AQuantityConstantLocations<TLocations>
{
    private NamedType? Unit { get; }

    public QuantityConstantProcessingDiagnostics(NamedType unit)
    {
        Unit = unit;
    }

    public QuantityConstantProcessingDiagnostics()
    {
        Unit = null;
    }

    public Diagnostic NullName(IQuantityConstantProcessingContext context, TDefinition definition)
    {
        return DiagnosticConstruction.NullConstantName(definition.Locations.Name?.AsRoslynLocation());
    }

    public Diagnostic EmptyName(IQuantityConstantProcessingContext context, TDefinition definition) => NullName(context, definition);

    public Diagnostic DuplicateName(IQuantityConstantProcessingContext context, TDefinition definition)
    {
        return DiagnosticConstruction.DuplicateConstantName(definition.Locations.Name?.AsRoslynLocation(), definition.Name!, context.Type.Name);
    }

    public Diagnostic NullUnit(IQuantityConstantProcessingContext context, TDefinition definition)
    {
        if (Unit is null)
        {
            return DiagnosticConstruction.NullUnrecognizedUnitNameUnknownType(definition.Locations.Unit?.AsRoslynLocation());
        }

        return DiagnosticConstruction.NullUnrecognizedUnitName(definition.Locations.Unit?.AsRoslynLocation(), Unit.Value.Name);
    }

    public Diagnostic EmptyUnit(IQuantityConstantProcessingContext context, TDefinition definition) => NullUnit(context, definition);

    public Diagnostic NullMultiples(IQuantityConstantProcessingContext context, TDefinition definition)
    {
        return DiagnosticConstruction.NullConstantMultiplesName(definition.Locations.Multiples?.AsRoslynLocation(), definition.Name!);
    }

    public Diagnostic EmptyMultiples(IQuantityConstantProcessingContext context, TDefinition definition) => NullMultiples(context, definition);

    public Diagnostic InvalidMultiples(IQuantityConstantProcessingContext context, TDefinition definition)
    {
        return DiagnosticConstruction.InvalidConstantMultiplesName(definition.Locations.Multiples?.AsRoslynLocation(), definition.Multiples!, definition.Name!);
    }

    public Diagnostic DuplicateMultiples(IQuantityConstantProcessingContext context, TDefinition definition, string interpretedMultiples)
    {
        if (definition.Locations.ExplicitlySetMultiples)
        {
            return DiagnosticConstruction.DuplicateConstantMultiplesName(definition.Locations.Multiples?.AsRoslynLocation(), interpretedMultiples, context.Type.Name);
        }

        return DiagnosticConstruction.DuplicateConstantMultiplesName(definition.Locations.AttributeName.AsRoslynLocation(), interpretedMultiples, context.Type.Name);
    }

    public Diagnostic MultiplesDisabledButNameSpecified(IQuantityConstantProcessingContext context, TDefinition definition)
    {
        return DiagnosticConstruction.ConstantMultiplesDisabledButNameSpecified(definition.Locations.Multiples?.AsRoslynLocation(), definition.Name!);
    }
}
