namespace SharpMeasures.Generators.Scalars.Parsing.SpecializedSharpMeasuresScalar;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators;
using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Scalars.Parsing.Abstraction;
using SharpMeasures.Generators.Scalars.Parsing.SharpMeasuresScalar;
using SharpMeasures.Generators.Raw.Scalars;
using SharpMeasures.Generators.Raw.Units;
using SharpMeasures.Generators.Raw.Units.UnitInstances;
using SharpMeasures.Generators.Raw.Vectors;

using System;
using System.Collections.Generic;
using System.Linq;
using SharpMeasures.Generators.Raw.Vectors.Groups;

internal interface ISpecializedSharpMeasuresScalarResolutionDiagnostics
{
    public abstract Diagnostic? TypeAlreadyUnit(ISpecializedSharpMeasuresScalarResolutionContext context, UnresolvedSpecializedSharpMeasuresScalarDefinition definition);
    public abstract Diagnostic? TypeAlreadyScalar(ISpecializedSharpMeasuresScalarResolutionContext context, UnresolvedSpecializedSharpMeasuresScalarDefinition definition);
    public abstract Diagnostic? OriginalNotScalar(ISpecializedSharpMeasuresScalarResolutionContext context, UnresolvedSpecializedSharpMeasuresScalarDefinition definition);
    public abstract Diagnostic? RootScalarNotResolved(ISpecializedSharpMeasuresScalarResolutionContext context, UnresolvedSpecializedSharpMeasuresScalarDefinition definition);
    public abstract Diagnostic? TypeNotVector(ISpecializedSharpMeasuresScalarResolutionContext context, UnresolvedSpecializedSharpMeasuresScalarDefinition definition);
    public abstract Diagnostic? DifferenceNotScalar(ISpecializedSharpMeasuresScalarResolutionContext context, UnresolvedSpecializedSharpMeasuresScalarDefinition definition);
    public abstract Diagnostic? UnrecognizedDefaultUnit(ISpecializedSharpMeasuresScalarResolutionContext context, UnresolvedSpecializedSharpMeasuresScalarDefinition definition, IRawUnitType unit);
    public abstract Diagnostic? ReciprocalNotScalar(ISpecializedSharpMeasuresScalarResolutionContext context, UnresolvedSpecializedSharpMeasuresScalarDefinition definition);
    public abstract Diagnostic? SquareNotScalar(ISpecializedSharpMeasuresScalarResolutionContext context, UnresolvedSpecializedSharpMeasuresScalarDefinition definition);
    public abstract Diagnostic? CubeNotScalar(ISpecializedSharpMeasuresScalarResolutionContext context, UnresolvedSpecializedSharpMeasuresScalarDefinition definition);
    public abstract Diagnostic? SquareRootNotScalar(ISpecializedSharpMeasuresScalarResolutionContext context, UnresolvedSpecializedSharpMeasuresScalarDefinition definition);
    public abstract Diagnostic? CubeRootNotScalar(ISpecializedSharpMeasuresScalarResolutionContext context, UnresolvedSpecializedSharpMeasuresScalarDefinition definition);
}

internal interface ISpecializedSharpMeasuresScalarResolutionContext : IProcessingContext
{
    public abstract IRawUnitPopulation UnitPopulation { get; }
    public abstract IUnresolvedScalarPopulationWithData ScalarPopulation { get; }
    public abstract IRawVectorPopulation VectorPopulation { get; }
}

internal class SpecializedSharpMeasuresScalarResolver : IProcesser<ISpecializedSharpMeasuresScalarResolutionContext, UnresolvedSpecializedSharpMeasuresScalarDefinition, SpecializedSharpMeasuresScalarDefinition>
{
    private ISpecializedSharpMeasuresScalarResolutionDiagnostics Diagnostics { get; }

    public SpecializedSharpMeasuresScalarResolver(ISpecializedSharpMeasuresScalarResolutionDiagnostics diagnostics)
    {
        Diagnostics = diagnostics;
    }

    public IOptionalWithDiagnostics<SpecializedSharpMeasuresScalarDefinition> Process(ISpecializedSharpMeasuresScalarResolutionContext context, UnresolvedSpecializedSharpMeasuresScalarDefinition definition)
    {
        if (context.UnitPopulation.Units.ContainsKey(context.Type.AsNamedType()))
        {
            return OptionalWithDiagnostics.Empty<SpecializedSharpMeasuresScalarDefinition>(Diagnostics.TypeAlreadyUnit(context, definition));
        }

        if (context.ScalarPopulation.DuplicatelyDefined.ContainsKey(context.Type.AsNamedType()))
        {
            return OptionalWithDiagnostics.Empty<SpecializedSharpMeasuresScalarDefinition>(Diagnostics.TypeAlreadyScalar(context, definition));
        }

        if (context.ScalarPopulation.Scalars.TryGetValue(definition.OriginalScalar, out var originalScalar) is false)
        {
            return OptionalWithDiagnostics.Empty<SpecializedSharpMeasuresScalarDefinition>(Diagnostics.OriginalNotScalar(context, definition));
        }

        if (context.ScalarPopulation.ScalarBases.TryGetValue(context.Type.AsNamedType(), out var scalarBase) is false)
        {
            return OptionalWithDiagnostics.Empty<SpecializedSharpMeasuresScalarDefinition>(Diagnostics.RootScalarNotResolved(context, definition));
        }

        if (context.UnitPopulation.Units.TryGetValue(scalarBase.Definition.Unit, out var unit) is false)
        {
            return OptionalWithDiagnostics.EmptyWithoutDiagnostics<SpecializedSharpMeasuresScalarDefinition>();
        }

        var processedVector = ProcessVector(context, definition);
        var allDiagnostics = processedVector.Diagnostics;

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

        SpecializedSharpMeasuresScalarDefinition product = new(originalScalar, definition.InheritDerivations, definition.InheritConstants,
            definition.InheritConversions, definition.InheritBases, definition.InheritUnits, unit, resolvedVector, scalarBase.Definition.UseUnitBias, resolvedImplementSum,
            resolvedImplementDifference, resolvedDifference, resolvedDefaultUnit, resolvedDefaultUnitSymbol, resolvedReciprocal, resolvedSquare, resolvedCube,
            resolvedSquareRoot, resolvedCubeRoot, resolvedGenerateDocumentation, SharpMeasuresScalarLocations.Empty);

        return OptionalWithDiagnostics.Result(product, allDiagnostics);
    }

    private IOptionalWithDiagnostics<IRawVectorGroupType> ProcessVector(ISpecializedSharpMeasuresScalarResolutionContext context, UnresolvedSpecializedSharpMeasuresScalarDefinition definition)
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

    private IOptionalWithDiagnostics<IRawScalarType> ProcessDifference(ISpecializedSharpMeasuresScalarResolutionContext context, UnresolvedSpecializedSharpMeasuresScalarDefinition definition)
    {
        if (definition.Difference is null)
        {
            return OptionalWithDiagnostics.EmptyWithoutDiagnostics<IRawScalarType>();
        }

        if (context.ScalarPopulation.Scalars.TryGetValue(definition.Difference.Value, out var scalar) is false)
        {
            return OptionalWithDiagnostics.Empty<IRawScalarType>(Diagnostics.DifferenceNotScalar(context, definition));
        }

        return OptionalWithDiagnostics.Result(scalar);
    }

    private IResultWithDiagnostics<IRawUnitInstance?> ProcessDefaultUnitName(ISpecializedSharpMeasuresScalarResolutionContext context, UnresolvedSpecializedSharpMeasuresScalarDefinition definition, IRawUnitType unit)
    {
        if (definition.DefaultUnitName is null)
        {
            return ResultWithDiagnostics.Construct<IRawUnitInstance?>(null);
        }

        if (unit.UnitsByName.TryGetValue(definition.DefaultUnitName, out var unitInstance) is false)
        {
            return ResultWithDiagnostics.Construct<IRawUnitInstance?>(null, Diagnostics.UnrecognizedDefaultUnit(context, definition, unit));
        }

        return ResultWithDiagnostics.Construct<IRawUnitInstance?>(unitInstance);
    }

    private static IResultWithDiagnostics<IRawScalarType?> ProcessPowerQuantity(ISpecializedSharpMeasuresScalarResolutionContext context, UnresolvedSpecializedSharpMeasuresScalarDefinition definition, NamedType? quantity, Func<ISpecializedSharpMeasuresScalarResolutionContext, UnresolvedSpecializedSharpMeasuresScalarDefinition, Diagnostic?> typeNotScalarDiagnosticsDelegate)
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

    private static IRawVectorGroupType? ResolveVector(ISpecializedSharpMeasuresScalarResolutionContext context, IRawScalar scalar)
    {
        return RecursivelySearchForDefinedVector(scalar, context.ScalarPopulation, context.VectorPopulation, static (scalar) => scalar.Vector);
    }

    private static bool ResolveImplementSum(ISpecializedSharpMeasuresScalarResolutionContext context, IRawScalar scalar)
    {
        return RecursivelySearchForDefined(scalar, context.ScalarPopulation, static (scalar) => scalar.ImplementSum)!.Value;
    }

    private static bool ResolveImplementDifference(ISpecializedSharpMeasuresScalarResolutionContext context, IRawScalar scalar)
    {
        return RecursivelySearchForDefined(scalar, context.ScalarPopulation, static (scalar) => scalar.ImplementDifference)!.Value;
    }

    private static IRawScalarType ResolveDifference(ISpecializedSharpMeasuresScalarResolutionContext context, IRawScalar scalar)
    {
        return RecursivelySearchForDefinedScalar(scalar, context.ScalarPopulation, static (scalar) => scalar.Difference)!;
    }

    private static IRawUnitInstance? ResolveDefaultUnit(ISpecializedSharpMeasuresScalarResolutionContext context, IRawScalar scalar, IRawUnitType unit)
    {
        return RecursivelySearchForDefinedUnitInstance(scalar, context.ScalarPopulation, unit, static (scalar) => scalar.DefaultUnitName);
    }

    private static string? ResolveDefaultUnitSymbol(ISpecializedSharpMeasuresScalarResolutionContext context, IRawScalar scalar)
    {
        return RecursivelySearchForDefined(scalar, context.ScalarPopulation, static (scalar) => scalar.DefaultUnitSymbol);
    }

    private static IRawScalarType? ResolveReciprocal(ISpecializedSharpMeasuresScalarResolutionContext context, IRawScalar scalar)
    {
        return RecursivelySearchForDefinedScalar(scalar, context.ScalarPopulation, static (scalar) => scalar.Reciprocal);
    }

    private static IRawScalarType? ResolveSquare(ISpecializedSharpMeasuresScalarResolutionContext context, IRawScalar scalar)
    {
        return RecursivelySearchForDefinedScalar(scalar, context.ScalarPopulation, static (scalar) => scalar.Square);
    }

    private static IRawScalarType? ResolveCube(ISpecializedSharpMeasuresScalarResolutionContext context, IRawScalar scalar)
    {
        return RecursivelySearchForDefinedScalar(scalar, context.ScalarPopulation, static (scalar) => scalar.Cube);
    }

    private static IRawScalarType? ResolveSquareRoot(ISpecializedSharpMeasuresScalarResolutionContext context, IRawScalar scalar)
    {
        return RecursivelySearchForDefinedScalar(scalar, context.ScalarPopulation, static (scalar) => scalar.SquareRoot);
    }

    private static IRawScalarType? ResolveCubeRoot(ISpecializedSharpMeasuresScalarResolutionContext context, IRawScalar scalar)
    {
        return RecursivelySearchForDefinedScalar(scalar, context.ScalarPopulation, static (scalar) => scalar.CubeRoot);
    }

    private static bool? ResolveGenerateDocumentation(ISpecializedSharpMeasuresScalarResolutionContext context, IRawScalar scalar)
    {
        return RecursivelySearchForDefined(scalar, context.ScalarPopulation, static (scalar) => scalar.GenerateDocumentation);
    }

    private static T? RecursivelySearch<T>(IRawScalar scalar, IRawScalarPopulation scalarPopulation, Func<IRawScalar, bool> predicate, Func<IRawScalar, T?> transform)
    {
        if (predicate(scalar))
        {
            return transform(scalar);
        }

        if (scalar is IRawScalarSpecialization scalarSpecialization && scalarPopulation.Scalars.TryGetValue(scalarSpecialization.OriginalQuantity, out var originalScalar))
        {
            return RecursivelySearch(originalScalar.Definition, scalarPopulation, predicate, transform);
        }

        return default;
    }

    private static T? RecursivelySearchForDefined<T>(IRawScalar scalar, IRawScalarPopulation scalarPopulation, Func<IRawScalar, T?> transform)
    {
        return RecursivelySearch<T>(scalar, scalarPopulation, (scalar) => transform(scalar) is not null, transform);
    }

    private static T? RecursivelySearchForDefinedPopulationItem<T>(IRawScalar scalar, IRawScalarPopulation scalarPopulation, IReadOnlyDictionary<NamedType, T> population, Func<IRawScalar, NamedType?> transform)
    {
        return RecursivelySearchForDefined(scalar, scalarPopulation, transformWrapper);

        T? transformWrapper(IRawScalar scalar)
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

    private static T? RecursivelySearchForDefinedPopulationItem<T>(IRawScalar scalar, IRawScalarPopulation scalarPopulation, IReadOnlyDictionary<string, T> population, Func<IRawScalar, string?> transform)
    {
        return RecursivelySearchForDefined(scalar, scalarPopulation, transformWrapper);

        T? transformWrapper(IRawScalar scalar)
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

    private static IRawScalarType? RecursivelySearchForDefinedScalar(IRawScalar scalar, IRawScalarPopulation scalarPopulation, Func<IRawScalar, NamedType?> transform)
    {
        return RecursivelySearchForDefinedPopulationItem(scalar, scalarPopulation, scalarPopulation.Scalars, transform);
    }

    private static IRawVectorGroupType? RecursivelySearchForDefinedVector(IRawScalar scalar, IRawScalarPopulation scalarPopulation, IRawVectorPopulation vectorPopulation, Func<IRawScalar, NamedType?> transform)
    {
        return RecursivelySearchForDefinedPopulationItem(scalar, scalarPopulation, vectorPopulation.VectorGroups, transform);
    }

    private static IRawUnitInstance? RecursivelySearchForDefinedUnitInstance(IRawScalar scalar, IRawScalarPopulation scalarPopulation, IRawUnitType unit, Func<IRawScalar, string?> transform)
    {
        return RecursivelySearchForDefinedPopulationItem(scalar, scalarPopulation, unit.UnitsByName, transform);
    }
}
