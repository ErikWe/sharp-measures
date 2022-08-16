namespace SharpMeasures.Generators.Vectors.Parsing.SharpMeasuresVectorGroup;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;

using System.Linq;

internal interface ISharpMeasuresVectorGroupProcessingDiagnostics
{
    public abstract Diagnostic? NameSuggestsDimension(IProcessingContext context, RawSharpMeasuresVectorGroupDefinition definition, int interpretedDimension);

    public abstract Diagnostic? NullUnit(IProcessingContext context, RawSharpMeasuresVectorGroupDefinition definition);
    public abstract Diagnostic? NullScalar(IProcessingContext context, RawSharpMeasuresVectorGroupDefinition definition);

    public abstract Diagnostic? DifferenceDisabledButQuantitySpecified(IProcessingContext context, RawSharpMeasuresVectorGroupDefinition definition);
    public abstract Diagnostic? NullDifferenceQuantity(IProcessingContext context, RawSharpMeasuresVectorGroupDefinition definition);

    public abstract Diagnostic? NullDefaultUnit(IProcessingContext context, RawSharpMeasuresVectorGroupDefinition definition);
    public abstract Diagnostic? EmptyDefaultUnit(IProcessingContext context, RawSharpMeasuresVectorGroupDefinition definition);
    public abstract Diagnostic? SetDefaultSymbolButNotUnit(IProcessingContext context, RawSharpMeasuresVectorGroupDefinition definition);
    public abstract Diagnostic? SetDefaultUnitButNotSymbol(IProcessingContext context, RawSharpMeasuresVectorGroupDefinition definition);
}

internal class SharpMeasuresVectorGroupProcesser : AProcesser<IProcessingContext, RawSharpMeasuresVectorGroupDefinition, UnresolvedSharpMeasuresVectorGroupDefinition>
{
    private ISharpMeasuresVectorGroupProcessingDiagnostics Diagnostics { get; }

    public SharpMeasuresVectorGroupProcesser(ISharpMeasuresVectorGroupProcessingDiagnostics diagnostics)
    {
        Diagnostics = diagnostics;
    }

    public override IOptionalWithDiagnostics<UnresolvedSharpMeasuresVectorGroupDefinition> Process(IProcessingContext context, RawSharpMeasuresVectorGroupDefinition definition)
    {
        if (VerifyRequiredPropertiesSet(definition) is false)
        {
            return OptionalWithDiagnostics.Empty<UnresolvedSharpMeasuresVectorGroupDefinition>();
        }

        var validity = CheckValidity(context, definition);
        var allDiagnostics = validity.Diagnostics;

        if (validity.IsInvalid)
        {
            return OptionalWithDiagnostics.Empty<UnresolvedSharpMeasuresVectorGroupDefinition>(allDiagnostics);
        }

        var product = ProcessDefinition(context, definition);
        allDiagnostics = allDiagnostics.Concat(product);

        return OptionalWithDiagnostics.Result(product.Result, allDiagnostics);
    }

    private static bool VerifyRequiredPropertiesSet(RawSharpMeasuresVectorGroupDefinition definition)
    {
        return definition.Locations.ExplicitlySetUnit;
    }

    private IOptionalWithDiagnostics<UnresolvedSharpMeasuresVectorGroupDefinition> ProcessDefinition(IProcessingContext context, RawSharpMeasuresVectorGroupDefinition definition)
    {
        var processedDefaultUnitData = ProcessDefaultUnitData(context, definition);
        var allDiagnostics = processedDefaultUnitData.Diagnostics;

        UnresolvedSharpMeasuresVectorGroupDefinition product = new(definition.Unit!.Value, definition.Scalar, definition.ImplementSum,
            definition.ImplementDifference, definition.Difference ?? context.Type.AsNamedType(), processedDefaultUnitData.Result.Name,
            processedDefaultUnitData.Result.Symbol, definition.GenerateDocumentation, definition.Locations);

        return ResultWithDiagnostics.Construct(product, allDiagnostics);
    }

    private IResultWithDiagnostics<(string? Name, string? Symbol)> ProcessDefaultUnitData(IProcessingContext context, RawSharpMeasuresVectorGroupDefinition definition)
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

    private IValidityWithDiagnostics CheckValidity(IProcessingContext context, RawSharpMeasuresVectorGroupDefinition definition)
    {
        return IterativeValidation.DiagnoseAndMergeWhileValid(context, definition, CheckNameValidity, CheckUnitValidity, CheckScalarValidity, CheckDifferenceValidity);
    }

    private IValidityWithDiagnostics CheckNameValidity(IProcessingContext context, RawSharpMeasuresVectorGroupDefinition definition)
    {
        if (Utility.InterpretDimensionFromName(context.Type.Name) is int result)
        {
            return ValidityWithDiagnostics.ValidWithDiagnostics(Diagnostics.NameSuggestsDimension(context, definition, result));
        }

        return ValidityWithDiagnostics.Valid;
    }

    private IValidityWithDiagnostics CheckUnitValidity(IProcessingContext context, RawSharpMeasuresVectorGroupDefinition definition)
    {
        if (definition.Unit is null)
        {
            return ValidityWithDiagnostics.Invalid(Diagnostics.NullUnit(context, definition));
        }

        return ValidityWithDiagnostics.Valid;
    }

    private IValidityWithDiagnostics CheckScalarValidity(IProcessingContext context, RawSharpMeasuresVectorGroupDefinition definition)
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

    private IValidityWithDiagnostics CheckDifferenceValidity(IProcessingContext context, RawSharpMeasuresVectorGroupDefinition definition)
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
