namespace SharpMeasures.Generators.Scalars.Parsing.GeneratedScalar;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;

using System;
using System.Collections.Generic;
using System.Linq;

internal interface IGeneratedScalarProcessingDiagnostics
{
    public abstract Diagnostic? NullUnit(IProcessingContext context, RawGeneratedScalarDefinition definition);
    public abstract Diagnostic? NullVector(IProcessingContext context, RawGeneratedScalarDefinition definition);

    public abstract Diagnostic? DifferenceDisabledButQuantitySpecified(IProcessingContext context, RawGeneratedScalarDefinition definition);
    public abstract Diagnostic? NullDifferenceQuantity(IProcessingContext context, RawGeneratedScalarDefinition definition);

    public abstract Diagnostic? NullDefaultUnit(IProcessingContext context, RawGeneratedScalarDefinition definition);
    public abstract Diagnostic? EmptyDefaultUnit(IProcessingContext context, RawGeneratedScalarDefinition definition);
    public abstract Diagnostic? SetDefaultSymbolButNotUnit(IProcessingContext context, RawGeneratedScalarDefinition definition);

    public abstract Diagnostic? NullReciprocalQuantity(IProcessingContext context, RawGeneratedScalarDefinition definition);
    public abstract Diagnostic? NullSquareQuantity(IProcessingContext context, RawGeneratedScalarDefinition definition);
    public abstract Diagnostic? NullCubeQuantity(IProcessingContext context, RawGeneratedScalarDefinition definition);
    public abstract Diagnostic? NullSquareRootQuantity(IProcessingContext context, RawGeneratedScalarDefinition definition);
    public abstract Diagnostic? NullCubeRootQuantity(IProcessingContext context, RawGeneratedScalarDefinition definition);
}

internal class GeneratedScalarProcesser : AProcesser<IProcessingContext, RawGeneratedScalarDefinition, GeneratedScalarDefinition>
{
    private IGeneratedScalarProcessingDiagnostics Diagnostics { get; }

    public GeneratedScalarProcesser(IGeneratedScalarProcessingDiagnostics diagnostics)
    {
        Diagnostics = diagnostics;
    }

    public override IOptionalWithDiagnostics<GeneratedScalarDefinition> Process(IProcessingContext context, RawGeneratedScalarDefinition definition)
    {
        var validity = CheckValidity(context, definition);
        IEnumerable<Diagnostic> allDiagnostics = validity.Diagnostics;

        if (validity.IsInvalid)
        {
            return OptionalWithDiagnostics.Empty<GeneratedScalarDefinition>(allDiagnostics);
        }

        var product = ProcessDefinition(context, definition);
        allDiagnostics = allDiagnostics.Concat(product);

        return product.ReplaceDiagnostics(allDiagnostics);
    }

    private IResultWithDiagnostics<GeneratedScalarDefinition> ProcessDefinition(IProcessingContext context, RawGeneratedScalarDefinition definition)
    {
        IEnumerable<Diagnostic> allDiagnostics = Array.Empty<Diagnostic>();

        var processedDefaultUnitData = ProcessDefaultUnitData(context, definition);
        allDiagnostics = allDiagnostics.Concat(processedDefaultUnitData);

        GeneratedScalarDefinition product = new(definition.Unit!.Value, definition.Vector, definition.UseUnitBias, definition.ImplementSum,
            definition.ImplementDifference, definition.Difference ?? context.Type.AsNamedType(), processedDefaultUnitData.Result.Name,
            processedDefaultUnitData.Result.Symbol, definition.Reciprocal, definition.Square, definition.Cube, definition.SquareRoot, definition.CubeRoot,
            definition.GenerateDocumentation, definition.Locations);

        return ResultWithDiagnostics.Construct(product, allDiagnostics);
    }

    private IResultWithDiagnostics<(string? Name, string? Symbol)> ProcessDefaultUnitData(IProcessingContext context, RawGeneratedScalarDefinition definition)
    {
        if (definition.Locations.ExplicitlySetDefaultUnitName is false && definition.DefaultUnitSymbol is not null)
        {
            return ResultWithDiagnostics.Construct<(string?, string?)>((null, null), Diagnostics.SetDefaultSymbolButNotUnit(context, definition));
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

    private IValidityWithDiagnostics CheckValidity(IProcessingContext context, RawGeneratedScalarDefinition definition)
    {
        return IterativeValidity.DiagnoseAndMergeWhileValid(validities);

        IEnumerable<IValidityWithDiagnostics> validities()
        {
            yield return IterativeValidity.DiagnoseAndMergeWhileValid(context, definition, CheckUnitValidity, CheckVectorValidity, CheckDifferenceValidity);
            yield return CheckPowerValidity(context, definition, definition.Locations.ExplicitlySetReciprocal, definition.Reciprocal, Diagnostics.NullReciprocalQuantity);
            yield return CheckPowerValidity(context, definition, definition.Locations.ExplicitlySetSquare, definition.Square, Diagnostics.NullSquareQuantity);
            yield return CheckPowerValidity(context, definition, definition.Locations.ExplicitlySetCube, definition.Cube, Diagnostics.NullCubeQuantity);
            yield return CheckPowerValidity(context, definition, definition.Locations.ExplicitlySetSquareRoot, definition.SquareRoot, Diagnostics.NullSquareRootQuantity);
            yield return CheckPowerValidity(context, definition, definition.Locations.ExplicitlySetCubeRoot, definition.CubeRoot, Diagnostics.NullCubeRootQuantity);
        }
    }

    private IValidityWithDiagnostics CheckUnitValidity(IProcessingContext context, RawGeneratedScalarDefinition definition)
    {
        if (definition.Unit is null)
        {
            return ValidityWithDiagnostics.Invalid(Diagnostics.NullUnit(context, definition));
        }

        return ValidityWithDiagnostics.Valid;
    }

    private IValidityWithDiagnostics CheckVectorValidity(IProcessingContext context, RawGeneratedScalarDefinition definition)
    {
        if (definition.Locations.ExplicitlySetVector is false)
        {
            return ValidityWithDiagnostics.Valid;
        }

        if (definition.Vector is null)
        {
            return ValidityWithDiagnostics.ValidWithDiagnostics(Diagnostics.NullVector(context, definition));
        }

        return ValidityWithDiagnostics.Valid;
    }

    private IValidityWithDiagnostics CheckDifferenceValidity(IProcessingContext context, RawGeneratedScalarDefinition definition)
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

    private static IValidityWithDiagnostics CheckPowerValidity(IProcessingContext context, RawGeneratedScalarDefinition definition, bool explicitlySet,
        NamedType? value, Func<IProcessingContext, RawGeneratedScalarDefinition, Diagnostic?> nullQuantityDiagnosticsDelegate)
    {
        if (explicitlySet is false)
        {
            return ValidityWithDiagnostics.Valid;
        }

        if (value is null)
        {
            return ValidityWithDiagnostics.ValidWithDiagnostics(nullQuantityDiagnosticsDelegate(context, definition));
        }

        return ValidityWithDiagnostics.Valid;
    }
}
