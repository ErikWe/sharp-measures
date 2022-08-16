namespace SharpMeasures.Generators.Scalars.Parsing.SharpMeasuresScalar;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators;
using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Scalars.Parsing.Abstraction;
using SharpMeasures.Generators.Raw.Scalars;
using SharpMeasures.Generators.Raw.Units;
using SharpMeasures.Generators.Raw.Units.UnitInstances;
using SharpMeasures.Generators.Raw.Vectors;

using System;
using System.Linq;
using SharpMeasures.Generators.Raw.Vectors.Groups;

internal interface ISharpMeasuresScalarResolutionDiagnostics
{
    public abstract Diagnostic? TypeAlreadyUnit(ISharpMeasuresScalarResolutionContext context, UnresolvedSharpMeasuresScalarDefinition definition);
    public abstract Diagnostic? TypeNotUnit(ISharpMeasuresScalarResolutionContext context, UnresolvedSharpMeasuresScalarDefinition definition);
    public abstract Diagnostic? UnitNotIncludingBiasTerm(ISharpMeasuresScalarResolutionContext context, UnresolvedSharpMeasuresScalarDefinition definition);
    public abstract Diagnostic? TypeNotVector(ISharpMeasuresScalarResolutionContext context, UnresolvedSharpMeasuresScalarDefinition definition);
    public abstract Diagnostic? DifferenceNotScalar(ISharpMeasuresScalarResolutionContext context, UnresolvedSharpMeasuresScalarDefinition definition);
    public abstract Diagnostic? UnrecognizedDefaultUnit(ISharpMeasuresScalarResolutionContext context, UnresolvedSharpMeasuresScalarDefinition definition);
    public abstract Diagnostic? ReciprocalNotScalar(ISharpMeasuresScalarResolutionContext context, UnresolvedSharpMeasuresScalarDefinition definition);
    public abstract Diagnostic? SquareNotScalar(ISharpMeasuresScalarResolutionContext context, UnresolvedSharpMeasuresScalarDefinition definition);
    public abstract Diagnostic? CubeNotScalar(ISharpMeasuresScalarResolutionContext context, UnresolvedSharpMeasuresScalarDefinition definition);
    public abstract Diagnostic? SquareRootNotScalar(ISharpMeasuresScalarResolutionContext context, UnresolvedSharpMeasuresScalarDefinition definition);
    public abstract Diagnostic? CubeRootNotScalar(ISharpMeasuresScalarResolutionContext context, UnresolvedSharpMeasuresScalarDefinition definition);
}

internal interface ISharpMeasuresScalarResolutionContext : IProcessingContext
{
    public abstract IRawUnitPopulation UnitPopulation { get; }
    public abstract IUnresolvedScalarPopulationWithData ScalarPopulation { get; }
    public abstract IRawVectorPopulation VectorPopulation { get; }
}

internal class SharpMeasuresScalarResolver : IProcesser<ISharpMeasuresScalarResolutionContext, UnresolvedSharpMeasuresScalarDefinition, SharpMeasuresScalarDefinition>
{
    private ISharpMeasuresScalarResolutionDiagnostics Diagnostics { get; }

    public SharpMeasuresScalarResolver(ISharpMeasuresScalarResolutionDiagnostics diagnostics)
    {
        Diagnostics = diagnostics;
    }

    public IOptionalWithDiagnostics<SharpMeasuresScalarDefinition> Process(ISharpMeasuresScalarResolutionContext context, UnresolvedSharpMeasuresScalarDefinition definition)
    {
        if (context.UnitPopulation.Units.ContainsKey(context.Type.AsNamedType()))
        {
            return OptionalWithDiagnostics.Empty<SharpMeasuresScalarDefinition>(Diagnostics.TypeAlreadyUnit(context, definition));
        }

        var processedUnit = ProcessUnit(context, definition);
        var allDiagnostics = processedUnit.Diagnostics;

        if (processedUnit.LacksResult)
        {
            return OptionalWithDiagnostics.Empty<SharpMeasuresScalarDefinition>(allDiagnostics);
        }

        var processedVector = ProcessVector(context, definition);
        allDiagnostics = allDiagnostics.Concat(processedVector.Diagnostics);

        var processedDifference = ProcessDifference(context, definition);

        var processedDefaultUnitName = ProcessDefaultUnitName(context, definition, processedUnit.Result);

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

    private IOptionalWithDiagnostics<IRawUnitType> ProcessUnit(ISharpMeasuresScalarResolutionContext context, UnresolvedSharpMeasuresScalarDefinition definition)
    {
        if (context.UnitPopulation.Units.TryGetValue(definition.Unit, out var unit) is false)
        {
            return OptionalWithDiagnostics.Empty<IRawUnitType>(Diagnostics.TypeNotUnit(context, definition));
        }

        if (definition.UseUnitBias && unit.Definition.BiasTerm is false)
        {
            return OptionalWithDiagnostics.Empty<IRawUnitType>(Diagnostics.UnitNotIncludingBiasTerm(context, definition));
        }

        return OptionalWithDiagnostics.Result(unit);
    }

    private IOptionalWithDiagnostics<IRawVectorGroupType> ProcessVector(ISharpMeasuresScalarResolutionContext context,
        UnresolvedSharpMeasuresScalarDefinition definition)
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

    private IResultWithDiagnostics<IRawScalarType> ProcessDifference(ISharpMeasuresScalarResolutionContext context, UnresolvedSharpMeasuresScalarDefinition definition)
    {
        if (context.ScalarPopulation.Scalars.TryGetValue(definition.Difference, out var difference) is false)
        {
            var diagnostics = Diagnostics.DifferenceNotScalar(context, definition);
            var selfType = context.ScalarPopulation.Scalars[context.Type.AsNamedType()];

            return ResultWithDiagnostics.Construct(selfType, diagnostics);
        }

        return ResultWithDiagnostics.Construct(difference);
    }

    private IResultWithDiagnostics<IRawUnitInstance?> ProcessDefaultUnitName(ISharpMeasuresScalarResolutionContext context,
        UnresolvedSharpMeasuresScalarDefinition definition, IRawUnitType unit)
    {
        if (definition.DefaultUnitName is null)
        {
            return ResultWithDiagnostics.Construct<IRawUnitInstance?>(null);
        }

        if (unit.UnitsByName.TryGetValue(definition.DefaultUnitName, out var unitInstance) is false)
        {
            return ResultWithDiagnostics.Construct<IRawUnitInstance?>(null, Diagnostics.UnrecognizedDefaultUnit(context, definition));
        }

        return ResultWithDiagnostics.Construct<IRawUnitInstance?>(unitInstance);
    }

    private static IResultWithDiagnostics<IRawScalarType?> ProcessPowerQuantity(ISharpMeasuresScalarResolutionContext context,
        UnresolvedSharpMeasuresScalarDefinition definition, NamedType? quantity,
        Func<ISharpMeasuresScalarResolutionContext, UnresolvedSharpMeasuresScalarDefinition, Diagnostic?> typeNotScalarDiagnosticsDelegate)
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
