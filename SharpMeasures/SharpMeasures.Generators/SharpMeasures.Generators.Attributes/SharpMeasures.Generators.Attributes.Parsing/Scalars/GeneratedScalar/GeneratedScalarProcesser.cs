namespace SharpMeasures.Generators.Attributes.Parsing.Scalars;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;

using System;
using System.Collections.Generic;
using System.Linq;

public interface IGeneratedScalarDiagnostics
{
    public abstract Diagnostic? NullUnit(IProcessingContext context, RawGeneratedScalar definition);
    public abstract Diagnostic? NullVector(IProcessingContext context, RawGeneratedScalar definition);
    public abstract Diagnostic? NullDefaultUnit(IProcessingContext context, RawGeneratedScalar definition);
    public abstract Diagnostic? EmptyDefaultUnit(IProcessingContext context, RawGeneratedScalar definition);
    public abstract Diagnostic? SetDefaultSymbolButNotUnit(IProcessingContext context, RawGeneratedScalar definition);

    public abstract Diagnostic? NullPowerQuantity(MinimalLocation? location);
}

public class GeneratedScalarProcesser : AProcesser<IProcessingContext, RawGeneratedScalar, GeneratedScalar>
{
    private IGeneratedScalarDiagnostics Diagnostics { get; }

    public GeneratedScalarProcesser(IGeneratedScalarDiagnostics diagnostics)
    {
        Diagnostics = diagnostics;
    }

    public override IOptionalWithDiagnostics<GeneratedScalar> Process(IProcessingContext context, RawGeneratedScalar definition)
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
            return OptionalWithDiagnostics.Empty<GeneratedScalar>(allDiagnostics);
        }

        var product = ProcessDefinition(context, definition);
        allDiagnostics = allDiagnostics.Concat(product);

        return product.ReplaceDiagnostics(allDiagnostics);
    }

    private IResultWithDiagnostics<GeneratedScalar> ProcessDefinition(IProcessingContext context, RawGeneratedScalar definition)
    {
        IEnumerable<Diagnostic> allDiagnostics = Array.Empty<Diagnostic>();

        var processedDefaultUnitData = ProcessDefaultUnitData(context, definition);
        allDiagnostics = allDiagnostics.Concat(processedDefaultUnitData);

        GeneratedScalar product = new(definition.Unit!.Value, definition.Vector, definition.Biased, processedDefaultUnitData.Result.Name,
            processedDefaultUnitData.Result.Symbol, definition.Reciprocal, definition.Square, definition.Cube, definition.SquareRoot, definition.CubeRoot,
            definition.GenerateDocumentation, definition.Locations);

        return ResultWithDiagnostics.Construct(product, allDiagnostics);
    }

    private IResultWithDiagnostics<(string? Name, string? Symbol)> ProcessDefaultUnitData(IProcessingContext context, RawGeneratedScalar definition)
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

    private IValidityWithDiagnostics CheckValidity(IProcessingContext context, RawGeneratedScalar definition)
    {
        return IterativeValidity.DiagnoseAndMergeWhileValid(validities);

        IEnumerable<IValidityWithDiagnostics> validities()
        {
            yield return IterativeValidity.DiagnoseAndMergeWhileValid(context, definition, CheckUnitValidity, CheckVectorValidity);
            yield return CheckPowerValidity(definition.Locations.ExplicitlySetReciprocal, definition.Reciprocal, definition.Locations.Reciprocal);
            yield return CheckPowerValidity(definition.Locations.ExplicitlySetSquare, definition.Square, definition.Locations.Square);
            yield return CheckPowerValidity(definition.Locations.ExplicitlySetCube, definition.Cube, definition.Locations.Cube);
            yield return CheckPowerValidity(definition.Locations.ExplicitlySetSquareRoot, definition.SquareRoot, definition.Locations.SquareRoot);
            yield return CheckPowerValidity(definition.Locations.ExplicitlySetCubeRoot, definition.CubeRoot, definition.Locations.CubeRoot);
        }
    }

    private IValidityWithDiagnostics CheckUnitValidity(IProcessingContext context, RawGeneratedScalar definition)
    {
        if (definition.Unit is null)
        {
            return ValidityWithDiagnostics.Invalid(Diagnostics.NullUnit(context, definition));
        }

        return ValidityWithDiagnostics.Valid;
    }

    private IValidityWithDiagnostics CheckVectorValidity(IProcessingContext context, RawGeneratedScalar definition)
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

    private IValidityWithDiagnostics CheckPowerValidity(bool explicitlySet, NamedType? value, MinimalLocation? location)
    {
        if (explicitlySet is false)
        {
            return ValidityWithDiagnostics.Valid;
        }

        if (value is null)
        {
            return ValidityWithDiagnostics.ValidWithDiagnostics(Diagnostics.NullPowerQuantity(location));
        }

        return ValidityWithDiagnostics.Valid;
    }
}
