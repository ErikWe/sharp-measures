namespace SharpMeasures.Generators.Vectors.Parsing.SharpMeasuresVector;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Quantities.Parsing.DefaultUnit;

internal interface ISharpMeasuresVectorProcessingDiagnostics : IDefaultUnitProcessingDiagnostics
{
    public abstract Diagnostic? NullUnit(IProcessingContext context, RawSharpMeasuresVectorDefinition definition);
    public abstract Diagnostic? NullScalar(IProcessingContext context, RawSharpMeasuresVectorDefinition definition);

    public abstract Diagnostic? MissingDimension(IProcessingContext context, RawSharpMeasuresVectorDefinition definition);
    public abstract Diagnostic? InvalidDimension(IProcessingContext context, RawSharpMeasuresVectorDefinition definition);
    public abstract Diagnostic? InvalidInterpretedDimension(IProcessingContext context, RawSharpMeasuresVectorDefinition definition, int dimension);
    public abstract Diagnostic? VectorNameAndDimensionConflict(IProcessingContext context, RawSharpMeasuresVectorDefinition definition, int interpretedDimension);

    public abstract Diagnostic? DifferenceDisabledButQuantitySpecified(IProcessingContext context, RawSharpMeasuresVectorDefinition definition);
    public abstract Diagnostic? NullDifferenceQuantity(IProcessingContext context, RawSharpMeasuresVectorDefinition definition);
}

internal class SharpMeasuresVectorProcesser : AProcesser<IProcessingContext, RawSharpMeasuresVectorDefinition, SharpMeasuresVectorDefinition>
{
    private ISharpMeasuresVectorProcessingDiagnostics Diagnostics { get; }

    public SharpMeasuresVectorProcesser(ISharpMeasuresVectorProcessingDiagnostics diagnostics)
    {
        Diagnostics = diagnostics;
    }

    public override IOptionalWithDiagnostics<SharpMeasuresVectorDefinition> Process(IProcessingContext context, RawSharpMeasuresVectorDefinition definition)
    {
        var dimension = VerifyRequiredPropertiesSet(definition)
            .Validate(() => ValidateSpecifiedDimensionNotInvalid(context, definition))
            .Merge(() => ProcessDimension(context, definition))
            .Validate((dimension) => ValidateInterpretedDimensionNotInvalid(context, definition, dimension))
            .Validate((dimension) => ValidateSpecifiedAndInterpretedDimensionNotConflicts(context, definition, dimension));

        if (dimension.LacksResult)
        {
            return dimension.AsEmptyOptional<SharpMeasuresVectorDefinition>();
        }

        return dimension
            .Reduce()
            .Validate(() => ValidateUnitNotNull(context, definition))
            .Validate(() => ValidateScalarNotNull(context, definition))
            .Validate(() => ValidateDifferenceNotNull(context, definition))
            .Validate(() => ValidateDifferenceNotUnexpectedlySpecified(context, definition))
            .Merge(() => DefaultUnitProcesser.Process(context, Diagnostics, definition))
            .Transform((defaultUnit) => ProduceResult(definition, dimension.Result, defaultUnit.Name, defaultUnit.Symbol));
    }

    private static SharpMeasuresVectorDefinition ProduceResult(RawSharpMeasuresVectorDefinition definition, int dimension, string? defaultUnitName, string? defaultUnitSymbol)
    {
        return new(definition.Unit!.Value, definition.Scalar, dimension, definition.ImplementSum, definition.ImplementDifference, definition.Difference, defaultUnitName, defaultUnitSymbol,
            definition.GenerateDocumentation, definition.Locations);
    }

    private static IValidityWithDiagnostics VerifyRequiredPropertiesSet(RawSharpMeasuresVectorDefinition definition)
    {
        return ValidityWithDiagnostics.ConditionalWithoutDiagnostics(definition.Locations.ExplicitlySetUnit);
    }

    private IValidityWithDiagnostics ValidateSpecifiedDimensionNotInvalid(IProcessingContext context, RawSharpMeasuresVectorDefinition definition)
    {
        var invalidSpecifiedDimension = definition.Locations.ExplicitlySetDimension && DimensionParsingUtility.CheckVectorDimensionValidity(definition.Dimension!.Value) is false;

        return ValidityWithDiagnostics.Conditional(invalidSpecifiedDimension is false, () => Diagnostics.InvalidDimension(context, definition));
    }

    private IOptionalWithDiagnostics<int> ProcessDimension(IProcessingContext context, RawSharpMeasuresVectorDefinition definition)
    {
        int? dimension = DimensionParsingUtility.InterpretDimensionFromName(context.Type.Name) ?? definition.Dimension;

        return OptionalWithDiagnostics.Conditional(dimension is not null, () => dimension!.Value, () => Diagnostics.MissingDimension(context, definition));
    }

    private IValidityWithDiagnostics ValidateInterpretedDimensionNotInvalid(IProcessingContext context, RawSharpMeasuresVectorDefinition definition, int dimension)
    {
        var invalidInterpretedDimension = definition.Locations.ExplicitlySetDimension is false && DimensionParsingUtility.CheckVectorDimensionValidity(dimension) is false;

        return ValidityWithDiagnostics.Conditional(invalidInterpretedDimension is false, () => Diagnostics.InvalidInterpretedDimension(context, definition, dimension));
    }

    private IValidityWithDiagnostics ValidateSpecifiedAndInterpretedDimensionNotConflicts(IProcessingContext context, RawSharpMeasuresVectorDefinition definition, int dimension)
    {
        var specifiedAndInterpretedDimensionConflicts = definition.Locations.ExplicitlySetDimension && dimension != definition.Dimension;

        return ValidityWithDiagnostics.Conditional(specifiedAndInterpretedDimensionConflicts is false, () => Diagnostics.VectorNameAndDimensionConflict(context, definition, dimension));
    }

    private IValidityWithDiagnostics ValidateUnitNotNull(IProcessingContext context, RawSharpMeasuresVectorDefinition definition)
    {
        return ValidityWithDiagnostics.Conditional(definition.Unit is not null, () => Diagnostics.NullUnit(context, definition));
    }

    private IValidityWithDiagnostics ValidateScalarNotNull(IProcessingContext context, RawSharpMeasuresVectorDefinition definition)
    {
        var scalarIsNull = definition.Locations.ExplicitlySetScalar && definition.Scalar is null;

        return ValidityWithDiagnostics.ValidWithConditionalDiagnostics(scalarIsNull, () => Diagnostics.NullScalar(context, definition));
    }

    private IValidityWithDiagnostics ValidateDifferenceNotNull(IProcessingContext context, RawSharpMeasuresVectorDefinition definition)
    {
        var differenceIsNull = definition.Locations.ExplicitlySetDifference && definition.Difference is null;

        return ValidityWithDiagnostics.ValidWithConditionalDiagnostics(differenceIsNull, () => Diagnostics.NullDifferenceQuantity(context, definition));
    }

    private IValidityWithDiagnostics ValidateDifferenceNotUnexpectedlySpecified(IProcessingContext context, RawSharpMeasuresVectorDefinition definition)
    {
        var differenceUnexpectedlySpecified = definition.ImplementDifference is false && definition.Locations.ExplicitlySetDifference;

        return ValidityWithDiagnostics.ValidWithConditionalDiagnostics(differenceUnexpectedlySpecified, () => Diagnostics.DifferenceDisabledButQuantitySpecified(context, definition));
    }
}
