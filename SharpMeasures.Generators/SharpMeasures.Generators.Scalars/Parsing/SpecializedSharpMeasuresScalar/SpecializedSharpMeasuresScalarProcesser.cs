namespace SharpMeasures.Generators.Scalars.Parsing.SpecializedSharpMeasuresScalar;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Quantities.Parsing.DefaultUnitInstance;

using System;

internal interface ISpecializedSharpMeasuresScalarProcessingDiagnostics : IDefaultUnitInstanceProcessingDiagnostics
{
    public abstract Diagnostic? NullOriginalScalar(IProcessingContext context, RawSpecializedSharpMeasuresScalarDefinition definition);

    public abstract Diagnostic? UnrecognizedForwardsCastOperatorBehaviour(IProcessingContext context, RawSpecializedSharpMeasuresScalarDefinition definition);
    public abstract Diagnostic? UnrecognizedBackwardsCastOperatorBehaviour(IProcessingContext context, RawSpecializedSharpMeasuresScalarDefinition definition);

    public abstract Diagnostic? NullVector(IProcessingContext context, RawSpecializedSharpMeasuresScalarDefinition definition);

    public abstract Diagnostic? NullDifferenceQuantity(IProcessingContext context, RawSpecializedSharpMeasuresScalarDefinition definition);
    public abstract Diagnostic? DifferenceDisabledButQuantitySpecified(IProcessingContext context, RawSpecializedSharpMeasuresScalarDefinition definition);
}

internal sealed class SpecializedSharpMeasuresScalarProcesser : AProcesser<IProcessingContext, RawSpecializedSharpMeasuresScalarDefinition, SpecializedSharpMeasuresScalarDefinition>
{
    private ISpecializedSharpMeasuresScalarProcessingDiagnostics Diagnostics { get; }

    public SpecializedSharpMeasuresScalarProcesser(ISpecializedSharpMeasuresScalarProcessingDiagnostics diagnostics)
    {
        Diagnostics = diagnostics;
    }

    public override IOptionalWithDiagnostics<SpecializedSharpMeasuresScalarDefinition> Process(IProcessingContext context, RawSpecializedSharpMeasuresScalarDefinition definition)
    {
        return VerifyRequiredPropertiesSet(definition)
            .Validate(() => ValidateOriginalScalarNotNull(context, definition))
            .Validate(() => ValidateOriginalScalarNotUndefined(definition))
            .Validate(() => ValidateForwardsCastOperatorBehaviourDefined(context, definition))
            .Validate(() => ValidateBackwardsCastOperatorBehaviourDefined(context, definition))
            .Validate(() => ValidateVectorNotNull(context, definition))
            .Validate(() => ValidateDifferenceNotNull(context, definition))
            .Validate(() => ValidateDifferenceNotUnexpectedlySpecified(context, definition))
            .Merge(() => DefaultUnitInstanceProcesser.Process(context, Diagnostics, definition))
            .Transform((defaultUnitInstance) => ProduceResult(definition, defaultUnitInstance.Name, defaultUnitInstance.Symbol));
    }

    private static SpecializedSharpMeasuresScalarDefinition ProduceResult(RawSpecializedSharpMeasuresScalarDefinition definition, string? defaultUnitInstanceName, string? defaultUnitInstanceSymbol)
    {
        return new(definition.OriginalQuantity!.Value, definition.InheritDerivations, definition.InheritProcesses, definition.InheritConstants, definition.InheritConversions, definition.InheritBases, definition.InheritUnits, definition.ForwardsCastOperatorBehaviour, definition.BackwardsCastOperatorBehaviour, definition.Vector, definition.ImplementSum, definition.ImplementDifference, definition.Difference, defaultUnitInstanceName, defaultUnitInstanceSymbol, definition.Locations);
    }

    private static IValidityWithDiagnostics VerifyRequiredPropertiesSet(RawSpecializedSharpMeasuresScalarDefinition definition)
    {
        return ValidityWithDiagnostics.ConditionalWithoutDiagnostics(definition.Locations.ExplicitlySetOriginalQuantity);
    }

    private IValidityWithDiagnostics ValidateOriginalScalarNotNull(IProcessingContext context, RawSpecializedSharpMeasuresScalarDefinition definition)
    {
        return ValidityWithDiagnostics.Conditional(definition.OriginalQuantity is not null, () => Diagnostics.NullOriginalScalar(context, definition));
    }

    private static IValidityWithDiagnostics ValidateOriginalScalarNotUndefined(RawSpecializedSharpMeasuresScalarDefinition definition)
    {
        return ValidityWithDiagnostics.ConditionalWithoutDiagnostics(definition.OriginalQuantity!.Value != NamedType.Empty);
    }

    private IValidityWithDiagnostics ValidateForwardsCastOperatorBehaviourDefined(IProcessingContext context, RawSpecializedSharpMeasuresScalarDefinition definition)
    {
        var enumDefined = Enum.IsDefined(typeof(ConversionOperatorBehaviour), definition.ForwardsCastOperatorBehaviour);

        return ValidityWithDiagnostics.Conditional(enumDefined, () => Diagnostics.UnrecognizedForwardsCastOperatorBehaviour(context, definition));
    }

    private IValidityWithDiagnostics ValidateBackwardsCastOperatorBehaviourDefined(IProcessingContext context, RawSpecializedSharpMeasuresScalarDefinition definition)
    {
        var enumDefined = Enum.IsDefined(typeof(ConversionOperatorBehaviour), definition.BackwardsCastOperatorBehaviour);

        return ValidityWithDiagnostics.Conditional(enumDefined, () => Diagnostics.UnrecognizedBackwardsCastOperatorBehaviour(context, definition));
    }

    private IValidityWithDiagnostics ValidateVectorNotNull(IProcessingContext context, RawSpecializedSharpMeasuresScalarDefinition definition)
    {
        var vectorIsNull = definition.Locations.ExplicitlySetVector && definition.Vector is null;

        return ValidityWithDiagnostics.ValidWithConditionalDiagnostics(vectorIsNull, () => Diagnostics.NullVector(context, definition));
    }

    private IValidityWithDiagnostics ValidateDifferenceNotNull(IProcessingContext context, RawSpecializedSharpMeasuresScalarDefinition definition)
    {
        var differenceIsNull = definition.Locations.ExplicitlySetDifference && definition.Difference is null;

        return ValidityWithDiagnostics.ValidWithConditionalDiagnostics(differenceIsNull, () => Diagnostics.NullDifferenceQuantity(context, definition));
    }

    private IValidityWithDiagnostics ValidateDifferenceNotUnexpectedlySpecified(IProcessingContext context, RawSpecializedSharpMeasuresScalarDefinition definition)
    {
        var differenceUnexpectedlySpecified = definition.ImplementDifference is false && definition.Locations.ExplicitlySetDifference;

        return ValidityWithDiagnostics.ValidWithConditionalDiagnostics(differenceUnexpectedlySpecified, () => Diagnostics.DifferenceDisabledButQuantitySpecified(context, definition));
    }
}
