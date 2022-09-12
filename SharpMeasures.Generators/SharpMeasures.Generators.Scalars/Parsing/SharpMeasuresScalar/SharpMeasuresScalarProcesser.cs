namespace SharpMeasures.Generators.Scalars.Parsing.SharpMeasuresScalar;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Quantities.Parsing.DefaultUnitInstance;

using System;

internal interface ISharpMeasuresScalarProcessingDiagnostics : IDefaultUnitInstanceProcessingDiagnostics
{
    public abstract Diagnostic? NullUnit(IProcessingContext context, RawSharpMeasuresScalarDefinition definition);
    public abstract Diagnostic? NullVector(IProcessingContext context, RawSharpMeasuresScalarDefinition definition);

    public abstract Diagnostic? DifferenceDisabledButQuantitySpecified(IProcessingContext context, RawSharpMeasuresScalarDefinition definition);
    public abstract Diagnostic? NullDifferenceQuantity(IProcessingContext context, RawSharpMeasuresScalarDefinition definition);

    public abstract Diagnostic? NullReciprocalQuantity(IProcessingContext context, RawSharpMeasuresScalarDefinition definition);
    public abstract Diagnostic? NullSquareQuantity(IProcessingContext context, RawSharpMeasuresScalarDefinition definition);
    public abstract Diagnostic? NullCubeQuantity(IProcessingContext context, RawSharpMeasuresScalarDefinition definition);
    public abstract Diagnostic? NullSquareRootQuantity(IProcessingContext context, RawSharpMeasuresScalarDefinition definition);
    public abstract Diagnostic? NullCubeRootQuantity(IProcessingContext context, RawSharpMeasuresScalarDefinition definition);
}

internal class SharpMeasuresScalarProcesser : AProcesser<IProcessingContext, RawSharpMeasuresScalarDefinition, SharpMeasuresScalarDefinition>
{
    private ISharpMeasuresScalarProcessingDiagnostics Diagnostics { get; }

    public SharpMeasuresScalarProcesser(ISharpMeasuresScalarProcessingDiagnostics diagnostics)
    {
        Diagnostics = diagnostics;
    }

    public override IOptionalWithDiagnostics<SharpMeasuresScalarDefinition> Process(IProcessingContext context, RawSharpMeasuresScalarDefinition definition)
    {
        return VerifyRequiredPropertiesSet(definition)
            .Validate(() => ValidateUnitNotNull(context, definition))
            .Validate(() => ValidateVectorNotNull(context, definition))
            .Validate(() => ValidateDifferenceNotNull(context, definition))
            .Validate(() => ValidateDifferenceNotUnexpectedlySpecified(context, definition))
            .Validate(() => ValidatePowerPropertyNotNull(definition.Locations.ExplicitlySetReciprocal, definition.Reciprocal, () => Diagnostics.NullReciprocalQuantity(context, definition)))
            .Validate(() => ValidatePowerPropertyNotNull(definition.Locations.ExplicitlySetSquare, definition.Square, () => Diagnostics.NullSquareQuantity(context, definition)))
            .Validate(() => ValidatePowerPropertyNotNull(definition.Locations.ExplicitlySetCube, definition.Cube, () => Diagnostics.NullCubeQuantity(context, definition)))
            .Validate(() => ValidatePowerPropertyNotNull(definition.Locations.ExplicitlySetSquareRoot, definition.SquareRoot, () => Diagnostics.NullSquareRootQuantity(context, definition)))
            .Validate(() => ValidatePowerPropertyNotNull(definition.Locations.ExplicitlySetCubeRoot, definition.CubeRoot, () => Diagnostics.NullCubeRootQuantity(context, definition)))
            .Merge(() => DefaultUnitInstanceProcesser.Process(context, Diagnostics, definition))
            .Transform((defaultUnitInstance) => ProduceResult(context, definition, defaultUnitInstance.Name, defaultUnitInstance.Symbol));
    }

    private static SharpMeasuresScalarDefinition ProduceResult(IProcessingContext context, RawSharpMeasuresScalarDefinition definition, string? defaultUnitInstanceName, string? defaultUnitInstanceSymbol)
    {
        var difference = definition.Difference;

        if (definition.Locations.ExplicitlySetDifference is false)
        {
            difference = context.Type.AsNamedType();
        }

        return new(definition.Unit!.Value, definition.Vector, definition.UseUnitBias, definition.ImplementSum, definition.ImplementDifference, difference, defaultUnitInstanceName,
            defaultUnitInstanceSymbol, definition.Reciprocal, definition.Square, definition.Cube, definition.SquareRoot, definition.CubeRoot, definition.GenerateDocumentation, definition.Locations);
    }

    private static IValidityWithDiagnostics VerifyRequiredPropertiesSet(RawSharpMeasuresScalarDefinition definition)
    {
        return ValidityWithDiagnostics.ConditionalWithoutDiagnostics(definition.Locations.ExplicitlySetUnit);
    }

    private IValidityWithDiagnostics ValidateUnitNotNull(IProcessingContext context, RawSharpMeasuresScalarDefinition definition)
    {
        return ValidityWithDiagnostics.Conditional(definition.Unit is not null, () => Diagnostics.NullUnit(context, definition));
    }

    private IValidityWithDiagnostics ValidateVectorNotNull(IProcessingContext context, RawSharpMeasuresScalarDefinition definition)
    {
        var vectorIsNull = definition.Locations.ExplicitlySetVector && definition.Vector is null;

        return ValidityWithDiagnostics.ValidWithConditionalDiagnostics(vectorIsNull, () => Diagnostics.NullVector(context, definition));
    }

    private IValidityWithDiagnostics ValidateDifferenceNotNull(IProcessingContext context, RawSharpMeasuresScalarDefinition definition)
    {
        var differenceIsNull = definition.Locations.ExplicitlySetDifference && definition.Difference is null;

        return ValidityWithDiagnostics.ValidWithConditionalDiagnostics(differenceIsNull, () => Diagnostics.NullDifferenceQuantity(context, definition));
    }

    private IValidityWithDiagnostics ValidateDifferenceNotUnexpectedlySpecified(IProcessingContext context, RawSharpMeasuresScalarDefinition definition)
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
