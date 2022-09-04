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
    public abstract Diagnostic? NullUnitInstanceName(IQuantityConstantProcessingContext context, TDefinition definition);
    public abstract Diagnostic? EmptyUnitInstanceName(IQuantityConstantProcessingContext context, TDefinition definition);
    public abstract Diagnostic? NullMultiples(IQuantityConstantProcessingContext context, TDefinition definition);
    public abstract Diagnostic? EmptyMultiples(IQuantityConstantProcessingContext context, TDefinition definition);
    public abstract Diagnostic? InvalidMultiples(IQuantityConstantProcessingContext context, TDefinition definition);
    public abstract Diagnostic? DuplicateMultiples(IQuantityConstantProcessingContext context, TDefinition definition, string interpretedMultiples);
    public abstract Diagnostic? MultiplesReservedByName(IQuantityConstantProcessingContext context, TDefinition definition, string interpretedMultiples);
    public abstract Diagnostic? NameAndMultiplesIdentical(IQuantityConstantProcessingContext context, TDefinition definition);
    public abstract Diagnostic? MultiplesDisabledButNameSpecified(IQuantityConstantProcessingContext context, TDefinition definition);
    public abstract Diagnostic? SetRegexSubstitutionButNotPattern(IQuantityConstantProcessingContext context, TDefinition definition);
}

public interface IQuantityConstantProcessingContext : IProcessingContext
{
    public abstract HashSet<string> ReservedConstantNames { get; }
    public abstract HashSet<string> ReservedConstantMultiples { get; }
}

public abstract class AQuantityConstantProcesser<TContext, TDefinition, TLocations, TProduct> : AActionableProcesser<TContext, TDefinition, TProduct>
    where TContext : IQuantityConstantProcessingContext
    where TDefinition : ARawQuantityConstantDefinition<TDefinition, TLocations>
    where TLocations : AQuantityConstantLocations<TLocations>
    where TProduct : AQuantityConstantDefinition<TLocations>
{
    private IQuantityConstantProcessingDiagnostics<TDefinition, TLocations> Diagnostics { get; }

    protected AQuantityConstantProcesser(IQuantityConstantProcessingDiagnostics<TDefinition, TLocations> diagnostics)
    {
        Diagnostics = diagnostics;
    }

    public override void OnSuccessfulProcess(TContext context, TDefinition definition, TProduct product)
    {
        context.ReservedConstantNames.Add(product.Name);

        if (product.Multiples is not null)
        {
            context.ReservedConstantMultiples.Add(product.Multiples);
        }
    }

    protected virtual IValidityWithDiagnostics VerifyRequiredPropertiesSet(TDefinition definition)
    {
        var requiredPropertiesSet = definition.Locations.ExplicitlySetName && definition.Locations.ExplicitlySetUnitInstanceName;

        return ValidityWithDiagnostics.ConditionalWithoutDiagnostics(requiredPropertiesSet);
    }

    protected IResultWithDiagnostics<(bool Generate, string? Name)> ProcessMultiplesPropertyData(TContext context, TDefinition definition)
    {
        var processedMultiplesName = ProcessMultiples(context, definition);

        if (processedMultiplesName.LacksResult || definition.GenerateMultiplesProperty is false)
        {
            return ResultWithDiagnostics.Construct<(bool, string?)>((false, null), processedMultiplesName.Diagnostics);
        }

        return ResultWithDiagnostics.Construct<(bool, string?)>((true, processedMultiplesName.Result), processedMultiplesName.Diagnostics);
    }

    protected virtual IValidityWithDiagnostics Validate(TContext context, TDefinition definition)
    {
        return ValidateName(context, definition)
            .Validate(() => ValidateUnitInstanceName(context, definition));
    }

    private IValidityWithDiagnostics ValidateName(TContext context, TDefinition definition)
    {
        return ValidateNameNotNull(context, definition)
            .Validate(() => ValidateNameNotEmpty(context, definition))
            .Validate(() => ValidateNameNotDuplicate(context, definition))
            .Validate(() => ValidateNameNotReservedByMultiples(context, definition));
    }

    private IValidityWithDiagnostics ValidateNameNotNull(TContext context, TDefinition definition)
    {
        return ValidityWithDiagnostics.Conditional(definition.Name is not null, () => Diagnostics.NullName(context, definition));
    }

    private IValidityWithDiagnostics ValidateNameNotEmpty(TContext context, TDefinition definition)
    {
        return ValidityWithDiagnostics.Conditional(definition.Name!.Length is not 0, () => Diagnostics.EmptyName(context, definition));
    }

    private IValidityWithDiagnostics ValidateNameNotDuplicate(TContext context, TDefinition definition)
    {
        var nameDuplicate = context.ReservedConstantNames.Contains(definition.Name!);

        return ValidityWithDiagnostics.Conditional(nameDuplicate is false, () => Diagnostics.DuplicateName(context, definition));
    }

    private IValidityWithDiagnostics ValidateNameNotReservedByMultiples(TContext context, TDefinition definition)
    {
        var nameReservedByMultiples = context.ReservedConstantMultiples.Contains(definition.Name!);

        return ValidityWithDiagnostics.Conditional(nameReservedByMultiples is false, () => Diagnostics.NameReservedByMultiples(context, definition));
    }

    private IValidityWithDiagnostics ValidateUnitInstanceName(TContext context, TDefinition definition)
    {
        return ValidateUnitInstanceNameNotNull(context, definition)
            .Validate(() => ValidateUnitInstanceNameNotEmpty(context, definition));
    }

    private IValidityWithDiagnostics ValidateUnitInstanceNameNotNull(TContext context, TDefinition definition)
    {
        return ValidityWithDiagnostics.Conditional(definition.UnitInstanceName is not null, () => Diagnostics.NullUnitInstanceName(context, definition));
    }

    private IValidityWithDiagnostics ValidateUnitInstanceNameNotEmpty(TContext context, TDefinition definition)
    {
        return ValidityWithDiagnostics.Conditional(definition.UnitInstanceName!.Length is not 0, () => Diagnostics.EmptyUnitInstanceName(context, definition));
    }

    private IOptionalWithDiagnostics<string> ProcessMultiples(TContext context, TDefinition definition)
    {
        return ValidateMultiples(context, definition)
            .Transform(() => InterpretMultiples(definition))
            .Merge((interpretedMultiples) => ProcessInterpretedMultiples(context, definition, interpretedMultiples))
            .Validate((interpretedMultiples) => ValidateInterpretedMultiples(context, definition, interpretedMultiples));
    }

    private IValidityWithDiagnostics ValidateMultiples(TContext context, TDefinition definition)
    {
        return ValidateMultiplesNotUnnecessesarilySet(context, definition)
            .Validate(() => ValidateMultiplesRegexSubstitutionNotUnnecessarilySet(context, definition))
            .Validate(() => ValidateMultiplesNotNull(context, definition))
            .Validate(() => ValidateMultiplesNotEmpty(context, definition));
    }

    private IValidityWithDiagnostics ValidateMultiplesNotUnnecessesarilySet(TContext context, TDefinition definition)
    {
        var multiplesUnnecessarilySet = definition.Locations.ExplicitlySetGenerateMultiplesProperty && definition.GenerateMultiplesProperty is false && definition.Locations.ExplicitlySetMultiples;

        return ValidityWithDiagnostics.Conditional(multiplesUnnecessarilySet is false, () => Diagnostics.MultiplesDisabledButNameSpecified(context, definition));
    }

    private IValidityWithDiagnostics ValidateMultiplesRegexSubstitutionNotUnnecessarilySet(TContext context, TDefinition definition)
    {
        var multiplesRegexSubstitutionUnnecessarilySet = definition.Locations.ExplicitlySetMultiples is false && definition.Locations.ExplicitlySetMultiplesRegexSubstitution
            && definition.MultiplesRegexSubstitution is not null && definition.MultiplesRegexSubstitution.Length > 0;

        return ValidityWithDiagnostics.Conditional(multiplesRegexSubstitutionUnnecessarilySet is false, () => Diagnostics.SetRegexSubstitutionButNotPattern(context, definition));
    }

    private IValidityWithDiagnostics ValidateMultiplesNotNull(TContext context, TDefinition definition)
    {
        var multiplesNull = definition.Locations.ExplicitlySetMultiples && definition.Multiples is null;

        return ValidityWithDiagnostics.Conditional(multiplesNull is false, () => Diagnostics.NullMultiples(context, definition));
    }

    private IValidityWithDiagnostics ValidateMultiplesNotEmpty(TContext context, TDefinition definition)
    {
        var multiplesEmpty = definition.Locations.ExplicitlySetMultiples && definition.Multiples!.Length is 0;

        return ValidityWithDiagnostics.Conditional(multiplesEmpty is false, () => Diagnostics.EmptyMultiples(context, definition));
    }

    private static string? InterpretMultiples(TDefinition definition)
    {
        if (definition.Multiples is null || definition.Multiples.Length is 0)
        {
            return definition.Name;
        }

        if (definition.MultiplesRegexSubstitution is null || definition.MultiplesRegexSubstitution.Length is 0)
        {
            return SimpleTextExpression.Interpret(definition.Name!, definition.Multiples);
        }

        return SimpleTextExpression.Interpret(definition.Name!, definition.Multiples, definition.MultiplesRegexSubstitution);
    }

    private IOptionalWithDiagnostics<string> ProcessInterpretedMultiples(TContext context, TDefinition definition, string? interpretedMultiples)
    {
        return OptionalWithDiagnostics.Conditional(interpretedMultiples is not null, interpretedMultiples!, () => Diagnostics.InvalidMultiples(context, definition));
    }

    private IValidityWithDiagnostics ValidateInterpretedMultiples(TContext context, TDefinition definition, string interpretedMultiples)
    {
        return ValidateMultiplesNotDuplicate(context, definition, interpretedMultiples)
            .Validate(() => ValidateMultiplesNotReservedByName(context, definition, interpretedMultiples))
            .Validate(() => ValidateNameAndMultiplesNotIdentical(context, definition, interpretedMultiples));
    }

    private IValidityWithDiagnostics ValidateMultiplesNotDuplicate(TContext context, TDefinition definition, string interpretedMultiples)
    {
        var multiplesDuplicate = context.ReservedConstantMultiples.Contains(interpretedMultiples);

        return ValidityWithDiagnostics.Conditional(multiplesDuplicate is false, () => Diagnostics.DuplicateMultiples(context, definition, interpretedMultiples));
    }

    private IValidityWithDiagnostics ValidateMultiplesNotReservedByName(TContext context, TDefinition definition, string interpretedMultiples)
    {
        var multiplesReservedByName = context.ReservedConstantNames.Contains(interpretedMultiples);

        return ValidityWithDiagnostics.Conditional(multiplesReservedByName is false, () => Diagnostics.MultiplesReservedByName(context, definition, interpretedMultiples));
    }

    private IValidityWithDiagnostics ValidateNameAndMultiplesNotIdentical(TContext context, TDefinition definition, string interpretedMultiples)
    {
        var nameAndMultiplesIdentical = definition.Name == interpretedMultiples;

        return ValidityWithDiagnostics.Conditional(nameAndMultiplesIdentical is false, () => Diagnostics.NameAndMultiplesIdentical(context, definition));
    }
}
