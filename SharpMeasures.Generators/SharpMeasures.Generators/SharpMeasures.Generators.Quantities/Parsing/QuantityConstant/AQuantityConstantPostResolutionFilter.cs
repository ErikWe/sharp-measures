namespace SharpMeasures.Generators.Quantities.Parsing.QuantityConstant;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;

using System.Collections.Generic;

public interface IQuantityConstantPostResolutionFilterDiagnostics<TDefinition, TLocations>
    where TDefinition : AQuantityConstantDefinition<TLocations>
    where TLocations : AQuantityConstantLocations<TLocations>
{
    public abstract Diagnostic? DuplicateConstantName(IQuantityConstantPostResolutionFilterContext context, TDefinition definition);
    public abstract Diagnostic? DuplicateConstantMultiplesName(IQuantityConstantPostResolutionFilterContext context, TDefinition definition);

    public abstract Diagnostic? ConstantNameReservedByUnit(IQuantityConstantPostResolutionFilterContext context, TDefinition definition);
    public abstract Diagnostic? ConstantMultiplesNameReservedByUnit(IQuantityConstantPostResolutionFilterContext context, TDefinition definition);
}

public interface IQuantityConstantPostResolutionFilterContext : IValidationContext
{
    public abstract HashSet<string> InheritedConstantNames { get; }
    public abstract HashSet<string> InheritedConstantMultiplesNames { get; }

    public abstract HashSet<string> IncludedUnits { get; }
}

public abstract class AQuantityConstantPostResolutionFilter<TContext, TDefinition, TLocations> : AValidator<TContext, TDefinition>
    where TContext : IQuantityConstantPostResolutionFilterContext
    where TDefinition : AQuantityConstantDefinition<TLocations>
    where TLocations : AQuantityConstantLocations<TLocations>
{
    private IQuantityConstantPostResolutionFilterDiagnostics<TDefinition, TLocations> Diagnostics { get; }

    protected AQuantityConstantPostResolutionFilter(IQuantityConstantPostResolutionFilterDiagnostics<TDefinition, TLocations> diagnostics)
    {
        Diagnostics = diagnostics;
    }

    protected virtual IValidityWithDiagnostics CheckNameValidity(TContext context, TDefinition definition)
    {
        if (context.InheritedConstantNames.Contains(definition.Name))
        {
            return ValidityWithDiagnostics.Invalid(Diagnostics.DuplicateConstantName(context, definition));
        }

        if (definition.GenerateMultiplesProperty && context.InheritedConstantMultiplesNames.Contains(definition.Multiples!))
        {
            return ValidityWithDiagnostics.Invalid(Diagnostics.DuplicateConstantMultiplesName(context, definition));
        }

        if (context.IncludedUnits.Contains(definition.Name))
        {
            return ValidityWithDiagnostics.Invalid(Diagnostics.ConstantNameReservedByUnit(context, definition));
        }

        if (definition.GenerateMultiplesProperty && context.IncludedUnits.Contains(definition.Multiples!))
        {
            return ValidityWithDiagnostics.Invalid(Diagnostics.ConstantMultiplesNameReservedByUnit(context, definition));
        }

        return ValidityWithDiagnostics.Valid;
    }
}
