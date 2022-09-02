﻿namespace SharpMeasures.Generators.Scalars.Parsing.SpecializedSharpMeasuresScalar;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators;
using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Quantities.Parsing.DefaultUnitInstance;
using SharpMeasures.Generators.Scalars.Parsing.Abstraction;
using SharpMeasures.Generators.Units;
using SharpMeasures.Generators.Vectors;

using System;
using System.Linq;

internal interface ISpecializedSharpMeasuresScalarValidationDiagnostics : IDefaultUnitInstanceValidationDiagnostics
{
    public abstract Diagnostic? TypeAlreadyUnit(ISpecializedSharpMeasuresScalarValidationContext context, SpecializedSharpMeasuresScalarDefinition definition);
    public abstract Diagnostic? TypeAlreadyScalar(ISpecializedSharpMeasuresScalarValidationContext context, SpecializedSharpMeasuresScalarDefinition definition);
    public abstract Diagnostic? OriginalNotScalar(ISpecializedSharpMeasuresScalarValidationContext context, SpecializedSharpMeasuresScalarDefinition definition);
    public abstract Diagnostic? RootScalarNotResolved(ISpecializedSharpMeasuresScalarValidationContext context, SpecializedSharpMeasuresScalarDefinition definition);
    public abstract Diagnostic? TypeNotVector(ISpecializedSharpMeasuresScalarValidationContext context, SpecializedSharpMeasuresScalarDefinition definition);
    public abstract Diagnostic? DifferenceNotScalar(ISpecializedSharpMeasuresScalarValidationContext context, SpecializedSharpMeasuresScalarDefinition definition);
    public abstract Diagnostic? ReciprocalNotScalar(ISpecializedSharpMeasuresScalarValidationContext context, SpecializedSharpMeasuresScalarDefinition definition);
    public abstract Diagnostic? SquareNotScalar(ISpecializedSharpMeasuresScalarValidationContext context, SpecializedSharpMeasuresScalarDefinition definition);
    public abstract Diagnostic? CubeNotScalar(ISpecializedSharpMeasuresScalarValidationContext context, SpecializedSharpMeasuresScalarDefinition definition);
    public abstract Diagnostic? SquareRootNotScalar(ISpecializedSharpMeasuresScalarValidationContext context, SpecializedSharpMeasuresScalarDefinition definition);
    public abstract Diagnostic? CubeRootNotScalar(ISpecializedSharpMeasuresScalarValidationContext context, SpecializedSharpMeasuresScalarDefinition definition);
}

internal interface ISpecializedSharpMeasuresScalarValidationContext : IProcessingContext
{
    public abstract IUnitPopulation UnitPopulation { get; }
    public abstract IScalarPopulationWithData ScalarPopulation { get; }
    public abstract IVectorPopulation VectorPopulation { get; }
}

internal class SpecializedSharpMeasuresScalarValidator : IProcesser<ISpecializedSharpMeasuresScalarValidationContext, SpecializedSharpMeasuresScalarDefinition, SpecializedSharpMeasuresScalarDefinition>
{
    private ISpecializedSharpMeasuresScalarValidationDiagnostics Diagnostics { get; }

    public SpecializedSharpMeasuresScalarValidator(ISpecializedSharpMeasuresScalarValidationDiagnostics diagnostics)
    {
        Diagnostics = diagnostics;
    }

    public IOptionalWithDiagnostics<SpecializedSharpMeasuresScalarDefinition> Process(ISpecializedSharpMeasuresScalarValidationContext context, SpecializedSharpMeasuresScalarDefinition definition)
    {
        var validity = ValidateTypeNotAlreadyUnit(context, definition)
            .Validate(() => ValidateTypeNotAlreadyScalar(context, definition))
            .Validate(() => ValidateOriginalScalarIsScalar(context, definition))
            .Validate(() => ValidateScalarRootResolved(context, definition))
            .Validate(() => ValidateUnitIsUnit(context));

        if (validity.IsInvalid)
        {
            return validity.AsEmptyOptional<SpecializedSharpMeasuresScalarDefinition>();
        }

        var vector = ValidateVectorIsVector(context, definition).Transform(definition.Vector);
        var difference = ValidateDifferenceIsScalar(context, definition).Transform(definition.Difference);

        var unit = context.ScalarPopulation.ScalarBases[context.Type.AsNamedType()].Definition.Unit;
        var defaultUnitInstanceValidity = DefaultUnitInstanceValidator.Validate(context, Diagnostics, definition, context.UnitPopulation, unit);

        var defaultUnitInstanceName = defaultUnitInstanceValidity.Transform(definition.DefaultUnitInstanceName);
        var defaultUnitInstanceSymbol = defaultUnitInstanceValidity.Transform(definition.DefaultUnitInstanceSymbol);

        var reciprocal = ValidatePowerIsScalar(context, definition, definition.Reciprocal, Diagnostics.ReciprocalNotScalar).Transform(definition.Reciprocal);
        var square = ValidatePowerIsScalar(context, definition, definition.Square, Diagnostics.SquareNotScalar).Transform(definition.Square);
        var cube = ValidatePowerIsScalar(context, definition, definition.Cube, Diagnostics.CubeNotScalar).Transform(definition.Cube);
        var squareRoot = ValidatePowerIsScalar(context, definition, definition.SquareRoot, Diagnostics.SquareRootNotScalar).Transform(definition.SquareRoot);
        var cubeRoot = ValidatePowerIsScalar(context, definition, definition.CubeRoot, Diagnostics.CubeRootNotScalar).Transform(definition.CubeRoot);

        var product = ProduceResult(context, definition, vector.NullableValueResult(), difference.NullableValueResult(), defaultUnitInstanceName.NullableReferenceResult(), defaultUnitInstanceSymbol.NullableReferenceResult(), reciprocal.NullableValueResult(),
            square.NullableValueResult(), cube.NullableValueResult(), squareRoot.NullableValueResult(), cubeRoot.NullableValueResult());

        var allDiagnostics = validity.Diagnostics.Concat(vector).Concat(difference).Concat(defaultUnitInstanceValidity).Concat(reciprocal).Concat(square).Concat(cube).Concat(squareRoot).Concat(cubeRoot);

        return OptionalWithDiagnostics.Result(product, allDiagnostics);
    }

    private static SpecializedSharpMeasuresScalarDefinition ProduceResult(ISpecializedSharpMeasuresScalarValidationContext context, SpecializedSharpMeasuresScalarDefinition definition, NamedType? vector,
        NamedType? difference, string? defaultUnitInstanceName, string? defaultUnitInstanceSymbol, NamedType? reciprocal, NamedType? square, NamedType? cube, NamedType? squareRoot, NamedType? cubeRoot)
    {
        return new(definition.OriginalQuantity, definition.InheritDerivations, definition.InheritConstants, definition.InheritConversions, definition.InheritBases, definition.InheritUnits, vector,
            definition.ImplementSum, definition.ImplementDifference, difference, defaultUnitInstanceName, defaultUnitInstanceSymbol, reciprocal, square, cube, squareRoot, cubeRoot, definition.GenerateDocumentation,
            definition.Locations);
    }

    private IValidityWithDiagnostics ValidateTypeNotAlreadyUnit(ISpecializedSharpMeasuresScalarValidationContext context, SpecializedSharpMeasuresScalarDefinition definition)
    {
        var typeAlreadyUnit = context.UnitPopulation.Units.ContainsKey(context.Type.AsNamedType());

        return ValidityWithDiagnostics.Conditional(typeAlreadyUnit is false, () => Diagnostics.TypeAlreadyUnit(context, definition));
    }

    private IValidityWithDiagnostics ValidateTypeNotAlreadyScalar(ISpecializedSharpMeasuresScalarValidationContext context, SpecializedSharpMeasuresScalarDefinition definition)
    {
        var typeAlreadyScalar = context.ScalarPopulation.DuplicatelyDefinedScalars.ContainsKey(context.Type.AsNamedType());

        return ValidityWithDiagnostics.Conditional(typeAlreadyScalar is false, () => Diagnostics.TypeAlreadyScalar(context, definition));
    }

    private IValidityWithDiagnostics ValidateOriginalScalarIsScalar(ISpecializedSharpMeasuresScalarValidationContext context, SpecializedSharpMeasuresScalarDefinition definition)
    {
        var originalScalarIsScalar = context.ScalarPopulation.Scalars.ContainsKey(definition.OriginalQuantity);

        return ValidityWithDiagnostics.Conditional(originalScalarIsScalar, () => Diagnostics.OriginalNotScalar(context, definition));
    }

    private IValidityWithDiagnostics ValidateScalarRootResolved(ISpecializedSharpMeasuresScalarValidationContext context, SpecializedSharpMeasuresScalarDefinition definition)
    {
        var correctlyResolvedScalarBase = context.ScalarPopulation.ScalarBases.ContainsKey(definition.OriginalQuantity);

        return ValidityWithDiagnostics.Conditional(correctlyResolvedScalarBase, () => Diagnostics.RootScalarNotResolved(context, definition));
    }

    private static IValidityWithDiagnostics ValidateUnitIsUnit(ISpecializedSharpMeasuresScalarValidationContext context)
    {
        var unitIsUnit = context.UnitPopulation.Units.ContainsKey(context.ScalarPopulation.ScalarBases[context.Type.AsNamedType()].Definition.Unit);

        return ValidityWithDiagnostics.ConditionalWithoutDiagnostics(unitIsUnit);
    }

    private IValidityWithDiagnostics ValidateVectorIsVector(ISpecializedSharpMeasuresScalarValidationContext context, SpecializedSharpMeasuresScalarDefinition definition)
    {
        var vectorIsNotVector = definition.Vector is not null && (context.VectorPopulation.Vectors.ContainsKey(definition.Vector.Value) is false
            || context.VectorPopulation.Groups.ContainsKey(definition.Vector.Value) is false || context.VectorPopulation.GroupMembers.ContainsKey(definition.Vector.Value) is false);

        return ValidityWithDiagnostics.Conditional(vectorIsNotVector is false, () => Diagnostics.TypeNotVector(context, definition));
    }

    private IValidityWithDiagnostics ValidateDifferenceIsScalar(ISpecializedSharpMeasuresScalarValidationContext context, SpecializedSharpMeasuresScalarDefinition definition)
    {
        var differenceIsNotScalar = definition.Difference is not null && context.ScalarPopulation.Scalars.ContainsKey(definition.Difference.Value) is false;

        return ValidityWithDiagnostics.Conditional(differenceIsNotScalar is false, () => Diagnostics.DifferenceNotScalar(context, definition));
    }

    private static IValidityWithDiagnostics ValidatePowerIsScalar(ISpecializedSharpMeasuresScalarValidationContext context, SpecializedSharpMeasuresScalarDefinition definition, NamedType? powerQuantity,
        Func<ISpecializedSharpMeasuresScalarValidationContext, SpecializedSharpMeasuresScalarDefinition, Diagnostic?> typeNotScalarDiagnosticsDelegate)
    {
        var powerQuantityIsNotScalar = powerQuantity is not null && context.ScalarPopulation.Scalars.ContainsKey(powerQuantity.Value) is false;

        return ValidityWithDiagnostics.Conditional(powerQuantityIsNotScalar is false, () => typeNotScalarDiagnosticsDelegate(context, definition));
    }
}
