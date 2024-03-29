﻿namespace SharpMeasures.Generators.Vectors.Parsing.SpecializedSharpMeasuresVector;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Quantities.Parsing.DefaultUnitInstance;
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Units;

using System.Linq;

internal interface ISpecializedSharpMeasuresVectorValidationDiagnostics : IDefaultUnitInstanceValidationDiagnostics
{
    public abstract Diagnostic? TypeAlreadyUnit(ISpecializedSharpMeasuresVectorValidationContext context, SpecializedSharpMeasuresVectorDefinition definition);
    public abstract Diagnostic? TypeAlreadyScalar(ISpecializedSharpMeasuresVectorValidationContext context, SpecializedSharpMeasuresVectorDefinition definition);
    public abstract Diagnostic? TypeAlreadyVectorBase(ISpecializedSharpMeasuresVectorValidationContext context, SpecializedSharpMeasuresVectorDefinition definition);
    public abstract Diagnostic? OriginalNotVector(ISpecializedSharpMeasuresVectorValidationContext context, SpecializedSharpMeasuresVectorDefinition definition);
    public abstract Diagnostic? RootVectorNotResolved(ISpecializedSharpMeasuresVectorValidationContext context, SpecializedSharpMeasuresVectorDefinition definition);
    public abstract Diagnostic? VectorNameAndDimensionConflict(ISpecializedSharpMeasuresVectorValidationContext context, SpecializedSharpMeasuresVectorDefinition definition, int interpretedDimension, int inheritedDimension);
    public abstract Diagnostic? TypeNotScalar(ISpecializedSharpMeasuresVectorValidationContext context, SpecializedSharpMeasuresVectorDefinition definition);
    public abstract Diagnostic? DifferenceNotVector(ISpecializedSharpMeasuresVectorValidationContext context, SpecializedSharpMeasuresVectorDefinition definition);
    public abstract Diagnostic? DifferenceVectorInvalidDimension(ISpecializedSharpMeasuresVectorValidationContext context, SpecializedSharpMeasuresVectorDefinition definition, int expectedDimension, int actualDimension);
    public abstract Diagnostic? DifferenceVectorGroupLacksMatchingDimension(ISpecializedSharpMeasuresVectorValidationContext context, SpecializedSharpMeasuresVectorDefinition definition, int dimension);
}

internal interface ISpecializedSharpMeasuresVectorValidationContext : IProcessingContext
{
    public abstract VectorProcessingData ProcessingData { get; }

    public abstract IUnitPopulation UnitPopulation { get; }
    public abstract IScalarPopulation ScalarPopulation { get; }
    public abstract IVectorPopulation VectorPopulation { get; }
}

internal sealed class SpecializedSharpMeasuresVectorValidator : IProcesser<ISpecializedSharpMeasuresVectorValidationContext, SpecializedSharpMeasuresVectorDefinition, SpecializedSharpMeasuresVectorDefinition>
{
    private ISpecializedSharpMeasuresVectorValidationDiagnostics Diagnostics { get; }

    public SpecializedSharpMeasuresVectorValidator(ISpecializedSharpMeasuresVectorValidationDiagnostics diagnostics)
    {
        Diagnostics = diagnostics;
    }

    public IOptionalWithDiagnostics<SpecializedSharpMeasuresVectorDefinition> Process(ISpecializedSharpMeasuresVectorValidationContext context, SpecializedSharpMeasuresVectorDefinition definition)
    {
        var validity = ValidateTypeNotAlreadyUnit(context, definition)
            .Validate(() => ValidateTypeNotAlreadyScalar(context, definition))
            .Validate(() => ValidateTypeNotAlreadyVectorBase(context, definition))
            .Validate(() => ValidateTypeNotDuplicatelyDefined(context))
            .Validate(() => ValidateOriginalVectorIsVector(context, definition))
            .Validate(() => ValidateVectorRootResolved(context, definition))
            .Validate(() => ValidateNameAndDimensionNotConflicts(context, definition))
            .Validate(() => ValidateUnitIsUnit(context));

        if (validity.IsInvalid)
        {
            return validity.AsEmptyOptional<SpecializedSharpMeasuresVectorDefinition>();
        }

        var scalar = ValidateScalarIsScalar(context, definition).Transform(definition.Scalar);
        var difference = ValidateDifference(context, definition);

        var unit = context.VectorPopulation.VectorBases[context.Type.AsNamedType()].Definition.Unit;
        var defaultUnitInstanceValidity = DefaultUnitInstanceValidator.Validate(context, Diagnostics, definition, context.UnitPopulation, unit);

        var defaultUnitInstanceName = defaultUnitInstanceValidity.Transform(definition.DefaultUnitInstanceName);
        var defaultUnitInstanceSymbol = defaultUnitInstanceValidity.Transform(definition.DefaultUnitInstanceSymbol);

        var product = ProduceResult(definition, scalar.NullableValueResult(), difference.NullableValueResult(), defaultUnitInstanceName.NullableReferenceResult(), defaultUnitInstanceSymbol.NullableReferenceResult());

        var allDiagnostics = validity.Diagnostics.Concat(scalar).Concat(difference).Concat(defaultUnitInstanceValidity);

        return OptionalWithDiagnostics.Result(product, allDiagnostics);
    }

    private static SpecializedSharpMeasuresVectorDefinition ProduceResult(SpecializedSharpMeasuresVectorDefinition definition, NamedType? scalar, NamedType? difference, string? defaultUnitInstanceName, string? defaultUnitInstanceSymbol)
    {
        return new(definition.OriginalQuantity, definition.InheritOperations, definition.InheritProcesses, definition.InheritConstants, definition.InheritConversions, definition.InheritUnits, definition.ForwardsCastOperatorBehaviour, definition.BackwardsCastOperatorBehaviour,
            scalar, definition.ImplementSum, definition.ImplementDifference, difference, defaultUnitInstanceName, defaultUnitInstanceSymbol, definition.Locations);
    }

    private IValidityWithDiagnostics ValidateTypeNotAlreadyUnit(ISpecializedSharpMeasuresVectorValidationContext context, SpecializedSharpMeasuresVectorDefinition definition)
    {
        var typeAlreadyUnit = context.UnitPopulation.Units.ContainsKey(context.Type.AsNamedType());

        return ValidityWithDiagnostics.Conditional(typeAlreadyUnit is false, () => Diagnostics.TypeAlreadyUnit(context, definition));
    }

    private IValidityWithDiagnostics ValidateTypeNotAlreadyScalar(ISpecializedSharpMeasuresVectorValidationContext context, SpecializedSharpMeasuresVectorDefinition definition)
    {
        var typeAlreadyScalar = context.ScalarPopulation.Scalars.ContainsKey(context.Type.AsNamedType());

        return ValidityWithDiagnostics.Conditional(typeAlreadyScalar is false, () => Diagnostics.TypeAlreadyScalar(context, definition));
    }

    private IValidityWithDiagnostics ValidateTypeNotAlreadyVectorBase(ISpecializedSharpMeasuresVectorValidationContext context, SpecializedSharpMeasuresVectorDefinition definition)
    {
        var typeAlreadyVectorBase = context.ProcessingData.VectorSpecializationsAlreadyDefinedAsVectorBases.ContainsKey(context.Type.AsNamedType());

        return ValidityWithDiagnostics.Conditional(typeAlreadyVectorBase is false, () => Diagnostics.TypeAlreadyVectorBase(context, definition));
    }

    private static IValidityWithDiagnostics ValidateTypeNotDuplicatelyDefined(ISpecializedSharpMeasuresVectorValidationContext context)
    {
        var typeDuplicatelyDefined = context.ProcessingData.DuplicatelyDefinedVectorSpecializations.ContainsKey(context.Type.AsNamedType());

        return ValidityWithDiagnostics.ConditionalWithoutDiagnostics(typeDuplicatelyDefined is false);
    }

    private IValidityWithDiagnostics ValidateOriginalVectorIsVector(ISpecializedSharpMeasuresVectorValidationContext context, SpecializedSharpMeasuresVectorDefinition definition)
    {
        var originalVectorIsVector = context.VectorPopulation.Vectors.ContainsKey(definition.OriginalQuantity);

        return ValidityWithDiagnostics.Conditional(originalVectorIsVector, () => Diagnostics.OriginalNotVector(context, definition));
    }

    private IValidityWithDiagnostics ValidateVectorRootResolved(ISpecializedSharpMeasuresVectorValidationContext context, SpecializedSharpMeasuresVectorDefinition definition)
    {
        var correctlyResolvedVectorBase = context.VectorPopulation.VectorBases.ContainsKey(context.Type.AsNamedType());

        return ValidityWithDiagnostics.Conditional(correctlyResolvedVectorBase, () => Diagnostics.RootVectorNotResolved(context, definition));
    }

    private IValidityWithDiagnostics ValidateNameAndDimensionNotConflicts(ISpecializedSharpMeasuresVectorValidationContext context, SpecializedSharpMeasuresVectorDefinition definition)
    {
        var interpretedDimension = DimensionParsingUtility.InterpretDimensionFromName(context.Type.Name);
        var actualDimension = context.VectorPopulation.VectorBases[context.Type.AsNamedType()].Definition.Dimension;

        return ValidityWithDiagnostics.Conditional(actualDimension == interpretedDimension, () => Diagnostics.VectorNameAndDimensionConflict(context, definition, interpretedDimension!.Value, actualDimension));
    }

    private static IValidityWithDiagnostics ValidateUnitIsUnit(ISpecializedSharpMeasuresVectorValidationContext context)
    {
        var unitIsUnit = context.UnitPopulation.Units.ContainsKey(context.VectorPopulation.VectorBases[context.Type.AsNamedType()].Definition.Unit);

        return ValidityWithDiagnostics.ConditionalWithoutDiagnostics(unitIsUnit);
    }

    private IValidityWithDiagnostics ValidateScalarIsScalar(ISpecializedSharpMeasuresVectorValidationContext context, SpecializedSharpMeasuresVectorDefinition definition)
    {
        if (definition.Scalar is not null && definition.Scalar.Value == NamedType.Empty)
        {
            return ValidityWithDiagnostics.InvalidWithoutDiagnostics;
        }

        var scalarIsNotScalar = definition.Scalar is not null && context.ScalarPopulation.Scalars.ContainsKey(definition.Scalar.Value) is false;

        return ValidityWithDiagnostics.Conditional(scalarIsNotScalar is false, () => Diagnostics.TypeNotScalar(context, definition));
    }

    private IOptionalWithDiagnostics<NamedType> ValidateDifference(ISpecializedSharpMeasuresVectorValidationContext context, SpecializedSharpMeasuresVectorDefinition definition)
    {
        if (definition.Difference is null)
        {
            return OptionalWithDiagnostics.Empty<NamedType>();
        }

        var dimension = context.VectorPopulation.VectorBases[context.Type.AsNamedType()].Definition.Dimension;

        if (context.VectorPopulation.VectorBases.TryGetValue(definition.Difference.Value, out var vector))
        {
            return OptionalWithDiagnostics.Conditional(vector.Definition.Dimension == dimension, definition.Difference.Value, () => Diagnostics.DifferenceVectorInvalidDimension(context, definition, dimension, vector.Definition.Dimension));
        }

        if (context.VectorPopulation.GroupBases.TryGetValue(definition.Difference.Value, out var group))
        {
            var groupHasMemberOfMatchingDimension = context.VectorPopulation.GroupMembersByGroup[definition.Difference.Value].GroupMembersByDimension.TryGetValue(dimension, out var correspondingMember);

            if (groupHasMemberOfMatchingDimension)
            {
                return OptionalWithDiagnostics.Result(correspondingMember.Type.AsNamedType());
            }

            return OptionalWithDiagnostics.Empty<NamedType>(Diagnostics.DifferenceVectorGroupLacksMatchingDimension(context, definition, dimension));
        }

        if (context.VectorPopulation.GroupMembers.TryGetValue(definition.Difference.Value, out var groupMember))
        {
            return OptionalWithDiagnostics.Conditional(groupMember.Definition.Dimension == dimension, definition.Difference.Value, () => Diagnostics.DifferenceVectorInvalidDimension(context, definition, dimension, groupMember.Definition.Dimension));
        }

        if (definition.Difference == NamedType.Empty)
        {
            return OptionalWithDiagnostics.Empty<NamedType>();
        }

        return OptionalWithDiagnostics.Empty<NamedType>(Diagnostics.DifferenceNotVector(context, definition));
    }
}
