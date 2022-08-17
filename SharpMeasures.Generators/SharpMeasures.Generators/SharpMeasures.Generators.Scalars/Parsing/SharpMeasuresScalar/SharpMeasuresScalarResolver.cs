namespace SharpMeasures.Generators.Scalars.Parsing.SharpMeasuresScalar;

using Microsoft.CodeAnalysis;

using OneOf;

using SharpMeasures.Generators;
using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Quantities.Parsing.DefaultUnit;
using SharpMeasures.Generators.Raw.Scalars;
using SharpMeasures.Generators.Raw.Units;
using SharpMeasures.Generators.Raw.Vectors;
using SharpMeasures.Generators.Raw.Vectors.Groups;
using SharpMeasures.Generators.Scalars.Parsing.Abstraction;

using System;
using System.Linq;

internal interface ISharpMeasuresScalarResolutionDiagnostics : IDefaultUnitResolutionDiagnostics
{
    public abstract Diagnostic? TypeAlreadyUnit(ISharpMeasuresScalarResolutionContext context, RawSharpMeasuresScalarDefinition definition);
    public abstract Diagnostic? TypeAlreadyScalar(ISharpMeasuresScalarResolutionContext context, RawSharpMeasuresScalarDefinition definition);
    public abstract Diagnostic? TypeNotUnit(ISharpMeasuresScalarResolutionContext context, RawSharpMeasuresScalarDefinition definition);
    public abstract Diagnostic? UnitNotIncludingBiasTerm(ISharpMeasuresScalarResolutionContext context, RawSharpMeasuresScalarDefinition definition);
    public abstract Diagnostic? TypeNotVector(ISharpMeasuresScalarResolutionContext context, RawSharpMeasuresScalarDefinition definition);
    public abstract Diagnostic? DifferenceNotScalar(ISharpMeasuresScalarResolutionContext context, RawSharpMeasuresScalarDefinition definition);
    public abstract Diagnostic? ReciprocalNotScalar(ISharpMeasuresScalarResolutionContext context, RawSharpMeasuresScalarDefinition definition);
    public abstract Diagnostic? SquareNotScalar(ISharpMeasuresScalarResolutionContext context, RawSharpMeasuresScalarDefinition definition);
    public abstract Diagnostic? CubeNotScalar(ISharpMeasuresScalarResolutionContext context, RawSharpMeasuresScalarDefinition definition);
    public abstract Diagnostic? SquareRootNotScalar(ISharpMeasuresScalarResolutionContext context, RawSharpMeasuresScalarDefinition definition);
    public abstract Diagnostic? CubeRootNotScalar(ISharpMeasuresScalarResolutionContext context, RawSharpMeasuresScalarDefinition definition);
}

internal interface ISharpMeasuresScalarResolutionContext : IProcessingContext
{
    public abstract IRawUnitPopulation UnitPopulation { get; }
    public abstract IUnresolvedScalarPopulationWithData ScalarPopulation { get; }
    public abstract IRawVectorPopulation VectorPopulation { get; }
}

internal class SharpMeasuresScalarResolver : IProcesser<ISharpMeasuresScalarResolutionContext, RawSharpMeasuresScalarDefinition, SharpMeasuresScalarDefinition>
{
    private ISharpMeasuresScalarResolutionDiagnostics Diagnostics { get; }

    public SharpMeasuresScalarResolver(ISharpMeasuresScalarResolutionDiagnostics diagnostics)
    {
        Diagnostics = diagnostics;
    }

    public IOptionalWithDiagnostics<SharpMeasuresScalarDefinition> Process(ISharpMeasuresScalarResolutionContext context, RawSharpMeasuresScalarDefinition definition)
    {
        var unit = ValidateTypeNotAlreadyUnit(context, definition)
            .Validate(() => ValidateTypeNotAlreadyScalar(context, definition))
            .Merge(() => ResolveUnit(context, definition))
            .Validate((unit) => ValidateUnitNotIncorrectlyUnbiased(context, definition, unit));



        var processedVector = ProcessVector(context, definition);
        allDiagnostics = allDiagnostics.Concat(processedVector.Diagnostics);

        var processedDifference = ProcessDifference(context, definition);

        var processedDefaultUnitName = DefaultUnitResolver.Resolve(context, Diagnostics, definition, processedUnit.Result);

        var processedReciprocal = ProcessPowerQuantity(context, definition, definition.Reciprocal, Diagnostics.ReciprocalNotScalar);
        var processedSquare = ProcessPowerQuantity(context, definition, definition.Square, Diagnostics.SquareNotScalar);
        var processedCube = ProcessPowerQuantity(context, definition, definition.Cube, Diagnostics.CubeNotScalar);
        var processedSquareRoot = ProcessPowerQuantity(context, definition, definition.SquareRoot, Diagnostics.SquareRootNotScalar);
        var processedCubeRoot = ProcessPowerQuantity(context, definition, definition.CubeRoot, Diagnostics.CubeRootNotScalar);

        allDiagnostics = allDiagnostics.Concat(processedDifference.Diagnostics).Concat(processedDefaultUnitName.Diagnostics).Concat(processedReciprocal.Diagnostics)
            .Concat(processedSquare.Diagnostics).Concat(processedCube.Diagnostics).Concat(processedSquareRoot.Diagnostics).Concat(processedCubeRoot.Diagnostics);

        SharpMeasuresScalarDefinition product = new(processedUnit.Result, processedVector.NullableResult, definition.UseUnitBias, definition.ImplementSum,
            definition.ImplementDifference, processedDifference.Result, processedDefaultUnitName.Result, definition.DefaultUnitSymbol, processedReciprocal.NullableResult,
            processedSquare.NullableResult, processedCube.NullableResult, processedSquareRoot.NullableResult, processedCubeRoot.NullableResult,
            definition.GenerateDocumentation, definition.Locations);

        return OptionalWithDiagnostics.Result(product, allDiagnostics);
    }

    private IValidityWithDiagnostics ValidateTypeNotAlreadyUnit(ISharpMeasuresScalarResolutionContext context, RawSharpMeasuresScalarDefinition definition)
    {
        var typeAlreadyUnit = context.UnitPopulation.Units.ContainsKey(context.Type.AsNamedType());

        return ValidityWithDiagnostics.Conditional(typeAlreadyUnit is false, () => Diagnostics.TypeAlreadyUnit(context, definition));
    }

    private IValidityWithDiagnostics ValidateTypeNotAlreadyScalar(ISharpMeasuresScalarResolutionContext context, RawSharpMeasuresScalarDefinition definition)
    {
        var typeAlreadyScalar = context.ScalarPopulation.DuplicatelyDefined.ContainsKey(context.Type.AsNamedType());

        return ValidityWithDiagnostics.Conditional(typeAlreadyScalar is false, () => Diagnostics.TypeAlreadyScalar(context, definition));
    }

    private IOptionalWithDiagnostics<IRawUnitType> ResolveUnit(ISharpMeasuresScalarResolutionContext context, RawSharpMeasuresScalarDefinition definition)
    {
        var unitCorrectlyResolved = context.UnitPopulation.Units.TryGetValue(definition.Unit, out var unit);

        return OptionalWithDiagnostics.Conditional(unitCorrectlyResolved, unit, () => Diagnostics.TypeNotUnit(context, definition));
    }

    private IValidityWithDiagnostics ValidateUnitNotIncorrectlyUnbiased(ISharpMeasuresScalarResolutionContext context, RawSharpMeasuresScalarDefinition definition, IRawUnitType unit)
    {
        var unitNotIncorrectlyUnbiased = definition.UseUnitBias is false || unit.Definition.BiasTerm;

        return ValidityWithDiagnostics.Conditional(unitNotIncorrectlyUnbiased, () => Diagnostics.UnitNotIncludingBiasTerm(context, definition));
    }

    private IOptionalWithDiagnostics<OneOf<IRawVectorType, IRawVectorGroupType, IRawVectorGroupMemberType>> ResolveVector(ISharpMeasuresScalarResolutionContext context, RawSharpMeasuresScalarDefinition definition)
    {

    }

    private IOptionalWithDiagnostics<IRawVectorGroupType> ProcessVector(ISharpMeasuresScalarResolutionContext context, RawSharpMeasuresScalarDefinition definition)
    {
        if (definition.Vector is null)
        {
            return OptionalWithDiagnostics.EmptyWithoutDiagnostics<IRawVectorGroupType>();
        }

        if (context.VectorPopulation.VectorGroups.TryGetValue(definition.Vector.Value, out var vectorGroup) is false)
        {
            return OptionalWithDiagnostics.Empty<IRawVectorGroupType>(Diagnostics.TypeNotVector(context, definition));
        }

        return OptionalWithDiagnostics.Result(vectorGroup);
    }

    private IResultWithDiagnostics<IRawScalarType> ProcessDifference(ISharpMeasuresScalarResolutionContext context, RawSharpMeasuresScalarDefinition definition)
    {
        if (context.ScalarPopulation.Scalars.TryGetValue(definition.Difference, out var difference) is false)
        {
            var diagnostics = Diagnostics.DifferenceNotScalar(context, definition);
            var selfType = context.ScalarPopulation.Scalars[context.Type.AsNamedType()];

            return ResultWithDiagnostics.Construct(selfType, diagnostics);
        }

        return ResultWithDiagnostics.Construct(difference);
    }

    private static IResultWithDiagnostics<IRawScalarType?> ProcessPowerQuantity(ISharpMeasuresScalarResolutionContext context,
        RawSharpMeasuresScalarDefinition definition, NamedType? quantity,
        Func<ISharpMeasuresScalarResolutionContext, RawSharpMeasuresScalarDefinition, Diagnostic?> typeNotScalarDiagnosticsDelegate)
    {
        if (quantity is null)
        {
            return ResultWithDiagnostics.Construct<IRawScalarType?>(null);
        }

        if (context.ScalarPopulation.Scalars.TryGetValue(quantity.Value, out var scalar) is false)
        {
            return ResultWithDiagnostics.Construct<IRawScalarType?>(null, typeNotScalarDiagnosticsDelegate(context, definition));
        }

        return ResultWithDiagnostics.Construct<IRawScalarType?>(scalar);
    }
}
