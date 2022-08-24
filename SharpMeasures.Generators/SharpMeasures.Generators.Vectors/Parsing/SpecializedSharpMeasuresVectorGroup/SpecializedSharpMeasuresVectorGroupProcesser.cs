namespace SharpMeasures.Generators.Vectors.Parsing.SpecializedSharpMeasuresVectorGroup;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Quantities.Parsing.DefaultUnit;

internal interface ISpecializedSharpMeasuresVectorGroupProcessingDiagnostics : IDefaultUnitProcessingDiagnostics
{
    public abstract Diagnostic? NameSuggestsDimension(IProcessingContext context, RawSpecializedSharpMeasuresVectorGroupDefinition definition, int interpretedDimension);

    public abstract Diagnostic? NullOriginalVectorGroup(IProcessingContext context, RawSpecializedSharpMeasuresVectorGroupDefinition definition);
    public abstract Diagnostic? NullScalar(IProcessingContext context, RawSpecializedSharpMeasuresVectorGroupDefinition definition);

    public abstract Diagnostic? DifferenceDisabledButQuantitySpecified(IProcessingContext context, RawSpecializedSharpMeasuresVectorGroupDefinition definition);
    public abstract Diagnostic? NullDifferenceQuantity(IProcessingContext context, RawSpecializedSharpMeasuresVectorGroupDefinition definition);
}

internal class SpecializedSharpMeasuresVectorGroupProcesser : AProcesser<IProcessingContext, RawSpecializedSharpMeasuresVectorGroupDefinition, SpecializedSharpMeasuresVectorGroupDefinition>
{
    private ISpecializedSharpMeasuresVectorGroupProcessingDiagnostics Diagnostics { get; }

    public SpecializedSharpMeasuresVectorGroupProcesser(ISpecializedSharpMeasuresVectorGroupProcessingDiagnostics diagnostics)
    {
        Diagnostics = diagnostics;
    }

    public override IOptionalWithDiagnostics<SpecializedSharpMeasuresVectorGroupDefinition> Process(IProcessingContext context, RawSpecializedSharpMeasuresVectorGroupDefinition definition)
    {
        return VerifyRequiredPropertiesSet(definition)
            .Validate(() => ValidateNameNotSuggestingDimension(context, definition))
            .Validate(() => ValidateOriginalVectorGroupNotNull(context, definition))
            .Validate(() => ValidateScalarNotNull(context, definition))
            .Validate(() => ValidateDifferenceNotNull(context, definition))
            .Validate(() => ValidateDifferenceNotUnexpectedlySpecified(context, definition))
            .Merge(() => DefaultUnitProcesser.Process(context, Diagnostics, definition))
            .Transform((defaultUnit) => ProduceResult(definition, defaultUnit.Name, defaultUnit.Symbol));
    }

    private static SpecializedSharpMeasuresVectorGroupDefinition ProduceResult(RawSpecializedSharpMeasuresVectorGroupDefinition definition, string? defaultUnitName, string? defaultUnitSymbol)
    {
        return new(definition.OriginalVectorGroup!.Value, definition.InheritDerivations, definition.InheritConstants, definition.InheritConversions, definition.InheritUnits, definition.Scalar,
            definition.ImplementSum, definition.ImplementDifference, definition.Difference, defaultUnitName, defaultUnitSymbol, definition.GenerateDocumentation, definition.Locations);
    }

    private static IValidityWithDiagnostics VerifyRequiredPropertiesSet(RawSpecializedSharpMeasuresVectorGroupDefinition definition)
    {
        return ValidityWithDiagnostics.ConditionalWithoutDiagnostics(definition.Locations.ExplicitlySetOriginalVectorGroup);
    }

    private IValidityWithDiagnostics ValidateNameNotSuggestingDimension(IProcessingContext context, RawSpecializedSharpMeasuresVectorGroupDefinition definition)
    {
        int? interpretedDimension = DimensionParsingUtility.InterpretDimensionFromName(context.Type.Name);

        return ValidityWithDiagnostics.ValidWithConditionalDiagnostics(interpretedDimension is not null, () => Diagnostics.NameSuggestsDimension(context, definition, interpretedDimension!.Value));
    }

    private IValidityWithDiagnostics ValidateOriginalVectorGroupNotNull(IProcessingContext context, RawSpecializedSharpMeasuresVectorGroupDefinition definition)
    {
        return ValidityWithDiagnostics.Conditional(definition.OriginalVectorGroup is not null, () => Diagnostics.NullOriginalVectorGroup(context, definition));
    }

    private IValidityWithDiagnostics ValidateScalarNotNull(IProcessingContext context, RawSpecializedSharpMeasuresVectorGroupDefinition definition)
    {
        var scalarIsNull = definition.Locations.ExplicitlySetScalar && definition.Scalar is null;

        return ValidityWithDiagnostics.ValidWithConditionalDiagnostics(scalarIsNull, () => Diagnostics.NullScalar(context, definition));
    }

    private IValidityWithDiagnostics ValidateDifferenceNotNull(IProcessingContext context, RawSpecializedSharpMeasuresVectorGroupDefinition definition)
    {
        var differenceIsNull = definition.Locations.ExplicitlySetDifference && definition.Difference is null;

        return ValidityWithDiagnostics.ValidWithConditionalDiagnostics(differenceIsNull, () => Diagnostics.NullDifferenceQuantity(context, definition));
    }

    private IValidityWithDiagnostics ValidateDifferenceNotUnexpectedlySpecified(IProcessingContext context, RawSpecializedSharpMeasuresVectorGroupDefinition definition)
    {
        var differenceUnexpectedlySpecified = definition.ImplementDifference is false && definition.Locations.ExplicitlySetDifference;

        return ValidityWithDiagnostics.ValidWithConditionalDiagnostics(differenceUnexpectedlySpecified, () => Diagnostics.DifferenceDisabledButQuantitySpecified(context, definition));
    }
}
