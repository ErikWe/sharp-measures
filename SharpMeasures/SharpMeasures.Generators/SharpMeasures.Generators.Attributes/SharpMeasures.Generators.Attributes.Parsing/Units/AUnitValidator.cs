namespace SharpMeasures.Generators.Attributes.Parsing.Units;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;

using System;
using System.Collections.Generic;

public interface IUnitDiagnostics<in TDefinition>
    where TDefinition : IUnitDefinition
{
    public abstract Diagnostic? UnitNameNullOrEmpty(IUnitValidatorContext context, TDefinition definition);
    public abstract Diagnostic? DuplicateUnitName(IUnitValidatorContext context, TDefinition definition);

    public abstract Diagnostic? InvalidUnitPluralForm(IUnitValidatorContext context, TDefinition definition);
    public abstract Diagnostic? DuplicateUnitPluralForm(IUnitValidatorContext context, TDefinition definition);
}

public interface IUnitValidatorContext : IValidatorContext
{
    public abstract HashSet<string> ReservedUnits { get; }
    public abstract HashSet<string> ReservedUnitPlurals { get; }
}

public abstract class AUnitValidator<TContext, TDefinition> : AActionableValidator<TContext, TDefinition>
    where TContext : IUnitValidatorContext
    where TDefinition : IUnitDefinition
{
    private IUnitDiagnostics<TDefinition> Diagnostics { get; }

    protected AUnitValidator(IUnitDiagnostics<TDefinition> diagnostics)
    {
        Diagnostics = diagnostics;
    }

    public override void OnValidated(TContext context, TDefinition definition)
    {
        context.ReservedUnits.Add(definition.Name);
        context.ReservedUnitPlurals.Add(definition.ParsingData.InterpretedPlural);
    }

    public override IValidityWithDiagnostics CheckValidity(TContext context, TDefinition definition)
    {
        return ValidityWithDiagnostics.DiagnoseAndMergeWhileValid(context, definition, CheckNameValidity, CheckPluralValidity);
    }

    private IValidityWithDiagnostics CheckNameValidity(TContext context, TDefinition definition)
    {
        if (string.IsNullOrEmpty(definition.Name))
        {
            return CreateInvalidity(Diagnostics.UnitNameNullOrEmpty(context, definition));
        }

        if (context.ReservedUnits.Contains(definition.Name))
        {
            return CreateInvalidity(Diagnostics.DuplicateUnitName(context, definition));
        }

        return ValidityWithDiagnostics.Valid;
    }

    private IValidityWithDiagnostics CheckPluralValidity(TContext context, TDefinition definition)
    {
        if (string.IsNullOrEmpty(definition.ParsingData.InterpretedPlural))
        {
            return CreateInvalidity(Diagnostics.InvalidUnitPluralForm(context, definition));
        }

        if (context.ReservedUnitPlurals.Contains(definition.ParsingData.InterpretedPlural))
        {
            return CreateInvalidity(Diagnostics.DuplicateUnitPluralForm(context, definition));
        }

        return ValidityWithDiagnostics.Valid;
    }
}
