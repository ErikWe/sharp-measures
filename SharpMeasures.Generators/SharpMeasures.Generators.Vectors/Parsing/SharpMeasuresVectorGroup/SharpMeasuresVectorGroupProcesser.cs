namespace SharpMeasures.Generators.Vectors.Parsing.SharpMeasuresVectorGroup;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Quantities.Parsing.DefaultUnit;

internal interface ISharpMeasuresVectorGroupProcessingDiagnostics : IDefaultUnitProcessingDiagnostics
{
    public abstract Diagnostic? NameSuggestsDimension(IProcessingContext context, RawSharpMeasuresVectorGroupDefinition definition, int interpretedDimension);

    public abstract Diagnostic? NullUnit(IProcessingContext context, RawSharpMeasuresVectorGroupDefinition definition);
    public abstract Diagnostic? NullScalar(IProcessingContext context, RawSharpMeasuresVectorGroupDefinition definition);

    public abstract Diagnostic? DifferenceDisabledButQuantitySpecified(IProcessingContext context, RawSharpMeasuresVectorGroupDefinition definition);
    public abstract Diagnostic? NullDifferenceQuantity(IProcessingContext context, RawSharpMeasuresVectorGroupDefinition definition);
}

internal class SharpMeasuresVectorGroupProcesser : AProcesser<IProcessingContext, RawSharpMeasuresVectorGroupDefinition, SharpMeasuresVectorGroupDefinition>
{
    private ISharpMeasuresVectorGroupProcessingDiagnostics Diagnostics { get; }

    public SharpMeasuresVectorGroupProcesser(ISharpMeasuresVectorGroupProcessingDiagnostics diagnostics)
    {
        Diagnostics = diagnostics;
    }

    public override IOptionalWithDiagnostics<SharpMeasuresVectorGroupDefinition> Process(IProcessingContext context, RawSharpMeasuresVectorGroupDefinition definition)
    {
        return VerifyRequiredPropertiesSet(definition)
            .Validate(() => ValidateNameNotSuggestingDimension(context, definition))
            .Validate(() => ValidateUnitNotNull(context, definition))
            .Validate(() => ValidateScalarNotNull(context, definition))
            .Validate(() => ValidateDifferenceNotNull(context, definition))
            .Validate(() => ValidateDifferenceNotUnexpectedlySpecified(context, definition))
            .Merge(() => DefaultUnitProcesser.Process(context, Diagnostics, definition))
            .Transform((defaultUnit) => ProduceResult(definition, defaultUnit.Name, defaultUnit.Symbol));
    }

    private static SharpMeasuresVectorGroupDefinition ProduceResult(RawSharpMeasuresVectorGroupDefinition definition, string? defaultUnitName, string? defaultUnitSymbol)
    {
        return new(definition.Unit!.Value, definition.Scalar, definition.ImplementSum, definition.ImplementDifference, definition.Difference, defaultUnitName, defaultUnitSymbol,
            definition.GenerateDocumentation, definition.Locations);
    }

    private static IValidityWithDiagnostics VerifyRequiredPropertiesSet(RawSharpMeasuresVectorGroupDefinition definition)
    {
        return ValidityWithDiagnostics.ConditionalWithoutDiagnostics(definition.Locations.ExplicitlySetUnit);
    }

    private IValidityWithDiagnostics ValidateNameNotSuggestingDimension(IProcessingContext context, RawSharpMeasuresVectorGroupDefinition definition)
    {
        int? interpretedDimension = DimensionParsingUtility.InterpretDimensionFromName(context.Type.Name);

        return ValidityWithDiagnostics.ValidWithConditionalDiagnostics(interpretedDimension is not null, () => Diagnostics.NameSuggestsDimension(context, definition, interpretedDimension!.Value));
    }

    private IValidityWithDiagnostics ValidateUnitNotNull(IProcessingContext context, RawSharpMeasuresVectorGroupDefinition definition)
    {
        return ValidityWithDiagnostics.Conditional(definition.Unit is not null, () => Diagnostics.NullUnit(context, definition));
    }

    private IValidityWithDiagnostics ValidateScalarNotNull(IProcessingContext context, RawSharpMeasuresVectorGroupDefinition definition)
    {
        var scalarIsNull = definition.Locations.ExplicitlySetScalar && definition.Scalar is null;

        return ValidityWithDiagnostics.ValidWithConditionalDiagnostics(scalarIsNull, () => Diagnostics.NullScalar(context, definition));
    }

    private IValidityWithDiagnostics ValidateDifferenceNotNull(IProcessingContext context, RawSharpMeasuresVectorGroupDefinition definition)
    {
        var differenceIsNull = definition.Locations.ExplicitlySetDifference && definition.Difference is null;

        return ValidityWithDiagnostics.ValidWithConditionalDiagnostics(differenceIsNull, () => Diagnostics.NullDifferenceQuantity(context, definition));
    }

    private IValidityWithDiagnostics ValidateDifferenceNotUnexpectedlySpecified(IProcessingContext context, RawSharpMeasuresVectorGroupDefinition definition)
    {
        var differenceUnexpectedlySpecified = definition.ImplementDifference is false && definition.Locations.ExplicitlySetDifference;

        return ValidityWithDiagnostics.ValidWithConditionalDiagnostics(differenceUnexpectedlySpecified, () => Diagnostics.DifferenceDisabledButQuantitySpecified(context, definition));
    }
}
