namespace SharpMeasures.Generators.Scalars.Parsing.SpecializedSharpMeasuresScalar;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators;
using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Scalars.Parsing.SharpMeasuresScalar;
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
    public abstract Diagnostic? DifferenceNotScalar(ISpecializedSharpMeasuresScalarResolutionContext context, UnresolvedSpecializedSharpMeasuresScalarDefinition definition);
    public abstract Diagnostic? UnrecognizedDefaultUnit(ISpecializedSharpMeasuresScalarResolutionContext context, UnresolvedSpecializedSharpMeasuresScalarDefinition definition,
        IUnresolvedUnitType unit);
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

        if (context.ScalarPopulation.ScalarBases.TryGetValue(context.Type.AsNamedType(), out var baseScalar) is false)
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

        var resolvedVector = ResolveVector(context, definition);

        var resolvedImplementSum = ResolveImplementSum(context, definition);
        var resolvedImplementDifference = ResolveImplementDifference(context, definition);
        var resolvedDifference = ResolveDifference(context, definition);

        var resolvedDefaultUnit = ResolveDefaultUnit(context, definition, unit);
        var resolvedDefaultUnitSymbol = ResolveDefaultUnitSymbol(context, definition);

        var resolvedReciprocal = ResolveReciprocal(context, definition);
        var resolvedSquare = ResolveSquare(context, definition);
        var resolvedCube = ResolveCube(context, definition);
        var resolvedSquareRoot = ResolveSquareRoot(context, definition);
        var resolvedCubeRoot = ResolveCubeRoot(context, definition);

        var resolvedGenerateDocumentation = ResolveGenerateDocumentation(context, definition);

        SpecializedSharpMeasuresScalarDefinition product = new(processedOriginalScalar.Result, definition.InheritDerivations, definition.InheritConstants,
            definition.InheritConversions, definition.InheritBases, definition.InheritUnits, unit, resolvedVector, baseScalar.Definition.UseUnitBias, resolvedImplementSum,
            resolvedImplementDifference, resolvedDifference, resolvedDefaultUnit, resolvedDefaultUnitSymbol, resolvedReciprocal, resolvedSquare, resolvedCube,
            resolvedSquareRoot, resolvedCubeRoot, resolvedGenerateDocumentation, SharpMeasuresScalarLocations.Empty);

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

    private IOptionalWithDiagnostics<IUnresolvedVectorGroupType> ProcessVector(ISpecializedSharpMeasuresScalarResolutionContext context,
        UnresolvedSpecializedSharpMeasuresScalarDefinition definition)
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

    private static IUnresolvedVectorGroupType? ResolveVector(ISpecializedSharpMeasuresScalarResolutionContext context,
        IUnresolvedScalar scalar)
    {
        return RecursivelySearchForDefinedVector(scalar, context.ScalarPopulation, context.VectorPopulation, static (scalar) => scalar.VectorGroup);
    }

    private static bool ResolveImplementSum(ISpecializedSharpMeasuresScalarResolutionContext context, IUnresolvedScalar scalar)
    {
        return RecursivelySearchForDefined(scalar, context.ScalarPopulation, static (scalar) => scalar.ImplementSum)!.Value;
    }

    private static bool ResolveImplementDifference(ISpecializedSharpMeasuresScalarResolutionContext context, IUnresolvedScalar scalar)
    {
        return RecursivelySearchForDefined(scalar, context.ScalarPopulation, static (scalar) => scalar.ImplementDifference)!.Value;
    }

    private static IUnresolvedScalarType ResolveDifference(ISpecializedSharpMeasuresScalarResolutionContext context, IUnresolvedScalar scalar)
    {
        return RecursivelySearchForDefinedScalar(scalar, context.ScalarPopulation, static (scalar) => scalar.Difference)!;
    }

    private static IUnresolvedUnitInstance? ResolveDefaultUnit(ISpecializedSharpMeasuresScalarResolutionContext context,
        IUnresolvedScalar scalar, IUnresolvedUnitType unit)
    {
        return RecursivelySearchForDefinedUnitInstance(scalar, context.ScalarPopulation, unit, static (scalar) => scalar.DefaultUnitName);
    }

    private static string? ResolveDefaultUnitSymbol(ISpecializedSharpMeasuresScalarResolutionContext context,
        IUnresolvedScalar scalar)
    {
        return RecursivelySearchForDefined(scalar, context.ScalarPopulation, static (scalar) => scalar.DefaultUnitSymbol);
    }

    private static IUnresolvedScalarType? ResolveReciprocal(ISpecializedSharpMeasuresScalarResolutionContext context, IUnresolvedScalar scalar)
    {
        return RecursivelySearchForDefinedScalar(scalar, context.ScalarPopulation, static (scalar) => scalar.Reciprocal);
    }

    private static IUnresolvedScalarType? ResolveSquare(ISpecializedSharpMeasuresScalarResolutionContext context, IUnresolvedScalar scalar)
    {
        return RecursivelySearchForDefinedScalar(scalar, context.ScalarPopulation, static (scalar) => scalar.Square);
    }

    private static IUnresolvedScalarType? ResolveCube(ISpecializedSharpMeasuresScalarResolutionContext context, IUnresolvedScalar scalar)
    {
        return RecursivelySearchForDefinedScalar(scalar, context.ScalarPopulation, static (scalar) => scalar.Cube);
    }

    private static IUnresolvedScalarType? ResolveSquareRoot(ISpecializedSharpMeasuresScalarResolutionContext context, IUnresolvedScalar scalar)
    {
        return RecursivelySearchForDefinedScalar(scalar, context.ScalarPopulation, static (scalar) => scalar.SquareRoot);
    }

    private static IUnresolvedScalarType? ResolveCubeRoot(ISpecializedSharpMeasuresScalarResolutionContext context, IUnresolvedScalar scalar)
    {
        return RecursivelySearchForDefinedScalar(scalar, context.ScalarPopulation, static (scalar) => scalar.CubeRoot);
    }

    private static bool? ResolveGenerateDocumentation(ISpecializedSharpMeasuresScalarResolutionContext context, IUnresolvedScalar scalar)
    {
        return RecursivelySearchForDefined(scalar, context.ScalarPopulation, static (scalar) => scalar.GenerateDocumentation);
    }

    private static T? RecursivelySearch<T>(IUnresolvedScalar scalar, IUnresolvedScalarPopulation scalarPopulation, Func<IUnresolvedScalar, bool> predicate,
        Func<IUnresolvedScalar, T?> transform)
    {
        if (predicate(scalar))
        {
            return transform(scalar);
        }

        if (scalar is IUnresolvedScalarSpecialization scalarSpecialization
            && scalarPopulation.Scalars.TryGetValue(scalarSpecialization.OriginalQuantity, out var originalScalar))
        {
            return RecursivelySearch(originalScalar.Definition, scalarPopulation, predicate, transform);
        }

        return default;
    }

    private static T? RecursivelySearchForDefined<T>(IUnresolvedScalar scalar, IUnresolvedScalarPopulation scalarPopulation, Func<IUnresolvedScalar, T?> transform)
    {
        return RecursivelySearch<T>(scalar, scalarPopulation, (scalar) => transform(scalar) is not null, transform);
    }

    private static T? RecursivelySearchForDefinedPopulationItem<T>(IUnresolvedScalar scalar, IUnresolvedScalarPopulation scalarPopulation,
        IReadOnlyDictionary<NamedType, T> population, Func<IUnresolvedScalar, NamedType?> transform)
    {
        return RecursivelySearchForDefined(scalar, scalarPopulation, transformWrapper);

        T? transformWrapper(IUnresolvedScalar scalar)
        {
            NamedType? value = transform(scalar);

            if (value is null)
            {
                return default;
            }

            if (population.TryGetValue(value.Value, out var result) is false)
            {
                return default;
            }

            return result;
        }
    }

    private static T? RecursivelySearchForDefinedPopulationItem<T>(IUnresolvedScalar scalar, IUnresolvedScalarPopulation scalarPopulation,
        IReadOnlyDictionary<string, T> population, Func<IUnresolvedScalar, string?> transform)
    {
        return RecursivelySearchForDefined(scalar, scalarPopulation, transformWrapper);

        T? transformWrapper(IUnresolvedScalar scalar)
        {
            string? value = transform(scalar);

            if (value is null)
            {
                return default;
            }

            if (population.TryGetValue(value, out var result) is false)
            {
                return default;
            }

            return result;
        }
    }

    private static IUnresolvedScalarType? RecursivelySearchForDefinedScalar(IUnresolvedScalar scalar, IUnresolvedScalarPopulation scalarPopulation,
        Func<IUnresolvedScalar, NamedType?> transform)
    {
        return RecursivelySearchForDefinedPopulationItem(scalar, scalarPopulation, scalarPopulation.Scalars, transform);
    }

    private static IUnresolvedVectorGroupType? RecursivelySearchForDefinedVector(IUnresolvedScalar scalar, IUnresolvedScalarPopulation scalarPopulation,
        IUnresolvedVectorPopulation vectorPopulation, Func<IUnresolvedScalar, NamedType?> transform)
    {
        return RecursivelySearchForDefinedPopulationItem(scalar, scalarPopulation, vectorPopulation.VectorGroups, transform);
    }

    private static IUnresolvedUnitInstance? RecursivelySearchForDefinedUnitInstance(IUnresolvedScalar scalar, IUnresolvedScalarPopulation scalarPopulation,
        IUnresolvedUnitType unit, Func<IUnresolvedScalar, string?> transform)
    {
        return RecursivelySearchForDefinedPopulationItem(scalar, scalarPopulation, unit.UnitsByName, transform);
    }
}
