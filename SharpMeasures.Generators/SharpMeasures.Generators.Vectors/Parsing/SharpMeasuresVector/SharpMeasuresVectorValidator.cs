namespace SharpMeasures.Generators.Vectors.Parsing.SharpMeasuresVector;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Quantities.Parsing.DefaultUnit;
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Units;
using SharpMeasures.Generators.Vectors.Parsing.Abstraction;

using System.Linq;

internal interface ISharpMeasuresVectorValidationDiagnostics : IDefaultUnitValidationDiagnostics
{
    public abstract Diagnostic? TypeAlreadyUnit(ISharpMeasuresVectorValidationContext context, SharpMeasuresVectorDefinition definition);
    public abstract Diagnostic? TypeAlreadyScalar(ISharpMeasuresVectorValidationContext context, SharpMeasuresVectorDefinition definition);
    public abstract Diagnostic? TypeAlreadyVector(ISharpMeasuresVectorValidationContext context, SharpMeasuresVectorDefinition definition);
    public abstract Diagnostic? TypeNotUnit(ISharpMeasuresVectorValidationContext context, SharpMeasuresVectorDefinition definition);
    public abstract Diagnostic? TypeNotScalar(ISharpMeasuresVectorValidationContext context, SharpMeasuresVectorDefinition definition);
    public abstract Diagnostic? DifferenceNotVector(ISharpMeasuresVectorValidationContext context, SharpMeasuresVectorDefinition definition);
    public abstract Diagnostic? DifferenceVectorGroupLacksMatchingDimension(ISharpMeasuresVectorValidationContext context, SharpMeasuresVectorDefinition definition);
}

internal interface ISharpMeasuresVectorValidationContext : IProcessingContext
{
    public abstract IUnitPopulation UnitPopulation { get; }
    public abstract IScalarPopulation ScalarPopulation { get; }
    public abstract IVectorPopulationWithData VectorPopulation { get; }
}

internal class SharpMeasuresVectorValidator : IProcesser<ISharpMeasuresVectorValidationContext, SharpMeasuresVectorDefinition, SharpMeasuresVectorDefinition>
{
    private ISharpMeasuresVectorValidationDiagnostics Diagnostics { get; }

    public SharpMeasuresVectorValidator(ISharpMeasuresVectorValidationDiagnostics diagnostics)
    {
        Diagnostics = diagnostics;
    }

    public IOptionalWithDiagnostics<SharpMeasuresVectorDefinition> Process(ISharpMeasuresVectorValidationContext context, SharpMeasuresVectorDefinition definition)
    {
        var validity = ValidateTypeNotAlreadyUnit(context, definition)
            .Validate(() => ValidateTypeNotAlreadyScalar(context, definition))
            .Validate(() => ValidateTypeNotAlreadyVector(context, definition))
            .Merge(() => ResolveUnit(context, definition))
            .Reduce();

        if (validity.IsInvalid)
        {
            return validity.AsEmptyOptional<SharpMeasuresVectorDefinition>();
        }

        var scalar = ValidateScalarIsScalar(context, definition).Transform(definition.Scalar);
        var difference = ValidateDifference(context, definition).Transform(definition.Difference);

        var defaultUnitValidity = DefaultUnitValidator.Validate(context, Diagnostics, definition, context.UnitPopulation, definition.Unit);

        var defaultUnitName = defaultUnitValidity.Transform(definition.DefaultUnitName);
        var defaultUnitSymbol = defaultUnitValidity.Transform(definition.DefaultUnitSymbol);

        var product = ProduceResult(definition, scalar.NullableResult, difference.NullableResult, defaultUnitName.NullableResult, defaultUnitSymbol.NullableResult);

        var allDiagnostics = validity.Diagnostics.Concat(scalar).Concat(difference).Concat(defaultUnitValidity);

        return OptionalWithDiagnostics.Result(product, allDiagnostics);
    }

    private static SharpMeasuresVectorDefinition ProduceResult(SharpMeasuresVectorDefinition definition, NamedType? scalar, NamedType? difference, string? defaultUnitName, string? defaultUnitSymbol)
    {
        return new(definition.Unit, scalar, definition.Dimension, definition.ImplementSum, definition.ImplementDifference, difference, defaultUnitName, defaultUnitSymbol, definition.GenerateDocumentation, definition.Locations);
    }

    private IValidityWithDiagnostics ValidateTypeNotAlreadyUnit(ISharpMeasuresVectorValidationContext context, SharpMeasuresVectorDefinition definition)
    {
        var typeAlreadyUnit = context.UnitPopulation.Units.ContainsKey(context.Type.AsNamedType());

        return ValidityWithDiagnostics.Conditional(typeAlreadyUnit is false, () => Diagnostics.TypeAlreadyUnit(context, definition));
    }

    private IValidityWithDiagnostics ValidateTypeNotAlreadyScalar(ISharpMeasuresVectorValidationContext context, SharpMeasuresVectorDefinition definition)
    {
        var typeAlreadyScalar = context.ScalarPopulation.Scalars.ContainsKey(context.Type.AsNamedType());

        return ValidityWithDiagnostics.Conditional(typeAlreadyScalar is false, () => Diagnostics.TypeAlreadyScalar(context, definition));
    }

    private IValidityWithDiagnostics ValidateTypeNotAlreadyVector(ISharpMeasuresVectorValidationContext context, SharpMeasuresVectorDefinition definition)
    {
        var typeAlreadyVector = context.VectorPopulation.DuplicatelyDefinedVectors.ContainsKey(context.Type.AsNamedType());

        return ValidityWithDiagnostics.Conditional(typeAlreadyVector is false, () => Diagnostics.TypeAlreadyVector(context, definition));
    }

    private IOptionalWithDiagnostics<IUnitType> ResolveUnit(ISharpMeasuresVectorValidationContext context, SharpMeasuresVectorDefinition definition)
    {
        var correctlyResolvedUnit = context.UnitPopulation.Units.TryGetValue(definition.Unit, out var unit);

        return OptionalWithDiagnostics.Conditional(correctlyResolvedUnit, unit, () => Diagnostics.TypeNotUnit(context, definition));
    }

    private IValidityWithDiagnostics ValidateScalarIsScalar(ISharpMeasuresVectorValidationContext context, SharpMeasuresVectorDefinition definition)
    {
        var scalarIsNotScalar = definition.Scalar is null || context.ScalarPopulation.Scalars.ContainsKey(definition.Scalar.Value) is false;

        return ValidityWithDiagnostics.ValidWithConditionalDiagnostics(scalarIsNotScalar, () => Diagnostics.TypeNotScalar(context, definition));
    }

    private IValidityWithDiagnostics ValidateDifference(ISharpMeasuresVectorValidationContext context, SharpMeasuresVectorDefinition definition)
    {
        if (definition.Difference is null)
        {
            return ValidityWithDiagnostics.Valid;
        }

        if (context.VectorPopulation.VectorBases.TryGetValue(definition.Difference.Value, out var vector))
        {
            return ValidityWithDiagnostics.ValidWithConditionalDiagnostics(vector.Definition.Dimension != definition.Dimension, () => Diagnostics.DifferenceVectorGroupLacksMatchingDimension(context, definition));
        }

        if (context.VectorPopulation.GroupBases.TryGetValue(definition.Difference.Value, out var vectorGroup))
        {
            var groupHasMemberOfMatchingDimension = context.VectorPopulation.GroupMembersByGroup[definition.Difference.Value].GroupMembersByDimension.ContainsKey(definition.Dimension);

            return ValidityWithDiagnostics.ValidWithConditionalDiagnostics(groupHasMemberOfMatchingDimension is false, () => Diagnostics.DifferenceVectorGroupLacksMatchingDimension(context, definition));
        }

        if (context.VectorPopulation.GroupMembers.TryGetValue(definition.Difference.Value, out var vectorGroupMember))
        {
            return ValidityWithDiagnostics.ValidWithConditionalDiagnostics(vectorGroupMember.Definition.Dimension != definition.Dimension, () => Diagnostics.DifferenceVectorGroupLacksMatchingDimension(context, definition));
        }

        return ValidityWithDiagnostics.ValidWithDiagnostics(Diagnostics.DifferenceNotVector(context, definition));
    }
}
