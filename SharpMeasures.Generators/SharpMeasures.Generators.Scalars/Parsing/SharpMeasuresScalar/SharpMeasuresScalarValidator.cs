namespace SharpMeasures.Generators.Scalars.Parsing.SharpMeasuresScalar;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators;
using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Quantities.Parsing.DefaultUnit;
using SharpMeasures.Generators.Scalars.Parsing.Abstraction;
using SharpMeasures.Generators.Units;
using SharpMeasures.Generators.Vectors;

using System;
using System.Linq;

internal interface ISharpMeasuresScalarValidationDiagnostics : IDefaultUnitValidationDiagnostics
{
    public abstract Diagnostic? TypeAlreadyUnit(ISharpMeasuresScalarValidationContext context, SharpMeasuresScalarDefinition definition);
    public abstract Diagnostic? TypeNotUnit(ISharpMeasuresScalarValidationContext context, SharpMeasuresScalarDefinition definition);
    public abstract Diagnostic? UnitNotIncludingBiasTerm(ISharpMeasuresScalarValidationContext context, SharpMeasuresScalarDefinition definition);
    public abstract Diagnostic? TypeNotVector(ISharpMeasuresScalarValidationContext context, SharpMeasuresScalarDefinition definition);
    public abstract Diagnostic? DifferenceNotScalar(ISharpMeasuresScalarValidationContext context, SharpMeasuresScalarDefinition definition);
    public abstract Diagnostic? ReciprocalNotScalar(ISharpMeasuresScalarValidationContext context, SharpMeasuresScalarDefinition definition);
    public abstract Diagnostic? SquareNotScalar(ISharpMeasuresScalarValidationContext context, SharpMeasuresScalarDefinition definition);
    public abstract Diagnostic? CubeNotScalar(ISharpMeasuresScalarValidationContext context, SharpMeasuresScalarDefinition definition);
    public abstract Diagnostic? SquareRootNotScalar(ISharpMeasuresScalarValidationContext context, SharpMeasuresScalarDefinition definition);
    public abstract Diagnostic? CubeRootNotScalar(ISharpMeasuresScalarValidationContext context, SharpMeasuresScalarDefinition definition);
}

internal interface ISharpMeasuresScalarValidationContext : IProcessingContext
{
    public abstract IUnitPopulation UnitPopulation { get; }
    public abstract IScalarPopulationWithData ScalarPopulation { get; }
    public abstract IVectorPopulation VectorPopulation { get; }
}

internal class SharpMeasuresScalarValidator : IProcesser<ISharpMeasuresScalarValidationContext, SharpMeasuresScalarDefinition, SharpMeasuresScalarDefinition>
{
    private ISharpMeasuresScalarValidationDiagnostics Diagnostics { get; }

    public SharpMeasuresScalarValidator(ISharpMeasuresScalarValidationDiagnostics diagnostics)
    {
        Diagnostics = diagnostics;
    }

    public IOptionalWithDiagnostics<SharpMeasuresScalarDefinition> Process(ISharpMeasuresScalarValidationContext context, SharpMeasuresScalarDefinition definition)
    {
        var validity = ValidateTypeNotAlreadyUnit(context, definition)
            .Merge(() => ResolveUnit(context, definition))
            .Validate((unit) => ValidateUnitNotIncorrectlyUnbiased(context, definition, unit))
            .Reduce();

        if (validity.IsInvalid)
        {
            return validity.AsEmptyOptional<SharpMeasuresScalarDefinition>();
        }

        var vector = ValidateVectorIsVector(context, definition).Transform(definition.Vector);
        var difference = ValidateDifferenceIsScalar(context, definition).Transform(definition.Difference);

        var defaultUnitValidity = DefaultUnitValidator.Validate(context, Diagnostics, definition, context.UnitPopulation, definition.Unit);

        var defaultUnitName = defaultUnitValidity.Transform(definition.DefaultUnitName);
        var defaultUnitSymbol = defaultUnitValidity.Transform(definition.DefaultUnitSymbol);

        var reciprocal = ValidatePowerIsScalar(context, definition, definition.Reciprocal, Diagnostics.ReciprocalNotScalar).Transform(definition.Reciprocal);
        var square = ValidatePowerIsScalar(context, definition, definition.Square, Diagnostics.SquareNotScalar).Transform(definition.Square);
        var cube = ValidatePowerIsScalar(context, definition, definition.Cube, Diagnostics.CubeNotScalar).Transform(definition.Cube);
        var squareRoot = ValidatePowerIsScalar(context, definition, definition.SquareRoot, Diagnostics.SquareRootNotScalar).Transform(definition.SquareRoot);
        var cubeRoot = ValidatePowerIsScalar(context, definition, definition.CubeRoot, Diagnostics.CubeRootNotScalar).Transform(definition.CubeRoot);

        var product = ProduceResult(definition, vector.NullableResult, difference.NullableResult, defaultUnitName.NullableResult, defaultUnitSymbol.NullableResult, reciprocal.NullableResult,
            square.NullableResult, cube.NullableResult, squareRoot.NullableResult, cubeRoot.NullableResult);

        var allDiagnostics = validity.Diagnostics.Concat(vector).Concat(difference).Concat(defaultUnitValidity).Concat(reciprocal).Concat(square).Concat(cube).Concat(squareRoot).Concat(cubeRoot);

        return OptionalWithDiagnostics.Result(product, allDiagnostics);
    }

    private static SharpMeasuresScalarDefinition ProduceResult(SharpMeasuresScalarDefinition definition, NamedType? vector, NamedType? difference, string? defaultUnitName, string? defaultUnitSymbol,
        NamedType? reciprocal, NamedType? square, NamedType? cube, NamedType? squareRoot, NamedType? cubeRoot)
    {
        return new(definition.Unit, vector, definition.UseUnitBias, definition.ImplementSum, definition.ImplementDifference, difference, defaultUnitName, defaultUnitSymbol, reciprocal, square, cube,
            squareRoot, cubeRoot, definition.GenerateDocumentation, definition.Locations);
    }

    private IValidityWithDiagnostics ValidateTypeNotAlreadyUnit(ISharpMeasuresScalarValidationContext context, SharpMeasuresScalarDefinition definition)
    {
        var typeAlreadyUnit = context.UnitPopulation.Units.ContainsKey(context.Type.AsNamedType());

        return ValidityWithDiagnostics.Conditional(typeAlreadyUnit is false, () => Diagnostics.TypeAlreadyUnit(context, definition));
    }

    private IOptionalWithDiagnostics<IUnitType> ResolveUnit(ISharpMeasuresScalarValidationContext context, SharpMeasuresScalarDefinition definition)
    {
        var correctlyResolvedUnit = context.UnitPopulation.Units.TryGetValue(definition.Unit, out var unit);

        return OptionalWithDiagnostics.Conditional(correctlyResolvedUnit, unit, () => Diagnostics.TypeNotUnit(context, definition));
    }

    private IValidityWithDiagnostics ValidateUnitNotIncorrectlyUnbiased(ISharpMeasuresScalarValidationContext context, SharpMeasuresScalarDefinition definition, IUnitType unit)
    {
        var unitNotIncorrectlyUnbiased = definition.UseUnitBias is false || unit.Definition.BiasTerm;

        return ValidityWithDiagnostics.Conditional(unitNotIncorrectlyUnbiased, () => Diagnostics.UnitNotIncludingBiasTerm(context, definition));
    }

    private IValidityWithDiagnostics ValidateVectorIsVector(ISharpMeasuresScalarValidationContext context, SharpMeasuresScalarDefinition definition)
    {
        var vectorIsNotVector = definition.Vector is not null && (context.VectorPopulation.Vectors.ContainsKey(definition.Vector.Value) is false
            || context.VectorPopulation.Groups.ContainsKey(definition.Vector.Value) is false || context.VectorPopulation.GroupMembers.ContainsKey(definition.Vector.Value) is false);

        return ValidityWithDiagnostics.Conditional(vectorIsNotVector is false, () => Diagnostics.TypeNotVector(context, definition));
    }

    private IValidityWithDiagnostics ValidateDifferenceIsScalar(ISharpMeasuresScalarValidationContext context, SharpMeasuresScalarDefinition definition)
    {
        var differenceIsNotScalar = definition.Difference is not null && context.ScalarPopulation.Scalars.ContainsKey(definition.Difference.Value) is false;

        return ValidityWithDiagnostics.Conditional(differenceIsNotScalar is false, () => Diagnostics.DifferenceNotScalar(context, definition));
    }

    private static IValidityWithDiagnostics ValidatePowerIsScalar(ISharpMeasuresScalarValidationContext context, SharpMeasuresScalarDefinition definition, NamedType? powerQuantity,
        Func<ISharpMeasuresScalarValidationContext, SharpMeasuresScalarDefinition, Diagnostic?> typeNotScalarDiagnosticsDelegate)
    {
        var powerQuantityIsNotScalar = powerQuantity is not null && context.ScalarPopulation.Scalars.ContainsKey(powerQuantity.Value) is false;

        return ValidityWithDiagnostics.Conditional(powerQuantityIsNotScalar is false, () => typeNotScalarDiagnosticsDelegate(context, definition));
    }
}
