namespace SharpMeasures.Generators.Scalars.Parsing.SharpMeasuresScalar;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators;
using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Unresolved.Scalars;
using SharpMeasures.Generators.Unresolved.Units;
using SharpMeasures.Generators.Unresolved.Units.UnitInstances;
using SharpMeasures.Generators.Unresolved.Vectors;

using System;
using System.Linq;

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
    public abstract IUnresolvedUnitPopulation UnitPopulation { get; }
    public abstract IUnresolvedScalarPopulation ScalarPopulation { get; }
    public abstract IUnresolvedVectorPopulation VectorPopulation { get; }
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

    private IOptionalWithDiagnostics<IUnresolvedUnitType> ProcessUnit(ISharpMeasuresScalarResolutionContext context, UnresolvedSharpMeasuresScalarDefinition definition)
    {
        if (context.UnitPopulation.Units.TryGetValue(definition.Unit, out var unit) is false)
        {
            return OptionalWithDiagnostics.Empty<IUnresolvedUnitType>(Diagnostics.TypeNotUnit(context, definition));
        }

        if (definition.UseUnitBias && unit.Definition.BiasTerm is false)
        {
            return OptionalWithDiagnostics.Empty<IUnresolvedUnitType>(Diagnostics.UnitNotIncludingBiasTerm(context, definition));
        }

        return OptionalWithDiagnostics.Result(unit);
    }

    private IOptionalWithDiagnostics<IUnresolvedVectorGroupType> ProcessVector(ISharpMeasuresScalarResolutionContext context,
        UnresolvedSharpMeasuresScalarDefinition definition)
    {
        if (definition.VectorGroup is null)
        {
            return OptionalWithDiagnostics.EmptyWithoutDiagnostics<IUnresolvedVectorGroupType>();
        }

        if (context.VectorPopulation.VectorGroups.TryGetValue(definition.VectorGroup.Value, out var vectorGroup) is false)
        {
            return OptionalWithDiagnostics.Empty<IUnresolvedVectorGroupType>(Diagnostics.TypeNotVector(context, definition));
        }

        return OptionalWithDiagnostics.Result(vectorGroup);
    }

    private IOptionalWithDiagnostics<IUnresolvedScalarType> ProcessDifference(ISharpMeasuresScalarResolutionContext context, UnresolvedSharpMeasuresScalarDefinition definition)
    {
        if (context.ScalarPopulation.Scalars.TryGetValue(definition.Difference, out var scalar) is false)
        {
            return OptionalWithDiagnostics.Empty<IUnresolvedScalarType>(Diagnostics.DifferenceNotScalar(context, definition));
        }

        return OptionalWithDiagnostics.Result(scalar);
    }

    private IResultWithDiagnostics<IUnresolvedUnitInstance?> ProcessDefaultUnitName(ISharpMeasuresScalarResolutionContext context,
        UnresolvedSharpMeasuresScalarDefinition definition, IUnresolvedUnitType unit)
    {
        if (definition.DefaultUnitName is null)
        {
            return ResultWithDiagnostics.Construct<IUnresolvedUnitInstance?>(null);
        }

        if (unit.UnitsByName.TryGetValue(definition.DefaultUnitName, out var unitInstance) is false)
        {
            return ResultWithDiagnostics.Construct<IUnresolvedUnitInstance?>(null, Diagnostics.UnrecognizedDefaultUnit(context, definition));
        }

        return ResultWithDiagnostics.Construct<IUnresolvedUnitInstance?>(unitInstance);
    }

    private static IResultWithDiagnostics<IUnresolvedScalarType?> ProcessPowerQuantity(ISharpMeasuresScalarResolutionContext context,
        UnresolvedSharpMeasuresScalarDefinition definition, NamedType? quantity,
        Func<ISharpMeasuresScalarResolutionContext, UnresolvedSharpMeasuresScalarDefinition, Diagnostic?> typeNotScalarDiagnosticsDelegate)
    {
        if (quantity is null)
        {
            return ResultWithDiagnostics.Construct<IUnresolvedScalarType?>(null);
        }

        if (context.ScalarPopulation.Scalars.TryGetValue(quantity.Value, out var scalar) is false)
        {
            return ResultWithDiagnostics.Construct<IUnresolvedScalarType?>(null, typeNotScalarDiagnosticsDelegate(context, definition));
        }

        return ResultWithDiagnostics.Construct<IUnresolvedScalarType?>(scalar);
    }
}
