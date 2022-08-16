namespace SharpMeasures.Generators.Vectors.Parsing.SpecializedSharpMeasuresVector;

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
using System.Linq;

internal interface ISpecializedSharpMeasuresVectorResolutionDiagnostics
{
    public abstract Diagnostic? TypeAlreadyUnit(ISpecializedSharpMeasuresVectorResolutionContext context, UnresolvedSpecializedSharpMeasuresVectorDefinition definition);
    public abstract Diagnostic? TypeAlreadyScalar(ISpecializedSharpMeasuresVectorResolutionContext context, UnresolvedSpecializedSharpMeasuresVectorDefinition definition);
    public abstract Diagnostic? TypeAlreadyVector(ISpecializedSharpMeasuresVectorResolutionContext context, UnresolvedSpecializedSharpMeasuresVectorDefinition definition);
    public abstract Diagnostic? OriginalNotVector(ISpecializedSharpMeasuresVectorResolutionContext context, UnresolvedSpecializedSharpMeasuresVectorDefinition definition);
    public abstract Diagnostic? RootVectorNotResolved(ISpecializedSharpMeasuresVectorResolutionContext context, UnresolvedSpecializedSharpMeasuresVectorDefinition definition);
    public abstract Diagnostic? VectorNameAndDimensionMismatch(ISpecializedSharpMeasuresVectorResolutionContext context, UnresolvedSpecializedSharpMeasuresVectorDefinition definition, int interpretedDimension, int inheritedDimension);
    public abstract Diagnostic? TypeNotScalar(ISpecializedSharpMeasuresVectorResolutionContext context, UnresolvedSpecializedSharpMeasuresVectorDefinition definition);
    public abstract Diagnostic? DifferenceNotVector(ISpecializedSharpMeasuresVectorResolutionContext context, UnresolvedSpecializedSharpMeasuresVectorDefinition definition);
    public abstract Diagnostic? DifferenceVectorGroupLacksMatchingDimension(ISpecializedSharpMeasuresVectorResolutionContext context, UnresolvedSpecializedSharpMeasuresVectorDefinition definition, int dimension);
    public abstract Diagnostic? UnrecognizedDefaultUnit(ISpecializedSharpMeasuresVectorResolutionContext context, UnresolvedSpecializedSharpMeasuresVectorDefinition definition, IRawUnitType unit);
}

internal interface ISpecializedSharpMeasuresVectorResolutionContext : IProcessingContext
{
    public abstract IRawUnitPopulation UnitPopulation { get; }
    public abstract IRawScalarPopulation ScalarPopulation { get; }
    public abstract IUnresolvedVectorPopulationWithData VectorPopulation { get; }
}

internal class SpecializedSharpMeasuresVectorResolver
    : IProcesser<ISpecializedSharpMeasuresVectorResolutionContext, UnresolvedSpecializedSharpMeasuresVectorDefinition, SpecializedSharpMeasuresVectorDefinition>
{
    private ISpecializedSharpMeasuresVectorResolutionDiagnostics Diagnostics { get; }

    public SpecializedSharpMeasuresVectorResolver(ISpecializedSharpMeasuresVectorResolutionDiagnostics diagnostics)
    {
        Diagnostics = diagnostics;
    }

    public IOptionalWithDiagnostics<SpecializedSharpMeasuresVectorDefinition> Process(ISpecializedSharpMeasuresVectorResolutionContext context, UnresolvedSpecializedSharpMeasuresVectorDefinition definition)
    {
        if (context.UnitPopulation.Units.ContainsKey(context.Type.AsNamedType()))
        {
            return OptionalWithDiagnostics.Empty<SpecializedSharpMeasuresVectorDefinition>(Diagnostics.TypeAlreadyUnit(context, definition));
        }

        if (context.ScalarPopulation.Scalars.ContainsKey(context.Type.AsNamedType()))
        {
            return OptionalWithDiagnostics.Empty<SpecializedSharpMeasuresVectorDefinition>(Diagnostics.TypeAlreadyScalar(context, definition));
        }

        if (context.VectorPopulation.DuplicatelyDefinedIndividualVectors.ContainsKey(context.Type.AsNamedType()))
        {
            return OptionalWithDiagnostics.Empty<SpecializedSharpMeasuresVectorDefinition>(Diagnostics.TypeAlreadyVector(context, definition));
        }

        if (context.VectorPopulation.IndividualVectors.TryGetValue(definition.OriginalVector, out var originalVector) is false)
        {
            return OptionalWithDiagnostics.Empty<SpecializedSharpMeasuresVectorDefinition>(Diagnostics.OriginalNotVector(context, definition));
        }

        if (context.VectorPopulation.IndividualVectorBases.TryGetValue(context.Type.AsNamedType(), out var vectorBase) is false)
        {
            return OptionalWithDiagnostics.Empty<SpecializedSharpMeasuresVectorDefinition>(Diagnostics.RootVectorNotResolved(context, definition));
        }

        if (context.UnitPopulation.Units.TryGetValue(vectorBase.Definition.Unit, out var unit) is false)
        {
            return OptionalWithDiagnostics.EmptyWithoutDiagnostics<SpecializedSharpMeasuresVectorDefinition>();
        }

        var dimensionValidity = CheckDimensionValidity(context, definition, vectorBase.Definition.Dimension);

        var processedVector = ProcessScalar(context, definition);
        var processedDifference = ProcessDifference(context, definition, vectorBase.Definition.Dimension);
        var processedDefaultUnitName = ProcessDefaultUnitName(context, definition, unit);

        var allDiagnostics = dimensionValidity.Diagnostics.Concat(processedVector.Diagnostics).Concat(processedDifference.Diagnostics).Concat(processedDefaultUnitName.Diagnostics);

        var resolvedScalar = ResolveScalar(context, definition);

        var resolvedImplementSum = ResolveImplementSum(context, definition);
        var resolvedImplementDifference = ResolveImplementDifference(context, definition);
        var resolvedDifference = ResolveDifference(context, definition);

        var resolvedDefaultUnit = ResolveDefaultUnit(context, definition, unit);
        var resolvedDefaultUnitSymbol = ResolveDefaultUnitSymbol(context, definition);

        var resolvedGenerateDocumentation = ResolveGenerateDocumentation(context, definition);

        SpecializedSharpMeasuresVectorDefinition product = new(originalVector, definition.InheritDerivations, definition.InheritConstants,
            definition.InheritConversions, definition.InheritUnits, unit, resolvedScalar, vectorBase.Definition.Dimension, resolvedImplementSum, resolvedImplementDifference,
            resolvedDifference, resolvedDefaultUnit, resolvedDefaultUnitSymbol, resolvedGenerateDocumentation, SharpMeasuresVectorLocations.Empty);

        return OptionalWithDiagnostics.Result(product, allDiagnostics);
    }

    private IValidityWithDiagnostics CheckDimensionValidity(ISpecializedSharpMeasuresVectorResolutionContext context, UnresolvedSpecializedSharpMeasuresVectorDefinition definition, int dimension)
    {
        if (Utility.InterpretDimensionFromName(context.Type.Name) is int result && result != dimension)
        {
            return ValidityWithDiagnostics.ValidWithDiagnostics(Diagnostics.VectorNameAndDimensionMismatch(context, definition, result, dimension));
        }

        return ValidityWithDiagnostics.Valid;
    }

    private IOptionalWithDiagnostics<IRawScalarType> ProcessScalar(ISpecializedSharpMeasuresVectorResolutionContext context, UnresolvedSpecializedSharpMeasuresVectorDefinition definition)
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

    private IOptionalWithDiagnostics<IRawVectorGroupType> ProcessDifference(ISpecializedSharpMeasuresVectorResolutionContext context, UnresolvedSpecializedSharpMeasuresVectorDefinition definition, int dimension)
    {
        if (definition.Difference is null)
        {
            return OptionalWithDiagnostics.EmptyWithoutDiagnostics<IRawVectorGroupType>();
        }

        if (context.VectorPopulation.IndividualVectors.TryGetValue(definition.Difference.Value, out var vector) is false)
        {
            if (context.VectorPopulation.VectorGroups.TryGetValue(definition.Difference.Value, out var vectorGroup) is false)
            {
                return OptionalWithDiagnostics.Empty<IRawVectorGroupType>(Diagnostics.DifferenceNotVector(context, definition));
            }

            if (context.VectorPopulation.VectorGroupMembersByGroup[vectorGroup.Type.AsNamedType()].VectorGroupMembersByDimension.ContainsKey(dimension) is false)
            {
                return OptionalWithDiagnostics.Empty<IRawVectorGroupType>(Diagnostics.DifferenceVectorGroupLacksMatchingDimension(context, definition, dimension));
            }

            return OptionalWithDiagnostics.Result(vectorGroup);
        }

        return OptionalWithDiagnostics.Result(vector as IRawVectorGroupType);
    }

    private IResultWithDiagnostics<IRawUnitInstance?> ProcessDefaultUnitName(ISpecializedSharpMeasuresVectorResolutionContext context, UnresolvedSpecializedSharpMeasuresVectorDefinition definition, IRawUnitType unit)
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

    private static IRawScalarType? ResolveScalar(ISpecializedSharpMeasuresVectorResolutionContext context, IRawVector vector)
    {
        return RecursivelySearchForDefinedScalar(vector, context.ScalarPopulation, context.VectorPopulation, static (vector) => vector.Scalar);
    }

    private static bool ResolveImplementSum(ISpecializedSharpMeasuresVectorResolutionContext context, IRawVector vector)
    {
        return RecursivelySearchForDefined(vector, context.VectorPopulation, static (vector) => vector.ImplementSum)!.Value;
    }

    private static bool ResolveImplementDifference(ISpecializedSharpMeasuresVectorResolutionContext context, IRawVector vector)
    {
        return RecursivelySearchForDefined(vector, context.VectorPopulation, static (vector) => vector.ImplementDifference)!.Value;
    }

    private static IRawVectorGroupType ResolveDifference(ISpecializedSharpMeasuresVectorResolutionContext context, IRawVector vector)
    {
        return RecursivelySearchForDefinedIndividualVector(vector, context.VectorPopulation, static (vector) => vector.Difference)!;
    }

    private static IRawUnitInstance? ResolveDefaultUnit(ISpecializedSharpMeasuresVectorResolutionContext context, IRawVector vector,
        IRawUnitType unit)
    {
        return RecursivelySearchForDefinedUnitInstance(vector, context.VectorPopulation, unit, static (vector) => vector.DefaultUnitName);
    }

    private static string? ResolveDefaultUnitSymbol(ISpecializedSharpMeasuresVectorResolutionContext context, IRawVector vector)
    {
        return RecursivelySearchForDefined(vector, context.VectorPopulation, static (vector) => vector.DefaultUnitSymbol);
    }

    private static bool? ResolveGenerateDocumentation(ISpecializedSharpMeasuresVectorResolutionContext context, IRawVector vector)
    {
        return RecursivelySearchForDefined(vector, context.VectorPopulation, static (vector) => vector.GenerateDocumentation);
    }

    private static T? RecursivelySearch<T>(IRawVector vector, IRawVectorPopulation vectorPopulation, Func<IRawVector, bool> predicate, Func<IRawVector, T?> transform)
    {
        if (predicate(vector))
        {
            return transform(vector);
        }

        if (vector is IRawVectorSpecialization vectorSpecialization
            && vectorPopulation.IndividualVectors.TryGetValue(vectorSpecialization.OriginalQuantity, out var originalVector))
        {
            return RecursivelySearch(originalVector.Definition, vectorPopulation, predicate, transform);
        }

        return default;
    }

    private static T? RecursivelySearchForDefined<T>(IRawVector vector, IRawVectorPopulation vectorPopulation, Func<IRawVector, T?> transform)
    {
        return RecursivelySearch<T>(vector, vectorPopulation, (vector) => transform(vector) is not null, transform);
    }

    private static T? RecursivelySearchForDefinedPopulationItem<T>(IRawVector vector, IRawVectorPopulation vectorPopulation, IReadOnlyDictionary<NamedType, T> population, Func<IRawVector, NamedType?> transform)
    {
        return RecursivelySearchForDefined(vector, vectorPopulation, transformWrapper);

        T? transformWrapper(IRawVector vector)
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

    private static T? RecursivelySearchForDefinedPopulationItem<T>(IRawVector vector, IRawVectorPopulation vectorPopulation, IReadOnlyDictionary<string, T> population, Func<IRawVector, string?> transform)
    {
        return RecursivelySearchForDefined(vector, vectorPopulation, transformWrapper);

        T? transformWrapper(IRawVector vector)
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

    private static IRawScalarType? RecursivelySearchForDefinedScalar(IRawVector vector, IRawScalarPopulation scalarPopulation, IRawVectorPopulation vectorPopulation, Func<IRawVector, NamedType?> transform)
    {
        return RecursivelySearchForDefinedPopulationItem(vector, vectorPopulation, scalarPopulation.Scalars, transform);
    }

    private static IRawVectorGroupType? RecursivelySearchForDefinedIndividualVector(IRawVector vector, IRawVectorPopulation vectorPopulation, Func<IRawVector, NamedType?> transform)
    {
        return RecursivelySearchForDefinedPopulationItem(vector, vectorPopulation, vectorPopulation.IndividualVectors, transform);
    }

    private static IRawUnitInstance? RecursivelySearchForDefinedUnitInstance(IRawVector vector, IRawVectorPopulation vectorPopulation, IRawUnitType unit, Func<IRawVector, string?> transform)
    {
        return RecursivelySearchForDefinedPopulationItem(vector, vectorPopulation, unit.UnitsByName, transform);
    }
}
