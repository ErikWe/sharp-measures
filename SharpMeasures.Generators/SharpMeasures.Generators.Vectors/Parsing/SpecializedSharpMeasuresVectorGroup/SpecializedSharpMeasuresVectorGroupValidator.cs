namespace SharpMeasures.Generators.Vectors.Parsing.SpecializedSharpMeasuresVectorGroup;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Quantities.Parsing.DefaultUnitInstance;
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Units;

using System.Linq;

internal interface ISpecializedSharpMeasuresVectorGroupValidationDiagnostics : IDefaultUnitInstanceValidationDiagnostics
{
    public abstract Diagnostic? TypeAlreadyUnit(ISpecializedSharpMeasuresVectorGroupValidationContext context, SpecializedSharpMeasuresVectorGroupDefinition definition);
    public abstract Diagnostic? TypeAlreadyScalar(ISpecializedSharpMeasuresVectorGroupValidationContext context, SpecializedSharpMeasuresVectorGroupDefinition definition);
    public abstract Diagnostic? TypeAlreadyVector(ISpecializedSharpMeasuresVectorGroupValidationContext context, SpecializedSharpMeasuresVectorGroupDefinition definition);
    public abstract Diagnostic? TypeAlreadyVectorGroupBase(ISpecializedSharpMeasuresVectorGroupValidationContext context, SpecializedSharpMeasuresVectorGroupDefinition definition);
    public abstract Diagnostic? OriginalNotVectorGroup(ISpecializedSharpMeasuresVectorGroupValidationContext context, SpecializedSharpMeasuresVectorGroupDefinition definition);
    public abstract Diagnostic? RootVectorGroupNotResolved(ISpecializedSharpMeasuresVectorGroupValidationContext context, SpecializedSharpMeasuresVectorGroupDefinition definition);
    public abstract Diagnostic? TypeNotScalar(ISpecializedSharpMeasuresVectorGroupValidationContext context, SpecializedSharpMeasuresVectorGroupDefinition definition);
    public abstract Diagnostic? DifferenceNotVectorGroup(ISpecializedSharpMeasuresVectorGroupValidationContext context, SpecializedSharpMeasuresVectorGroupDefinition definition);
}

internal interface ISpecializedSharpMeasuresVectorGroupValidationContext : IProcessingContext
{
    public abstract VectorProcessingData ProcessingData { get; }

    public abstract IUnitPopulation UnitPopulation { get; }
    public abstract IScalarPopulation ScalarPopulation { get; }
    public abstract IVectorPopulation VectorPopulation { get; }
}

internal sealed class SpecializedSharpMeasuresVectorGroupValidator : IProcesser<ISpecializedSharpMeasuresVectorGroupValidationContext, SpecializedSharpMeasuresVectorGroupDefinition, SpecializedSharpMeasuresVectorGroupDefinition>
{
    private ISpecializedSharpMeasuresVectorGroupValidationDiagnostics Diagnostics { get; }

    public SpecializedSharpMeasuresVectorGroupValidator(ISpecializedSharpMeasuresVectorGroupValidationDiagnostics diagnostics)
    {
        Diagnostics = diagnostics;
    }

    public IOptionalWithDiagnostics<SpecializedSharpMeasuresVectorGroupDefinition> Process(ISpecializedSharpMeasuresVectorGroupValidationContext context, SpecializedSharpMeasuresVectorGroupDefinition definition)
    {
        var validity = ValidateTypeNotAlreadyUnit(context, definition)
            .Validate(() => ValidateTypeNotAlreadyScalar(context, definition))
            .Validate(() => ValidateTypeNotAlreadyVector(context, definition))
            .Validate(() => ValidateTypeNotAlreadyVectorGroupBase(context, definition))
            .Validate(() => ValidateTypeNotDuplicatelyDefined(context))
            .Validate(() => ValidateOriginalVectorGroupIsVectorGroup(context, definition))
            .Validate(() => ValidateVectorGroupRootResolved(context, definition))
            .Validate(() => ValidateUnitIsUnit(context));

        if (validity.IsInvalid)
        {
            return validity.AsEmptyOptional<SpecializedSharpMeasuresVectorGroupDefinition>();
        }

        var scalar = ValidateScalarIsScalar(context, definition).Transform(definition.Scalar);
        var difference = ValidateDifferenceIsVectorGroup(context, definition).Transform(definition.Difference);

        var unit = context.VectorPopulation.GroupBases[context.Type.AsNamedType()].Definition.Unit;
        var defaultUnitInstanceValidity = DefaultUnitInstanceValidator.Validate(context, Diagnostics, definition, context.UnitPopulation, unit);

        var defaultUnitInstanceName = defaultUnitInstanceValidity.Transform(definition.DefaultUnitInstanceName);
        var defaultUnitInstanceSymbol = defaultUnitInstanceValidity.Transform(definition.DefaultUnitInstanceSymbol);

        var product = ProduceResult(definition, scalar.NullableValueResult(), difference.NullableValueResult(), defaultUnitInstanceName.NullableReferenceResult(), defaultUnitInstanceSymbol.NullableReferenceResult());
        var allDiagnostics = validity.Diagnostics.Concat(scalar).Concat(difference).Concat(defaultUnitInstanceValidity);

        return OptionalWithDiagnostics.Result(product, allDiagnostics);
    }

    private static SpecializedSharpMeasuresVectorGroupDefinition ProduceResult(SpecializedSharpMeasuresVectorGroupDefinition definition, NamedType? scalar, NamedType? difference, string? defaultUnitInstanceName, string? defaultUnitInstanceSymbol)
    {
        return new(definition.OriginalQuantity, definition.InheritOperations, definition.InheritConstants, definition.InheritConversions, definition.InheritUnits, definition.ForwardsCastOperatorBehaviour, definition.BackwardsCastOperatorBehaviour, scalar, definition.ImplementSum,
            definition.ImplementDifference, difference, defaultUnitInstanceName, defaultUnitInstanceSymbol, definition.Locations);
    }

    private IValidityWithDiagnostics ValidateTypeNotAlreadyUnit(ISpecializedSharpMeasuresVectorGroupValidationContext context, SpecializedSharpMeasuresVectorGroupDefinition definition)
    {
        var typeAlreadyUnit = context.UnitPopulation.Units.ContainsKey(context.Type.AsNamedType());

        return ValidityWithDiagnostics.Conditional(typeAlreadyUnit is false, () => Diagnostics.TypeAlreadyUnit(context, definition));
    }

    private IValidityWithDiagnostics ValidateTypeNotAlreadyScalar(ISpecializedSharpMeasuresVectorGroupValidationContext context, SpecializedSharpMeasuresVectorGroupDefinition definition)
    {
        var typeAlreadyScalar = context.ScalarPopulation.Scalars.ContainsKey(context.Type.AsNamedType());

        return ValidityWithDiagnostics.Conditional(typeAlreadyScalar is false, () => Diagnostics.TypeAlreadyScalar(context, definition));
    }

    private IValidityWithDiagnostics ValidateTypeNotAlreadyVector(ISpecializedSharpMeasuresVectorGroupValidationContext context, SpecializedSharpMeasuresVectorGroupDefinition definition)
    {
        var typeAlreadyVector = context.VectorPopulation.Vectors.ContainsKey(context.Type.AsNamedType());

        return ValidityWithDiagnostics.Conditional(typeAlreadyVector is false, () => Diagnostics.TypeAlreadyVector(context, definition));
    }

    private IValidityWithDiagnostics ValidateTypeNotAlreadyVectorGroupBase(ISpecializedSharpMeasuresVectorGroupValidationContext context, SpecializedSharpMeasuresVectorGroupDefinition definition)
    {
        var typeAlreadyVectorGroupBase = context.ProcessingData.GroupSpecializationsAlreadyDefinedAsGroupBases.ContainsKey(context.Type.AsNamedType());

        return ValidityWithDiagnostics.Conditional(typeAlreadyVectorGroupBase is false, () => Diagnostics.TypeAlreadyVectorGroupBase(context, definition));
    }

    private static IValidityWithDiagnostics ValidateTypeNotDuplicatelyDefined(ISpecializedSharpMeasuresVectorGroupValidationContext context)
    {
        var typeDuplicatelyDefined = context.ProcessingData.DuplicatelyDefinedGroupSpecializations.ContainsKey(context.Type.AsNamedType());

        return ValidityWithDiagnostics.ConditionalWithoutDiagnostics(typeDuplicatelyDefined is false);
    }

    private IValidityWithDiagnostics ValidateOriginalVectorGroupIsVectorGroup(ISpecializedSharpMeasuresVectorGroupValidationContext context, SpecializedSharpMeasuresVectorGroupDefinition definition)
    {
        var originalVectorGroupIsVectorGroup = context.VectorPopulation.Groups.ContainsKey(definition.OriginalQuantity);

        return ValidityWithDiagnostics.Conditional(originalVectorGroupIsVectorGroup, () => Diagnostics.OriginalNotVectorGroup(context, definition));
    }

    private IValidityWithDiagnostics ValidateVectorGroupRootResolved(ISpecializedSharpMeasuresVectorGroupValidationContext context, SpecializedSharpMeasuresVectorGroupDefinition definition)
    {
        var correctlyResolvedVectorGroupBase = context.VectorPopulation.GroupBases.ContainsKey(context.Type.AsNamedType());

        return ValidityWithDiagnostics.Conditional(correctlyResolvedVectorGroupBase, () => Diagnostics.RootVectorGroupNotResolved(context, definition));
    }

    private static IValidityWithDiagnostics ValidateUnitIsUnit(ISpecializedSharpMeasuresVectorGroupValidationContext context)
    {
        var unitIsUnit = context.UnitPopulation.Units.ContainsKey(context.VectorPopulation.GroupBases[context.Type.AsNamedType()].Definition.Unit);

        return ValidityWithDiagnostics.ConditionalWithoutDiagnostics(unitIsUnit);
    }

    private IValidityWithDiagnostics ValidateScalarIsScalar(ISpecializedSharpMeasuresVectorGroupValidationContext context, SpecializedSharpMeasuresVectorGroupDefinition definition)
    {
        if (definition.Scalar is not null && definition.Scalar.Value == NamedType.Empty)
        {
            return ValidityWithDiagnostics.InvalidWithoutDiagnostics;
        }

        var scalarIsNotScalar = definition.Scalar is not null && context.ScalarPopulation.Scalars.ContainsKey(definition.Scalar.Value) is false;

        return ValidityWithDiagnostics.Conditional(scalarIsNotScalar is false, () => Diagnostics.TypeNotScalar(context, definition));
    }

    private IValidityWithDiagnostics ValidateDifferenceIsVectorGroup(ISpecializedSharpMeasuresVectorGroupValidationContext context, SpecializedSharpMeasuresVectorGroupDefinition definition)
    {
        if (definition.Difference is not null && definition.Difference.Value == NamedType.Empty)
        {
            return ValidityWithDiagnostics.InvalidWithoutDiagnostics;
        }

        var differenceIsNotVectorGroup = definition.Difference is not null && context.VectorPopulation.Groups.ContainsKey(definition.Difference.Value) is false;

        return ValidityWithDiagnostics.Conditional(differenceIsNotVectorGroup is false, () => Diagnostics.DifferenceNotVectorGroup(context, definition));
    }
}
