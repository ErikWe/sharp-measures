namespace SharpMeasures.Generators.Scalars.Parsing.SharpMeasuresScalar;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Quantities.Parsing.DefaultUnitInstance;
using SharpMeasures.Generators.Scalars.Parsing.Abstraction;
using SharpMeasures.Generators.Units;
using SharpMeasures.Generators.Vectors;

using System.Linq;

internal interface ISharpMeasuresScalarValidationDiagnostics : IDefaultUnitInstanceValidationDiagnostics
{
    public abstract Diagnostic? TypeAlreadyUnit(ISharpMeasuresScalarValidationContext context, SharpMeasuresScalarDefinition definition);
    public abstract Diagnostic? TypeNotUnit(ISharpMeasuresScalarValidationContext context, SharpMeasuresScalarDefinition definition);
    public abstract Diagnostic? UnitNotIncludingBiasTerm(ISharpMeasuresScalarValidationContext context, SharpMeasuresScalarDefinition definition);
    public abstract Diagnostic? TypeNotVector(ISharpMeasuresScalarValidationContext context, SharpMeasuresScalarDefinition definition);
    public abstract Diagnostic? DifferenceNotScalar(ISharpMeasuresScalarValidationContext context, SharpMeasuresScalarDefinition definition);
}

internal interface ISharpMeasuresScalarValidationContext : IProcessingContext
{
    public abstract ScalarProcessingData ProcessingData { get; }

    public abstract IUnitPopulation UnitPopulation { get; }
    public abstract IScalarPopulation ScalarPopulation { get; }
    public abstract IVectorPopulation VectorPopulation { get; }
}

internal sealed class SharpMeasuresScalarValidator : IProcesser<ISharpMeasuresScalarValidationContext, SharpMeasuresScalarDefinition, SharpMeasuresScalarDefinition>
{
    private ISharpMeasuresScalarValidationDiagnostics Diagnostics { get; }

    public SharpMeasuresScalarValidator(ISharpMeasuresScalarValidationDiagnostics diagnostics)
    {
        Diagnostics = diagnostics;
    }

    public IOptionalWithDiagnostics<SharpMeasuresScalarDefinition> Process(ISharpMeasuresScalarValidationContext context, SharpMeasuresScalarDefinition definition)
    {
        var validity = ValidateTypeNotAlreadyUnit(context, definition)
            .Validate(() => ValidateTypeNotDuplicatelyDefined(context))
            .Merge(() => ResolveUnit(context, definition))
            .Validate((unit) => ValidateUnitNotIncorrectlyUnbiased(context, definition, unit))
            .Reduce();

        if (validity.IsInvalid)
        {
            return validity.AsEmptyOptional<SharpMeasuresScalarDefinition>();
        }

        var vector = ValidateVectorIsVector(context, definition).Transform(definition.Vector);
        var difference = ValidateDifferenceIsScalar(context, definition).Transform(definition.Difference);

        var defaultUnitInstanceValidity = DefaultUnitInstanceValidator.Validate(context, Diagnostics, definition, context.UnitPopulation, definition.Unit);

        var defaultUnitInstanceName = defaultUnitInstanceValidity.Transform(definition.DefaultUnitInstanceName);
        var defaultUnitInstanceSymbol = defaultUnitInstanceValidity.Transform(definition.DefaultUnitInstanceSymbol);

        var product = ProduceResult(definition, vector.NullableValueResult(), difference.NullableValueResult(), defaultUnitInstanceName.NullableReferenceResult(), defaultUnitInstanceSymbol.NullableReferenceResult());

        var allDiagnostics = validity.Diagnostics.Concat(vector).Concat(difference).Concat(defaultUnitInstanceValidity);

        return OptionalWithDiagnostics.Result(product, allDiagnostics);
    }

    private static SharpMeasuresScalarDefinition ProduceResult(SharpMeasuresScalarDefinition definition, NamedType? vector, NamedType? difference, string? defaultUnitInstanceName, string? defaultUnitInstanceSymbol)
    {
        return new(definition.Unit, vector, definition.UseUnitBias, definition.ImplementSum, definition.ImplementDifference, difference, defaultUnitInstanceName, defaultUnitInstanceSymbol, definition.GenerateDocumentation, definition.Locations);
    }

    private IValidityWithDiagnostics ValidateTypeNotAlreadyUnit(ISharpMeasuresScalarValidationContext context, SharpMeasuresScalarDefinition definition)
    {
        var typeAlreadyUnit = context.UnitPopulation.Units.ContainsKey(context.Type.AsNamedType());

        return ValidityWithDiagnostics.Conditional(typeAlreadyUnit is false, () => Diagnostics.TypeAlreadyUnit(context, definition));
    }

    private static IValidityWithDiagnostics ValidateTypeNotDuplicatelyDefined(ISharpMeasuresScalarValidationContext context)
    {
        var typeDuplicatelyDefined = context.ProcessingData.DuplicatelyDefinedScalarBases.ContainsKey(context.Type.AsNamedType());

        return ValidityWithDiagnostics.ConditionalWithoutDiagnostics(typeDuplicatelyDefined is false);
    }

    private IOptionalWithDiagnostics<IUnitType> ResolveUnit(ISharpMeasuresScalarValidationContext context, SharpMeasuresScalarDefinition definition)
    {
        var correctlyResolvedUnit = context.UnitPopulation.Units.TryGetValue(definition.Unit, out var unit);

        return OptionalWithDiagnostics.Conditional(correctlyResolvedUnit, unit, () => Diagnostics.TypeNotUnit(context, definition));
    }

    private IValidityWithDiagnostics ValidateUnitNotIncorrectlyUnbiased(ISharpMeasuresScalarValidationContext context, SharpMeasuresScalarDefinition definition, IUnitType unit)
    {
        var unitNotIncorrectlyUnbiased = definition.UseUnitBias is false || unit.Definition.BiasTerm;

        return ValidityWithDiagnostics.Conditional(unitNotIncorrectlyUnbiased, () => Diagnostics.UnitNotIncludingBiasTerm(context, definition));
    }

    private IValidityWithDiagnostics ValidateVectorIsVector(ISharpMeasuresScalarValidationContext context, SharpMeasuresScalarDefinition definition)
    {
        if (definition.Vector is not null && definition.Vector.Value == NamedType.Empty)
        {
            return ValidityWithDiagnostics.InvalidWithoutDiagnostics;
        }

        var vectorIsNotVector = definition.Vector is not null && context.VectorPopulation.Vectors.ContainsKey(definition.Vector.Value) is false && context.VectorPopulation.Groups.ContainsKey(definition.Vector.Value) is false && context.VectorPopulation.GroupMembers.ContainsKey(definition.Vector.Value) is false;

        return ValidityWithDiagnostics.Conditional(vectorIsNotVector is false, () => Diagnostics.TypeNotVector(context, definition));
    }

    private IValidityWithDiagnostics ValidateDifferenceIsScalar(ISharpMeasuresScalarValidationContext context, SharpMeasuresScalarDefinition definition)
    {
        if (definition.Difference is not null && definition.Difference.Value == NamedType.Empty)
        {
            return ValidityWithDiagnostics.InvalidWithoutDiagnostics;
        }

        var differenceIsNotScalar = definition.Difference is not null && context.ScalarPopulation.Scalars.ContainsKey(definition.Difference.Value) is false;

        return ValidityWithDiagnostics.Conditional(differenceIsNotScalar is false, () => Diagnostics.DifferenceNotScalar(context, definition));
    }
}
