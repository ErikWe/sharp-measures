namespace SharpMeasures.Generators.Vectors.Parsing.SpecializedSharpMeasuresVector;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Quantities.Parsing.DefaultUnit;

internal interface ISpecializedSharpMeasuresVectorProcessingDiagnostics : IDefaultUnitProcessingDiagnostics
{
    public abstract Diagnostic? NullOriginalVector(IProcessingContext context, RawSpecializedSharpMeasuresVectorDefinition definition);
    public abstract Diagnostic? NullScalar(IProcessingContext context, RawSpecializedSharpMeasuresVectorDefinition definition);

    public abstract Diagnostic? NullDifferenceQuantity(IProcessingContext context, RawSpecializedSharpMeasuresVectorDefinition definition);
    public abstract Diagnostic? DifferenceDisabledButQuantitySpecified(IProcessingContext context, RawSpecializedSharpMeasuresVectorDefinition definition);
}

internal class SpecializedSharpMeasuresVectorProcesser : AProcesser<IProcessingContext, RawSpecializedSharpMeasuresVectorDefinition, SpecializedSharpMeasuresVectorDefinition>
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
            .Validate(() => ValidateScalarNotNull(context, definition))
            .Validate(() => ValidateDifferenceNotNull(context, definition))
            .Validate(() => ValidateDifferenceNotUnexpectedlySpecified(context, definition))
            .Merge(() => DefaultUnitProcesser.Process(context, Diagnostics, definition))
            .Transform((defaultUnit) => ProduceResult(definition, defaultUnit.Name, defaultUnit.Symbol));
    }

    private static SpecializedSharpMeasuresVectorDefinition ProduceResult(RawSpecializedSharpMeasuresVectorDefinition definition, string? defaultUnitName, string? defaultUnitSymbol)
    {
        return new(definition.OriginalVector!.Value, definition.InheritDerivations, definition.InheritConstants, definition.InheritConversions, definition.InheritUnits, definition.Scalar, definition.ImplementSum,
            definition.ImplementDifference, definition.Difference, defaultUnitName, defaultUnitSymbol, definition.GenerateDocumentation, definition.Locations);
    }

    private static IValidityWithDiagnostics VerifyRequiredPropertiesSet(RawSpecializedSharpMeasuresVectorDefinition definition)
    {
        return ValidityWithDiagnostics.ConditionalWithoutDiagnostics(definition.Locations.ExplicitlySetOriginalVector);
    }

    private IValidityWithDiagnostics ValidateOriginalVectorNotNull(IProcessingContext context, RawSpecializedSharpMeasuresVectorDefinition definition)
    {
        return ValidityWithDiagnostics.Conditional(definition.OriginalVector is not null, () => Diagnostics.NullOriginalVector(context, definition));
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
