namespace SharpMeasures.Generators.Vectors.Parsing.SpecializedSharpMeasuresVector;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Quantities.Parsing.DefaultUnit;
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Units;
using SharpMeasures.Generators.Vectors.Parsing.Abstraction;

using System.Linq;

internal interface ISpecializedSharpMeasuresVectorValidationDiagnostics : IDefaultUnitValidationDiagnostics
{
    public abstract Diagnostic? TypeAlreadyUnit(ISpecializedSharpMeasuresVectorValidationContext context, SpecializedSharpMeasuresVectorDefinition definition);
    public abstract Diagnostic? TypeAlreadyScalar(ISpecializedSharpMeasuresVectorValidationContext context, SpecializedSharpMeasuresVectorDefinition definition);
    public abstract Diagnostic? TypeAlreadyVector(ISpecializedSharpMeasuresVectorValidationContext context, SpecializedSharpMeasuresVectorDefinition definition);
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
    public abstract IUnitPopulation UnitPopulation { get; }
    public abstract IScalarPopulation ScalarPopulation { get; }
    public abstract IVectorPopulationWithData VectorPopulation { get; }
}

internal class SpecializedSharpMeasuresVectorValidator : IProcesser<ISpecializedSharpMeasuresVectorValidationContext, SpecializedSharpMeasuresVectorDefinition, SpecializedSharpMeasuresVectorDefinition>
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
            .Validate(() => ValidateTypeNotAlreadyVector(context, definition))
            .Validate(() => ValidateOriginalVectorIsVector(context, definition))
            .Validate(() => ValidateVectorRootResolved(context, definition))
            .Validate(() => ValidateNameAndDimensionNotConflicts(context, definition))
            .Validate(() => ValidateUnitIsUnit(context));

        if (validity.IsInvalid)
        {
            return validity.AsEmptyOptional<SpecializedSharpMeasuresVectorDefinition>();
        }

        var scalar = ValidateScalarIsScalar(context, definition).Transform(definition.Scalar);
        var difference = ValidateDifference(context, definition).Transform(definition.Difference);

        var unit = context.VectorPopulation.VectorBases[context.Type.AsNamedType()].Definition.Unit;
        var defaultUnitValidity = DefaultUnitValidator.Validate(context, Diagnostics, definition, context.UnitPopulation, unit);

        var defaultUnitName = defaultUnitValidity.Transform(definition.DefaultUnitName);
        var defaultUnitSymbol = defaultUnitValidity.Transform(definition.DefaultUnitSymbol);

        var product = ProduceResult(definition, scalar.NullableResult, difference.NullableResult, defaultUnitName.NullableResult, defaultUnitSymbol.NullableResult);

        var allDiagnostics = validity.Diagnostics.Concat(scalar).Concat(difference).Concat(defaultUnitValidity);

        return OptionalWithDiagnostics.Result(product, allDiagnostics);
    }

    private static SpecializedSharpMeasuresVectorDefinition ProduceResult(SpecializedSharpMeasuresVectorDefinition definition, NamedType? scalar, NamedType? difference, string? defaultUnitName, string? defaultUnitSymbol)
    {
        return new(definition.OriginalVector, definition.InheritDerivations, definition.InheritConstants, definition.InheritConversions, definition.InheritUnits, scalar, definition.ImplementSum,
            definition.ImplementDifference, difference, defaultUnitName, defaultUnitSymbol, definition.GenerateDocumentation, definition.Locations);
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

    private IValidityWithDiagnostics ValidateTypeNotAlreadyVector(ISpecializedSharpMeasuresVectorValidationContext context, SpecializedSharpMeasuresVectorDefinition definition)
    {
        var typeAlreadyVector = context.VectorPopulation.DuplicatelyDefinedVectors.ContainsKey(context.Type.AsNamedType());

        return ValidityWithDiagnostics.Conditional(typeAlreadyVector is false, () => Diagnostics.TypeAlreadyVector(context, definition));
    }

    private IValidityWithDiagnostics ValidateOriginalVectorIsVector(ISpecializedSharpMeasuresVectorValidationContext context, SpecializedSharpMeasuresVectorDefinition definition)
    {
        var originalVectorIsVector = context.VectorPopulation.Vectors.ContainsKey(definition.OriginalVector);

        return ValidityWithDiagnostics.Conditional(originalVectorIsVector, () => Diagnostics.OriginalNotVector(context, definition));
    }

    private IValidityWithDiagnostics ValidateVectorRootResolved(ISpecializedSharpMeasuresVectorValidationContext context, SpecializedSharpMeasuresVectorDefinition definition)
    {
        var correctlyResolvedVectorBase = context.VectorPopulation.VectorBases.ContainsKey(context.Type.AsNamedType());

        return ValidityWithDiagnostics.Conditional(correctlyResolvedVectorBase, () => Diagnostics.RootVectorNotResolved(context, definition));
    }

    private IValidityWithDiagnostics ValidateNameAndDimensionNotConflicts(ISpecializedSharpMeasuresVectorValidationContext context, SpecializedSharpMeasuresVectorDefinition definition)
    {
        int? interpretedDimension = DimensionParsingUtility.InterpretDimensionFromName(context.Type.Name);
        int actualDimension = context.VectorPopulation.VectorBases[context.Type.AsNamedType()].Definition.Dimension;

        return ValidityWithDiagnostics.Conditional(actualDimension == interpretedDimension, () => Diagnostics.VectorNameAndDimensionConflict(context, definition, interpretedDimension!.Value, actualDimension));
    }

    private static IValidityWithDiagnostics ValidateUnitIsUnit(ISpecializedSharpMeasuresVectorValidationContext context)
    {
        var unitIsUnit = context.UnitPopulation.Units.ContainsKey(context.VectorPopulation.VectorBases[context.Type.AsNamedType()].Definition.Unit);

        return ValidityWithDiagnostics.ConditionalWithoutDiagnostics(unitIsUnit);
    }

    private IValidityWithDiagnostics ValidateScalarIsScalar(ISpecializedSharpMeasuresVectorValidationContext context, SpecializedSharpMeasuresVectorDefinition definition)
    {
        var scalarIsNotScalar = definition.Scalar is not null && context.ScalarPopulation.Scalars.ContainsKey(definition.Scalar.Value) is false;

        return ValidityWithDiagnostics.ValidWithConditionalDiagnostics(scalarIsNotScalar, () => Diagnostics.TypeNotScalar(context, definition));
    }

    private IValidityWithDiagnostics ValidateDifference(ISpecializedSharpMeasuresVectorValidationContext context, SpecializedSharpMeasuresVectorDefinition definition)
    {
        if (definition.Difference is null)
        {
            return ValidityWithDiagnostics.Valid;
        }

        int dimension = context.VectorPopulation.VectorBases[context.Type.AsNamedType()].Definition.Dimension;

        if (context.VectorPopulation.VectorBases.TryGetValue(definition.Difference.Value, out var vector))
        {
            return ValidityWithDiagnostics.Conditional(vector.Definition.Dimension == dimension, () => Diagnostics.DifferenceVectorInvalidDimension(context, definition, dimension, vector.Definition.Dimension));
        }

        if (context.VectorPopulation.GroupBases.TryGetValue(definition.Difference.Value, out var group))
        {
            var groupHasMemberOfMatchingDimension = context.VectorPopulation.GroupMembersByGroup[definition.Difference.Value].GroupMembersByDimension.ContainsKey(dimension);

            return ValidityWithDiagnostics.Conditional(groupHasMemberOfMatchingDimension, () => Diagnostics.DifferenceVectorGroupLacksMatchingDimension(context, definition, dimension));
        }

        if (context.VectorPopulation.GroupMembers.TryGetValue(definition.Difference.Value, out var groupMember))
        {
            return ValidityWithDiagnostics.Conditional(groupMember.Definition.Dimension == dimension, () => Diagnostics.DifferenceVectorInvalidDimension(context, definition, dimension, groupMember.Definition.Dimension));
        }

        return ValidityWithDiagnostics.ValidWithDiagnostics(Diagnostics.DifferenceNotVector(context, definition));
    }
}
