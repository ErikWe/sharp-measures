namespace SharpMeasures.Generators.Vectors.Parsing.VectorConstant;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;

using System.Collections.Generic;
using System.Linq;

internal interface IVectorConstantProcessingDiagnostics
{
    public abstract Diagnostic? NullName(IVectorConstantProcessingContext context, RawVectorConstantDefinition definition);
    public abstract Diagnostic? EmptyName(IVectorConstantProcessingContext context, RawVectorConstantDefinition definition);
    public abstract Diagnostic? DuplicateName(IVectorConstantProcessingContext context, RawVectorConstantDefinition definition);
    public abstract Diagnostic? NullUnit(IVectorConstantProcessingContext context, RawVectorConstantDefinition definition);
    public abstract Diagnostic? EmptyUnit(IVectorConstantProcessingContext context, RawVectorConstantDefinition definition);
    public abstract Diagnostic? InvalidValueDimension(IVectorConstantProcessingContext context, RawVectorConstantDefinition definition);
    public abstract Diagnostic? NullMultiples(IVectorConstantProcessingContext context, RawVectorConstantDefinition definition);
    public abstract Diagnostic? EmptyMultiples(IVectorConstantProcessingContext context, RawVectorConstantDefinition definition);
    public abstract Diagnostic? InvalidMultiples(IVectorConstantProcessingContext context, RawVectorConstantDefinition definition);
    public abstract Diagnostic? DuplicateMultiples(IVectorConstantProcessingContext context, RawVectorConstantDefinition definition);
    public abstract Diagnostic? MultiplesDisabledButNameSpecified(IVectorConstantProcessingContext context, RawVectorConstantDefinition definition);
}

internal interface IVectorConstantProcessingContext : IProcessingContext
{
    public abstract int Dimension { get; }

    public abstract HashSet<string> ReservedConstants { get; }
    public abstract HashSet<string> ReservedConstantMultiples { get; }
}

internal class VectorConstantProcesser : AActionableProcesser<IVectorConstantProcessingContext, RawVectorConstantDefinition, VectorConstantDefinition>
{
    private IVectorConstantProcessingDiagnostics Diagnostics { get; }

    public VectorConstantProcesser(IVectorConstantProcessingDiagnostics diagnostics)
    {
        Diagnostics = diagnostics;
    }

    public override void OnSuccessfulProcess(IVectorConstantProcessingContext context, RawVectorConstantDefinition definition, VectorConstantDefinition product)
    {
        context.ReservedConstants.Add(product.Name);

        if (product.Multiples is not null)
        {
            context.ReservedConstantMultiples.Add(product.Multiples);
        }
    }

    public override IOptionalWithDiagnostics<VectorConstantDefinition> Process(IVectorConstantProcessingContext context, RawVectorConstantDefinition definition)
    {
        if (VerifyRequiredPropertiesSet(definition) is false)
        {
            return OptionalWithDiagnostics.Empty<VectorConstantDefinition>();
        }

        var validity = CheckValidity(context, definition);
        IEnumerable<Diagnostic> allDiagnostics = validity.Diagnostics;

        if (validity.IsInvalid)
        {
            return OptionalWithDiagnostics.Empty<VectorConstantDefinition>(allDiagnostics);
        }

        var product = ProcessDefinition(context, definition);
        allDiagnostics = allDiagnostics.Concat(product);

        return product.ReplaceDiagnostics(allDiagnostics);
    }

    private static bool VerifyRequiredPropertiesSet(RawVectorConstantDefinition definition)
    {
        return definition.Locations.ExplicitlySetName && definition.Locations.ExplicitlySetUnit && definition.Locations.ExplicitlySetValue;
    }

    private IResultWithDiagnostics<VectorConstantDefinition> ProcessDefinition(IVectorConstantProcessingContext context, RawVectorConstantDefinition definition)
    {
        var processedMultiplesPropertyData = ProcessMultiplesPropertyData(context, definition);

        VectorConstantDefinition product = new(definition.Name!, definition.Unit!, definition.Value, processedMultiplesPropertyData.Result.Generate,
            processedMultiplesPropertyData.Result.Name, definition.Locations);

        return ResultWithDiagnostics.Construct(product, processedMultiplesPropertyData.Diagnostics);
    }

    private IResultWithDiagnostics<(bool Generate, string? Name)> ProcessMultiplesPropertyData(IVectorConstantProcessingContext context,
        RawVectorConstantDefinition definition)
    {
        var processedMultiplesName = ProcessMultiples(context, definition);

        if (processedMultiplesName.LacksResult)
        {
            return ResultWithDiagnostics.Construct<(bool, string?)>((false, null), processedMultiplesName.Diagnostics);
        }

        return ResultWithDiagnostics.Construct<(bool, string?)>((true, processedMultiplesName.Result), processedMultiplesName.Diagnostics);
    }

    private IOptionalWithDiagnostics<string> ProcessMultiples(IVectorConstantProcessingContext context, RawVectorConstantDefinition definition)
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

    private IValidityWithDiagnostics CheckValidity(IVectorConstantProcessingContext context, RawVectorConstantDefinition definition)
    {
        return IterativeValidity.DiagnoseAndMergeWhileValid(context, definition, CheckNameValidity, CheckUnitValidity, CheckValueValidity);
    }

    private IValidityWithDiagnostics CheckNameValidity(IVectorConstantProcessingContext context, RawVectorConstantDefinition definition)
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

    private IValidityWithDiagnostics CheckUnitValidity(IVectorConstantProcessingContext context, RawVectorConstantDefinition definition)
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

    private IValidityWithDiagnostics CheckValueValidity(IVectorConstantProcessingContext context, RawVectorConstantDefinition definition)
    {
        if (definition.Value.Count != context.Dimension)
        {
            return ValidityWithDiagnostics.Invalid(Diagnostics.InvalidValueDimension(context, definition));
        }

        return ValidityWithDiagnostics.Valid;
    }
}
