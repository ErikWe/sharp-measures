namespace SharpMeasures.Generators.Quantities.Parsing.QuantityConstant;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Units;

using System.Collections.Generic;
using System.Linq;

public interface IQuantityConstantValidationDiagnostics<TDefinition, TLocations>
    where TDefinition : AQuantityConstantDefinition<TLocations>
    where TLocations : AQuantityConstantLocations<TLocations>
{
    public abstract Diagnostic? UnrecognizedUnit(IQuantityConstantValidationContext context, TDefinition definition);

    public abstract Diagnostic? DuplicateName(IQuantityConstantValidationContext context, TDefinition definition);
    public abstract Diagnostic? NameReservedByMultiples(IQuantityConstantValidationContext context, TDefinition definition);

    public abstract Diagnostic? DuplicateMultiples(IQuantityConstantValidationContext context, TDefinition definition);
    public abstract Diagnostic? MultiplesReservedByName(IQuantityConstantValidationContext context, TDefinition definition);

    public abstract Diagnostic? NameReservedByUnitPlural(IQuantityConstantValidationContext context, TDefinition definition);
    public abstract Diagnostic? MultiplesReservedByUnitPlural(IQuantityConstantValidationContext context, TDefinition definition);
}

public interface IQuantityConstantValidationContext : IProcessingContext
{
    public abstract IUnitType UnitType { get; }

    public abstract HashSet<string> InheritedConstantNames { get; }
    public abstract HashSet<string> InheritedConstantMultiples { get; }

    public abstract HashSet<string> IncludedUnitPlurals { get; }
}

public abstract class AQuantityConstantValidator<TContext, TDefinition, TLocations> : AProcesser<TContext, TDefinition, TDefinition>
    where TContext : IQuantityConstantValidationContext
    where TDefinition : AQuantityConstantDefinition<TLocations>
    where TLocations : AQuantityConstantLocations<TLocations>
{
    private IQuantityConstantValidationDiagnostics<TDefinition, TLocations> Diagnostics { get; }

    protected AQuantityConstantValidator(IQuantityConstantValidationDiagnostics<TDefinition, TLocations> diagnostics)
    {
        Diagnostics = diagnostics;
    }

    public override IOptionalWithDiagnostics<TDefinition> Process(TContext context, TDefinition definition)
    {
        var validity = ValidateConstant(context, definition);

        if (validity.IsInvalid)
        {
            return validity.AsEmptyOptional<TDefinition>();
        }

        var multiplesValidity = ValidateMultiples(context, definition);
        var allDiagnostics = validity.Diagnostics.Concat(multiplesValidity);

        var product = ProduceResult(definition, multiplesValidity.IsValid && definition.GenerateMultiplesProperty);

        return OptionalWithDiagnostics.Result(product, allDiagnostics);
    }

    protected abstract TDefinition ProduceResult(TDefinition definition, bool generateMultiples);

    protected virtual IValidityWithDiagnostics ValidateConstant(TContext context, TDefinition definition)
    {
        return ValidateUnitExists(context, definition)
            .Validate(() => ValidateNameNotDuplicate(context, definition))
            .Validate(() => ValidateNameNotReservedByMultiples(context, definition))
            .Validate(() => ValidateNameNotReservedByUnitPlural(context, definition));
    }

    protected virtual IValidityWithDiagnostics ValidateMultiples(TContext context, TDefinition definition)
    {
        return ValidateMultiplesNotDuplicate(context, definition)
            .Validate(() => ValidateMultiplesNotReservedByName(context, definition))
            .Validate(() => ValidateMultiplesNotReservedByUnitPlural(context, definition));
    }

    private IValidityWithDiagnostics ValidateUnitExists(TContext context, TDefinition definition)
    {
        var unitExists = context.UnitType.UnitsByName.ContainsKey(definition.Unit);

        return ValidityWithDiagnostics.Conditional(unitExists, () => Diagnostics.UnrecognizedUnit(context, definition));
    }

    private IValidityWithDiagnostics ValidateNameNotDuplicate(TContext context, TDefinition definition)
    {
        var duplicateUnit = context.InheritedConstantNames.Contains(definition.Name);

        return ValidityWithDiagnostics.Conditional(duplicateUnit is false, () => Diagnostics.DuplicateName(context, definition));
    }

    private IValidityWithDiagnostics ValidateNameNotReservedByMultiples(TContext context, TDefinition definition)
    {
        var nameReservedByMultiples = context.InheritedConstantMultiples.Contains(definition.Name);

        return ValidityWithDiagnostics.Conditional(nameReservedByMultiples is false, () => Diagnostics.NameReservedByMultiples(context, definition));
    }

    private IValidityWithDiagnostics ValidateMultiplesNotDuplicate(TContext context, TDefinition definition)
    {
        var duplicateMultiples = definition.GenerateMultiplesProperty && context.InheritedConstantMultiples.Contains(definition.Multiples!);

        return ValidityWithDiagnostics.Conditional(duplicateMultiples is false, () => Diagnostics.DuplicateMultiples(context, definition));
    }

    private IValidityWithDiagnostics ValidateMultiplesNotReservedByName(TContext context, TDefinition definition)
    {
        var multiplesReservedByName = definition.GenerateMultiplesProperty && context.InheritedConstantNames.Contains(definition.Multiples!);

        return ValidityWithDiagnostics.Conditional(multiplesReservedByName is false, () => Diagnostics.MultiplesReservedByName(context, definition));
    }

    private IValidityWithDiagnostics ValidateNameNotReservedByUnitPlural(TContext context, TDefinition definition)
    {
        var nameReservedByUnitPlural = context.IncludedUnitPlurals.Contains(definition.Name);

        return ValidityWithDiagnostics.Conditional(nameReservedByUnitPlural is false, () => Diagnostics.NameReservedByUnitPlural(context, definition));
    }

    private IValidityWithDiagnostics ValidateMultiplesNotReservedByUnitPlural(TContext context, TDefinition definition)
    {
        var multiplesReservedByUnitPlural = definition.GenerateMultiplesProperty && context.IncludedUnitPlurals.Contains(definition.Multiples!);

        return ValidityWithDiagnostics.Conditional(multiplesReservedByUnitPlural is false, () => Diagnostics.MultiplesReservedByUnitPlural(context, definition));
    }
}
