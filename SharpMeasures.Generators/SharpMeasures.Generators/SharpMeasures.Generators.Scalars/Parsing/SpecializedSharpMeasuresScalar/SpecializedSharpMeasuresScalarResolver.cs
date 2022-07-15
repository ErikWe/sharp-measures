namespace SharpMeasures.Generators.Scalars.Parsing.SpecializedSharpMeasuresScalar;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators;
using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Unresolved.Scalars;
using SharpMeasures.Generators.Unresolved.Units;
using SharpMeasures.Generators.Unresolved.Units.UnitInstances;
using SharpMeasures.Generators.Unresolved.Vectors;

using System;
using System.Collections.Generic;
using System.Linq;

internal interface ISpecializedSharpMeasuresScalarResolutionDiagnostics
{
    public abstract Diagnostic? TypeAlreadyUnit(ISpecializedSharpMeasuresScalarResolutionContext context, UnresolvedSpecializedSharpMeasuresScalarDefinition definition);
    public abstract Diagnostic? OriginalNotScalar(ISpecializedSharpMeasuresScalarResolutionContext context, UnresolvedSpecializedSharpMeasuresScalarDefinition definition);
    public abstract Diagnostic? TypeNotVector(ISpecializedSharpMeasuresScalarResolutionContext context, UnresolvedSpecializedSharpMeasuresScalarDefinition definition);
    public abstract Diagnostic? UnrecognizedDefaultUnit(ISpecializedSharpMeasuresScalarResolutionContext context, UnresolvedSpecializedSharpMeasuresScalarDefinition definition,
        IUnresolvedUnitType unit);
    public abstract Diagnostic? DifferenceNotScalar(ISpecializedSharpMeasuresScalarResolutionContext context, UnresolvedSpecializedSharpMeasuresScalarDefinition definition);
    public abstract Diagnostic? ReciprocalNotScalar(ISpecializedSharpMeasuresScalarResolutionContext context, UnresolvedSpecializedSharpMeasuresScalarDefinition definition);
    public abstract Diagnostic? SquareNotScalar(ISpecializedSharpMeasuresScalarResolutionContext context, UnresolvedSpecializedSharpMeasuresScalarDefinition definition);
    public abstract Diagnostic? CubeNotScalar(ISpecializedSharpMeasuresScalarResolutionContext context, UnresolvedSpecializedSharpMeasuresScalarDefinition definition);
    public abstract Diagnostic? SquareRootNotScalar(ISpecializedSharpMeasuresScalarResolutionContext context, UnresolvedSpecializedSharpMeasuresScalarDefinition definition);
    public abstract Diagnostic? CubeRootNotScalar(ISpecializedSharpMeasuresScalarResolutionContext context, UnresolvedSpecializedSharpMeasuresScalarDefinition definition);
}

internal interface ISpecializedSharpMeasuresScalarResolutionContext : IProcessingContext
{
    public abstract IUnresolvedUnitPopulation UnitPopulation { get; }
    public abstract IUnresolvedScalarPopulation ScalarPopulation { get; }
    public abstract IUnresolvedVectorPopulation VectorPopulation { get; }
}

internal class SpecializedSharpMeasuresScalarResolver
    : IProcesser<ISpecializedSharpMeasuresScalarResolutionContext, UnresolvedSpecializedSharpMeasuresScalarDefinition, SpecializedSharpMeasuresScalarDefinition>
{
    private ISpecializedSharpMeasuresScalarResolutionDiagnostics Diagnostics { get; }

    public SpecializedSharpMeasuresScalarResolver(ISpecializedSharpMeasuresScalarResolutionDiagnostics diagnostics)
    {
        Diagnostics = diagnostics;
    }

    public IOptionalWithDiagnostics<SpecializedSharpMeasuresScalarDefinition> Process(ISpecializedSharpMeasuresScalarResolutionContext context,
        UnresolvedSpecializedSharpMeasuresScalarDefinition definition)
    {
        if (context.UnitPopulation.Units.ContainsKey(context.Type.AsNamedType()))
        {
            return OptionalWithDiagnostics.Empty<SpecializedSharpMeasuresScalarDefinition>(Diagnostics.TypeAlreadyUnit(context, definition));
        }

        if (context.ScalarPopulation.BaseScalarByScalarType.TryGetValue(context.Type.AsNamedType(), out var baseScalar) is false)
        {
            return OptionalWithDiagnostics.Empty<SpecializedSharpMeasuresScalarDefinition>(Diagnostics.OriginalNotScalar(context, definition));
        }

        if (context.UnitPopulation.Units.TryGetValue(baseScalar.Definition.Unit, out var unit) is false)
        {
            return OptionalWithDiagnostics.EmptyWithoutDiagnostics<SpecializedSharpMeasuresScalarDefinition>();
        }

        var processedOriginalScalar = ProcessOriginalScalar(context, definition);
        var allDiagnostics = processedOriginalScalar.Diagnostics;

        if (processedOriginalScalar.LacksResult)
        {
            return OptionalWithDiagnostics.Empty<SpecializedSharpMeasuresScalarDefinition>(allDiagnostics);
        }

        var processedVector = ProcessVector(context, definition);
        allDiagnostics = processedVector.Diagnostics;

        var processedDifference = ProcessDifference(context, definition);

        var processedDefaultUnitName = ProcessDefaultUnitName(context, definition, unit);

        var processedReciprocal = ProcessPowerQuantity(context, definition, definition.Reciprocal, Diagnostics.ReciprocalNotScalar);
        var processedSquare = ProcessPowerQuantity(context, definition, definition.Square, Diagnostics.SquareNotScalar);
        var processedCube = ProcessPowerQuantity(context, definition, definition.Cube, Diagnostics.CubeNotScalar);
        var processedSquareRoot = ProcessPowerQuantity(context, definition, definition.SquareRoot, Diagnostics.SquareRootNotScalar);
        var processedCubeRoot = ProcessPowerQuantity(context, definition, definition.CubeRoot, Diagnostics.CubeRootNotScalar);

        allDiagnostics = allDiagnostics.Concat(processedDifference.Diagnostics).Concat(processedDefaultUnitName.Diagnostics).Concat(processedReciprocal.Diagnostics)
            .Concat(processedSquare.Diagnostics).Concat(processedCube.Diagnostics).Concat(processedSquareRoot.Diagnostics).Concat(processedCubeRoot.Diagnostics);

        SpecializedSharpMeasuresScalarDefinition product = new(processedOriginalScalar.Result, definition.InheritDerivations, definition.InheritConstants,
            definition.InheritConvertibleScalars, definition.InheritBases, definition.InheritUnits, processedVector.Result, definition.ImplementSum,
            definition.ImplementDifference, processedDifference.Result, processedDefaultUnitName.Result, definition.DefaultUnitSymbol, processedReciprocal.NullableResult,
            processedSquare.NullableResult, processedCube.NullableResult, processedSquareRoot.NullableResult, processedCubeRoot.NullableResult,
            definition.GenerateDocumentation, definition.Locations);

        return OptionalWithDiagnostics.Result(product, allDiagnostics);
    }

    private IOptionalWithDiagnostics<IUnresolvedScalarType> ProcessOriginalScalar(ISpecializedSharpMeasuresScalarResolutionContext context,
        UnresolvedSpecializedSharpMeasuresScalarDefinition definition)
    {
        if (context.ScalarPopulation.Scalars.TryGetValue(definition.OriginalScalar, out var scalar) is false)
        {
            return OptionalWithDiagnostics.Empty<IUnresolvedScalarType>(Diagnostics.OriginalNotScalar(context, definition));
        }

        return OptionalWithDiagnostics.Result(scalar);
    }

    private IOptionalWithDiagnostics<IReadOnlyList<IUnresolvedVectorType>> ProcessVector(ISpecializedSharpMeasuresScalarResolutionContext context,
        UnresolvedSpecializedSharpMeasuresScalarDefinition definition)
    {
        if (definition.Vector is null)
        {
            return OptionalWithDiagnostics.EmptyWithoutDiagnostics<IReadOnlyList<IUnresolvedVectorType>>();
        }

        if (context.VectorPopulation.Vectors.TryGetValue(definition.Vector.Value, out var vector) is false)
        {
            if (context.VectorPopulation.Groups.TryGetValue(definition.Vector.Value, out var vectorGroup) is false)
            {
                return OptionalWithDiagnostics.Empty<IReadOnlyList<IUnresolvedVectorType>>(Diagnostics.TypeNotVector(context, definition));
            }

            return OptionalWithDiagnostics.Result(vectorGroup.Values.ToList() as IReadOnlyList<IUnresolvedVectorType>);
        }

        return OptionalWithDiagnostics.Result(new[] { vector } as IReadOnlyList<IUnresolvedVectorType>);
    }

    private IOptionalWithDiagnostics<IUnresolvedScalarType> ProcessDifference(ISpecializedSharpMeasuresScalarResolutionContext context,
        UnresolvedSpecializedSharpMeasuresScalarDefinition definition)
    {
        if (definition.Difference is null)
        {
            return OptionalWithDiagnostics.EmptyWithoutDiagnostics<IUnresolvedScalarType>();
        }

        if (context.ScalarPopulation.Scalars.TryGetValue(definition.Difference.Value, out var scalar) is false)
        {
            return OptionalWithDiagnostics.Empty<IUnresolvedScalarType>(Diagnostics.DifferenceNotScalar(context, definition));
        }

        return OptionalWithDiagnostics.Result(scalar);
    }

    private IResultWithDiagnostics<IUnresolvedUnitInstance?> ProcessDefaultUnitName(ISpecializedSharpMeasuresScalarResolutionContext context,
        UnresolvedSpecializedSharpMeasuresScalarDefinition definition, IUnresolvedUnitType unit)
    {
        if (definition.DefaultUnitName is null)
        {
            return ResultWithDiagnostics.Construct<IUnresolvedUnitInstance?>(null);
        }

        if (unit.UnitsByName.TryGetValue(definition.DefaultUnitName, out var unitInstance) is false)
        {
            return ResultWithDiagnostics.Construct<IUnresolvedUnitInstance?>(null, Diagnostics.UnrecognizedDefaultUnit(context, definition, unit));
        }

        return ResultWithDiagnostics.Construct<IUnresolvedUnitInstance?>(unitInstance);
    }

    private static IResultWithDiagnostics<IUnresolvedScalarType?> ProcessPowerQuantity(ISpecializedSharpMeasuresScalarResolutionContext context,
        UnresolvedSpecializedSharpMeasuresScalarDefinition definition, NamedType? quantity,
        Func<ISpecializedSharpMeasuresScalarResolutionContext, UnresolvedSpecializedSharpMeasuresScalarDefinition, Diagnostic?> typeNotScalarDiagnosticsDelegate)
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
