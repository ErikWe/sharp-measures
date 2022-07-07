namespace SharpMeasures.Generators.Scalars.Parsing.SpecializedSharpMeasuresScalar;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;

using System;
using System.Collections.Generic;
using System.Linq;

internal interface ISpecializedSharpMeasuresScalarProcessingDiagnostics
{
    public abstract Diagnostic? NullOriginalScalar(IProcessingContext context, RawSpecializedSharpMeasuresScalarDefinition definition);
    public abstract Diagnostic? NullVector(IProcessingContext context, RawSpecializedSharpMeasuresScalarDefinition definition);

    public abstract Diagnostic? DifferenceDisabledButQuantitySpecified(IProcessingContext context, RawSpecializedSharpMeasuresScalarDefinition definition);
    public abstract Diagnostic? NullDifferenceQuantity(IProcessingContext context, RawSpecializedSharpMeasuresScalarDefinition definition);

    public abstract Diagnostic? NullDefaultUnit(IProcessingContext context, RawSpecializedSharpMeasuresScalarDefinition definition);
    public abstract Diagnostic? EmptyDefaultUnit(IProcessingContext context, RawSpecializedSharpMeasuresScalarDefinition definition);
    public abstract Diagnostic? SetDefaultSymbolButNotUnit(IProcessingContext context, RawSpecializedSharpMeasuresScalarDefinition definition);
    public abstract Diagnostic? SetDefaultUnitButNotSymbol(IProcessingContext context, RawSpecializedSharpMeasuresScalarDefinition definition);

    public abstract Diagnostic? NullReciprocalQuantity(IProcessingContext context, RawSpecializedSharpMeasuresScalarDefinition definition);
    public abstract Diagnostic? NullSquareQuantity(IProcessingContext context, RawSpecializedSharpMeasuresScalarDefinition definition);
    public abstract Diagnostic? NullCubeQuantity(IProcessingContext context, RawSpecializedSharpMeasuresScalarDefinition definition);
    public abstract Diagnostic? NullSquareRootQuantity(IProcessingContext context, RawSpecializedSharpMeasuresScalarDefinition definition);
    public abstract Diagnostic? NullCubeRootQuantity(IProcessingContext context, RawSpecializedSharpMeasuresScalarDefinition definition);
}

internal class SpecializedSharpMeasuresScalarProcesser : AProcesser<IProcessingContext, RawSpecializedSharpMeasuresScalarDefinition, SpecializedSharpMeasuresScalarDefinition>
{
    private ISpecializedSharpMeasuresScalarProcessingDiagnostics Diagnostics { get; }

    public SpecializedSharpMeasuresScalarProcesser(ISpecializedSharpMeasuresScalarProcessingDiagnostics diagnostics)
    {
        Diagnostics = diagnostics;
    }

    public override IOptionalWithDiagnostics<SpecializedSharpMeasuresScalarDefinition> Process(IProcessingContext context, RawSpecializedSharpMeasuresScalarDefinition definition)
    {
        if (VerifyRequiredPropertiesSet(definition) is false)
        {
            return OptionalWithDiagnostics.Empty<SpecializedSharpMeasuresScalarDefinition>();
        }

        var validity = CheckValidity(context, definition);
        IEnumerable<Diagnostic> allDiagnostics = validity.Diagnostics;

        if (validity.IsInvalid)
        {
            return OptionalWithDiagnostics.Empty<SpecializedSharpMeasuresScalarDefinition>(allDiagnostics);
        }

        var product = ProcessDefinition(context, definition);
        allDiagnostics = allDiagnostics.Concat(product);

        return product.ReplaceDiagnostics(allDiagnostics);
    }

    private static bool VerifyRequiredPropertiesSet(RawSpecializedSharpMeasuresScalarDefinition definition)
    {
        return definition.Locations.ExplicitlySetOriginalScalar;
    }

    private IResultWithDiagnostics<SpecializedSharpMeasuresScalarDefinition> ProcessDefinition(IProcessingContext context, RawSpecializedSharpMeasuresScalarDefinition definition)
    {
        IEnumerable<Diagnostic> allDiagnostics = Array.Empty<Diagnostic>();

        var processedDefaultUnitData = ProcessDefaultUnitData(context, definition);
        allDiagnostics = allDiagnostics.Concat(processedDefaultUnitData);

        SpecializedSharpMeasuresScalarDefinition product = new(definition.OriginalScalar!.Value, definition.InheritDerivations, definition.InheritConstants,
            definition.InheritBases, definition.InheritUnits, definition.Vector, definition.ImplementSum, definition.ImplementDifference, definition.Difference,
            processedDefaultUnitData.Result.Name, processedDefaultUnitData.Result.Symbol, definition.Reciprocal, definition.Square, definition.Cube, definition.SquareRoot,
            definition.CubeRoot, definition.GenerateDocumentation, definition.Locations);

        return ResultWithDiagnostics.Construct(product, allDiagnostics);
    }

    private IResultWithDiagnostics<(string? Name, string? Symbol)> ProcessDefaultUnitData(IProcessingContext context, RawSpecializedSharpMeasuresScalarDefinition definition)
    {
        if (definition.Locations.ExplicitlySetDefaultUnitName is false && definition.DefaultUnitSymbol is not null)
        {
            return ResultWithDiagnostics.Construct<(string?, string?)>((null, null), Diagnostics.SetDefaultSymbolButNotUnit(context, definition));
        }

        if (definition.Locations.ExplicitlySetDefaultUnitSymbol is false && definition.DefaultUnitName is not null)
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

    private IValidityWithDiagnostics CheckValidity(IProcessingContext context, RawSpecializedSharpMeasuresScalarDefinition definition)
    {
        return IterativeValidity.DiagnoseAndMergeWhileValid(validities());

        IEnumerable<IValidityWithDiagnostics> validities()
        {
            yield return CheckOriginalScalarValidity(context, definition);
            yield return CheckVectorValidity(context, definition);
            yield return CheckDifferenceValidity(context, definition);
            yield return CheckPowerValidity(context, definition, definition.Locations.ExplicitlySetReciprocal, definition.Reciprocal, Diagnostics.NullReciprocalQuantity);
            yield return CheckPowerValidity(context, definition, definition.Locations.ExplicitlySetSquare, definition.Square, Diagnostics.NullSquareQuantity);
            yield return CheckPowerValidity(context, definition, definition.Locations.ExplicitlySetCube, definition.Cube, Diagnostics.NullCubeQuantity);
            yield return CheckPowerValidity(context, definition, definition.Locations.ExplicitlySetSquareRoot, definition.SquareRoot, Diagnostics.NullSquareRootQuantity);
            yield return CheckPowerValidity(context, definition, definition.Locations.ExplicitlySetCubeRoot, definition.CubeRoot, Diagnostics.NullCubeRootQuantity);
        }
    }

    private IValidityWithDiagnostics CheckOriginalScalarValidity(IProcessingContext context, RawSpecializedSharpMeasuresScalarDefinition definition)
    {
        if (definition.OriginalScalar is null)
        {
            return ValidityWithDiagnostics.Invalid(Diagnostics.NullOriginalScalar(context, definition));
        }

        return ValidityWithDiagnostics.Valid;
    }

    private IValidityWithDiagnostics CheckVectorValidity(IProcessingContext context, RawSpecializedSharpMeasuresScalarDefinition definition)
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

    private IValidityWithDiagnostics CheckDifferenceValidity(IProcessingContext context, RawSpecializedSharpMeasuresScalarDefinition definition)
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

    private static IValidityWithDiagnostics CheckPowerValidity(IProcessingContext context, RawSpecializedSharpMeasuresScalarDefinition definition, bool explicitlySet,
        NamedType? value, Func<IProcessingContext, RawSpecializedSharpMeasuresScalarDefinition, Diagnostic?> nullQuantityDiagnosticsDelegate)
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
