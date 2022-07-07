namespace SharpMeasures.Generators.Units.Parsing.Abstractions;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Attributes.Parsing.Utility;
using SharpMeasures.Generators.Diagnostics;

using System.Collections.Generic;

internal interface IUnitProcessingDiagnostics<in TDefinition, in TLocations>
    where TDefinition : IRawUnitDefinition<TLocations>
    where TLocations : IUnitLocations
{
    public abstract Diagnostic? NullUnitName(IUnitProcessingContext context, TDefinition definition);
    public abstract Diagnostic? EmptyUnitName(IUnitProcessingContext context, TDefinition definition);
    public abstract Diagnostic? DuplicateUnitName(IUnitProcessingContext context, TDefinition definition);

    public abstract Diagnostic? NullUnitPluralForm(IUnitProcessingContext context, TDefinition definition);
    public abstract Diagnostic? EmptyUnitPluralForm(IUnitProcessingContext context, TDefinition definition);
    public abstract Diagnostic? InvalidUnitPluralForm(IUnitProcessingContext context, TDefinition definition);
    public abstract Diagnostic? DuplicateUnitPluralForm(IUnitProcessingContext context, TDefinition definition, string interpretedPlural);
}

internal interface IUnitProcessingContext : IProcessingContext
{
    public abstract HashSet<string> ReservedUnits { get; }
    public abstract HashSet<string> ReservedUnitPlurals { get; }
}

internal abstract class AUnitProcesser<TContext, TDefinition, TLocations, TProduct> : AActionableProcesser<TContext, TDefinition, TProduct>
    where TContext : IUnitProcessingContext
    where TDefinition : IRawUnitDefinition<TLocations>
    where TLocations : IUnitLocations
    where TProduct : IUnresolvedUnitDefinition<TLocations>
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

    protected virtual bool VerifyRequiredPropertiesSet(TDefinition definition)
    {
        return definition.Locations.ExplicitlySetName && definition.Locations.ExplicitlySetPlural;
    }

    protected IValidityWithDiagnostics CheckUnitValidity(TContext context, TDefinition definition)
    {
        return CheckNameValidity(context, definition);
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

    protected IOptionalWithDiagnostics<string> ProcessPlural(TContext context, TDefinition definition)
    {
        if (definition.Plural is null)
        {
            return OptionalWithDiagnostics.Empty<string>(Diagnostics.NullUnitPluralForm(context, definition));
        }

        if (definition.Plural.Length is 0)
        {
            return OptionalWithDiagnostics.Empty<string>(Diagnostics.EmptyUnitPluralForm(context, definition));
        }

        string? interpretedPlural = SimpleTextExpression.Interpret(definition.Name, definition.Plural);

        if (interpretedPlural is null)
        {
            return OptionalWithDiagnostics.Empty<string>(Diagnostics.InvalidUnitPluralForm(context, definition));
        }

        if (context.ReservedUnitPlurals.Contains(interpretedPlural))
        {
            return OptionalWithDiagnostics.Empty<string>(Diagnostics.DuplicateUnitPluralForm(context, definition, interpretedPlural));
        }

        return OptionalWithDiagnostics.Result(interpretedPlural);
    }
}
