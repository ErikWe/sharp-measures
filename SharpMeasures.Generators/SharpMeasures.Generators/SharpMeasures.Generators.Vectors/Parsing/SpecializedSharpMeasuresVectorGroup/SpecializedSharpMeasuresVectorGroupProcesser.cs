namespace SharpMeasures.Generators.Vectors.Parsing.SpecializedSharpMeasuresVectorGroup;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;

using System.Linq;

internal interface ISpecializedSharpMeasuresVectorGroupProcessingDiagnostics
{
    public abstract Diagnostic? NameSuggestsDimension(IProcessingContext context, RawSpecializedSharpMeasuresVectorGroupDefinition definition, int interpretedDimension);

    public abstract Diagnostic? NullOriginalVectorGroup(IProcessingContext context, RawSpecializedSharpMeasuresVectorGroupDefinition definition);
    public abstract Diagnostic? NullScalar(IProcessingContext context, RawSpecializedSharpMeasuresVectorGroupDefinition definition);

    public abstract Diagnostic? DifferenceDisabledButQuantitySpecified(IProcessingContext context, RawSpecializedSharpMeasuresVectorGroupDefinition definition);
    public abstract Diagnostic? NullDifferenceQuantity(IProcessingContext context, RawSpecializedSharpMeasuresVectorGroupDefinition definition);

    public abstract Diagnostic? NullDefaultUnit(IProcessingContext context, RawSpecializedSharpMeasuresVectorGroupDefinition definition);
    public abstract Diagnostic? EmptyDefaultUnit(IProcessingContext context, RawSpecializedSharpMeasuresVectorGroupDefinition definition);
    public abstract Diagnostic? SetDefaultSymbolButNotUnit(IProcessingContext context, RawSpecializedSharpMeasuresVectorGroupDefinition definition);
    public abstract Diagnostic? SetDefaultUnitButNotSymbol(IProcessingContext context, RawSpecializedSharpMeasuresVectorGroupDefinition definition);
}

internal class SpecializedSharpMeasuresVectorGroupProcesser
    : AProcesser<IProcessingContext, RawSpecializedSharpMeasuresVectorGroupDefinition, UnresolvedSpecializedSharpMeasuresVectorGroupDefinition>
{
    private ISpecializedSharpMeasuresVectorGroupProcessingDiagnostics Diagnostics { get; }

    public SpecializedSharpMeasuresVectorGroupProcesser(ISpecializedSharpMeasuresVectorGroupProcessingDiagnostics diagnostics)
    {
        Diagnostics = diagnostics;
    }

    public override IOptionalWithDiagnostics<UnresolvedSpecializedSharpMeasuresVectorGroupDefinition> Process(IProcessingContext context,
        RawSpecializedSharpMeasuresVectorGroupDefinition definition)
    {
        if (VerifyRequiredPropertiesSet(definition) is false)
        {
            return OptionalWithDiagnostics.Empty<UnresolvedSpecializedSharpMeasuresVectorGroupDefinition>();
        }

        var validity = CheckValidity(context, definition);
        var allDiagnostics = validity.Diagnostics;

        if (validity.IsInvalid)
        {
            return OptionalWithDiagnostics.Empty<UnresolvedSpecializedSharpMeasuresVectorGroupDefinition>(allDiagnostics);
        }

        var product = ProcessDefinition(context, definition);
        allDiagnostics = allDiagnostics.Concat(product);

        return OptionalWithDiagnostics.Result(product.Result, allDiagnostics);
    }

    private static bool VerifyRequiredPropertiesSet(RawSpecializedSharpMeasuresVectorGroupDefinition definition)
    {
        return definition.Locations.ExplicitlySetOriginalVectorGroup;
    }

    private IOptionalWithDiagnostics<UnresolvedSpecializedSharpMeasuresVectorGroupDefinition> ProcessDefinition(IProcessingContext context,
        RawSpecializedSharpMeasuresVectorGroupDefinition definition)
    {
        var processedDefaultUnitData = ProcessDefaultUnitData(context, definition);
        var allDiagnostics = processedDefaultUnitData.Diagnostics;

        UnresolvedSpecializedSharpMeasuresVectorGroupDefinition product = new(definition.OriginalVectorGroup!.Value, definition.InheritDerivations, definition.InheritConstants,
            definition.InheritConversions, definition.InheritUnits, definition.Scalar, definition.ImplementSum, definition.ImplementDifference, definition.Difference,
            processedDefaultUnitData.Result.Name, processedDefaultUnitData.Result.Symbol, definition.GenerateDocumentation, definition.Locations);

        return ResultWithDiagnostics.Construct(product, allDiagnostics);
    }

    private IResultWithDiagnostics<(string? Name, string? Symbol)> ProcessDefaultUnitData(IProcessingContext context, RawSpecializedSharpMeasuresVectorGroupDefinition definition)
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

    private IValidityWithDiagnostics CheckValidity(IProcessingContext context, RawSpecializedSharpMeasuresVectorGroupDefinition definition)
    {
        return IterativeValidation.DiagnoseAndMergeWhileValid(context, definition, CheckNameValidity, CheckOriginalVectorGroupValidity, CheckScalarValidity, CheckDifferenceValidity);
    }

    private IValidityWithDiagnostics CheckNameValidity(IProcessingContext context, RawSpecializedSharpMeasuresVectorGroupDefinition definition)
    {
        if (Utility.InterpretDimensionFromName(context.Type.Name) is int result)
        {
            return ValidityWithDiagnostics.ValidWithDiagnostics(Diagnostics.NameSuggestsDimension(context, definition, result));
        }

        return ValidityWithDiagnostics.Valid;
    }

    private IValidityWithDiagnostics CheckOriginalVectorGroupValidity(IProcessingContext context, RawSpecializedSharpMeasuresVectorGroupDefinition definition)
    {
        if (definition.OriginalVectorGroup is null)
        {
            return ValidityWithDiagnostics.Invalid(Diagnostics.NullOriginalVectorGroup(context, definition));
        }

        return ValidityWithDiagnostics.Valid;
    }

    private IValidityWithDiagnostics CheckScalarValidity(IProcessingContext context, RawSpecializedSharpMeasuresVectorGroupDefinition definition)
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

    private IValidityWithDiagnostics CheckDifferenceValidity(IProcessingContext context, RawSpecializedSharpMeasuresVectorGroupDefinition definition)
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
