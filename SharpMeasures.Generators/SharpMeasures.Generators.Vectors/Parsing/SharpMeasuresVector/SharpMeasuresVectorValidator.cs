namespace SharpMeasures.Generators.Vectors.Parsing.SharpMeasuresVector;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Quantities.Parsing.DefaultUnitInstance;
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Units;

using System.Linq;

internal interface ISharpMeasuresVectorValidationDiagnostics : IDefaultUnitInstanceValidationDiagnostics
{
    public abstract Diagnostic? TypeAlreadyUnit(ISharpMeasuresVectorValidationContext context, SharpMeasuresVectorDefinition definition);
    public abstract Diagnostic? TypeAlreadyScalar(ISharpMeasuresVectorValidationContext context, SharpMeasuresVectorDefinition definition);
    public abstract Diagnostic? TypeNotUnit(ISharpMeasuresVectorValidationContext context, SharpMeasuresVectorDefinition definition);
    public abstract Diagnostic? TypeNotScalar(ISharpMeasuresVectorValidationContext context, SharpMeasuresVectorDefinition definition);
    public abstract Diagnostic? DifferenceNotVector(ISharpMeasuresVectorValidationContext context, SharpMeasuresVectorDefinition definition);
    public abstract Diagnostic? DifferenceVectorInvalidDimension(ISharpMeasuresVectorValidationContext context, SharpMeasuresVectorDefinition definition, int dimension);
    public abstract Diagnostic? DifferenceVectorGroupLacksMatchingDimension(ISharpMeasuresVectorValidationContext context, SharpMeasuresVectorDefinition definition);
}

internal interface ISharpMeasuresVectorValidationContext : IProcessingContext
{
    public abstract VectorProcessingData ProcessingData { get; }

    public abstract IUnitPopulation UnitPopulation { get; }
    public abstract IScalarPopulation ScalarPopulation { get; }
    public abstract IVectorPopulation VectorPopulation { get; }
}

internal sealed class SharpMeasuresVectorValidator : IProcesser<ISharpMeasuresVectorValidationContext, SharpMeasuresVectorDefinition, SharpMeasuresVectorDefinition>
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
            .Validate(() => ValidateTypeNotDuplicatelyDefined(context))
            .Merge(() => ResolveUnit(context, definition))
            .Reduce();

        if (validity.IsInvalid)
        {
            return validity.AsEmptyOptional<SharpMeasuresVectorDefinition>();
        }

        var scalar = ValidateScalarIsScalar(context, definition).Transform(definition.Scalar);
        var difference = ValidateDifference(context, definition);

        var defaultUnitInstanceValidity = DefaultUnitInstanceValidator.Validate(context, Diagnostics, definition, context.UnitPopulation, definition.Unit);

        var defaultUnitInstanceName = defaultUnitInstanceValidity.Transform(definition.DefaultUnitInstanceName);
        var defaultUnitInstanceSymbol = defaultUnitInstanceValidity.Transform(definition.DefaultUnitInstanceSymbol);

        var product = ProduceResult(definition, scalar.NullableValueResult(), difference.NullableValueResult(), defaultUnitInstanceName.NullableReferenceResult(), defaultUnitInstanceSymbol.NullableReferenceResult());

        var allDiagnostics = validity.Diagnostics.Concat(scalar).Concat(difference).Concat(defaultUnitInstanceValidity);

        return OptionalWithDiagnostics.Result(product, allDiagnostics);
    }

    private static SharpMeasuresVectorDefinition ProduceResult(SharpMeasuresVectorDefinition definition, NamedType? scalar, NamedType? difference, string? defaultUnitInstanceName, string? defaultUnitInstanceSymbol)
    {
        return new(definition.Unit, scalar, definition.Dimension, definition.ImplementSum, definition.ImplementDifference, difference, defaultUnitInstanceName, defaultUnitInstanceSymbol, definition.GenerateDocumentation, definition.Locations);
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

    private static IValidityWithDiagnostics ValidateTypeNotDuplicatelyDefined(ISharpMeasuresVectorValidationContext context)
    {
        var typeDuplicatelyDefined = context.ProcessingData.DuplicatelyDefinedVectorBases.ContainsKey(context.Type.AsNamedType());

        return ValidityWithDiagnostics.ConditionalWithoutDiagnostics(typeDuplicatelyDefined is false);
    }

    private IOptionalWithDiagnostics<IUnitType> ResolveUnit(ISharpMeasuresVectorValidationContext context, SharpMeasuresVectorDefinition definition)
    {
        var correctlyResolvedUnit = context.UnitPopulation.Units.TryGetValue(definition.Unit, out var unit);

        return OptionalWithDiagnostics.Conditional(correctlyResolvedUnit, unit, () => Diagnostics.TypeNotUnit(context, definition));
    }

    private IValidityWithDiagnostics ValidateScalarIsScalar(ISharpMeasuresVectorValidationContext context, SharpMeasuresVectorDefinition definition)
    {
        var scalarIsNotScalar = definition.Scalar is not null && context.ScalarPopulation.Scalars.ContainsKey(definition.Scalar.Value) is false;

        return ValidityWithDiagnostics.Conditional(scalarIsNotScalar is false, () => Diagnostics.TypeNotScalar(context, definition));
    }

    private IOptionalWithDiagnostics<NamedType> ValidateDifference(ISharpMeasuresVectorValidationContext context, SharpMeasuresVectorDefinition definition)
    {
        if (definition.Difference is null)
        {
            return OptionalWithDiagnostics.Empty<NamedType>();
        }

        if (context.VectorPopulation.VectorBases.TryGetValue(definition.Difference.Value, out var vector))
        {
            return OptionalWithDiagnostics.Conditional(vector.Definition.Dimension == definition.Dimension, definition.Difference.Value, () => Diagnostics.DifferenceVectorInvalidDimension(context, definition, vector.Definition.Dimension));
        }

        if (context.VectorPopulation.GroupBases.TryGetValue(definition.Difference.Value, out var group))
        {
            var groupHasMemberOfMatchingDimension = context.VectorPopulation.GroupMembersByGroup[definition.Difference.Value].GroupMembersByDimension.TryGetValue(definition.Dimension, out var correspondingMember);

            return OptionalWithDiagnostics.Conditional(groupHasMemberOfMatchingDimension, () => correspondingMember.Type.AsNamedType(), () => Diagnostics.DifferenceVectorGroupLacksMatchingDimension(context, definition));
        }

        if (context.VectorPopulation.GroupMembers.TryGetValue(definition.Difference.Value, out var groupMember))
        {
            return OptionalWithDiagnostics.Conditional(groupMember.Definition.Dimension == definition.Dimension, definition.Difference.Value, () => Diagnostics.DifferenceVectorInvalidDimension(context, definition, groupMember.Definition.Dimension));
        }

        return OptionalWithDiagnostics.Empty<NamedType>(Diagnostics.DifferenceNotVector(context, definition));
    }
}
