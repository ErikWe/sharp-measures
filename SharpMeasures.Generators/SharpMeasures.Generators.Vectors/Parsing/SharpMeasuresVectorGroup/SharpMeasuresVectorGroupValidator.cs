namespace SharpMeasures.Generators.Vectors.Parsing.SharpMeasuresVectorGroup;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Quantities.Parsing.DefaultUnitInstance;
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Units;

using System.Linq;

internal interface ISharpMeasuresVectorGroupValidationDiagnostics : IDefaultUnitInstanceValidationDiagnostics
{
    public abstract Diagnostic? TypeAlreadyUnit(ISharpMeasuresVectorGroupValidationContext context, SharpMeasuresVectorGroupDefinition definition);
    public abstract Diagnostic? TypeAlreadyScalar(ISharpMeasuresVectorGroupValidationContext context, SharpMeasuresVectorGroupDefinition definition);
    public abstract Diagnostic? TypeAlreadyVector(ISharpMeasuresVectorGroupValidationContext context, SharpMeasuresVectorGroupDefinition definition);
    public abstract Diagnostic? TypeNotUnit(ISharpMeasuresVectorGroupValidationContext context, SharpMeasuresVectorGroupDefinition definition);
    public abstract Diagnostic? TypeNotScalar(ISharpMeasuresVectorGroupValidationContext context, SharpMeasuresVectorGroupDefinition definition);
    public abstract Diagnostic? DifferenceNotVectorGroup(ISharpMeasuresVectorGroupValidationContext context, SharpMeasuresVectorGroupDefinition definition);
}

internal interface ISharpMeasuresVectorGroupValidationContext : IProcessingContext
{
    public abstract VectorProcessingData ProcessingData { get; }

    public abstract IUnitPopulation UnitPopulation { get; }
    public abstract IScalarPopulation ScalarPopulation { get; }
    public abstract IVectorPopulation VectorPopulation { get; }
}

internal sealed class SharpMeasuresVectorGroupValidator : IProcesser<ISharpMeasuresVectorGroupValidationContext, SharpMeasuresVectorGroupDefinition, SharpMeasuresVectorGroupDefinition>
{
    private ISharpMeasuresVectorGroupValidationDiagnostics Diagnostics { get; }

    public SharpMeasuresVectorGroupValidator(ISharpMeasuresVectorGroupValidationDiagnostics diagnostics)
    {
        Diagnostics = diagnostics;
    }

    public IOptionalWithDiagnostics<SharpMeasuresVectorGroupDefinition> Process(ISharpMeasuresVectorGroupValidationContext context, SharpMeasuresVectorGroupDefinition definition)
    {
        var validity = ValidateTypeNotAlreadyUnit(context, definition)
            .Validate(() => ValidateTypeNotAlreadyScalar(context, definition))
            .Validate(() => ValidateTypeNotAlreadyVector(context, definition))
            .Validate(() => ValidateTypeNotDuplicatelyDefined(context))
            .Merge(() => ResolveUnit(context, definition))
            .Reduce();

        if (validity.IsInvalid)
        {
            return validity.AsEmptyOptional<SharpMeasuresVectorGroupDefinition>();
        }

        var scalar = ValidateScalarIsScalar(context, definition).Transform(definition.Scalar);
        var difference = ValidateDifferenceIsVectorGroup(context, definition).Transform(definition.Difference);

        var defaultUnitInstanceValidity = DefaultUnitInstanceValidator.Validate(context, Diagnostics, definition, context.UnitPopulation, definition.Unit);

        var defaultUnitInstanceName = defaultUnitInstanceValidity.Transform(definition.DefaultUnitInstanceName);
        var defaultUnitInstanceSymbol = defaultUnitInstanceValidity.Transform(definition.DefaultUnitInstanceSymbol);

        var product = ProduceResult(definition, scalar.NullableValueResult(), difference.NullableValueResult(), defaultUnitInstanceName.NullableReferenceResult(), defaultUnitInstanceSymbol.NullableReferenceResult());
        var allDiagnostics = validity.Diagnostics.Concat(scalar).Concat(difference).Concat(defaultUnitInstanceValidity);

        return OptionalWithDiagnostics.Result(product, allDiagnostics);
    }

    private static SharpMeasuresVectorGroupDefinition ProduceResult(SharpMeasuresVectorGroupDefinition definition, NamedType? scalar, NamedType? difference, string? defaultUnitInstanceName, string? defaultUnitInstanceSymbol)
    {
        return new(definition.Unit, scalar, definition.ImplementSum, definition.ImplementDifference, difference, defaultUnitInstanceName, defaultUnitInstanceSymbol, definition.GenerateDocumentation, definition.Locations);
    }

    private IValidityWithDiagnostics ValidateTypeNotAlreadyUnit(ISharpMeasuresVectorGroupValidationContext context, SharpMeasuresVectorGroupDefinition definition)
    {
        var typeAlreadyUnit = context.UnitPopulation.Units.ContainsKey(context.Type.AsNamedType());

        return ValidityWithDiagnostics.Conditional(typeAlreadyUnit is false, () => Diagnostics.TypeAlreadyUnit(context, definition));
    }

    private IValidityWithDiagnostics ValidateTypeNotAlreadyScalar(ISharpMeasuresVectorGroupValidationContext context, SharpMeasuresVectorGroupDefinition definition)
    {
        var typeAlreadyScalar = context.ScalarPopulation.Scalars.ContainsKey(context.Type.AsNamedType());

        return ValidityWithDiagnostics.Conditional(typeAlreadyScalar is false, () => Diagnostics.TypeAlreadyScalar(context, definition));
    }

    private IValidityWithDiagnostics ValidateTypeNotAlreadyVector(ISharpMeasuresVectorGroupValidationContext context, SharpMeasuresVectorGroupDefinition definition)
    {
        var typeAlreadyVector = context.VectorPopulation.Vectors.ContainsKey(context.Type.AsNamedType());

        return ValidityWithDiagnostics.Conditional(typeAlreadyVector is false, () => Diagnostics.TypeAlreadyVector(context, definition));
    }

    private static IValidityWithDiagnostics ValidateTypeNotDuplicatelyDefined(ISharpMeasuresVectorGroupValidationContext context)
    {
        var typeDuplicatelyDefined = context.ProcessingData.DuplicatelyDefinedGroupBases.ContainsKey(context.Type.AsNamedType());

        return ValidityWithDiagnostics.ConditionalWithoutDiagnostics(typeDuplicatelyDefined is false);
    }

    private IOptionalWithDiagnostics<IUnitType> ResolveUnit(ISharpMeasuresVectorGroupValidationContext context, SharpMeasuresVectorGroupDefinition definition)
    {
        var correctlyResolvedUnit = context.UnitPopulation.Units.TryGetValue(definition.Unit, out var unit);

        return OptionalWithDiagnostics.Conditional(correctlyResolvedUnit, unit, () => Diagnostics.TypeNotUnit(context, definition));
    }

    private IValidityWithDiagnostics ValidateScalarIsScalar(ISharpMeasuresVectorGroupValidationContext context, SharpMeasuresVectorGroupDefinition definition)
    {
        var scalarIsNotScalar = definition.Scalar is not null && context.ScalarPopulation.Scalars.ContainsKey(definition.Scalar.Value) is false;

        return ValidityWithDiagnostics.Conditional(scalarIsNotScalar is false, () => Diagnostics.TypeNotScalar(context, definition));
    }

    private IValidityWithDiagnostics ValidateDifferenceIsVectorGroup(ISharpMeasuresVectorGroupValidationContext context, SharpMeasuresVectorGroupDefinition definition)
    {
        var differenceIsNotVectorGroup = definition.Difference is not null && context.VectorPopulation.Groups.ContainsKey(definition.Difference.Value) is false;

        return ValidityWithDiagnostics.Conditional(differenceIsNotVectorGroup is false, () => Diagnostics.DifferenceNotVectorGroup(context, definition));
    }
}
