namespace SharpMeasures.Generators.Attributes.Parsing.Vectors;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;

using System;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

public interface IGeneratedVectorDiagnostics
{
    public abstract Diagnostic? NullUnit(IProcessingContext context, RawGeneratedVector definition);
    public abstract Diagnostic? NullScalar(IProcessingContext context, RawGeneratedVector definition);

    public abstract Diagnostic? MissingDimension(IProcessingContext context, RawGeneratedVector definition);
    public abstract Diagnostic? InvalidDimension(IProcessingContext context, RawGeneratedVector definition);

    public abstract Diagnostic? NullDefaultUnit(IProcessingContext context, RawGeneratedVector definition);
    public abstract Diagnostic? EmptyDefaultUnit(IProcessingContext context, RawGeneratedVector definition);
    public abstract Diagnostic? SetDefaultSymbolButNotUnit(IProcessingContext context, RawGeneratedVector definition);
}

public class GeneratedVectorProcesser : AProcesser<IProcessingContext, RawGeneratedVector, GeneratedVector>
{
    private IGeneratedVectorDiagnostics Diagnostics { get; }

    public GeneratedVectorProcesser(IGeneratedVectorDiagnostics diagnostics)
    {
        Diagnostics = diagnostics;
    }

    public override IOptionalWithDiagnostics<GeneratedVector> Process(IProcessingContext context, RawGeneratedVector definition)
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
        var allDiagnostics = validity.Diagnostics;

        if (validity.IsInvalid)
        {
            return OptionalWithDiagnostics.Empty<GeneratedVector>(allDiagnostics);
        }

        var product = ProcessDefinition(context, definition);
        allDiagnostics = allDiagnostics.Concat(product);

        return product.ReplaceDiagnostics(allDiagnostics);
    }

    private IOptionalWithDiagnostics<GeneratedVector> ProcessDefinition(IProcessingContext context, RawGeneratedVector definition)
    {
        var processedDefaultUnitData = ProcessDefaultUnitData(context, definition);
        var allDiagnostics = processedDefaultUnitData.Diagnostics;

        var processedDimensionality = ProcessDimension(context, definition);
        allDiagnostics = allDiagnostics.Concat(processedDimensionality.Diagnostics);

        if (processedDimensionality.LacksResult)
        {
            return OptionalWithDiagnostics.Empty<GeneratedVector>(allDiagnostics);
        }

        GeneratedVector product = new(definition.Unit!.Value, definition.Scalar, processedDimensionality.Result, processedDefaultUnitData.Result.Name,
            processedDefaultUnitData.Result.Symbol, definition.GenerateDocumentation, definition.Locations);

        return ResultWithDiagnostics.Construct(product, allDiagnostics);
    }

    private IOptionalWithDiagnostics<int> ProcessDimension(IProcessingContext context, RawGeneratedVector definition)
    {
        if (definition.Locations.ExplicitlySetDimension)
        {
            if (definition.Dimension < 2)
            {
                return OptionalWithDiagnostics.Empty<int>(Diagnostics.InvalidDimension(context, definition));
            }

            return OptionalWithDiagnostics.Result(definition.Dimension);
        }

        var trailingNumber = Regex.Match(context.Type.Name, @"\d+$", RegexOptions.RightToLeft);
        if (trailingNumber.Success)
        {
            if (int.TryParse(trailingNumber.Value, NumberStyles.None, CultureInfo.InvariantCulture, out int result))
            {
                return OptionalWithDiagnostics.Result(result);
            }

            return OptionalWithDiagnostics.EmptyWithoutDiagnostics<int>();
        }

        return OptionalWithDiagnostics.Empty<int>(Diagnostics.MissingDimension(context, definition));
    }

    private IResultWithDiagnostics<(string? Name, string? Symbol)> ProcessDefaultUnitData(IProcessingContext context, RawGeneratedVector definition)
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

    private IValidityWithDiagnostics CheckValidity(IProcessingContext context, RawGeneratedVector definition)
    {
        if (definition is null)
        {
            throw new ArgumentNullException(nameof(definition));
        }

        return IterativeValidity.DiagnoseAndMergeWhileValid(context, definition, CheckUnitValidity, CheckScalarValidity);
    }

    private IValidityWithDiagnostics CheckUnitValidity(IProcessingContext context, RawGeneratedVector definition)
    {
        if (definition.Unit is null)
        {
            return ValidityWithDiagnostics.Invalid(Diagnostics.NullUnit(context, definition));
        }

        return ValidityWithDiagnostics.Valid;
    }

    private IValidityWithDiagnostics CheckScalarValidity(IProcessingContext context, RawGeneratedVector definition)
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
}
