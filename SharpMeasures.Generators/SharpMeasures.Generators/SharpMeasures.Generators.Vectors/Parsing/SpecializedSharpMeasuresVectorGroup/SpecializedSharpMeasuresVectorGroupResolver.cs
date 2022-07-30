namespace SharpMeasures.Generators.Vectors.Parsing.SpecializedSharpMeasuresVectorGroup;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Unresolved.Scalars;
using SharpMeasures.Generators.Unresolved.Units;
using SharpMeasures.Generators.Unresolved.Units.UnitInstances;
using SharpMeasures.Generators.Unresolved.Vectors;
using SharpMeasures.Generators.Vectors.Parsing.Abstraction;
using SharpMeasures.Generators.Vectors.Parsing.SharpMeasuresVectorGroup;

using System;
using System.Collections.Generic;
using System.Linq;

internal interface ISpecializedSharpMeasuresVectorGroupResolutionDiagnostics
{
    public abstract Diagnostic? TypeAlreadyUnit(ISpecializedSharpMeasuresVectorGroupResolutionContext context, UnresolvedSpecializedSharpMeasuresVectorGroupDefinition definition);
    public abstract Diagnostic? TypeAlreadyScalar(ISpecializedSharpMeasuresVectorGroupResolutionContext context, UnresolvedSpecializedSharpMeasuresVectorGroupDefinition definition);
    public abstract Diagnostic? TypeAlreadyVector(ISpecializedSharpMeasuresVectorGroupResolutionContext context, UnresolvedSpecializedSharpMeasuresVectorGroupDefinition definition);
    public abstract Diagnostic? OriginalNotVectorGroup(ISpecializedSharpMeasuresVectorGroupResolutionContext context, UnresolvedSpecializedSharpMeasuresVectorGroupDefinition definition);
    public abstract Diagnostic? TypeNotScalar(ISpecializedSharpMeasuresVectorGroupResolutionContext context, UnresolvedSpecializedSharpMeasuresVectorGroupDefinition definition);
    public abstract Diagnostic? DifferenceNotVectorGroup(ISpecializedSharpMeasuresVectorGroupResolutionContext context, UnresolvedSpecializedSharpMeasuresVectorGroupDefinition definition);
    public abstract Diagnostic? UnrecognizedDefaultUnit(ISpecializedSharpMeasuresVectorGroupResolutionContext context,
        UnresolvedSpecializedSharpMeasuresVectorGroupDefinition definition, IUnresolvedUnitType unit);
}

internal interface ISpecializedSharpMeasuresVectorGroupResolutionContext : IProcessingContext
{
    public abstract IUnresolvedUnitPopulation UnitPopulation { get; }
    public abstract IUnresolvedScalarPopulation ScalarPopulation { get; }
    public abstract IUnresolvedVectorPopulationWithData VectorPopulation { get; }
}

internal class SpecializedSharpMeasuresVectorGroupResolver
    : IProcesser<ISpecializedSharpMeasuresVectorGroupResolutionContext, UnresolvedSpecializedSharpMeasuresVectorGroupDefinition, SpecializedSharpMeasuresVectorGroupDefinition>
{
    private ISpecializedSharpMeasuresVectorGroupResolutionDiagnostics Diagnostics { get; }

    public SpecializedSharpMeasuresVectorGroupResolver(ISpecializedSharpMeasuresVectorGroupResolutionDiagnostics diagnostics)
    {
        Diagnostics = diagnostics;
    }

    public IOptionalWithDiagnostics<SpecializedSharpMeasuresVectorGroupDefinition> Process(ISpecializedSharpMeasuresVectorGroupResolutionContext context,
        UnresolvedSpecializedSharpMeasuresVectorGroupDefinition definition)
    {
        if (context.UnitPopulation.Units.ContainsKey(context.Type.AsNamedType()))
        {
            return OptionalWithDiagnostics.Empty<SpecializedSharpMeasuresVectorGroupDefinition>(Diagnostics.TypeAlreadyUnit(context, definition));
        }

        if (context.ScalarPopulation.Scalars.ContainsKey(context.Type.AsNamedType()))
        {
            return OptionalWithDiagnostics.Empty<SpecializedSharpMeasuresVectorGroupDefinition>(Diagnostics.TypeAlreadyScalar(context, definition));
        }

        if (context.VectorPopulation.DuplicatelyDefined.ContainsKey(context.Type.AsNamedType()))
        {
            return OptionalWithDiagnostics.Empty<SpecializedSharpMeasuresVectorGroupDefinition>(Diagnostics.TypeAlreadyVector(context, definition));
        }

        if (context.VectorPopulation.UnassignedSpecializations.ContainsKey(context.Type.AsNamedType()))
        {
            return OptionalWithDiagnostics.Empty<SpecializedSharpMeasuresVectorGroupDefinition>(Diagnostics.OriginalNotVectorGroup(context, definition));
        }

        var vectorGroupBase = context.VectorPopulation.VectorGroupBases[context.Type.AsNamedType()];

        if (context.UnitPopulation.Units.TryGetValue(vectorGroupBase.Definition.Unit, out var unit) is false)
        {
            return OptionalWithDiagnostics.EmptyWithoutDiagnostics<SpecializedSharpMeasuresVectorGroupDefinition>();
        }

        var processedOriginalVector = ProcessOriginalVectorGroup(context, definition);
        var allDiagnostics = processedOriginalVector.Diagnostics;

        if (processedOriginalVector.LacksResult)
        {
            return OptionalWithDiagnostics.Empty<SpecializedSharpMeasuresVectorGroupDefinition>(allDiagnostics);
        }

        var processedScalar = ProcessScalar(context, definition);
        var processedDifference = ProcessDifference(context, definition);
        var processedDefaultUnitName = ProcessDefaultUnitName(context, definition, unit);

        allDiagnostics = allDiagnostics.Concat(processedScalar.Diagnostics).Concat(processedDifference.Diagnostics).Concat(processedDefaultUnitName.Diagnostics);

        var resolvedScalar = ResolveScalar(context, definition);

        var resolvedImplementSum = ResolveImplementSum(context, definition);
        var resolvedImplementDifference = ResolveImplementDifference(context, definition);
        var resolvedDifference = ResolveDifference(context, definition);

        var resolvedDefaultUnit = ResolveDefaultUnit(context, definition, unit);
        var resolvedDefaultUnitSymbol = ResolveDefaultUnitSymbol(context, definition);

        var resolvedGenerateDocumentation = ResolveGenerateDocumentation(context, definition);

        SpecializedSharpMeasuresVectorGroupDefinition product = new(processedOriginalVector.Result, definition.InheritDerivations, definition.InheritConstants,
            definition.InheritConversions, definition.InheritUnits, unit, resolvedScalar, resolvedImplementSum, resolvedImplementDifference,
            resolvedDifference, resolvedDefaultUnit, resolvedDefaultUnitSymbol, resolvedGenerateDocumentation, SharpMeasuresVectorGroupLocations.Empty);

        return OptionalWithDiagnostics.Result(product, allDiagnostics);
    }

    private IOptionalWithDiagnostics<IUnresolvedVectorGroupType> ProcessOriginalVectorGroup(ISpecializedSharpMeasuresVectorGroupResolutionContext context,
        UnresolvedSpecializedSharpMeasuresVectorGroupDefinition definition)
    {
        if (context.VectorPopulation.VectorGroups.TryGetValue(definition.OriginalVectorGroup, out var vectorGroup) is false)
        {
            return OptionalWithDiagnostics.Empty<IUnresolvedVectorGroupType>(Diagnostics.OriginalNotVectorGroup(context, definition));
        }

        return OptionalWithDiagnostics.Result(vectorGroup);
    }

    private IOptionalWithDiagnostics<IUnresolvedScalarType> ProcessScalar(ISpecializedSharpMeasuresVectorGroupResolutionContext context,
        UnresolvedSpecializedSharpMeasuresVectorGroupDefinition definition)
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

    private IOptionalWithDiagnostics<IUnresolvedVectorGroupType> ProcessDifference(ISpecializedSharpMeasuresVectorGroupResolutionContext context,
        UnresolvedSpecializedSharpMeasuresVectorGroupDefinition definition)
    {
        if (definition.Difference is null)
        {
            return OptionalWithDiagnostics.EmptyWithoutDiagnostics<IUnresolvedVectorGroupType>();
        }

        if (context.VectorPopulation.VectorGroups.TryGetValue(definition.Difference.Value, out var vectorGroup) is false)
        {
            return OptionalWithDiagnostics.Empty<IUnresolvedVectorGroupType>(Diagnostics.DifferenceNotVectorGroup(context, definition));
        }

        return OptionalWithDiagnostics.Result(vectorGroup);
    }

    private IResultWithDiagnostics<IUnresolvedUnitInstance?> ProcessDefaultUnitName(ISpecializedSharpMeasuresVectorGroupResolutionContext context,
        UnresolvedSpecializedSharpMeasuresVectorGroupDefinition definition, IUnresolvedUnitType unit)
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

    private static IUnresolvedScalarType? ResolveScalar(ISpecializedSharpMeasuresVectorGroupResolutionContext context,
        IUnresolvedVectorGroup vector)
    {
        return RecursivelySearchForDefinedScalar(vector, context.ScalarPopulation, context.VectorPopulation, static (vector) => vector.Scalar);
    }

    private static bool ResolveImplementSum(ISpecializedSharpMeasuresVectorGroupResolutionContext context, IUnresolvedVectorGroup vector)
    {
        return RecursivelySearchForDefined(vector, context.VectorPopulation, static (vector) => vector.ImplementSum)!.Value;
    }

    private static bool ResolveImplementDifference(ISpecializedSharpMeasuresVectorGroupResolutionContext context, IUnresolvedVectorGroup vector)
    {
        return RecursivelySearchForDefined(vector, context.VectorPopulation, static (vector) => vector.ImplementDifference)!.Value;
    }

    private static IUnresolvedVectorGroupType ResolveDifference(ISpecializedSharpMeasuresVectorGroupResolutionContext context, IUnresolvedVectorGroup vector)
    {
        return RecursivelySearchForDefinedVectorGroup(vector, context.VectorPopulation, static (vector) => vector.Difference)!;
    }

    private static IUnresolvedUnitInstance? ResolveDefaultUnit(ISpecializedSharpMeasuresVectorGroupResolutionContext context,
        IUnresolvedVectorGroup vector, IUnresolvedUnitType unit)
    {
        return RecursivelySearchForDefinedUnitInstance(vector, context.VectorPopulation, unit, static (vector) => vector.DefaultUnitName);
    }

    private static string? ResolveDefaultUnitSymbol(ISpecializedSharpMeasuresVectorGroupResolutionContext context,
        IUnresolvedVectorGroup vectorr)
    {
        return RecursivelySearchForDefined(vectorr, context.VectorPopulation, static (vector) => vector.DefaultUnitSymbol);
    }

    private static bool? ResolveGenerateDocumentation(ISpecializedSharpMeasuresVectorGroupResolutionContext context, IUnresolvedVectorGroup vector)
    {
        return RecursivelySearchForDefined(vector, context.VectorPopulation, static (vector) => vector.GenerateDocumentation);
    }

    private static T? RecursivelySearch<T>(IUnresolvedVectorGroup vector, IUnresolvedVectorPopulation vectorPopulation, Func<IUnresolvedVectorGroup, bool> predicate,
        Func<IUnresolvedVectorGroup, T?> transform)
    {
        if (predicate(vector))
        {
            return transform(vector);
        }

        if (vector is IUnresolvedVectorGroupSpecialization vectorSpecialization
            && vectorPopulation.VectorGroups.TryGetValue(vectorSpecialization.OriginalQuantity, out var originalVector))
        {
            return RecursivelySearch(originalVector.Definition, vectorPopulation, predicate, transform);
        }

        return default;
    }

    private static T? RecursivelySearchForDefined<T>(IUnresolvedVectorGroup vector, IUnresolvedVectorPopulation vectorPopulation, Func<IUnresolvedVectorGroup, T?> transform)
    {
        return RecursivelySearch<T>(vector, vectorPopulation, (vector) => transform(vector) is not null, transform);
    }

    private static T? RecursivelySearchForDefinedPopulationItem<T>(IUnresolvedVectorGroup vector, IUnresolvedVectorPopulation vectorPopulation,
        IReadOnlyDictionary<NamedType, T> population, Func<IUnresolvedVectorGroup, NamedType?> transform)
    {
        return RecursivelySearchForDefined(vector, vectorPopulation, transformWrapper);

        T? transformWrapper(IUnresolvedVectorGroup vector)
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

    private static T? RecursivelySearchForDefinedPopulationItem<T>(IUnresolvedVectorGroup vector, IUnresolvedVectorPopulation vectorPopulation,
        IReadOnlyDictionary<string, T> population, Func<IUnresolvedVectorGroup, string?> transform)
    {
        return RecursivelySearchForDefined(vector, vectorPopulation, transformWrapper);

        T? transformWrapper(IUnresolvedVectorGroup vector)
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

    private static IUnresolvedScalarType? RecursivelySearchForDefinedScalar(IUnresolvedVectorGroup vector, IUnresolvedScalarPopulation scalarPopulation,
        IUnresolvedVectorPopulation vectorPopulation, Func<IUnresolvedVectorGroup, NamedType?> transform)
    {
        return RecursivelySearchForDefinedPopulationItem(vector, vectorPopulation, scalarPopulation.Scalars, transform);
    }

    private static IUnresolvedVectorGroupType? RecursivelySearchForDefinedVectorGroup(IUnresolvedVectorGroup vector, IUnresolvedVectorPopulation vectorPopulation,
        Func<IUnresolvedVectorGroup, NamedType?> transform)
    {
        return RecursivelySearchForDefinedPopulationItem(vector, vectorPopulation, vectorPopulation.VectorGroups, transform);
    }

    private static IUnresolvedUnitInstance? RecursivelySearchForDefinedUnitInstance(IUnresolvedVectorGroup vector, IUnresolvedVectorPopulation vectorPopulation,
        IUnresolvedUnitType unit, Func<IUnresolvedVectorGroup, string?> transform)
    {
        return RecursivelySearchForDefinedPopulationItem(vector, vectorPopulation, unit.UnitsByName, transform);
    }
}
