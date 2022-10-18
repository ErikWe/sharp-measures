namespace SharpMeasures.Generators.Vectors.Parsing.SpecializedSharpMeasuresVectorGroup;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Quantities.Parsing.DefaultUnitInstance;

using System;

internal interface ISpecializedSharpMeasuresVectorGroupProcessingDiagnostics : IDefaultUnitInstanceProcessingDiagnostics
{
    public abstract Diagnostic? UnrecognizedForwardsCastOperatorBehaviour(IProcessingContext context, RawSpecializedSharpMeasuresVectorGroupDefinition definition);
    public abstract Diagnostic? UnrecognizedBackwardsCastOperatorBehaviour(IProcessingContext context, RawSpecializedSharpMeasuresVectorGroupDefinition definition);

    public abstract Diagnostic? NameSuggestsDimension(IProcessingContext context, RawSpecializedSharpMeasuresVectorGroupDefinition definition, int interpretedDimension);

    public abstract Diagnostic? NullOriginalVectorGroup(IProcessingContext context, RawSpecializedSharpMeasuresVectorGroupDefinition definition);
    public abstract Diagnostic? NullScalar(IProcessingContext context, RawSpecializedSharpMeasuresVectorGroupDefinition definition);

    public abstract Diagnostic? DifferenceDisabledButQuantitySpecified(IProcessingContext context, RawSpecializedSharpMeasuresVectorGroupDefinition definition);
    public abstract Diagnostic? NullDifferenceQuantity(IProcessingContext context, RawSpecializedSharpMeasuresVectorGroupDefinition definition);
}

internal sealed class SpecializedSharpMeasuresVectorGroupProcesser : AProcesser<IProcessingContext, RawSpecializedSharpMeasuresVectorGroupDefinition, SpecializedSharpMeasuresVectorGroupDefinition>
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
            .Validate(() => ValidateOriginalVectorGroupNotUndefined(definition))
            .Validate(() => ValidateForwardsCastOperatorBehaviourDefined(context, definition))
            .Validate(() => ValidateBackwardsCastOperatorBehaviourDefined(context, definition))
            .Validate(() => ValidateScalarNotNull(context, definition))
            .Validate(() => ValidateDifferenceNotNull(context, definition))
            .Validate(() => ValidateDifferenceNotUnexpectedlySpecified(context, definition))
            .Merge(() => DefaultUnitInstanceProcesser.Process(context, Diagnostics, definition))
            .Transform((defaultUnitInstance) => ProduceResult(definition, defaultUnitInstance.Name, defaultUnitInstance.Symbol));
    }

    private static SpecializedSharpMeasuresVectorGroupDefinition ProduceResult(RawSpecializedSharpMeasuresVectorGroupDefinition definition, string? defaultUnitInstanceName, string? defaultUnitInstanceSymbol)
    {
        return new(definition.OriginalQuantity!.Value, definition.InheritDerivations, definition.InheritConversions, definition.InheritUnits, definition.ForwardsCastOperatorBehaviour, definition.BackwardsCastOperatorBehaviour, definition.Scalar,
            definition.ImplementSum, definition.ImplementDifference, definition.Difference, defaultUnitInstanceName, defaultUnitInstanceSymbol, definition.Locations);
    }

    private static IValidityWithDiagnostics VerifyRequiredPropertiesSet(RawSpecializedSharpMeasuresVectorGroupDefinition definition)
    {
        return ValidityWithDiagnostics.ConditionalWithoutDiagnostics(definition.Locations.ExplicitlySetOriginalQuantity);
    }

    private IValidityWithDiagnostics ValidateNameNotSuggestingDimension(IProcessingContext context, RawSpecializedSharpMeasuresVectorGroupDefinition definition)
    {
        int? interpretedDimension = DimensionParsingUtility.InterpretDimensionFromName(context.Type.Name);

        return ValidityWithDiagnostics.ValidWithConditionalDiagnostics(interpretedDimension is not null, () => Diagnostics.NameSuggestsDimension(context, definition, interpretedDimension!.Value));
    }

    private IValidityWithDiagnostics ValidateOriginalVectorGroupNotNull(IProcessingContext context, RawSpecializedSharpMeasuresVectorGroupDefinition definition)
    {
        return ValidityWithDiagnostics.Conditional(definition.OriginalQuantity is not null, () => Diagnostics.NullOriginalVectorGroup(context, definition));
    }

    private static IValidityWithDiagnostics ValidateOriginalVectorGroupNotUndefined(RawSpecializedSharpMeasuresVectorGroupDefinition definition)
    {
        return ValidityWithDiagnostics.ConditionalWithoutDiagnostics(definition.OriginalQuantity!.Value != NamedType.Empty);
    }

    private IValidityWithDiagnostics ValidateForwardsCastOperatorBehaviourDefined(IProcessingContext context, RawSpecializedSharpMeasuresVectorGroupDefinition definition)
    {
        var enumDefined = Enum.IsDefined(typeof(ConversionOperatorBehaviour), definition.ForwardsCastOperatorBehaviour);

        return ValidityWithDiagnostics.Conditional(enumDefined, () => Diagnostics.UnrecognizedForwardsCastOperatorBehaviour(context, definition));
    }

    private IValidityWithDiagnostics ValidateBackwardsCastOperatorBehaviourDefined(IProcessingContext context, RawSpecializedSharpMeasuresVectorGroupDefinition definition)
    {
        var enumDefined = Enum.IsDefined(typeof(ConversionOperatorBehaviour), definition.BackwardsCastOperatorBehaviour);

        return ValidityWithDiagnostics.Conditional(enumDefined, () => Diagnostics.UnrecognizedBackwardsCastOperatorBehaviour(context, definition));
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
