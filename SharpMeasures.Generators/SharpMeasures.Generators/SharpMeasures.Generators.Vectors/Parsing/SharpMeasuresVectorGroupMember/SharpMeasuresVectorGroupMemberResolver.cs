namespace SharpMeasures.Generators.Vectors.Parsing.SharpMeasuresVectorGroupMember;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Raw.Scalars;
using SharpMeasures.Generators.Raw.Units;
using SharpMeasures.Generators.Raw.Units.UnitInstances;
using SharpMeasures.Generators.Raw.Vectors;
using SharpMeasures.Generators.Raw.Vectors.Groups;
using SharpMeasures.Generators.Vectors.Parsing.Abstraction;
using SharpMeasures.Generators.Vectors.Parsing.SharpMeasuresVector;

using System;
using System.Collections.Generic;

internal interface ISharpMeasuresVectorGroupMemberResolutionDiagnostics
{
    public abstract Diagnostic? TypeAlreadyUnit(ISharpMeasuresVectorGroupMemberResolutionContext context, UnresolvedSharpMeasuresVectorGroupMemberDefinition definition);
    public abstract Diagnostic? TypeAlreadyScalar(ISharpMeasuresVectorGroupMemberResolutionContext context, UnresolvedSharpMeasuresVectorGroupMemberDefinition definition);
    public abstract Diagnostic? TypeAlreadyVector(ISharpMeasuresVectorGroupMemberResolutionContext context, UnresolvedSharpMeasuresVectorGroupMemberDefinition definition);
    public abstract Diagnostic? TypeAlreadyVectorGroup(ISharpMeasuresVectorGroupMemberResolutionContext context, UnresolvedSharpMeasuresVectorGroupMemberDefinition definition);
    public abstract Diagnostic? TypeAlreadyVectorGroupMember(ISharpMeasuresVectorGroupMemberResolutionContext context, UnresolvedSharpMeasuresVectorGroupMemberDefinition definition);
    public abstract Diagnostic? TypeNotVectorGroup(ISharpMeasuresVectorGroupMemberResolutionContext context, UnresolvedSharpMeasuresVectorGroupMemberDefinition definition);
    public abstract Diagnostic? VectorGroupAlreadyContainsDimension(ISharpMeasuresVectorGroupMemberResolutionContext context, UnresolvedSharpMeasuresVectorGroupMemberDefinition definition);
}

internal interface ISharpMeasuresVectorGroupMemberResolutionContext : IProcessingContext
{
    public abstract IRawUnitPopulation UnitPopulation { get; }
    public abstract IRawScalarPopulation ScalarPopulation { get; }
    public abstract IUnresolvedVectorPopulationWithData VectorPopulation { get; }
}

internal class SharpMeasuresVectorGroupMemberResolver
    : IProcesser<ISharpMeasuresVectorGroupMemberResolutionContext, UnresolvedSharpMeasuresVectorGroupMemberDefinition, SharpMeasuresVectorGroupMemberDefinition>
{
    private ISharpMeasuresVectorGroupMemberResolutionDiagnostics Diagnostics { get; }

    public SharpMeasuresVectorGroupMemberResolver(ISharpMeasuresVectorGroupMemberResolutionDiagnostics diagnostics)
    {
        Diagnostics = diagnostics;
    }

    public IOptionalWithDiagnostics<SharpMeasuresVectorGroupMemberDefinition> Process(ISharpMeasuresVectorGroupMemberResolutionContext context, UnresolvedSharpMeasuresVectorGroupMemberDefinition definition)
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

        if (context.VectorPopulation.DuplicatelyDefinedVectorGroupMembers.ContainsKey(context.Type.AsNamedType()))
        {
            return OptionalWithDiagnostics.Empty<SharpMeasuresVectorGroupMemberDefinition>(Diagnostics.TypeAlreadyVectorGroupMember(context, definition));
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

    private IOptionalWithDiagnostics<IRawVectorGroupType> ProcessVectorGroup(ISharpMeasuresVectorGroupMemberResolutionContext context,
        UnresolvedSharpMeasuresVectorGroupMemberDefinition definition)
    {
        if (context.VectorPopulation.VectorGroups.TryGetValue(definition.VectorGroup, out var vectorGroup) is false || context.VectorPopulation.IndividualVectors.ContainsKey(definition.VectorGroup))
        {
            return OptionalWithDiagnostics.Empty<IRawVectorGroupType>(Diagnostics.TypeNotVectorGroup(context, definition));
        }

        if (context.VectorPopulation.VectorGroupMembersByGroup[definition.VectorGroup].VectorGroupMembersByDimension[definition.Dimension].Type != context.Type)
        {
            return OptionalWithDiagnostics.Empty<IRawVectorGroupType>(Diagnostics.VectorGroupAlreadyContainsDimension(context, definition));
        }

        return OptionalWithDiagnostics.Result(vectorGroup);
    }

    private static IRawScalarType? ResolveScalar(ISharpMeasuresVectorGroupMemberResolutionContext context, IRawVectorGroup vector)
    {
        return RecursivelySearchForDefinedScalar(vector, context.ScalarPopulation, context.VectorPopulation, static (vector) => vector.Scalar);
    }

    private static bool ResolveImplementSum(ISharpMeasuresVectorGroupMemberResolutionContext context, IRawVectorGroup vector)
    {
        return RecursivelySearchForDefined(vector, context.VectorPopulation, static (vector) => vector.ImplementSum)!.Value;
    }

    private static bool ResolveImplementDifference(ISharpMeasuresVectorGroupMemberResolutionContext context, IRawVectorGroup vector)
    {
        return RecursivelySearchForDefined(vector, context.VectorPopulation, static (vector) => vector.ImplementDifference)!.Value;
    }

    private static IRawVectorGroupType ResolveDifference(ISharpMeasuresVectorGroupMemberResolutionContext context, IRawVectorGroup vector)
    {
        return RecursivelySearchForDefinedVectorGroup(vector, context.VectorPopulation, static (vector) => vector.Difference)!;
    }

    private static IRawUnitInstance? ResolveDefaultUnit(ISharpMeasuresVectorGroupMemberResolutionContext context,
        IRawVectorGroup vector, IRawUnitType unit)
    {
        return RecursivelySearchForDefinedUnitInstance(vector, context.VectorPopulation, unit, static (vector) => vector.DefaultUnitName);
    }

    private static string? ResolveDefaultUnitSymbol(ISharpMeasuresVectorGroupMemberResolutionContext context,
        IRawVectorGroup vector)
    {
        return RecursivelySearchForDefined(vector, context.VectorPopulation, static (vector) => vector.DefaultUnitSymbol);
    }

    private static bool? ResolveGenerateDocumentation(ISharpMeasuresVectorGroupMemberResolutionContext context, IRawVectorGroup vector)
    {
        return RecursivelySearchForDefined(vector, context.VectorPopulation, static (vector) => vector.GenerateDocumentation);
    }

    private static T? RecursivelySearch<T>(IRawVectorGroup vector, IRawVectorPopulation vectorPopulation, Func<IRawVectorGroup, bool> predicate, Func<IRawVectorGroup, T?> transform)
    {
        if (predicate(vector))
        {
            return transform(vector);
        }

        if (vector is IRawVectorGroupSpecialization vectorSpecialization
            && vectorPopulation.VectorGroups.TryGetValue(vectorSpecialization.OriginalQuantity, out var originalVector))
        {
            return RecursivelySearch(originalVector.Definition, vectorPopulation, predicate, transform);
        }

        return default;
    }

    private static T? RecursivelySearchForDefined<T>(IRawVectorGroup vector, IRawVectorPopulation vectorPopulation, Func<IRawVectorGroup, T?> transform)
    {
        return RecursivelySearch<T>(vector, vectorPopulation, (vector) => transform(vector) is not null, transform);
    }

    private static T? RecursivelySearchForDefinedPopulationItem<T>(IRawVectorGroup vector, IRawVectorPopulation vectorPopulation,
        IReadOnlyDictionary<NamedType, T> population, Func<IRawVectorGroup, NamedType?> transform)
    {
        return RecursivelySearchForDefined(vector, vectorPopulation, transformWrapper);

        T? transformWrapper(IRawVectorGroup vector)
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

    private static T? RecursivelySearchForDefinedPopulationItem<T>(IRawVectorGroup vector, IRawVectorPopulation vectorPopulation,
        IReadOnlyDictionary<string, T> population, Func<IRawVectorGroup, string?> transform)
    {
        return RecursivelySearchForDefined(vector, vectorPopulation, transformWrapper);

        T? transformWrapper(IRawVectorGroup vector)
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

    private static IRawScalarType? RecursivelySearchForDefinedScalar(IRawVectorGroup vector, IRawScalarPopulation scalarPopulation,
        IRawVectorPopulation vectorPopulation, Func<IRawVectorGroup, NamedType?> transform)
    {
        return RecursivelySearchForDefinedPopulationItem(vector, vectorPopulation, scalarPopulation.Scalars, transform);
    }

    private static IRawVectorGroupType? RecursivelySearchForDefinedVectorGroup(IRawVectorGroup vector, IRawVectorPopulation vectorPopulation,
        Func<IRawVectorGroup, NamedType?> transform)
    {
        return RecursivelySearchForDefinedPopulationItem(vector, vectorPopulation, vectorPopulation.VectorGroups, transform);
    }

    private static IRawUnitInstance? RecursivelySearchForDefinedUnitInstance(IRawVectorGroup vector, IRawVectorPopulation vectorPopulation,
        IRawUnitType unit, Func<IRawVectorGroup, string?> transform)
    {
        return RecursivelySearchForDefinedPopulationItem(vector, vectorPopulation, unit.UnitsByName, transform);
    }
}
