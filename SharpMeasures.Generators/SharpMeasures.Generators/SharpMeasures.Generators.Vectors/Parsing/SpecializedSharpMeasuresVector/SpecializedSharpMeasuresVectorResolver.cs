﻿namespace SharpMeasures.Generators.Vectors.Parsing.SpecializedSharpMeasuresVector;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Unresolved.Scalars;
using SharpMeasures.Generators.Unresolved.Units;
using SharpMeasures.Generators.Unresolved.Units.UnitInstances;
using SharpMeasures.Generators.Unresolved.Vectors;
using SharpMeasures.Generators.Vectors.Parsing.SharpMeasuresVector;

using System;
using System.Collections.Generic;
using System.Linq;

internal interface ISpecializedSharpMeasuresVectorResolutionDiagnostics
{
    public abstract Diagnostic? TypeAlreadyUnit(ISpecializedSharpMeasuresVectorResolutionContext context, UnresolvedSpecializedSharpMeasuresVectorDefinition definition);
    public abstract Diagnostic? TypeAlreadyScalar(ISpecializedSharpMeasuresVectorResolutionContext context, UnresolvedSpecializedSharpMeasuresVectorDefinition definition);
    public abstract Diagnostic? OriginalNotVector(ISpecializedSharpMeasuresVectorResolutionContext context, UnresolvedSpecializedSharpMeasuresVectorDefinition definition);
    public abstract Diagnostic? TypeNotScalar(ISpecializedSharpMeasuresVectorResolutionContext context, UnresolvedSpecializedSharpMeasuresVectorDefinition definition);
    public abstract Diagnostic? DifferenceNotVector(ISpecializedSharpMeasuresVectorResolutionContext context, UnresolvedSpecializedSharpMeasuresVectorDefinition definition);
    public abstract Diagnostic? DifferenceVectorGroupLacksMatchingDimension(ISpecializedSharpMeasuresVectorResolutionContext context,
        UnresolvedSpecializedSharpMeasuresVectorDefinition definition, int dimension);
    public abstract Diagnostic? UnrecognizedDefaultUnit(ISpecializedSharpMeasuresVectorResolutionContext context, UnresolvedSpecializedSharpMeasuresVectorDefinition definition,
        IUnresolvedUnitType unit);
}

internal interface ISpecializedSharpMeasuresVectorResolutionContext : IProcessingContext
{
    public abstract IUnresolvedUnitPopulation UnitPopulation { get; }
    public abstract IUnresolvedScalarPopulation ScalarPopulation { get; }
    public abstract IUnresolvedVectorPopulation VectorPopulation { get; }
}

internal class SpecializedSharpMeasuresVectorResolver
    : IProcesser<ISpecializedSharpMeasuresVectorResolutionContext, UnresolvedSpecializedSharpMeasuresVectorDefinition, SpecializedSharpMeasuresVectorDefinition>
{
    private ISpecializedSharpMeasuresVectorResolutionDiagnostics Diagnostics { get; }

    public SpecializedSharpMeasuresVectorResolver(ISpecializedSharpMeasuresVectorResolutionDiagnostics diagnostics)
    {
        Diagnostics = diagnostics;
    }

    public IOptionalWithDiagnostics<SpecializedSharpMeasuresVectorDefinition> Process(ISpecializedSharpMeasuresVectorResolutionContext context,
        UnresolvedSpecializedSharpMeasuresVectorDefinition definition)
    {
        if (context.UnitPopulation.Units.ContainsKey(context.Type.AsNamedType()))
        {
            return OptionalWithDiagnostics.Empty<SpecializedSharpMeasuresVectorDefinition>(Diagnostics.TypeAlreadyUnit(context, definition));
        }

        if (context.ScalarPopulation.Scalars.ContainsKey(context.Type.AsNamedType()))
        {
            return OptionalWithDiagnostics.Empty<SpecializedSharpMeasuresVectorDefinition>(Diagnostics.TypeAlreadyScalar(context, definition));
        }

        if (context.VectorPopulation.IndividualVectorBases.TryGetValue(context.Type.AsNamedType(), out var baseVector) is false)
        {
            return OptionalWithDiagnostics.EmptyWithoutDiagnostics<SpecializedSharpMeasuresVectorDefinition>();
        }

        if (context.UnitPopulation.Units.TryGetValue(baseVector.Definition.Unit, out var unit) is false)
        {
            return OptionalWithDiagnostics.EmptyWithoutDiagnostics<SpecializedSharpMeasuresVectorDefinition>();
        }

        var processedOriginalVector = ProcessOriginalVector(context, definition);
        var allDiagnostics = processedOriginalVector.Diagnostics;

        if (processedOriginalVector.LacksResult)
        {
            return OptionalWithDiagnostics.Empty<SpecializedSharpMeasuresVectorDefinition>(allDiagnostics);
        }

        var processedVector = ProcessScalar(context, definition);
        var processedDifference = ProcessDifference(context, definition, baseVector.Definition.Dimension);
        var processedDefaultUnitName = ProcessDefaultUnitName(context, definition, unit);

        allDiagnostics = allDiagnostics.Concat(processedVector.Diagnostics).Concat(processedDifference.Diagnostics).Concat(processedDefaultUnitName.Diagnostics);

        var resolvedScalar = ResolveScalar(context, definition);

        var resolvedImplementSum = ResolveImplementSum(context, definition);
        var resolvedImplementDifference = ResolveImplementDifference(context, definition);
        var resolvedDifference = ResolveDifference(context, definition);

        var resolvedDefaultUnit = ResolveDefaultUnit(context, definition, unit);
        var resolvedDefaultUnitSymbol = ResolveDefaultUnitSymbol(context, definition);

        var resolvedGenerateDocumentation = ResolveGenerateDocumentation(context, definition);

        SpecializedSharpMeasuresVectorDefinition product = new(processedOriginalVector.Result, definition.InheritDerivations, definition.InheritConstants,
            definition.InheritConversions, definition.InheritUnits, unit, resolvedScalar, baseVector.Definition.Dimension, resolvedImplementSum, resolvedImplementDifference,
            resolvedDifference, resolvedDefaultUnit, resolvedDefaultUnitSymbol, resolvedGenerateDocumentation, SharpMeasuresVectorLocations.Empty);

        return OptionalWithDiagnostics.Result(product, allDiagnostics);
    }

    private IOptionalWithDiagnostics<IUnresolvedIndividualVectorType> ProcessOriginalVector(ISpecializedSharpMeasuresVectorResolutionContext context,
        UnresolvedSpecializedSharpMeasuresVectorDefinition definition)
    {
        if (context.VectorPopulation.IndividualVectors.TryGetValue(definition.OriginalVector, out var vector) is false)
        {
            return OptionalWithDiagnostics.Empty<IUnresolvedIndividualVectorType>(Diagnostics.OriginalNotVector(context, definition));
        }

        return OptionalWithDiagnostics.Result(vector);
    }

    private IOptionalWithDiagnostics<IUnresolvedScalarType> ProcessScalar(ISpecializedSharpMeasuresVectorResolutionContext context,
        UnresolvedSpecializedSharpMeasuresVectorDefinition definition)
    {
        if (definition.Scalar is null)
        {
            return OptionalWithDiagnostics.EmptyWithoutDiagnostics<IUnresolvedScalarType>();
        }

        if (context.ScalarPopulation.Scalars.TryGetValue(definition.Scalar.Value, out var scalar) is false)
        {
            return OptionalWithDiagnostics.Empty<IUnresolvedScalarType>(Diagnostics.TypeNotScalar(context, definition));
        }

        return OptionalWithDiagnostics.Result(scalar);
    }

    private IOptionalWithDiagnostics<IUnresolvedIndividualVectorType> ProcessDifference(ISpecializedSharpMeasuresVectorResolutionContext context,
        UnresolvedSpecializedSharpMeasuresVectorDefinition definition, int dimension)
    {
        if (definition.Difference is null)
        {
            return OptionalWithDiagnostics.EmptyWithoutDiagnostics<IUnresolvedIndividualVectorType>();
        }

        if (context.VectorPopulation.IndividualVectors.TryGetValue(definition.Difference.Value, out var vector) is false)
        {
            if (context.VectorPopulation.VectorGroups.TryGetValue(definition.Difference.Value, out var vectorGroup) is false)
            {
                return OptionalWithDiagnostics.Empty<IUnresolvedIndividualVectorType>(Diagnostics.DifferenceNotVector(context, definition));
            }

            if (vectorGroup.RegisteredMembersByDimension.TryGetValue(dimension, out var difference) is false)
            {
                return OptionalWithDiagnostics.Empty<IUnresolvedIndividualVectorType>(Diagnostics.DifferenceVectorGroupLacksMatchingDimension(context, definition, dimension));
            }

            return OptionalWithDiagnostics.Result(context.VectorPopulation.IndividualVectors[difference.Vector]);
        }

        return OptionalWithDiagnostics.Result(vector);
    }

    private IResultWithDiagnostics<IUnresolvedUnitInstance?> ProcessDefaultUnitName(ISpecializedSharpMeasuresVectorResolutionContext context,
        UnresolvedSpecializedSharpMeasuresVectorDefinition definition, IUnresolvedUnitType unit)
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

    private static IUnresolvedScalarType? ResolveScalar(ISpecializedSharpMeasuresVectorResolutionContext context, IUnresolvedIndividualVector vector)
    {
        return RecursivelySearchForDefinedScalar(vector, context.ScalarPopulation, context.VectorPopulation, static (vector) => vector.Scalar);
    }

    private static bool ResolveImplementSum(ISpecializedSharpMeasuresVectorResolutionContext context, IUnresolvedIndividualVector vector)
    {
        return RecursivelySearchForDefined(vector, context.VectorPopulation, static (vector) => vector.ImplementSum)!.Value;
    }

    private static bool ResolveImplementDifference(ISpecializedSharpMeasuresVectorResolutionContext context, IUnresolvedIndividualVector vector)
    {
        return RecursivelySearchForDefined(vector, context.VectorPopulation, static (vector) => vector.ImplementDifference)!.Value;
    }

    private static IUnresolvedVectorGroupType ResolveDifference(ISpecializedSharpMeasuresVectorResolutionContext context, IUnresolvedIndividualVector vector)
    {
        return RecursivelySearchForDefinedVectorGroup(vector, context.VectorPopulation, static (vector) => vector.Difference)!;
    }

    private static IUnresolvedUnitInstance? ResolveDefaultUnit(ISpecializedSharpMeasuresVectorResolutionContext context, IUnresolvedIndividualVector vector,
        IUnresolvedUnitType unit)
    {
        return RecursivelySearchForDefinedUnitInstance(vector, context.VectorPopulation, unit, static (vector) => vector.DefaultUnitName);
    }

    private static string? ResolveDefaultUnitSymbol(ISpecializedSharpMeasuresVectorResolutionContext context, IUnresolvedIndividualVector vector)
    {
        return RecursivelySearchForDefined(vector, context.VectorPopulation, static (vector) => vector.DefaultUnitSymbol);
    }

    private static bool? ResolveGenerateDocumentation(ISpecializedSharpMeasuresVectorResolutionContext context, IUnresolvedIndividualVector vector)
    {
        return RecursivelySearchForDefined(vector, context.VectorPopulation, static (vector) => vector.GenerateDocumentation);
    }

    private static T? RecursivelySearch<T>(IUnresolvedIndividualVector vector, IUnresolvedVectorPopulation vectorPopulation, Func<IUnresolvedIndividualVector, bool> predicate,
        Func<IUnresolvedIndividualVector, T?> transform)
    {
        if (predicate(vector))
        {
            return transform(vector);
        }

        if (vector is IUnresolvedIndividualVectorSpecialization vectorSpecialization
            && vectorPopulation.IndividualVectors.TryGetValue(vectorSpecialization.OriginalQuantity, out var originalVector))
        {
            return RecursivelySearch(originalVector.Definition, vectorPopulation, predicate, transform);
        }

        return default;
    }

    private static T? RecursivelySearchForDefined<T>(IUnresolvedIndividualVector vector, IUnresolvedVectorPopulation vectorPopulation,
        Func<IUnresolvedIndividualVector, T?> transform)
    {
        return RecursivelySearch<T>(vector, vectorPopulation, (vector) => transform(vector) is not null, transform);
    }

    private static T? RecursivelySearchForDefinedPopulationItem<T>(IUnresolvedIndividualVector vector, IUnresolvedVectorPopulation vectorPopulation,
        IReadOnlyDictionary<NamedType, T> population, Func<IUnresolvedIndividualVector, NamedType?> transform)
    {
        return RecursivelySearchForDefined(vector, vectorPopulation, transformWrapper);

        T? transformWrapper(IUnresolvedIndividualVector vector)
        {
            NamedType? value = transform(vector);

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

    private static T? RecursivelySearchForDefinedPopulationItem<T>(IUnresolvedIndividualVector vector, IUnresolvedVectorPopulation vectorPopulation,
        IReadOnlyDictionary<string, T> population, Func<IUnresolvedIndividualVector, string?> transform)
    {
        return RecursivelySearchForDefined(vector, vectorPopulation, transformWrapper);

        T? transformWrapper(IUnresolvedIndividualVector vector)
        {
            string? value = transform(vector);

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

    private static IUnresolvedScalarType? RecursivelySearchForDefinedScalar(IUnresolvedIndividualVector vector, IUnresolvedScalarPopulation scalarPopulation,
        IUnresolvedVectorPopulation vectorPopulation, Func<IUnresolvedIndividualVector, NamedType?> transform)
    {
        return RecursivelySearchForDefinedPopulationItem(vector, vectorPopulation, scalarPopulation.Scalars, transform);
    }

    private static IUnresolvedVectorGroupType? RecursivelySearchForDefinedVectorGroup(IUnresolvedIndividualVector vector, IUnresolvedVectorPopulation vectorPopulation,
        Func<IUnresolvedIndividualVector, NamedType?> transform)
    {
        return RecursivelySearchForDefinedPopulationItem(vector, vectorPopulation, vectorPopulation.VectorGroups, transform);
    }

    private static IUnresolvedUnitInstance? RecursivelySearchForDefinedUnitInstance(IUnresolvedIndividualVector vector, IUnresolvedVectorPopulation vectorPopulation,
        IUnresolvedUnitType unit, Func<IUnresolvedIndividualVector, string?> transform)
    {
        return RecursivelySearchForDefinedPopulationItem(vector, vectorPopulation, unit.UnitsByName, transform);
    }
}