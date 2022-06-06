namespace SharpMeasures.Generators.Attributes.Parsing.Vectors;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;

using System;
using System.Collections.Generic;
using System.Linq;

public interface IVectorConstantDiagnostics
{
    public abstract Diagnostic? NullName(IVectorConstantProcessingContext context, RawVectorConstant definition);
    public abstract Diagnostic? EmptyName(IVectorConstantProcessingContext context, RawVectorConstant definition);
    public abstract Diagnostic? DuplicateName(IVectorConstantProcessingContext context, RawVectorConstant definition);
    public abstract Diagnostic? NullUnit(IVectorConstantProcessingContext context, RawVectorConstant definition);
    public abstract Diagnostic? EmptyUnit(IVectorConstantProcessingContext context, RawVectorConstant definition);
    public abstract Diagnostic? InvalidValueDimension(IVectorConstantProcessingContext context, RawVectorConstant definition);
    public abstract Diagnostic? NullMultiplesName(IVectorConstantProcessingContext context, RawVectorConstant definition);
    public abstract Diagnostic? EmptyMultiplesName(IVectorConstantProcessingContext context, RawVectorConstant definition);
    public abstract Diagnostic? InvalidMultiplesName(IVectorConstantProcessingContext context, RawVectorConstant definition);
    public abstract Diagnostic? DuplicateMultiplesName(IVectorConstantProcessingContext context, RawVectorConstant definition);
}

public interface IVectorConstantProcessingContext : IProcessingContext
{
    public abstract int Dimension { get; }
    public abstract NamedType Unit { get; }

    public abstract HashSet<string> ReservedConstants { get; }
    public abstract HashSet<string> ReservedConstantMultiples { get; }
}

public class VectorConstantProcesser : AActionableProcesser<IVectorConstantProcessingContext, RawVectorConstant, VectorConstant>
{
    private IVectorConstantDiagnostics Diagnostics { get; }

    public VectorConstantProcesser(IVectorConstantDiagnostics diagnostics)
    {
        Diagnostics = diagnostics;
    }

    public override void OnSuccessfulProcess(IVectorConstantProcessingContext context, RawVectorConstant definition, VectorConstant product)
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

    public override IOptionalWithDiagnostics<VectorConstant> Process(IVectorConstantProcessingContext context, RawVectorConstant definition)
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
            return OptionalWithDiagnostics.Empty<VectorConstant>(allDiagnostics);
        }

        var product = ProcessDefinition(context, definition);
        allDiagnostics = allDiagnostics.Concat(product);

        return product.ReplaceDiagnostics(allDiagnostics);
    }

    private IResultWithDiagnostics<VectorConstant> ProcessDefinition(IVectorConstantProcessingContext context, RawVectorConstant definition)
    {
        var processedMultiplesPropertyData = ProcessMultiplesPropertyData(context, definition);

        VectorConstant product = new(definition.Name!, definition.Unit!, definition.Value, processedMultiplesPropertyData.Result.Generate,
            processedMultiplesPropertyData.Result.Name, definition.Locations);

        return ResultWithDiagnostics.Construct(product, processedMultiplesPropertyData.Diagnostics);
    }

    private IResultWithDiagnostics<(bool Generate, string? Name)> ProcessMultiplesPropertyData(IVectorConstantProcessingContext context,
        RawVectorConstant definition)
    {
        var processedMultiplesName = ProcessMultiplesName(context, definition);

        if (processedMultiplesName.LacksResult)
        {
            return ResultWithDiagnostics.Construct<(bool, string?)>((false, null), processedMultiplesName.Diagnostics);
        }

        return ResultWithDiagnostics.Construct<(bool, string?)>((true, processedMultiplesName.Result), processedMultiplesName.Diagnostics);
    }

    private IOptionalWithDiagnostics<string> ProcessMultiplesName(IVectorConstantProcessingContext context, RawVectorConstant definition)
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

    private IValidityWithDiagnostics CheckValidity(IVectorConstantProcessingContext context, RawVectorConstant definition)
    {
        return IterativeValidity.DiagnoseAndMergeWhileValid(context, definition, CheckNameValidity, CheckUnitValidity, CheckValueValidity);
    }

    private IValidityWithDiagnostics CheckNameValidity(IVectorConstantProcessingContext context, RawVectorConstant definition)
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

    private IValidityWithDiagnostics CheckUnitValidity(IVectorConstantProcessingContext context, RawVectorConstant definition)
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

    private IValidityWithDiagnostics CheckValueValidity(IVectorConstantProcessingContext context, RawVectorConstant definition)
    {
        if (definition.Value.Count != context.Dimension)
        {
            return ValidityWithDiagnostics.Invalid(Diagnostics.InvalidValueDimension(context, definition));
        }

        return ValidityWithDiagnostics.Valid;
    }
}
