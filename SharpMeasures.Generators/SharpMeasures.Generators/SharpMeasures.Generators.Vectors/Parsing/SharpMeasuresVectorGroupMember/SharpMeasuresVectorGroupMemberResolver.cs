namespace SharpMeasures.Generators.Vectors.Parsing.SharpMeasuresVectorGroupMember;

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

internal interface ISharpMeasuresVectorGroupMemberResolutionDiagnostics
{
    public abstract Diagnostic? TypeAlreadyUnit(ISharpMeasuresVectorGroupMemberResolutionContext context, UnresolvedSharpMeasuresVectorGroupMemberDefinition definition);
    public abstract Diagnostic? TypeAlreadyScalar(ISharpMeasuresVectorGroupMemberResolutionContext context, UnresolvedSharpMeasuresVectorGroupMemberDefinition definition);
    public abstract Diagnostic? TypeAlreadyVector(ISharpMeasuresVectorGroupMemberResolutionContext context, UnresolvedSharpMeasuresVectorGroupMemberDefinition definition);
    public abstract Diagnostic? TypeAlreadyVectorGroup(ISharpMeasuresVectorGroupMemberResolutionContext context, UnresolvedSharpMeasuresVectorGroupMemberDefinition definition);
    public abstract Diagnostic? TypeNotVectorGroup(ISharpMeasuresVectorGroupMemberResolutionContext context, UnresolvedSharpMeasuresVectorGroupMemberDefinition definition);
}

internal interface ISharpMeasuresVectorGroupMemberResolutionContext : IProcessingContext
{
    public abstract IUnresolvedUnitPopulation UnitPopulation { get; }
    public abstract IUnresolvedScalarPopulation ScalarPopulation { get; }
    public abstract IUnresolvedVectorPopulation VectorPopulation { get; }
}

internal class SharpMeasuresVectorGroupMemberResolver
    : IProcesser<ISharpMeasuresVectorGroupMemberResolutionContext, UnresolvedSharpMeasuresVectorGroupMemberDefinition, SharpMeasuresVectorGroupMemberDefinition>
{
    private ISharpMeasuresVectorGroupMemberResolutionDiagnostics Diagnostics { get; }

    public SharpMeasuresVectorGroupMemberResolver(ISharpMeasuresVectorGroupMemberResolutionDiagnostics diagnostics)
    {
        Diagnostics = diagnostics;
    }

    public IOptionalWithDiagnostics<SharpMeasuresVectorGroupMemberDefinition> Process(ISharpMeasuresVectorGroupMemberResolutionContext context,
        UnresolvedSharpMeasuresVectorGroupMemberDefinition definition)
    {
        if (context.UnitPopulation.Units.ContainsKey(context.Type.AsNamedType()))
        {
            return OptionalWithDiagnostics.Empty<SharpMeasuresVectorGroupMemberDefinition>(Diagnostics.TypeAlreadyUnit(context, definition));
        }

        if (context.ScalarPopulation.Scalars.ContainsKey(context.Type.AsNamedType()))
        {
            return OptionalWithDiagnostics.Empty<SharpMeasuresVectorGroupMemberDefinition>(Diagnostics.TypeAlreadyScalar(context, definition));
        }

        if (context.VectorPopulation.IndividualVectors.ContainsKey(context.Type.AsNamedType()))
        {
            return OptionalWithDiagnostics.Empty<SharpMeasuresVectorGroupMemberDefinition>(Diagnostics.TypeAlreadyVector(context, definition));
        }

        if (context.VectorPopulation.VectorGroups.ContainsKey(context.Type.AsNamedType()))
        {
            return OptionalWithDiagnostics.Empty<SharpMeasuresVectorGroupMemberDefinition>(Diagnostics.TypeAlreadyVectorGroup(context, definition));
        }

        var processedVectorGroup = ProcessVectorGroup(context, definition);
        var allDiagnostics = processedVectorGroup.Diagnostics;

        if (processedVectorGroup.LacksResult)
        {
            return OptionalWithDiagnostics.Empty<SharpMeasuresVectorGroupMemberDefinition>(allDiagnostics);
        }

        if (context.VectorPopulation.VectorGroupBases.TryGetValue(definition.VectorGroup, out var vectorGroupBase) is false)
        {
            return OptionalWithDiagnostics.Empty<SharpMeasuresVectorGroupMemberDefinition>();
        }

        if (context.UnitPopulation.Units.TryGetValue(vectorGroupBase.Definition.Unit, out var unit) is false)
        {
            return OptionalWithDiagnostics.Empty<SharpMeasuresVectorGroupMemberDefinition>();
        }

        var resolvedScalar = ResolveScalar(context, vectorGroupBase.Definition);

        var resolvedImplementSum = ResolveImplementSum(context, vectorGroupBase.Definition);
        var resolvedImplementDifference = ResolveImplementDifference(context, vectorGroupBase.Definition);
        var resolvedDifference = ResolveDifference(context, vectorGroupBase.Definition);

        var resolvedDefaultUnit = ResolveDefaultUnit(context, vectorGroupBase.Definition, unit);
        var resolvedDefaultUnitSymbol = ResolveDefaultUnitSymbol(context, vectorGroupBase.Definition);

        var resolvedGenerateDocumentation = ResolveGenerateDocumentation(context, vectorGroupBase.Definition);

        SharpMeasuresVectorGroupMemberDefinition product = new(processedVectorGroup.Result, unit, resolvedScalar, definition.Dimension, resolvedImplementSum,
            resolvedImplementDifference, resolvedDifference, resolvedDefaultUnit, resolvedDefaultUnitSymbol, resolvedGenerateDocumentation, SharpMeasuresVectorLocations.Empty);

        return OptionalWithDiagnostics.Result(product, allDiagnostics);
    }

    private IOptionalWithDiagnostics<IUnresolvedVectorGroupType> ProcessVectorGroup(ISharpMeasuresVectorGroupMemberResolutionContext context,
        UnresolvedSharpMeasuresVectorGroupMemberDefinition definition)
    {
        if (context.VectorPopulation.VectorGroups.TryGetValue(definition.VectorGroup, out var vectorGroup) is false)
        {
            return OptionalWithDiagnostics.Empty<IUnresolvedVectorGroupType>(Diagnostics.TypeNotVectorGroup(context, definition));
        }

        return OptionalWithDiagnostics.Result(vectorGroup);
    }

    private static IUnresolvedScalarType? ResolveScalar(ISharpMeasuresVectorGroupMemberResolutionContext context, IUnresolvedVectorGroup vector)
    {
        return RecursivelySearchForDefinedScalar(vector, context.ScalarPopulation, context.VectorPopulation, static (vector) => vector.Scalar);
    }

    private static bool ResolveImplementSum(ISharpMeasuresVectorGroupMemberResolutionContext context, IUnresolvedVectorGroup vector)
    {
        return RecursivelySearchForDefined(vector, context.VectorPopulation, static (vector) => vector.ImplementSum)!.Value;
    }

    private static bool ResolveImplementDifference(ISharpMeasuresVectorGroupMemberResolutionContext context, IUnresolvedVectorGroup vector)
    {
        return RecursivelySearchForDefined(vector, context.VectorPopulation, static (vector) => vector.ImplementDifference)!.Value;
    }

    private static IUnresolvedVectorGroupType ResolveDifference(ISharpMeasuresVectorGroupMemberResolutionContext context, IUnresolvedVectorGroup vector)
    {
        return RecursivelySearchForDefinedVectorGroup(vector, context.VectorPopulation, static (vector) => vector.Difference)!;
    }

    private static IUnresolvedUnitInstance? ResolveDefaultUnit(ISharpMeasuresVectorGroupMemberResolutionContext context,
        IUnresolvedVectorGroup vector, IUnresolvedUnitType unit)
    {
        return RecursivelySearchForDefinedUnitInstance(vector, context.VectorPopulation, unit, static (vector) => vector.DefaultUnitName);
    }

    private static string? ResolveDefaultUnitSymbol(ISharpMeasuresVectorGroupMemberResolutionContext context,
        IUnresolvedVectorGroup vector)
    {
        return RecursivelySearchForDefined(vector, context.VectorPopulation, static (vector) => vector.DefaultUnitSymbol);
    }

    private static bool? ResolveGenerateDocumentation(ISharpMeasuresVectorGroupMemberResolutionContext context, IUnresolvedVectorGroup vector)
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
