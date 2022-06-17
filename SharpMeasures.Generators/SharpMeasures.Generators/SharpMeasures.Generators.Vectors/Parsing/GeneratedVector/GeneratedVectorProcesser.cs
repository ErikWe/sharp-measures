namespace SharpMeasures.Generators.Vectors.Parsing.GeneratedVector;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;

using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

internal interface IGeneratedVectorProcessingDiagnostics
{
    public abstract Diagnostic? NullUnit(IProcessingContext context, RawGeneratedVectorDefinition definition);
    public abstract Diagnostic? NullScalar(IProcessingContext context, RawGeneratedVectorDefinition definition);

    public abstract Diagnostic? MissingDimension(IProcessingContext context, RawGeneratedVectorDefinition definition);
    public abstract Diagnostic? InvalidDimension(IProcessingContext context, RawGeneratedVectorDefinition definition);

    public abstract Diagnostic? NullDefaultUnit(IProcessingContext context, RawGeneratedVectorDefinition definition);
    public abstract Diagnostic? EmptyDefaultUnit(IProcessingContext context, RawGeneratedVectorDefinition definition);
    public abstract Diagnostic? NullDefaultSymbol(IProcessingContext context, RawGeneratedVectorDefinition definition);
    public abstract Diagnostic? EmptyDefaultSymbol(IProcessingContext context, RawGeneratedVectorDefinition definition);
    public abstract Diagnostic? SetDefaultSymbolButNotUnit(IProcessingContext context, RawGeneratedVectorDefinition definition);
    public abstract Diagnostic? SetDefaultUnitButNotSymbol(IProcessingContext context, RawGeneratedVectorDefinition definition);
}

internal class GeneratedVectorProcesser : AProcesser<IProcessingContext, RawGeneratedVectorDefinition, GeneratedVectorDefinition>
{
    private IGeneratedVectorProcessingDiagnostics Diagnostics { get; }

    public GeneratedVectorProcesser(IGeneratedVectorProcessingDiagnostics diagnostics)
    {
        Diagnostics = diagnostics;
    }

    public override IOptionalWithDiagnostics<GeneratedVectorDefinition> Process(IProcessingContext context, RawGeneratedVectorDefinition definition)
    {
        var validity = CheckValidity(context, definition);
        var allDiagnostics = validity.Diagnostics;

        if (validity.IsInvalid)
        {
            return OptionalWithDiagnostics.Empty<GeneratedVectorDefinition>(allDiagnostics);
        }

        var product = ProcessDefinition(context, definition);
        allDiagnostics = allDiagnostics.Concat(product);

        return product.ReplaceDiagnostics(allDiagnostics);
    }

    private IOptionalWithDiagnostics<GeneratedVectorDefinition> ProcessDefinition(IProcessingContext context, RawGeneratedVectorDefinition definition)
    {
        var processedDefaultUnitData = ProcessDefaultUnitData(context, definition);
        var allDiagnostics = processedDefaultUnitData.Diagnostics;

        var processedDimensionality = ProcessDimension(context, definition);
        allDiagnostics = allDiagnostics.Concat(processedDimensionality.Diagnostics);

        if (processedDimensionality.LacksResult)
        {
            return OptionalWithDiagnostics.Empty<GeneratedVectorDefinition>(allDiagnostics);
        }

        GeneratedVectorDefinition product = new(definition.Unit!.Value, definition.Scalar, processedDimensionality.Result, processedDefaultUnitData.Result.Name,
            processedDefaultUnitData.Result.Symbol, definition.GenerateDocumentation, definition.Locations);

        return ResultWithDiagnostics.Construct(product, allDiagnostics);
    }

    private IOptionalWithDiagnostics<int> ProcessDimension(IProcessingContext context, RawGeneratedVectorDefinition definition)
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

    private IResultWithDiagnostics<(string? Name, string? Symbol)> ProcessDefaultUnitData(IProcessingContext context, RawGeneratedVectorDefinition definition)
    {
        if (definition.Locations.ExplicitlySetDefaultUnitName && definition.Locations.ExplicitlySetDefaultUnitSymbol is false)
        {
            return ResultWithDiagnostics.Construct<(string?, string?)>((null, null), Diagnostics.SetDefaultUnitButNotSymbol(context, definition));
        }

        if (definition.Locations.ExplicitlySetDefaultUnitSymbol && definition.Locations.ExplicitlySetDefaultUnitName is false)
        {
            return ResultWithDiagnostics.Construct<(string?, string?)>((null, null), Diagnostics.SetDefaultSymbolButNotUnit(context, definition));
        }

        if (definition.Locations.ExplicitlySetDefaultUnitName is false || definition.Locations.ExplicitlySetDefaultUnitSymbol is false)
        {
            return ResultWithDiagnostics.Construct<(string?, string?)>((null, null));
        }

        if (definition.DefaultUnitName is null)
        {
            return ResultWithDiagnostics.Construct<(string?, string?)>((null, null), Diagnostics.NullDefaultUnit(context, definition));
        }

        if (definition.DefaultUnitName.Length is 0)
        {
            return ResultWithDiagnostics.Construct<(string?, string?)>((null, null), Diagnostics.EmptyDefaultUnit(context, definition));
        }

        if (definition.DefaultUnitSymbol is null)
        {
            return ResultWithDiagnostics.Construct<(string?, string?)>((null, null), Diagnostics.NullDefaultSymbol(context, definition));
        }

        if (definition.DefaultUnitSymbol.Length is 0)
        {
            return ResultWithDiagnostics.Construct<(string?, string?)>((null, null), Diagnostics.EmptyDefaultSymbol(context, definition));
        }

        return ResultWithDiagnostics.Construct<(string?, string?)>((definition.DefaultUnitName, definition.DefaultUnitSymbol));
    }

    private IValidityWithDiagnostics CheckValidity(IProcessingContext context, RawGeneratedVectorDefinition definition)
    {
        return IterativeValidity.DiagnoseAndMergeWhileValid(context, definition, CheckUnitValidity, CheckScalarValidity);
    }

    private IValidityWithDiagnostics CheckUnitValidity(IProcessingContext context, RawGeneratedVectorDefinition definition)
    {
        if (definition.Unit is null)
        {
            return ValidityWithDiagnostics.Invalid(Diagnostics.NullUnit(context, definition));
        }

        return ValidityWithDiagnostics.Valid;
    }

    private IValidityWithDiagnostics CheckScalarValidity(IProcessingContext context, RawGeneratedVectorDefinition definition)
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
