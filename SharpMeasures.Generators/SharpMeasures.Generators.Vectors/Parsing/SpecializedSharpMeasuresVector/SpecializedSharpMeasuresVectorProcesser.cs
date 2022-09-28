namespace SharpMeasures.Generators.Vectors.Parsing.SpecializedSharpMeasuresVector;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Quantities.Parsing.DefaultUnitInstance;

using System;

internal interface ISpecializedSharpMeasuresVectorProcessingDiagnostics : IDefaultUnitInstanceProcessingDiagnostics
{
    public abstract Diagnostic? UnrecognizedForwardsCastOperatorBehaviour(IProcessingContext context, RawSpecializedSharpMeasuresVectorDefinition definition);
    public abstract Diagnostic? UnrecognizedBackwardsCastOperatorBehaviour(IProcessingContext context, RawSpecializedSharpMeasuresVectorDefinition definition);

    public abstract Diagnostic? NullOriginalVector(IProcessingContext context, RawSpecializedSharpMeasuresVectorDefinition definition);
    public abstract Diagnostic? NullScalar(IProcessingContext context, RawSpecializedSharpMeasuresVectorDefinition definition);

    public abstract Diagnostic? NullDifferenceQuantity(IProcessingContext context, RawSpecializedSharpMeasuresVectorDefinition definition);
    public abstract Diagnostic? DifferenceDisabledButQuantitySpecified(IProcessingContext context, RawSpecializedSharpMeasuresVectorDefinition definition);
}

internal sealed class SpecializedSharpMeasuresVectorProcesser : AProcesser<IProcessingContext, RawSpecializedSharpMeasuresVectorDefinition, SpecializedSharpMeasuresVectorDefinition>
{
    private ISpecializedSharpMeasuresVectorProcessingDiagnostics Diagnostics { get; }

    public SpecializedSharpMeasuresVectorProcesser(ISpecializedSharpMeasuresVectorProcessingDiagnostics diagnostics)
    {
        Diagnostics = diagnostics;
    }

    public override IOptionalWithDiagnostics<SpecializedSharpMeasuresVectorDefinition> Process(IProcessingContext context, RawSpecializedSharpMeasuresVectorDefinition definition)
    {
        return VerifyRequiredPropertiesSet(definition)
            .Validate(() => ValidateOriginalVectorNotNull(context, definition))
            .Validate(() => ValidateForwardsCastOperatorBehaviourDefined(context, definition))
            .Validate(() => ValidateBackwardsCastOperatorBehaviourDefined(context, definition))
            .Validate(() => ValidateScalarNotNull(context, definition))
            .Validate(() => ValidateDifferenceNotNull(context, definition))
            .Validate(() => ValidateDifferenceNotUnexpectedlySpecified(context, definition))
            .Merge(() => DefaultUnitInstanceProcesser.Process(context, Diagnostics, definition))
            .Transform((defaultUnitInstance) => ProduceResult(definition, defaultUnitInstance.Name, defaultUnitInstance.Symbol));
    }

    private static SpecializedSharpMeasuresVectorDefinition ProduceResult(RawSpecializedSharpMeasuresVectorDefinition definition, string? defaultUnitInstanceName, string? defaultUnitInstanceSymbol)
    {
        return new(definition.OriginalQuantity!.Value, definition.InheritDerivations, definition.InheritProcesses, definition.InheritConstants, definition.InheritConversions, definition.InheritUnits, definition.ForwardsCastOperatorBehaviour, definition.BackwardsCastOperatorBehaviour,
            definition.Scalar, definition.ImplementSum, definition.ImplementDifference, definition.Difference, defaultUnitInstanceName, defaultUnitInstanceSymbol, definition.GenerateDocumentation, definition.Locations);
    }

    private static IValidityWithDiagnostics VerifyRequiredPropertiesSet(RawSpecializedSharpMeasuresVectorDefinition definition)
    {
        return ValidityWithDiagnostics.ConditionalWithoutDiagnostics(definition.Locations.ExplicitlySetOriginalQuantity);
    }

    private IValidityWithDiagnostics ValidateOriginalVectorNotNull(IProcessingContext context, RawSpecializedSharpMeasuresVectorDefinition definition)
    {
        return ValidityWithDiagnostics.Conditional(definition.OriginalQuantity is not null, () => Diagnostics.NullOriginalVector(context, definition));
    }

    private IValidityWithDiagnostics ValidateForwardsCastOperatorBehaviourDefined(IProcessingContext context, RawSpecializedSharpMeasuresVectorDefinition definition)
    {
        var enumDefined = Enum.IsDefined(typeof(ConversionOperatorBehaviour), definition.ForwardsCastOperatorBehaviour);

        return ValidityWithDiagnostics.Conditional(enumDefined, () => Diagnostics.UnrecognizedForwardsCastOperatorBehaviour(context, definition));
    }

    private IValidityWithDiagnostics ValidateBackwardsCastOperatorBehaviourDefined(IProcessingContext context, RawSpecializedSharpMeasuresVectorDefinition definition)
    {
        var enumDefined = Enum.IsDefined(typeof(ConversionOperatorBehaviour), definition.BackwardsCastOperatorBehaviour);

        return ValidityWithDiagnostics.Conditional(enumDefined, () => Diagnostics.UnrecognizedBackwardsCastOperatorBehaviour(context, definition));
    }

    private IValidityWithDiagnostics ValidateScalarNotNull(IProcessingContext context, RawSpecializedSharpMeasuresVectorDefinition definition)
    {
        var scalarIsNull = definition.Locations.ExplicitlySetScalar && definition.Scalar is null;

        return ValidityWithDiagnostics.ValidWithConditionalDiagnostics(scalarIsNull, () => Diagnostics.NullScalar(context, definition));
    }

    private IValidityWithDiagnostics ValidateDifferenceNotNull(IProcessingContext context, RawSpecializedSharpMeasuresVectorDefinition definition)
    {
        var differenceIsNull = definition.Locations.ExplicitlySetDifference && definition.Difference is null;

        return ValidityWithDiagnostics.ValidWithConditionalDiagnostics(differenceIsNull, () => Diagnostics.NullDifferenceQuantity(context, definition));
    }

    private IValidityWithDiagnostics ValidateDifferenceNotUnexpectedlySpecified(IProcessingContext context, RawSpecializedSharpMeasuresVectorDefinition definition)
    {
        var differenceUnexpectedlySpecified = definition.ImplementDifference is false && definition.Locations.ExplicitlySetDifference;

        return ValidityWithDiagnostics.ValidWithConditionalDiagnostics(differenceUnexpectedlySpecified, () => Diagnostics.DifferenceDisabledButQuantitySpecified(context, definition));
    }
}
