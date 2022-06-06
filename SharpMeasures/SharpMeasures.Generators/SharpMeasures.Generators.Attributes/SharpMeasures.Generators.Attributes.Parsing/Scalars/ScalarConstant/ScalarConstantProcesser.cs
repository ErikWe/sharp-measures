namespace SharpMeasures.Generators.Attributes.Parsing.Scalars;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;

using System;
using System.Collections.Generic;
using System.Linq;

public interface IScalarConstantDiagnostics
{
    public abstract Diagnostic? NullName(IScalarConstantProcessingContext context, RawScalarConstant definition);
    public abstract Diagnostic? EmptyName(IScalarConstantProcessingContext context, RawScalarConstant definition);
    public abstract Diagnostic? DuplicateName(IScalarConstantProcessingContext context, RawScalarConstant definition);
    public abstract Diagnostic? NullUnit(IScalarConstantProcessingContext context, RawScalarConstant definition);
    public abstract Diagnostic? EmptyUnit(IScalarConstantProcessingContext context, RawScalarConstant definition);
    public abstract Diagnostic? NullMultiplesName(IScalarConstantProcessingContext context, RawScalarConstant definition);
    public abstract Diagnostic? EmptyMultiplesName(IScalarConstantProcessingContext context, RawScalarConstant definition);
    public abstract Diagnostic? InvalidMultiplesName(IScalarConstantProcessingContext context, RawScalarConstant definition);
    public abstract Diagnostic? DuplicateMultiplesName(IScalarConstantProcessingContext context, RawScalarConstant definition);
}

public interface IScalarConstantProcessingContext : IProcessingContext
{
    public abstract NamedType Unit { get; }

    public abstract HashSet<string> ReservedConstants { get; }
    public abstract HashSet<string> ReservedConstantMultiples { get; }
}

public class ScalarConstantProcesser : AActionableProcesser<IScalarConstantProcessingContext, RawScalarConstant, ScalarConstant>
{
    private IScalarConstantDiagnostics Diagnostics { get; }

    public ScalarConstantProcesser(IScalarConstantDiagnostics diagnostics)
    {
        Diagnostics = diagnostics;
    }

    public override void OnSuccessfulProcess(IScalarConstantProcessingContext context, RawScalarConstant definition, ScalarConstant product)
    {
        if (context is null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        if (product is null)
        {
            throw new ArgumentNullException(nameof(product));
        }

        context.ReservedConstants.Add(product.Name);

        if (product.MultiplesName is not null)
        {
            context.ReservedConstantMultiples.Add(product.MultiplesName);
        }
    }

    public override IOptionalWithDiagnostics<ScalarConstant> Process(IScalarConstantProcessingContext context, RawScalarConstant definition)
    {
        if (context is null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        if (definition is null)
        {
            throw new ArgumentNullException(nameof(definition));
        }

        var validity = CheckValidity(context, definition);
        IEnumerable<Diagnostic> allDiagnostics = validity.Diagnostics;

        if (validity.IsInvalid)
        {
            return OptionalWithDiagnostics.Empty<ScalarConstant>(allDiagnostics);
        }

        var product = ProcessDefinition(context, definition);
        allDiagnostics = allDiagnostics.Concat(product);

        return product.ReplaceDiagnostics(allDiagnostics);
    }

    private IResultWithDiagnostics<ScalarConstant> ProcessDefinition(IScalarConstantProcessingContext context, RawScalarConstant definition)
    {
        var processedMultiplesPropertyData = ProcessMultiplesPropertyData(context, definition);

        ScalarConstant product = new(definition.Name!, definition.Unit!, definition.Value, processedMultiplesPropertyData.Result.Generate,
            processedMultiplesPropertyData.Result.Name, definition.Locations);

        return ResultWithDiagnostics.Construct(product, processedMultiplesPropertyData.Diagnostics);
    }

    private IResultWithDiagnostics<(bool Generate, string? Name)> ProcessMultiplesPropertyData(IScalarConstantProcessingContext context,
        RawScalarConstant definition)
    {
        var processedMultiplesName = ProcessMultiplesName(context, definition);

        if (processedMultiplesName.LacksResult)
        {
            return ResultWithDiagnostics.Construct<(bool, string?)>((false, null), processedMultiplesName.Diagnostics);
        }

        return ResultWithDiagnostics.Construct<(bool, string?)>((true, processedMultiplesName.Result), processedMultiplesName.Diagnostics);
    }

    private IOptionalWithDiagnostics<string> ProcessMultiplesName(IScalarConstantProcessingContext context, RawScalarConstant definition)
    {
        if (definition.Locations.ExplicitlySetMultiplesName is false || definition.GenerateMultiplesProperty is false)
        {
            return OptionalWithDiagnostics.EmptyWithoutDiagnostics<string>();
        }

        if (definition.ParsingData.InterpretedMultiplesName is null)
        {
            if (definition.MultiplesName is null)
            {
                return OptionalWithDiagnostics.Empty<string>(Diagnostics.NullMultiplesName(context, definition));
            }

            if (definition.MultiplesName.Length is 0)
            {
                return OptionalWithDiagnostics.Empty<string>(Diagnostics.EmptyMultiplesName(context, definition));
            }

            return OptionalWithDiagnostics.Empty<string>(Diagnostics.InvalidMultiplesName(context, definition));
        }

        if (context.ReservedConstantMultiples.Contains(definition.ParsingData.InterpretedMultiplesName))
        {
            return OptionalWithDiagnostics.Empty<string>(Diagnostics.DuplicateMultiplesName(context, definition));
        }

        return OptionalWithDiagnostics.Result(definition.ParsingData.InterpretedMultiplesName);
    }

    private IValidityWithDiagnostics CheckValidity(IScalarConstantProcessingContext context, RawScalarConstant definition)
    {
        return IterativeValidity.DiagnoseAndMergeWhileValid(context, definition, CheckNameValidity, CheckUnitValidity);
    }

    private IValidityWithDiagnostics CheckNameValidity(IScalarConstantProcessingContext context, RawScalarConstant definition)
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

    private IValidityWithDiagnostics CheckUnitValidity(IScalarConstantProcessingContext context, RawScalarConstant definition)
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
