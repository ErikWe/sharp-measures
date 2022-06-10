namespace SharpMeasures.Generators.Units.Parsing.Abstractions;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;

using System.Collections.Generic;

internal interface IUnitProcessingDiagnostics<in TDefinition>
    where TDefinition : IRawUnitDefinition
{
    public abstract Diagnostic? NullUnitName(IUnitProcessingContext context, TDefinition definition);
    public abstract Diagnostic? EmptyUnitName(IUnitProcessingContext context, TDefinition definition);
    public abstract Diagnostic? DuplicateUnitName(IUnitProcessingContext context, TDefinition definition);

    public abstract Diagnostic? NullUnitPluralForm(IUnitProcessingContext context, TDefinition definition);
    public abstract Diagnostic? EmptyUnitPluralForm(IUnitProcessingContext context, TDefinition definition);
    public abstract Diagnostic? InvalidUnitPluralForm(IUnitProcessingContext context, TDefinition definition);
    public abstract Diagnostic? DuplicateUnitPluralForm(IUnitProcessingContext context, TDefinition definition);
}

internal interface IUnitProcessingContext : IProcessingContext
{
    public abstract HashSet<string> ReservedUnits { get; }
    public abstract HashSet<string> ReservedUnitPlurals { get; }
}

internal abstract class AUnitProcesser<TContext, TDefinition, TProduct> : AActionableProcesser<TContext, TDefinition, TProduct>
    where TContext : IUnitProcessingContext
    where TDefinition : IRawUnitDefinition
    where TProduct : IUnitDefinition
{
    private IUnitProcessingDiagnostics<TDefinition> Diagnostics { get; }

    protected AUnitProcesser(IUnitProcessingDiagnostics<TDefinition> diagnostics)
    {
        Diagnostics = diagnostics;
    }

    public override void OnSuccessfulProcess(TContext context, TDefinition definition, TProduct product)
    {
        context.ReservedUnits.Add(definition.Name!);
        context.ReservedUnitPlurals.Add(definition.ParsingData.InterpretedPlural!);
    }

    protected IValidityWithDiagnostics CheckUnitValidity(TContext context, TDefinition definition)
    {
        return IterativeValidity.DiagnoseAndMergeWhileValid(context, definition, CheckNameValidity, CheckPluralValidity);
    }

    private IValidityWithDiagnostics CheckNameValidity(TContext context, TDefinition definition)
    {
        if (definition.Name is null)
        {
            return ValidityWithDiagnostics.Invalid(Diagnostics.NullUnitName(context, definition));
        }

        if (definition.Name.Length is 0)
        {
            return ValidityWithDiagnostics.Invalid(Diagnostics.EmptyUnitName(context, definition));
        }

        if (context.ReservedUnits.Contains(definition.Name))
        {
            return ValidityWithDiagnostics.Invalid(Diagnostics.DuplicateUnitName(context, definition));
        }

        return ValidityWithDiagnostics.Valid;
    }

    private IValidityWithDiagnostics CheckPluralValidity(TContext context, TDefinition definition)
    {
        if (definition.ParsingData.InterpretedPlural is null)
        {
            if (definition.Plural is null)
            {
                return ValidityWithDiagnostics.Invalid(Diagnostics.NullUnitPluralForm(context, definition));
            }

            if (definition.Plural.Length is 0)
            {
                return ValidityWithDiagnostics.Invalid(Diagnostics.EmptyUnitPluralForm(context, definition));
            }

            return ValidityWithDiagnostics.Invalid(Diagnostics.InvalidUnitPluralForm(context, definition));
        }

        if (context.ReservedUnitPlurals.Contains(definition.ParsingData.InterpretedPlural))
        {
            return ValidityWithDiagnostics.Invalid(Diagnostics.DuplicateUnitPluralForm(context, definition));
        }

        return ValidityWithDiagnostics.Valid;
    }
}
