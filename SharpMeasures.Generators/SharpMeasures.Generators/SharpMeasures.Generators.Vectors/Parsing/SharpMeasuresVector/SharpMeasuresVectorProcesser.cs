﻿namespace SharpMeasures.Generators.Vectors.Parsing.SharpMeasuresVector;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;

using System.Linq;

internal interface ISharpMeasuresVectorProcessingDiagnostics
{
    public abstract Diagnostic? NullUnit(IProcessingContext context, RawSharpMeasuresVectorDefinition definition);
    public abstract Diagnostic? NullScalar(IProcessingContext context, RawSharpMeasuresVectorDefinition definition);

    public abstract Diagnostic? MissingDimension(IProcessingContext context, RawSharpMeasuresVectorDefinition definition);
    public abstract Diagnostic? InvalidDimension(IProcessingContext context, RawSharpMeasuresVectorDefinition definition);
    public abstract Diagnostic? InvalidInterpretedDimension(IProcessingContext context, RawSharpMeasuresVectorDefinition definition, int dimension);
    public abstract Diagnostic? VectorNameAndDimensionMismatch(IProcessingContext context, RawSharpMeasuresVectorDefinition definition, int interpretedDimension);

    public abstract Diagnostic? DifferenceDisabledButQuantitySpecified(IProcessingContext context, RawSharpMeasuresVectorDefinition definition);
    public abstract Diagnostic? NullDifferenceQuantity(IProcessingContext context, RawSharpMeasuresVectorDefinition definition);

    public abstract Diagnostic? NullDefaultUnit(IProcessingContext context, RawSharpMeasuresVectorDefinition definition);
    public abstract Diagnostic? EmptyDefaultUnit(IProcessingContext context, RawSharpMeasuresVectorDefinition definition);
    public abstract Diagnostic? SetDefaultSymbolButNotUnit(IProcessingContext context, RawSharpMeasuresVectorDefinition definition);
    public abstract Diagnostic? SetDefaultUnitButNotSymbol(IProcessingContext context, RawSharpMeasuresVectorDefinition definition);
}

internal class SharpMeasuresVectorProcesser : AProcesser<IProcessingContext, RawSharpMeasuresVectorDefinition, UnresolvedSharpMeasuresVectorDefinition>
{
    private ISharpMeasuresVectorProcessingDiagnostics Diagnostics { get; }

    public SharpMeasuresVectorProcesser(ISharpMeasuresVectorProcessingDiagnostics diagnostics)
    {
        Diagnostics = diagnostics;
    }

    public override IOptionalWithDiagnostics<UnresolvedSharpMeasuresVectorDefinition> Process(IProcessingContext context, RawSharpMeasuresVectorDefinition definition)
    {
        if (VerifyRequiredPropertiesSet(definition) is false)
        {
            return OptionalWithDiagnostics.Empty<UnresolvedSharpMeasuresVectorDefinition>();
        }

        var validity = CheckValidity(context, definition);
        var allDiagnostics = validity.Diagnostics;

        if (validity.IsInvalid)
        {
            return OptionalWithDiagnostics.Empty<UnresolvedSharpMeasuresVectorDefinition>(allDiagnostics);
        }

        var product = ProcessDefinition(context, definition);
        allDiagnostics = allDiagnostics.Concat(product);

        if (product.LacksResult)
        {
            return OptionalWithDiagnostics.Empty<UnresolvedSharpMeasuresVectorDefinition>(allDiagnostics);
        }

        return OptionalWithDiagnostics.Result(product.Result, allDiagnostics);
    }

    private static bool VerifyRequiredPropertiesSet(RawSharpMeasuresVectorDefinition definition)
    {
        return definition.Locations.ExplicitlySetUnit;
    }

    private IOptionalWithDiagnostics<UnresolvedSharpMeasuresVectorDefinition> ProcessDefinition(IProcessingContext context, RawSharpMeasuresVectorDefinition definition)
    {
        var processedDefaultUnitData = ProcessDefaultUnitData(context, definition);
        var allDiagnostics = processedDefaultUnitData.Diagnostics;

        var processedDimensionality = ProcessDimension(context, definition);
        allDiagnostics = allDiagnostics.Concat(processedDimensionality.Diagnostics);

        if (processedDimensionality.LacksResult)
        {
            return OptionalWithDiagnostics.Empty<UnresolvedSharpMeasuresVectorDefinition>(allDiagnostics);
        }

        UnresolvedSharpMeasuresVectorDefinition product = new(definition.Unit!.Value, definition.Scalar, processedDimensionality.Result, definition.ImplementSum,
            definition.ImplementDifference, definition.Difference ?? context.Type.AsNamedType(), processedDefaultUnitData.Result.Name,
            processedDefaultUnitData.Result.Symbol, definition.GenerateDocumentation, definition.Locations);

        return ResultWithDiagnostics.Construct(product, allDiagnostics);
    }

    private IOptionalWithDiagnostics<int> ProcessDimension(IProcessingContext context, RawSharpMeasuresVectorDefinition definition)
    {
        if (definition.Locations.ExplicitlySetDimension && Utility.CheckVectorDimensionValidity(definition.Dimension) is false)
        {
            return OptionalWithDiagnostics.Empty<int>(Diagnostics.InvalidDimension(context, definition));
        }

        if (Utility.InterpretDimensionFromName(context.Type.Name) is int result)
        {
            if (definition.Locations.ExplicitlySetDimension is false)
            {
                if (Utility.CheckVectorDimensionValidity(result) is false)
                {
                    return OptionalWithDiagnostics.Empty<int>(Diagnostics.InvalidInterpretedDimension(context, definition, result));
                }

                return OptionalWithDiagnostics.Result(result);
            }

            if (result != definition.Dimension)
            {
                return OptionalWithDiagnostics.Result(definition.Dimension, Diagnostics.VectorNameAndDimensionMismatch(context, definition, result));
            }
        }

        if (definition.Locations.ExplicitlySetDimension)
        {
            return OptionalWithDiagnostics.Result(definition.Dimension);
        }

        return OptionalWithDiagnostics.Empty<int>(Diagnostics.MissingDimension(context, definition));
    }

    private IResultWithDiagnostics<(string? Name, string? Symbol)> ProcessDefaultUnitData(IProcessingContext context, RawSharpMeasuresVectorDefinition definition)
    {
        if (definition.Locations.ExplicitlySetDefaultUnitSymbol && definition.Locations.ExplicitlySetDefaultUnitName is false)
        {
            return ResultWithDiagnostics.Construct<(string?, string?)>((null, null), Diagnostics.SetDefaultSymbolButNotUnit(context, definition));
        }

        if (definition.Locations.ExplicitlySetDefaultUnitName && definition.Locations.ExplicitlySetDefaultUnitSymbol is false)
        {
            return ResultWithDiagnostics.Construct<(string?, string?)>((null, null), Diagnostics.SetDefaultUnitButNotSymbol(context, definition));
        }

        if (definition.Locations.ExplicitlySetDefaultUnitName)
        {
            if (definition.DefaultUnitName is null)
            {
                return ResultWithDiagnostics.Construct<(string?, string?)>((null, null), Diagnostics.NullDefaultUnit(context, definition));
            }

            if (definition.DefaultUnitName.Length is 0)
            {
                return ResultWithDiagnostics.Construct<(string?, string?)>((null, null), Diagnostics.EmptyDefaultUnit(context, definition));
            }
        }

        return ResultWithDiagnostics.Construct<(string?, string?)>((definition.DefaultUnitName, definition.DefaultUnitSymbol));
    }

    private IValidityWithDiagnostics CheckValidity(IProcessingContext context, RawSharpMeasuresVectorDefinition definition)
    {
        return IterativeValidity.DiagnoseAndMergeWhileValid(context, definition, CheckUnitValidity, CheckScalarValidity, CheckDifferenceValidity);
    }

    private IValidityWithDiagnostics CheckUnitValidity(IProcessingContext context, RawSharpMeasuresVectorDefinition definition)
    {
        if (definition.Unit is null)
        {
            return ValidityWithDiagnostics.Invalid(Diagnostics.NullUnit(context, definition));
        }

        return ValidityWithDiagnostics.Valid;
    }

    private IValidityWithDiagnostics CheckScalarValidity(IProcessingContext context, RawSharpMeasuresVectorDefinition definition)
    {
        if (definition.Locations.ExplicitlySetScalar is false)
        {
            return ValidityWithDiagnostics.Valid;
        }

        if (definition.Scalar is null)
        {
            return ValidityWithDiagnostics.Invalid(Diagnostics.NullScalar(context, definition));
        }

        return ValidityWithDiagnostics.Valid;
    }

    private IValidityWithDiagnostics CheckDifferenceValidity(IProcessingContext context, RawSharpMeasuresVectorDefinition definition)
    {
        if (definition.Locations.ExplicitlySetDifference is false)
        {
            return ValidityWithDiagnostics.Valid;
        }

        if (definition.Difference is null)
        {
            return ValidityWithDiagnostics.ValidWithDiagnostics(Diagnostics.NullDifferenceQuantity(context, definition));
        }

        if (definition.ImplementDifference is false)
        {
            return ValidityWithDiagnostics.ValidWithDiagnostics(Diagnostics.DifferenceDisabledButQuantitySpecified(context, definition));
        }

        return ValidityWithDiagnostics.Valid;
    }
}
