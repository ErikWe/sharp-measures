namespace SharpMeasures.Generators.Vectors.Parsing.SpecializedSharpMeasuresVectorGroup;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Raw.Scalars;
using SharpMeasures.Generators.Raw.Units;
using SharpMeasures.Generators.Raw.Units.UnitInstances;
using SharpMeasures.Generators.Raw.Vectors;
using SharpMeasures.Generators.Raw.Vectors.Groups;
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
    public abstract Diagnostic? RootVectorGroupNotResolved(ISpecializedSharpMeasuresVectorGroupResolutionContext context, UnresolvedSpecializedSharpMeasuresVectorGroupDefinition definition);
    public abstract Diagnostic? TypeNotScalar(ISpecializedSharpMeasuresVectorGroupResolutionContext context, UnresolvedSpecializedSharpMeasuresVectorGroupDefinition definition);
    public abstract Diagnostic? DifferenceNotVectorGroup(ISpecializedSharpMeasuresVectorGroupResolutionContext context, UnresolvedSpecializedSharpMeasuresVectorGroupDefinition definition);
    public abstract Diagnostic? UnrecognizedDefaultUnit(ISpecializedSharpMeasuresVectorGroupResolutionContext context, UnresolvedSpecializedSharpMeasuresVectorGroupDefinition definition, IRawUnitType unit);
}

internal interface ISpecializedSharpMeasuresVectorGroupResolutionContext : IProcessingContext
{
    public abstract IRawUnitPopulation UnitPopulation { get; }
    public abstract IRawScalarPopulation ScalarPopulation { get; }
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

        if (context.VectorPopulation.DuplicatelyDefinedVectorGroups.ContainsKey(context.Type.AsNamedType()))
        {
            return OptionalWithDiagnostics.Empty<SpecializedSharpMeasuresVectorGroupDefinition>(Diagnostics.TypeAlreadyVector(context, definition));
        }

        if (context.VectorPopulation.VectorGroups.TryGetValue(definition.OriginalVectorGroup, out var originalVectorGroup) is false
            || context.VectorPopulation.IndividualVectors.ContainsKey(definition.OriginalVectorGroup))
        {
            return OptionalWithDiagnostics.Empty<SpecializedSharpMeasuresVectorGroupDefinition>(Diagnostics.OriginalNotVectorGroup(context, definition));
        }

        if (context.VectorPopulation.VectorGroupBases.TryGetValue(context.Type.AsNamedType(), out var vectorGroupBase) is false)
        {
            return OptionalWithDiagnostics.Empty<SpecializedSharpMeasuresVectorGroupDefinition>(Diagnostics.RootVectorGroupNotResolved(context, definition));
        }

        if (context.UnitPopulation.Units.TryGetValue(vectorGroupBase.Definition.Unit, out var unit) is false)
        {
            return OptionalWithDiagnostics.EmptyWithoutDiagnostics<SpecializedSharpMeasuresVectorGroupDefinition>();
        }

        var processedScalar = ProcessScalar(context, definition);
        var processedDifference = ProcessDifference(context, definition);
        var processedDefaultUnitName = ProcessDefaultUnitName(context, definition, unit);

        var allDiagnostics = processedScalar.Diagnostics.Concat(processedDifference.Diagnostics).Concat(processedDefaultUnitName.Diagnostics);

        var resolvedScalar = ResolveScalar(context, definition);

        var resolvedImplementSum = ResolveImplementSum(context, definition);
        var resolvedImplementDifference = ResolveImplementDifference(context, definition);
        var resolvedDifference = ResolveDifference(context, definition);

        var resolvedDefaultUnit = ResolveDefaultUnit(context, definition, unit);
        var resolvedDefaultUnitSymbol = ResolveDefaultUnitSymbol(context, definition);

        var resolvedGenerateDocumentation = ResolveGenerateDocumentation(context, definition);

        SpecializedSharpMeasuresVectorGroupDefinition product = new(originalVectorGroup, definition.InheritDerivations, definition.InheritConstants,
            definition.InheritConversions, definition.InheritUnits, unit, resolvedScalar, resolvedImplementSum, resolvedImplementDifference,
            resolvedDifference, resolvedDefaultUnit, resolvedDefaultUnitSymbol, resolvedGenerateDocumentation, SharpMeasuresVectorGroupLocations.Empty);

        return OptionalWithDiagnostics.Result(product, allDiagnostics);
    }

    private IOptionalWithDiagnostics<IRawScalarType> ProcessScalar(ISpecializedSharpMeasuresVectorGroupResolutionContext context,
        UnresolvedSpecializedSharpMeasuresVectorGroupDefinition definition)
    {
        if (definition.Scalar is null)
        {
            return OptionalWithDiagnostics.EmptyWithoutDiagnostics<IRawScalarType>();
        }

        if (context.ScalarPopulation.Scalars.TryGetValue(definition.Scalar.Value, out var scalar) is false)
        {
            return OptionalWithDiagnostics.Empty<IRawScalarType>(Diagnostics.TypeNotScalar(context, definition));
        }

        return OptionalWithDiagnostics.Result(scalar);
    }

    private IOptionalWithDiagnostics<IRawVectorGroupType> ProcessDifference(ISpecializedSharpMeasuresVectorGroupResolutionContext context,
        UnresolvedSpecializedSharpMeasuresVectorGroupDefinition definition)
    {
        if (definition.Difference is null)
        {
            return OptionalWithDiagnostics.EmptyWithoutDiagnostics<IRawVectorGroupType>();
        }

        if (context.VectorPopulation.VectorGroups.TryGetValue(definition.Difference.Value, out var vectorGroup) is false || context.VectorPopulation.IndividualVectors.ContainsKey(definition.Difference.Value))
        {
            return OptionalWithDiagnostics.Empty<IRawVectorGroupType>(Diagnostics.DifferenceNotVectorGroup(context, definition));
        }

        return OptionalWithDiagnostics.Result(vectorGroup);
    }

    private IResultWithDiagnostics<IRawUnitInstance?> ProcessDefaultUnitName(ISpecializedSharpMeasuresVectorGroupResolutionContext context,
        UnresolvedSpecializedSharpMeasuresVectorGroupDefinition definition, IRawUnitType unit)
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

    private static IRawScalarType? ResolveScalar(ISpecializedSharpMeasuresVectorGroupResolutionContext context,
        IRawVectorGroup vector)
    {
        return RecursivelySearchForDefinedScalar(vector, context.ScalarPopulation, context.VectorPopulation, static (vector) => vector.Scalar);
    }

    private static bool ResolveImplementSum(ISpecializedSharpMeasuresVectorGroupResolutionContext context, IRawVectorGroup vector)
    {
        return RecursivelySearchForDefined(vector, context.VectorPopulation, static (vector) => vector.ImplementSum)!.Value;
    }

    private static bool ResolveImplementDifference(ISpecializedSharpMeasuresVectorGroupResolutionContext context, IRawVectorGroup vector)
    {
        return RecursivelySearchForDefined(vector, context.VectorPopulation, static (vector) => vector.ImplementDifference)!.Value;
    }

    private static IRawVectorGroupType ResolveDifference(ISpecializedSharpMeasuresVectorGroupResolutionContext context, IRawVectorGroup vector)
    {
        return RecursivelySearchForDefinedVectorGroup(vector, context.VectorPopulation, static (vector) => vector.Difference)!;
    }

    private static IRawUnitInstance? ResolveDefaultUnit(ISpecializedSharpMeasuresVectorGroupResolutionContext context,
        IRawVectorGroup vector, IRawUnitType unit)
    {
        return RecursivelySearchForDefinedUnitInstance(vector, context.VectorPopulation, unit, static (vector) => vector.DefaultUnitName);
    }

    private static string? ResolveDefaultUnitSymbol(ISpecializedSharpMeasuresVectorGroupResolutionContext context,
        IRawVectorGroup vectorr)
    {
        return RecursivelySearchForDefined(vectorr, context.VectorPopulation, static (vector) => vector.DefaultUnitSymbol);
    }

    private static bool? ResolveGenerateDocumentation(ISpecializedSharpMeasuresVectorGroupResolutionContext context, IRawVectorGroup vector)
    {
        return RecursivelySearchForDefined(vector, context.VectorPopulation, static (vector) => vector.GenerateDocumentation);
    }

    private static T? RecursivelySearch<T>(IRawVectorGroup vector, IRawVectorPopulation vectorPopulation, Func<IRawVectorGroup, bool> predicate,
        Func<IRawVectorGroup, T?> transform)
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
