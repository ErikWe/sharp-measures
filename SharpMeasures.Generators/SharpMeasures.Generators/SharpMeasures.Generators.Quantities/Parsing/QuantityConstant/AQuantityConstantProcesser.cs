namespace SharpMeasures.Generators.Quantities.Parsing.QuantityConstant;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Attributes.Parsing.Utility;
using SharpMeasures.Generators.Diagnostics;

using System.Collections.Generic;

public interface IQuantityConstantProcessingDiagnostics<TDefinition, TLocations>
    where TDefinition : ARawQuantityConstantDefinition<TDefinition, TLocations>
    where TLocations : AQuantityConstantLocations<TLocations>
{
    public abstract Diagnostic? NullName(IQuantityConstantProcessingContext context, TDefinition definition);
    public abstract Diagnostic? EmptyName(IQuantityConstantProcessingContext context, TDefinition definition);
    public abstract Diagnostic? DuplicateName(IQuantityConstantProcessingContext context, TDefinition definition);
    public abstract Diagnostic? NameReservedByMultiples(IQuantityConstantProcessingContext context, TDefinition definition);
    public abstract Diagnostic? NullUnit(IQuantityConstantProcessingContext context, TDefinition definition);
    public abstract Diagnostic? EmptyUnit(IQuantityConstantProcessingContext context, TDefinition definition);
    public abstract Diagnostic? NullMultiples(IQuantityConstantProcessingContext context, TDefinition definition);
    public abstract Diagnostic? EmptyMultiples(IQuantityConstantProcessingContext context, TDefinition definition);
    public abstract Diagnostic? InvalidMultiples(IQuantityConstantProcessingContext context, TDefinition definition);
    public abstract Diagnostic? DuplicateMultiples(IQuantityConstantProcessingContext context, TDefinition definition, string interpretedMultiples);
    public abstract Diagnostic? MultiplesReservedByName(IQuantityConstantProcessingContext context, TDefinition definition, string interpretedMultiples);
    public abstract Diagnostic? NameAndMultiplesIdentical(IQuantityConstantProcessingContext context, TDefinition definition);
    public abstract Diagnostic? MultiplesDisabledButNameSpecified(IQuantityConstantProcessingContext context, TDefinition definition);
}

public interface IQuantityConstantProcessingContext : IProcessingContext
{
    public abstract HashSet<string> ReservedConstants { get; }
    public abstract HashSet<string> ReservedConstantMultiples { get; }
}

public abstract class AQuantityConstantProcesser<TContext, TDefinition, TLocations, TProduct> : AActionableProcesser<TContext, TDefinition, TProduct>
    where TContext : IQuantityConstantProcessingContext
    where TDefinition : ARawQuantityConstantDefinition<TDefinition, TLocations>
    where TLocations : AQuantityConstantLocations<TLocations>
    where TProduct : AUnresolvedQuantityConstantDefinition<TLocations>
{
    private IQuantityConstantProcessingDiagnostics<TDefinition, TLocations> Diagnostics { get; }

    protected AQuantityConstantProcesser(IQuantityConstantProcessingDiagnostics<TDefinition, TLocations> diagnostics)
    {
        Diagnostics = diagnostics;
    }

    public override void OnSuccessfulProcess(TContext context, TDefinition definition, TProduct product)
    {
        context.ReservedConstants.Add(product.Name);

        if (product.Multiples is not null)
        {
            context.ReservedConstantMultiples.Add(product.Multiples);
        }
    }

    protected virtual bool VerifyRequiredPropertiesSet(TDefinition definition)
    {
        return definition.Locations.ExplicitlySetName && definition.Locations.ExplicitlySetUnit;
    }

    protected IResultWithDiagnostics<(bool Generate, string? Name)> ProcessMultiplesPropertyData(TContext context, TDefinition definition)
    {
        var processedMultiplesName = ProcessMultiples(context, definition);

        if (processedMultiplesName.LacksResult)
        {
            return ResultWithDiagnostics.Construct<(bool, string?)>((false, null), processedMultiplesName.Diagnostics);
        }

        return ResultWithDiagnostics.Construct<(bool, string?)>((true, processedMultiplesName.Result), processedMultiplesName.Diagnostics);
    }

    private IOptionalWithDiagnostics<string> ProcessMultiples(TContext context, TDefinition definition)
    {
        if (definition.Locations.ExplicitlySetGenerateMultiplesProperty && definition.GenerateMultiplesProperty is false)
        {
            if (definition.Locations.ExplicitlySetMultiples)
            {
                return OptionalWithDiagnostics.Empty<string>(Diagnostics.MultiplesDisabledButNameSpecified(context, definition));
            }

            return OptionalWithDiagnostics.Empty<string>();
        }

        if (definition.Multiples is null)
        {
            return OptionalWithDiagnostics.Empty<string>(Diagnostics.NullMultiples(context, definition));
        }

        if (definition.Multiples.Length is 0)
        {
            return OptionalWithDiagnostics.Empty<string>(Diagnostics.EmptyMultiples(context, definition));
        }

        string? interpretedPlural = SimpleTextExpression.Interpret(definition.Name, definition.Multiples);

        if (interpretedPlural is null)
        {
            return OptionalWithDiagnostics.Empty<string>(Diagnostics.InvalidMultiples(context, definition));
        }

        if (context.ReservedConstantMultiples.Contains(interpretedPlural))
        {
            return OptionalWithDiagnostics.Empty<string>(Diagnostics.DuplicateMultiples(context, definition, interpretedPlural));
        }

        if (context.ReservedConstants.Contains(interpretedPlural))
        {
            return OptionalWithDiagnostics.Empty<string>(Diagnostics.MultiplesReservedByName(context, definition, interpretedPlural));
        }

        if (definition.Name == interpretedPlural)
        {
            return OptionalWithDiagnostics.Empty<string>(Diagnostics.NameAndMultiplesIdentical(context, definition));
        }

        return OptionalWithDiagnostics.Result(interpretedPlural);
    }

    protected virtual IValidityWithDiagnostics CheckValidity(TContext context, TDefinition definition)
    {
        return IterativeValidity.DiagnoseAndMergeWhileValid(context, definition, CheckNameValidity, CheckUnitValidity);
    }

    private IValidityWithDiagnostics CheckNameValidity(TContext context, TDefinition definition)
    {
        if (definition.Name is null)
        {
            return ValidityWithDiagnostics.Invalid(Diagnostics.NullName(context, definition));
        }

        if (definition.Name.Length is 0)
        {
            return ValidityWithDiagnostics.Invalid(Diagnostics.EmptyName(context, definition));
        }

        if (context.ReservedConstants.Contains(definition.Name))
        {
            return ValidityWithDiagnostics.Invalid(Diagnostics.DuplicateName(context, definition));
        }

        if (context.ReservedConstantMultiples.Contains(definition.Name))
        {
            return ValidityWithDiagnostics.Invalid(Diagnostics.NameReservedByMultiples(context, definition));
        }

        return ValidityWithDiagnostics.Valid;
    }

    private IValidityWithDiagnostics CheckUnitValidity(TContext context, TDefinition definition)
    {
        if (definition.Unit is null)
        {
            return ValidityWithDiagnostics.Invalid(Diagnostics.NullUnit(context, definition));
        }

        if (definition.Unit.Length is 0)
        {
            return ValidityWithDiagnostics.Invalid(Diagnostics.EmptyUnit(context, definition));
        }

        return ValidityWithDiagnostics.Valid;
    }
}
