namespace SharpMeasures.Generators.Units.Parsing.Abstractions;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Attributes.Parsing.Utility;
using SharpMeasures.Generators.Diagnostics;

using System;
using System.Collections.Generic;

internal interface IUnitProcessingDiagnostics<in TDefinition, in TLocations>
    where TDefinition : IUnprocessedUnitDefinition<TLocations>
    where TLocations : IUnitLocations
{
    public abstract Diagnostic? NullUnitName(IUnitProcessingContext context, TDefinition definition);
    public abstract Diagnostic? EmptyUnitName(IUnitProcessingContext context, TDefinition definition);
    public abstract Diagnostic? DuplicateUnitName(IUnitProcessingContext context, TDefinition definition);
    public abstract Diagnostic? UnitNameReservedByUnitPluralForm(IUnitProcessingContext context, TDefinition definition);

    public abstract Diagnostic? NullUnitPluralForm(IUnitProcessingContext context, TDefinition definition);
    public abstract Diagnostic? EmptyUnitPluralForm(IUnitProcessingContext context, TDefinition definition);
    public abstract Diagnostic? InvalidUnitPluralForm(IUnitProcessingContext context, TDefinition definition);
    public abstract Diagnostic? DuplicateUnitPluralForm(IUnitProcessingContext context, TDefinition definition, string interpretedPlural);
    public abstract Diagnostic? UnitPluralFormReservedByUnitName(IUnitProcessingContext context, TDefinition definition, string interpretedPlural);
}

internal interface IUnitProcessingContext : IProcessingContext
{
    public abstract HashSet<string> ReservedUnits { get; }
    public abstract HashSet<string> ReservedUnitPlurals { get; }
}

internal abstract class AUnitProcesser<TContext, TDefinition, TLocations, TProduct> : AActionableProcesser<TContext, TDefinition, TProduct>
    where TContext : IUnitProcessingContext
    where TDefinition : IUnprocessedUnitDefinition<TLocations>
    where TLocations : IUnitLocations
    where TProduct : IRawUnitDefinition<TLocations>
{
    private IUnitProcessingDiagnostics<TDefinition, TLocations> Diagnostics { get; }

    protected AUnitProcesser(IUnitProcessingDiagnostics<TDefinition, TLocations> diagnostics)
    {
        Diagnostics = diagnostics;
    }

    public override void OnSuccessfulProcess(TContext context, TDefinition definition, TProduct product)
    {
        context.ReservedUnits.Add(product.Name);
        context.ReservedUnitPlurals.Add(product.Plural);
    }

    protected virtual IValidityWithDiagnostics VerifyRequiredPropertiesSet(TDefinition definition)
    {
        return ValidityWithDiagnostics.ConditionalWithoutDiagnostics(definition.Locations.ExplicitlySetName && definition.Locations.ExplicitlySetPlural);
    }

    protected IValidityWithDiagnostics ValidateUnitName(TContext context, TDefinition definition)
    {
        return ValidateUnitNameNotNull(context, definition)
            .Validate(() => ValidateUnitNameNotEmpty(context, definition))
            .Validate(() => ValidateUnitNameNotDuplicate(context, definition))
            .Validate(() => ValidateUnitNameNotReservedByPlural(context, definition));
    }

    private IValidityWithDiagnostics ValidateUnitNameNotNull(TContext context, TDefinition definition)
    {
        return ValidityWithDiagnostics.Conditional(definition.Name is not null, () => Diagnostics.NullUnitName(context, definition));
    }

    private IValidityWithDiagnostics ValidateUnitNameNotEmpty(TContext context, TDefinition definition)
    {
        return ValidityWithDiagnostics.Conditional(definition.Name!.Length is not 0, () => Diagnostics.EmptyUnitName(context, definition));
    }

    private IValidityWithDiagnostics ValidateUnitNameNotDuplicate(TContext context, TDefinition definition)
    {
        return ValidityWithDiagnostics.Conditional(context.ReservedUnits.Contains(definition.Name!) is false, () => Diagnostics.DuplicateUnitName(context, definition));
    }

    private IValidityWithDiagnostics ValidateUnitNameNotReservedByPlural(TContext context, TDefinition definition)
    {
        return ValidityWithDiagnostics.Conditional(context.ReservedUnitPlurals.Contains($"One{definition.Name!}") is false, () => Diagnostics.UnitNameReservedByUnitPluralForm(context, definition));
    }

    protected IOptionalWithDiagnostics<string> ProcessPlural(TContext context, TDefinition definition)
    {
        return ValidateUnitPluralNotNull(context, definition)
            .Validate(() => ValidateUnitPluralNotEmpty(context, definition))
            .Transform(() => InterpretPluralForm(definition))
            .Merge((interpretedPlural) => ProcessInterpretedPluralForm(context, definition, interpretedPlural))
            .Validate((interpretedPlural) => ValidateInterpretedPluralForm(context, definition, interpretedPlural));
    }

    private IValidityWithDiagnostics ValidateUnitPluralNotNull(TContext context, TDefinition definition)
    {
        return ValidityWithDiagnostics.Conditional(definition.Plural is not null, () => Diagnostics.NullUnitPluralForm(context, definition));
    }

    private IValidityWithDiagnostics ValidateUnitPluralNotEmpty(TContext context, TDefinition definition)
    {
        return ValidityWithDiagnostics.Conditional(definition.Plural!.Length is not 0, () => Diagnostics.EmptyUnitPluralForm(context, definition));
    }

    private static string? InterpretPluralForm(TDefinition definition) => SimpleTextExpression.Interpret(definition.Name, definition.Plural);

    private IOptionalWithDiagnostics<string> ProcessInterpretedPluralForm(TContext context, TDefinition definition, string? interpretedPluralForm)
    {
        return OptionalWithDiagnostics.Conditional(interpretedPluralForm is not null, interpretedPluralForm!, () => Diagnostics.InvalidUnitPluralForm(context, definition));
    }

    private IValidityWithDiagnostics ValidateInterpretedPluralForm(TContext context, TDefinition definition, string interpretedPluralForm)
    {
        return ValidateUnitPluralNotDuplicate(context, definition, interpretedPluralForm)
            .Validate(() => ValidateUnitPluralNotReservedByName(context, definition, interpretedPluralForm))
            .Validate(() => ValidateUnitPluralNotSameAsName(context, definition, interpretedPluralForm));
    }

    private IValidityWithDiagnostics ValidateUnitPluralNotDuplicate(TContext context, TDefinition definition, string interpretedPluralForm)
    {
        return ValidityWithDiagnostics.Conditional(context.ReservedUnitPlurals.Contains(interpretedPluralForm) is false, () => Diagnostics.DuplicateUnitPluralForm(context, definition, interpretedPluralForm));
    }

    private IValidityWithDiagnostics ValidateUnitPluralNotReservedByName(TContext context, TDefinition definition, string interpretedPluralForm)
    {
        var unitPluralReservedByName = interpretedPluralForm.StartsWith("One", StringComparison.InvariantCulture) && context.ReservedUnits.Contains(interpretedPluralForm.Substring(3));

        return ValidityWithDiagnostics.Conditional(unitPluralReservedByName is false, () => Diagnostics.UnitPluralFormReservedByUnitName(context, definition, interpretedPluralForm));
    }

    private IValidityWithDiagnostics ValidateUnitPluralNotSameAsName(TContext context, TDefinition definition, string interpretedPluralForm)
    {
        var unitPluralSameAsName = interpretedPluralForm.StartsWith("One", StringComparison.InvariantCulture) && interpretedPluralForm.Substring(3) == definition.Name;

        return ValidityWithDiagnostics.Conditional(unitPluralSameAsName is false, () => Diagnostics.UnitPluralFormReservedByUnitName(context, definition, interpretedPluralForm));
    }
}
