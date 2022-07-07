namespace SharpMeasures.Generators.Scalars.Parsing.ScalarConstant;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;

using System.Collections.Generic;
using System.Linq;

internal interface IScalarConstantDiagnostics
{
    public abstract Diagnostic? NullName(IScalarConstantProcessingContext context, RawScalarConstantDefinition definition);
    public abstract Diagnostic? EmptyName(IScalarConstantProcessingContext context, RawScalarConstantDefinition definition);
    public abstract Diagnostic? DuplicateName(IScalarConstantProcessingContext context, RawScalarConstantDefinition definition);
    public abstract Diagnostic? NullUnit(IScalarConstantProcessingContext context, RawScalarConstantDefinition definition);
    public abstract Diagnostic? EmptyUnit(IScalarConstantProcessingContext context, RawScalarConstantDefinition definition);
    public abstract Diagnostic? NullMultiples(IScalarConstantProcessingContext context, RawScalarConstantDefinition definition);
    public abstract Diagnostic? EmptyMultiples(IScalarConstantProcessingContext context, RawScalarConstantDefinition definition);
    public abstract Diagnostic? InvalidMultiples(IScalarConstantProcessingContext context, RawScalarConstantDefinition definition);
    public abstract Diagnostic? DuplicateMultiples(IScalarConstantProcessingContext context, RawScalarConstantDefinition definition);
    public abstract Diagnostic? MultiplesDisabledButNameSpecified(IScalarConstantProcessingContext context, RawScalarConstantDefinition definition);
}

internal interface IScalarConstantProcessingContext : IProcessingContext
{
    public abstract HashSet<string> ReservedConstants { get; }
    public abstract HashSet<string> ReservedConstantMultiples { get; }
}

internal class ScalarConstantProcesser : AActionableProcesser<IScalarConstantProcessingContext, RawScalarConstantDefinition, ScalarConstantDefinition>
{
    private IScalarConstantDiagnostics Diagnostics { get; }

    public ScalarConstantProcesser(IScalarConstantDiagnostics diagnostics)
    {
        Diagnostics = diagnostics;
    }

    public override void OnSuccessfulProcess(IScalarConstantProcessingContext context, RawScalarConstantDefinition definition, ScalarConstantDefinition product)
    {
        context.ReservedConstants.Add(product.Name);

        if (product.Multiples is not null)
        {
            context.ReservedConstantMultiples.Add(product.Multiples);
        }
    }

    public override IOptionalWithDiagnostics<ScalarConstantDefinition> Process(IScalarConstantProcessingContext context, RawScalarConstantDefinition definition)
    {
        if (VerifyRequiredPropertiesSet(definition) is false)
        {
            return OptionalWithDiagnostics.Empty<ScalarConstantDefinition>();
        }

        var validity = CheckValidity(context, definition);
        IEnumerable<Diagnostic> allDiagnostics = validity.Diagnostics;

        if (validity.IsInvalid)
        {
            return OptionalWithDiagnostics.Empty<ScalarConstantDefinition>(allDiagnostics);
        }

        var product = ProcessDefinition(context, definition);
        allDiagnostics = allDiagnostics.Concat(product);

        return product.ReplaceDiagnostics(allDiagnostics);
    }

    private static bool VerifyRequiredPropertiesSet(RawScalarConstantDefinition definition)
    {
        return definition.Locations.ExplicitlySetName && definition.Locations.ExplicitlySetUnit && definition.Locations.ExplicitlySetValue;
    }

    private IResultWithDiagnostics<ScalarConstantDefinition> ProcessDefinition(IScalarConstantProcessingContext context, RawScalarConstantDefinition definition)
    {
        var processedMultiplesPropertyData = ProcessMultiplesPropertyData(context, definition);

        ScalarConstantDefinition product = new(definition.Name!, definition.Unit!, definition.Value, processedMultiplesPropertyData.Result.Generate,
            processedMultiplesPropertyData.Result.Name, definition.Locations);

        return ResultWithDiagnostics.Construct(product, processedMultiplesPropertyData.Diagnostics);
    }

    private IResultWithDiagnostics<(bool Generate, string? Name)> ProcessMultiplesPropertyData(IScalarConstantProcessingContext context,
        RawScalarConstantDefinition definition)
    {
        var processedMultiplesName = ProcessMultiples(context, definition);

        if (processedMultiplesName.LacksResult)
        {
            return ResultWithDiagnostics.Construct<(bool, string?)>((false, null), processedMultiplesName.Diagnostics);
        }

        return ResultWithDiagnostics.Construct<(bool, string?)>((true, processedMultiplesName.Result), processedMultiplesName.Diagnostics);
    }

    private IOptionalWithDiagnostics<string> ProcessMultiples(IScalarConstantProcessingContext context, RawScalarConstantDefinition definition)
    {
        if (definition.Locations.ExplicitlySetGenerateMultiplesProperty && definition.GenerateMultiplesProperty is false)
        {
            if (definition.Locations.ExplicitlySetMultiples)
            {
                return OptionalWithDiagnostics.Empty<string>(Diagnostics.MultiplesDisabledButNameSpecified(context, definition));
            }

            return OptionalWithDiagnostics.Empty<string>();
        }

        if (definition.ParsingData.InterpretedMultiples is null)
        {
            if (definition.Multiples is null)
            {
                return OptionalWithDiagnostics.Empty<string>(Diagnostics.NullMultiples(context, definition));
            }

            if (definition.Multiples.Length is 0)
            {
                return OptionalWithDiagnostics.Empty<string>(Diagnostics.EmptyMultiples(context, definition));
            }

            return OptionalWithDiagnostics.Empty<string>(Diagnostics.InvalidMultiples(context, definition));
        }

        if (context.ReservedConstantMultiples.Contains(definition.ParsingData.InterpretedMultiples))
        {
            return OptionalWithDiagnostics.Empty<string>(Diagnostics.DuplicateMultiples(context, definition));
        }

        return OptionalWithDiagnostics.Result(definition.ParsingData.InterpretedMultiples);
    }

    private IValidityWithDiagnostics CheckValidity(IScalarConstantProcessingContext context, RawScalarConstantDefinition definition)
    {
        return IterativeValidity.DiagnoseAndMergeWhileValid(context, definition, CheckNameValidity, CheckUnitValidity);
    }

    private IValidityWithDiagnostics CheckNameValidity(IScalarConstantProcessingContext context, RawScalarConstantDefinition definition)
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

        return ValidityWithDiagnostics.Valid;
    }

    private IValidityWithDiagnostics CheckUnitValidity(IScalarConstantProcessingContext context, RawScalarConstantDefinition definition)
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
