namespace SharpMeasures.Generators.Scalars.Parsing.SharpMeasuresScalar;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Quantities.Parsing.DefaultUnit;

using System;

internal interface ISharpMeasuresScalarProcessingDiagnostics : IDefaultUnitProcessingDiagnostics
{
    public abstract Diagnostic? NullUnit(IProcessingContext context, UnprocessedSharpMeasuresScalarDefinition definition);
    public abstract Diagnostic? NullVector(IProcessingContext context, UnprocessedSharpMeasuresScalarDefinition definition);

    public abstract Diagnostic? DifferenceDisabledButQuantitySpecified(IProcessingContext context, UnprocessedSharpMeasuresScalarDefinition definition);
    public abstract Diagnostic? NullDifferenceQuantity(IProcessingContext context, UnprocessedSharpMeasuresScalarDefinition definition);

    public abstract Diagnostic? NullReciprocalQuantity(IProcessingContext context, UnprocessedSharpMeasuresScalarDefinition definition);
    public abstract Diagnostic? NullSquareQuantity(IProcessingContext context, UnprocessedSharpMeasuresScalarDefinition definition);
    public abstract Diagnostic? NullCubeQuantity(IProcessingContext context, UnprocessedSharpMeasuresScalarDefinition definition);
    public abstract Diagnostic? NullSquareRootQuantity(IProcessingContext context, UnprocessedSharpMeasuresScalarDefinition definition);
    public abstract Diagnostic? NullCubeRootQuantity(IProcessingContext context, UnprocessedSharpMeasuresScalarDefinition definition);
}

internal class SharpMeasuresScalarProcesser : AProcesser<IProcessingContext, UnprocessedSharpMeasuresScalarDefinition, RawSharpMeasuresScalarDefinition>
{
    private ISharpMeasuresScalarProcessingDiagnostics Diagnostics { get; }

    public SharpMeasuresScalarProcesser(ISharpMeasuresScalarProcessingDiagnostics diagnostics)
    {
        Diagnostics = diagnostics;
    }

    public override IOptionalWithDiagnostics<RawSharpMeasuresScalarDefinition> Process(IProcessingContext context, UnprocessedSharpMeasuresScalarDefinition definition)
    {
        return VerifyRequiredPropertiesSet(definition)
            .Validate(() => ValidateUnit(context, definition))
            .Validate(() => ValidateVector(context, definition))
            .Validate(() => ValidateDifference(context, definition))
            .Validate(() => ValidatePowerProperty(definition.Locations.ExplicitlySetReciprocal, definition.Reciprocal, () => Diagnostics.NullReciprocalQuantity(context, definition)))
            .Validate(() => ValidatePowerProperty(definition.Locations.ExplicitlySetSquare, definition.Square, () => Diagnostics.NullSquareQuantity(context, definition)))
            .Validate(() => ValidatePowerProperty(definition.Locations.ExplicitlySetCube, definition.Cube, () => Diagnostics.NullCubeQuantity(context, definition)))
            .Validate(() => ValidatePowerProperty(definition.Locations.ExplicitlySetSquareRoot, definition.SquareRoot, () => Diagnostics.NullSquareRootQuantity(context, definition)))
            .Validate(() => ValidatePowerProperty(definition.Locations.ExplicitlySetCubeRoot, definition.CubeRoot, () => Diagnostics.NullCubeRootQuantity(context, definition)))
            .Merge(() => DefaultUnitProcesser.Process(context, Diagnostics, definition))
            .Transform((defaultUnit) => ProduceResult(context, definition, defaultUnit.Name, defaultUnit.Symbol));
    }

    private static RawSharpMeasuresScalarDefinition ProduceResult(IProcessingContext context, UnprocessedSharpMeasuresScalarDefinition definition, string? defaultUnitName, string? defaultUnitSymbol)
    {
        return new(definition.Unit!.Value, definition.Vector, definition.UseUnitBias, definition.ImplementSum, definition.ImplementDifference, definition.Difference ?? context.Type.AsNamedType(),
            defaultUnitName, defaultUnitSymbol, definition.Reciprocal, definition.Square, definition.Cube, definition.SquareRoot, definition.CubeRoot, definition.GenerateDocumentation, definition.Locations);
    }

    private static IValidityWithDiagnostics VerifyRequiredPropertiesSet(UnprocessedSharpMeasuresScalarDefinition definition)
    {
        return ValidityWithDiagnostics.ConditionalWithoutDiagnostics(definition.Locations.ExplicitlySetUnit);
    }

    private IValidityWithDiagnostics ValidateUnit(IProcessingContext context, UnprocessedSharpMeasuresScalarDefinition definition)
    {
        return ValidityWithDiagnostics.Conditional(definition.Unit is not null, () => Diagnostics.NullUnit(context, definition));
    }

    private IValidityWithDiagnostics ValidateVector(IProcessingContext context, UnprocessedSharpMeasuresScalarDefinition definition)
    {
        if (definition.Locations.ExplicitlySetVector is false)
        {
            return ValidityWithDiagnostics.Valid;
        }

        if (definition.Vector is null)
        {
            return ValidityWithDiagnostics.ValidWithDiagnostics(Diagnostics.NullVector(context, definition));
        }

        return ValidityWithDiagnostics.Valid;
    }

    private IValidityWithDiagnostics ValidateDifference(IProcessingContext context, UnprocessedSharpMeasuresScalarDefinition definition)
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

    private static IValidityWithDiagnostics ValidatePowerProperty(bool explicitlySet, NamedType? value, Func<Diagnostic?> nullQuantityDiagnosticsDelegate)
    {
        if (explicitlySet is false)
        {
            return ValidityWithDiagnostics.Valid;
        }

        if (value is null)
        {
            return ValidityWithDiagnostics.ValidWithDiagnostics(nullQuantityDiagnosticsDelegate());
        }

        return ValidityWithDiagnostics.Valid;
    }
}
