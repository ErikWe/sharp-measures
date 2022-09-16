namespace SharpMeasures.Generators.Scalars.Parsing.SpecializedSharpMeasuresScalar;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Quantities.Parsing.DefaultUnitInstance;

using System;

internal interface ISpecializedSharpMeasuresScalarProcessingDiagnostics : IDefaultUnitInstanceProcessingDiagnostics
{
    public abstract Diagnostic? NullOriginalScalar(IProcessingContext context, RawSpecializedSharpMeasuresScalarDefinition definition);
    public abstract Diagnostic? NullVector(IProcessingContext context, RawSpecializedSharpMeasuresScalarDefinition definition);

    public abstract Diagnostic? NullDifferenceQuantity(IProcessingContext context, RawSpecializedSharpMeasuresScalarDefinition definition);
    public abstract Diagnostic? DifferenceDisabledButQuantitySpecified(IProcessingContext context, RawSpecializedSharpMeasuresScalarDefinition definition);

    public abstract Diagnostic? NullReciprocalQuantity(IProcessingContext context, RawSpecializedSharpMeasuresScalarDefinition definition);
    public abstract Diagnostic? NullSquareQuantity(IProcessingContext context, RawSpecializedSharpMeasuresScalarDefinition definition);
    public abstract Diagnostic? NullCubeQuantity(IProcessingContext context, RawSpecializedSharpMeasuresScalarDefinition definition);
    public abstract Diagnostic? NullSquareRootQuantity(IProcessingContext context, RawSpecializedSharpMeasuresScalarDefinition definition);
    public abstract Diagnostic? NullCubeRootQuantity(IProcessingContext context, RawSpecializedSharpMeasuresScalarDefinition definition);
}

internal class SpecializedSharpMeasuresScalarProcesser : AProcesser<IProcessingContext, RawSpecializedSharpMeasuresScalarDefinition, SpecializedSharpMeasuresScalarDefinition>
{
    private ISpecializedSharpMeasuresScalarProcessingDiagnostics Diagnostics { get; }

    public SpecializedSharpMeasuresScalarProcesser(ISpecializedSharpMeasuresScalarProcessingDiagnostics diagnostics)
    {
        Diagnostics = diagnostics;
    }

    public override IOptionalWithDiagnostics<SpecializedSharpMeasuresScalarDefinition> Process(IProcessingContext context, RawSpecializedSharpMeasuresScalarDefinition definition)
    {
        return VerifyRequiredPropertiesSet(definition)
            .Validate(() => ValidateOriginalScalar(context, definition))
            .Validate(() => ValidateVectorNotNull(context, definition))
            .Validate(() => ValidateDifferenceNotNull(context, definition))
            .Validate(() => ValidateDifferenceNotUnexpectedlySpecified(context, definition))
            .Validate(() => ValidatePowerPropertyNotNull(definition.Locations.ExplicitlySetReciprocal, definition.Reciprocal, () => Diagnostics.NullReciprocalQuantity(context, definition)))
            .Validate(() => ValidatePowerPropertyNotNull(definition.Locations.ExplicitlySetSquare, definition.Square, () => Diagnostics.NullSquareQuantity(context, definition)))
            .Validate(() => ValidatePowerPropertyNotNull(definition.Locations.ExplicitlySetCube, definition.Cube, () => Diagnostics.NullCubeQuantity(context, definition)))
            .Validate(() => ValidatePowerPropertyNotNull(definition.Locations.ExplicitlySetSquareRoot, definition.SquareRoot, () => Diagnostics.NullSquareRootQuantity(context, definition)))
            .Validate(() => ValidatePowerPropertyNotNull(definition.Locations.ExplicitlySetCubeRoot, definition.CubeRoot, () => Diagnostics.NullCubeRootQuantity(context, definition)))
            .Merge(() => DefaultUnitInstanceProcesser.Process(context, Diagnostics, definition))
            .Transform((defaultUnitInstance) => ProduceResult(definition, defaultUnitInstance.Name, defaultUnitInstance.Symbol));
    }

    private static SpecializedSharpMeasuresScalarDefinition ProduceResult(RawSpecializedSharpMeasuresScalarDefinition definition, string? defaultUnitInstanceName, string? defaultUnitInstanceSymbol)
    {
        return new(definition.OriginalQuantity!.Value, definition.InheritDerivations, definition.InheritConstants, definition.InheritConversions, definition.InheritBases, definition.InheritUnits,
            definition.Vector, definition.ImplementSum, definition.ImplementDifference, definition.Difference, defaultUnitInstanceName, defaultUnitInstanceSymbol, definition.Reciprocal, definition.Square, definition.Cube,
            definition.SquareRoot, definition.CubeRoot, definition.GenerateDocumentation, definition.Locations);
    }

    private static IValidityWithDiagnostics VerifyRequiredPropertiesSet(RawSpecializedSharpMeasuresScalarDefinition definition)
    {
        return ValidityWithDiagnostics.ConditionalWithoutDiagnostics(definition.Locations.ExplicitlySetOriginalQuantity);
    }

    private IValidityWithDiagnostics ValidateOriginalScalar(IProcessingContext context, RawSpecializedSharpMeasuresScalarDefinition definition)
    {
        return ValidityWithDiagnostics.Conditional(definition.OriginalQuantity is not null, () => Diagnostics.NullOriginalScalar(context, definition));
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

    private static IValidityWithDiagnostics ValidatePowerPropertyNotNull(bool explicitlySet, NamedType? value, Func<Diagnostic?> nullQuantityDiagnosticsDelegate)
    {
        var powerPropertyIsNull = explicitlySet && value is null;

        return ValidityWithDiagnostics.ValidWithConditionalDiagnostics(powerPropertyIsNull, () => nullQuantityDiagnosticsDelegate());
    }
}
