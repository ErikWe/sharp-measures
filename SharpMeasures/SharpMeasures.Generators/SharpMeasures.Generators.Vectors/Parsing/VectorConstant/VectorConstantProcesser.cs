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
    public abstract Diagnostic? InvalidValueDimension(IVectorConstantProcessingContext context, RawVectorConstantDefinition definition);
    public abstract Diagnostic? NullMultiplesName(IVectorConstantProcessingContext context, RawVectorConstantDefinition definition);
    public abstract Diagnostic? EmptyMultiplesName(IVectorConstantProcessingContext context, RawVectorConstantDefinition definition);
    public abstract Diagnostic? InvalidMultiplesName(IVectorConstantProcessingContext context, RawVectorConstantDefinition definition);
    public abstract Diagnostic? DuplicateMultiplesName(IVectorConstantProcessingContext context, RawVectorConstantDefinition definition);
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

        if (product.MultiplesName is not null)
        {
            context.ReservedConstantMultiples.Add(product.MultiplesName);
        }
    }

    public override IOptionalWithDiagnostics<VectorConstantDefinition> Process(IVectorConstantProcessingContext context, RawVectorConstantDefinition definition)
    {
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

    private IResultWithDiagnostics<VectorConstantDefinition> ProcessDefinition(IVectorConstantProcessingContext context, RawVectorConstantDefinition definition)
    {
        var processedMultiplesPropertyData = ProcessMultiplesPropertyData(context, definition);

        VectorConstantDefinition product = new(definition.Name!, definition.Unit, definition.Value, processedMultiplesPropertyData.Result.Generate,
            processedMultiplesPropertyData.Result.Name, definition.Locations);

        return ResultWithDiagnostics.Construct(product, processedMultiplesPropertyData.Diagnostics);
    }

    private IResultWithDiagnostics<(bool Generate, string? Name)> ProcessMultiplesPropertyData(IVectorConstantProcessingContext context,
        RawVectorConstantDefinition definition)
    {
        var processedMultiplesName = ProcessMultiplesName(context, definition);

        if (processedMultiplesName.LacksResult)
        {
            return ResultWithDiagnostics.Construct<(bool, string?)>((false, null), processedMultiplesName.Diagnostics);
        }

        return ResultWithDiagnostics.Construct<(bool, string?)>((true, processedMultiplesName.Result), processedMultiplesName.Diagnostics);
    }

    private IOptionalWithDiagnostics<string> ProcessMultiplesName(IVectorConstantProcessingContext context, RawVectorConstantDefinition definition)
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

    private IValidityWithDiagnostics CheckValidity(IVectorConstantProcessingContext context, RawVectorConstantDefinition definition)
    {
        return IterativeValidity.DiagnoseAndMergeWhileValid(context, definition, CheckNameValidity, CheckValueValidity);
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

    private IValidityWithDiagnostics CheckValueValidity(IVectorConstantProcessingContext context, RawVectorConstantDefinition definition)
    {
        if (definition.Value.Count != context.Dimension)
        {
            return ValidityWithDiagnostics.Invalid(Diagnostics.InvalidValueDimension(context, definition));
        }

        return ValidityWithDiagnostics.Valid;
    }
}
