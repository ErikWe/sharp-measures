namespace SharpMeasures.Generators.Units.Parsing.Abstractions;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Attributes.Parsing.Utility;
using SharpMeasures.Generators.Diagnostics;

using System;
using System.Collections.Generic;

internal interface IUnitInstanceProcessingDiagnostics<in TDefinition, in TLocations>
    where TDefinition : IRawUnitInstance<TLocations>
    where TLocations : IUnitInstanceLocations
{
    public abstract Diagnostic? NullUnitInstanceName(IUnitInstanceProcessingContext context, TDefinition definition);
    public abstract Diagnostic? EmptyUnitInstanceName(IUnitInstanceProcessingContext context, TDefinition definition);
    public abstract Diagnostic? DuplicateUnitInstanceName(IUnitInstanceProcessingContext context, TDefinition definition);
    public abstract Diagnostic? UnitInstanceNameReservedByUnitInstancePluralForm(IUnitInstanceProcessingContext context, TDefinition definition);

    public abstract Diagnostic? NullUnitInstancePluralForm(IUnitInstanceProcessingContext context, TDefinition definition);
    public abstract Diagnostic? EmptyUnitInstancePluralForm(IUnitInstanceProcessingContext context, TDefinition definition);
    public abstract Diagnostic? InvalidUnitInstancePluralForm(IUnitInstanceProcessingContext context, TDefinition definition);
    public abstract Diagnostic? DuplicateUnitInstancePluralForm(IUnitInstanceProcessingContext context, TDefinition definition, string interpretedPluralForm);
    public abstract Diagnostic? UnitInstancePluralFormReservedByUnitInstanceName(IUnitInstanceProcessingContext context, TDefinition definition, string interpretedPluralForm);
}

internal interface IUnitInstanceProcessingContext : IProcessingContext
{
    public abstract HashSet<string> ReservedUnitInstanceNames { get; }
    public abstract HashSet<string> ReservedUnitInstancePluralForms { get; }
}

internal abstract class AUnitInstanceProcesser<TContext, TDefinition, TLocations, TProduct> : AActionableProcesser<TContext, TDefinition, TProduct>
    where TContext : IUnitInstanceProcessingContext
    where TDefinition : IRawUnitInstance<TLocations>
    where TLocations : IUnitInstanceLocations
    where TProduct : IUnitInstance
{
    private IUnitInstanceProcessingDiagnostics<TDefinition, TLocations> Diagnostics { get; }

    protected AUnitInstanceProcesser(IUnitInstanceProcessingDiagnostics<TDefinition, TLocations> diagnostics)
    {
        Diagnostics = diagnostics;
    }

    public override void OnSuccessfulProcess(TContext context, TDefinition definition, TProduct product)
    {
        context.ReservedUnitInstanceNames.Add(product.Name);
        context.ReservedUnitInstancePluralForms.Add(product.PluralForm);
    }

    protected virtual IValidityWithDiagnostics VerifyRequiredPropertiesSet(TDefinition definition)
    {
        return ValidityWithDiagnostics.ConditionalWithoutDiagnostics(definition.Locations.ExplicitlySetName && definition.Locations.ExplicitlySetPluralForm);
    }

    protected IValidityWithDiagnostics ValidateUnitInstanceName(TContext context, TDefinition definition)
    {
        return ValidateUnitInstanceNameNotNull(context, definition)
            .Validate(() => ValidateUnitInstanceNameNotEmpty(context, definition))
            .Validate(() => ValidateUnitInstanceNameNotDuplicate(context, definition))
            .Validate(() => ValidateUnitInstanceNameNotReservedByPluralForm(context, definition));
    }

    private IValidityWithDiagnostics ValidateUnitInstanceNameNotNull(TContext context, TDefinition definition)
    {
        return ValidityWithDiagnostics.Conditional(definition.Name is not null, () => Diagnostics.NullUnitInstanceName(context, definition));
    }

    private IValidityWithDiagnostics ValidateUnitInstanceNameNotEmpty(TContext context, TDefinition definition)
    {
        return ValidityWithDiagnostics.Conditional(definition.Name!.Length is not 0, () => Diagnostics.EmptyUnitInstanceName(context, definition));
    }

    private IValidityWithDiagnostics ValidateUnitInstanceNameNotDuplicate(TContext context, TDefinition definition)
    {
        return ValidityWithDiagnostics.Conditional(context.ReservedUnitInstanceNames.Contains(definition.Name!) is false, () => Diagnostics.DuplicateUnitInstanceName(context, definition));
    }

    private IValidityWithDiagnostics ValidateUnitInstanceNameNotReservedByPluralForm(TContext context, TDefinition definition)
    {
        return ValidityWithDiagnostics.Conditional(context.ReservedUnitInstancePluralForms.Contains($"One{definition.Name!}") is false, () => Diagnostics.UnitInstanceNameReservedByUnitInstancePluralForm(context, definition));
    }

    protected IOptionalWithDiagnostics<string> ProcessUnitInstancePluralForm(TContext context, TDefinition definition)
    {
        return ValidateUnitInstancePluralFormNotNull(context, definition)
            .Validate(() => ValidateUnitInstancePluralFormNotEmpty(context, definition))
            .Transform(() => InterpretUnitInstancePluralForm(definition))
            .Merge((interpretedPluralForm) => ProcessInterpretedUnitInstancePluralForm(context, definition, interpretedPluralForm))
            .Validate((interpretedPluralForm) => ValidateInterpretedUnitInstancePluralForm(context, definition, interpretedPluralForm));
    }

    private IValidityWithDiagnostics ValidateUnitInstancePluralFormNotNull(TContext context, TDefinition definition)
    {
        return ValidityWithDiagnostics.Conditional(definition.PluralForm is not null, () => Diagnostics.NullUnitInstancePluralForm(context, definition));
    }

    private IValidityWithDiagnostics ValidateUnitInstancePluralFormNotEmpty(TContext context, TDefinition definition)
    {
        return ValidityWithDiagnostics.Conditional(definition.PluralForm!.Length is not 0, () => Diagnostics.EmptyUnitInstancePluralForm(context, definition));
    }

    private static string? InterpretUnitInstancePluralForm(TDefinition definition)
    {
        if (definition.PluralFormRegexSubstitution is null)
        {
            return SimpleTextExpression.Interpret(definition.Name!, definition.PluralForm!);
        }

        return SimpleTextExpression.Interpret(definition.Name!, definition.PluralForm!, definition.PluralFormRegexSubstitution);
    }

    private IOptionalWithDiagnostics<string> ProcessInterpretedUnitInstancePluralForm(TContext context, TDefinition definition, string? interpretedPluralForm)
    {
        return OptionalWithDiagnostics.Conditional(interpretedPluralForm is not null, interpretedPluralForm!, () => Diagnostics.InvalidUnitInstancePluralForm(context, definition));
    }

    private IValidityWithDiagnostics ValidateInterpretedUnitInstancePluralForm(TContext context, TDefinition definition, string interpretedPluralForm)
    {
        return ValidateUnitInstancePluralFormNotDuplicate(context, definition, interpretedPluralForm)
            .Validate(() => ValidateUnitInstancePluralFormNotReservedByUnitInstanceName(context, definition, interpretedPluralForm))
            .Validate(() => ValidateUnitInstancePluralFormNotSameAsUnitInstanceName(context, definition, interpretedPluralForm));
    }

    private IValidityWithDiagnostics ValidateUnitInstancePluralFormNotDuplicate(TContext context, TDefinition definition, string interpretedPluralForm)
    {
        return ValidityWithDiagnostics.Conditional(context.ReservedUnitInstancePluralForms.Contains(interpretedPluralForm) is false, () => Diagnostics.DuplicateUnitInstancePluralForm(context, definition, interpretedPluralForm));
    }

    private IValidityWithDiagnostics ValidateUnitInstancePluralFormNotReservedByUnitInstanceName(TContext context, TDefinition definition, string interpretedPluralForm)
    {
        var unitInstancePluralFormReservedByUnitInstanceName = interpretedPluralForm.StartsWith("One", StringComparison.InvariantCulture) && context.ReservedUnitInstanceNames.Contains(interpretedPluralForm.Substring(3));

        return ValidityWithDiagnostics.Conditional(unitInstancePluralFormReservedByUnitInstanceName is false, () => Diagnostics.UnitInstancePluralFormReservedByUnitInstanceName(context, definition, interpretedPluralForm));
    }

    private IValidityWithDiagnostics ValidateUnitInstancePluralFormNotSameAsUnitInstanceName(TContext context, TDefinition definition, string interpretedPluralForm)
    {
        var unitInstancePluralFormSameAsUnitInstanceName = interpretedPluralForm.StartsWith("One", StringComparison.InvariantCulture) && interpretedPluralForm.Substring(3) == definition.Name;

        return ValidityWithDiagnostics.Conditional(unitInstancePluralFormSameAsUnitInstanceName is false, () => Diagnostics.UnitInstancePluralFormReservedByUnitInstanceName(context, definition, interpretedPluralForm));
    }
}
