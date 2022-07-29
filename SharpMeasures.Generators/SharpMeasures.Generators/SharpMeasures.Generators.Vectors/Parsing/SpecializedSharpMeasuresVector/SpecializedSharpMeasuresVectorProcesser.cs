namespace SharpMeasures.Generators.Vectors.Parsing.SpecializedSharpMeasuresVector;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;

using System;
using System.Collections.Generic;
using System.Linq;

internal interface ISpecializedSharpMeasuresVectorProcessingDiagnostics
{
    public abstract Diagnostic? NullOriginalVector(IProcessingContext context, RawSpecializedSharpMeasuresVectorDefinition definition);
    public abstract Diagnostic? NullScalar(IProcessingContext context, RawSpecializedSharpMeasuresVectorDefinition definition);

    public abstract Diagnostic? NullDifferenceQuantity(IProcessingContext context, RawSpecializedSharpMeasuresVectorDefinition definition);
    public abstract Diagnostic? DifferenceDisabledButQuantitySpecified(IProcessingContext context, RawSpecializedSharpMeasuresVectorDefinition definition);

    public abstract Diagnostic? NullDefaultUnit(IProcessingContext context, RawSpecializedSharpMeasuresVectorDefinition definition);
    public abstract Diagnostic? EmptyDefaultUnit(IProcessingContext context, RawSpecializedSharpMeasuresVectorDefinition definition);
    public abstract Diagnostic? SetDefaultSymbolButNotUnit(IProcessingContext context, RawSpecializedSharpMeasuresVectorDefinition definition);
    public abstract Diagnostic? SetDefaultUnitButNotSymbol(IProcessingContext context, RawSpecializedSharpMeasuresVectorDefinition definition);
}

internal class SpecializedSharpMeasuresVectorProcesser : AProcesser<IProcessingContext, RawSpecializedSharpMeasuresVectorDefinition, UnresolvedSpecializedSharpMeasuresVectorDefinition>
{
    private ISpecializedSharpMeasuresVectorProcessingDiagnostics Diagnostics { get; }

    public SpecializedSharpMeasuresVectorProcesser(ISpecializedSharpMeasuresVectorProcessingDiagnostics diagnostics)
    {
        Diagnostics = diagnostics;
    }

    public override IOptionalWithDiagnostics<UnresolvedSpecializedSharpMeasuresVectorDefinition> Process(IProcessingContext context, RawSpecializedSharpMeasuresVectorDefinition definition)
    {
        if (VerifyRequiredPropertiesSet(definition) is false)
        {
            return OptionalWithDiagnostics.Empty<UnresolvedSpecializedSharpMeasuresVectorDefinition>();
        }

        var validity = CheckValidity(context, definition);
        IEnumerable<Diagnostic> allDiagnostics = validity.Diagnostics;

        if (validity.IsInvalid)
        {
            return OptionalWithDiagnostics.Empty<UnresolvedSpecializedSharpMeasuresVectorDefinition>(allDiagnostics);
        }

        var product = ProcessDefinition(context, definition);
        allDiagnostics = allDiagnostics.Concat(product);

        return OptionalWithDiagnostics.Result(product.Result, allDiagnostics);
    }

    private static bool VerifyRequiredPropertiesSet(RawSpecializedSharpMeasuresVectorDefinition definition)
    {
        return definition.Locations.ExplicitlySetOriginalVector;
    }

    private IResultWithDiagnostics<UnresolvedSpecializedSharpMeasuresVectorDefinition> ProcessDefinition(IProcessingContext context, RawSpecializedSharpMeasuresVectorDefinition definition)
    {
        IEnumerable<Diagnostic> allDiagnostics = Array.Empty<Diagnostic>();

        var processedDefaultUnitData = ProcessDefaultUnitData(context, definition);
        allDiagnostics = allDiagnostics.Concat(processedDefaultUnitData);

        UnresolvedSpecializedSharpMeasuresVectorDefinition product = new(definition.OriginalVector!.Value, definition.InheritDerivations, definition.InheritConstants,
            definition.InheritConversions, definition.InheritUnits, definition.Scalar, definition.ImplementSum, definition.ImplementDifference,
            definition.Difference, processedDefaultUnitData.Result.Name, processedDefaultUnitData.Result.Symbol, definition.GenerateDocumentation, definition.Locations);

        return ResultWithDiagnostics.Construct(product, allDiagnostics);
    }

    private IResultWithDiagnostics<(string? Name, string? Symbol)> ProcessDefaultUnitData(IProcessingContext context, RawSpecializedSharpMeasuresVectorDefinition definition)
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

    private IValidityWithDiagnostics CheckValidity(IProcessingContext context, RawSpecializedSharpMeasuresVectorDefinition definition)
    {
        return IterativeValidity.DiagnoseAndMergeWhileValid(context, definition, CheckOriginalVectorValidity, CheckScalarValidity, CheckDifferenceValidity);
    }

    private IValidityWithDiagnostics CheckOriginalVectorValidity(IProcessingContext context, RawSpecializedSharpMeasuresVectorDefinition definition)
    {
        if (definition.OriginalVector is null)
        {
            return ValidityWithDiagnostics.Invalid(Diagnostics.NullOriginalVector(context, definition));
        }

        return ValidityWithDiagnostics.Valid;
    }

    private IValidityWithDiagnostics CheckScalarValidity(IProcessingContext context, RawSpecializedSharpMeasuresVectorDefinition definition)
    {
        if (definition.Locations.ExplicitlySetScalar is false)
        {
            return ValidityWithDiagnostics.Valid;
        }

        if (definition.Scalar is null)
        {
            return ValidityWithDiagnostics.ValidWithDiagnostics(Diagnostics.NullScalar(context, definition));
        }

        return ValidityWithDiagnostics.Valid;
    }

    private IValidityWithDiagnostics CheckDifferenceValidity(IProcessingContext context, RawSpecializedSharpMeasuresVectorDefinition definition)
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
