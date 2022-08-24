namespace SharpMeasures.Generators.Units.Parsing.SharpMeasuresUnit;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Units.Parsing.Abstractions;

internal interface ISharpMeasuresUnitValidationDiagnostics
{
    public abstract Diagnostic? TypeAlreadyUnit(ISharpMeasuresUnitValidationContext context, SharpMeasuresUnitDefinition definition);
    public abstract Diagnostic? QuantityNotScalar(ISharpMeasuresUnitValidationContext context, SharpMeasuresUnitDefinition definition);
    public abstract Diagnostic? QuantityBiased(ISharpMeasuresUnitValidationContext context, SharpMeasuresUnitDefinition definition);
}

internal interface ISharpMeasuresUnitValidationContext : IValidationContext
{
    public abstract IUnitPopulationWithData UnitPopulation { get; }
    public abstract IScalarPopulation ScalarPopulation { get; }
}

internal class SharpMeasuresUnitValidator : AValidator<ISharpMeasuresUnitValidationContext, SharpMeasuresUnitDefinition>
{
    private ISharpMeasuresUnitValidationDiagnostics Diagnostics { get; }

    public SharpMeasuresUnitValidator(ISharpMeasuresUnitValidationDiagnostics diagnostics)
    {
        Diagnostics = diagnostics;
    }

    public override IValidityWithDiagnostics Validate(ISharpMeasuresUnitValidationContext context, SharpMeasuresUnitDefinition definition)
    {
        return ValidateTypeNotAlreadyUnit(context, definition)
            .Merge(() => ResolveQuantity(context, definition))
            .Validate((scalarBase) => ValidateQuantityNotBiased(context, definition, scalarBase))
            .Reduce();
    }

    private IValidityWithDiagnostics ValidateTypeNotAlreadyUnit(ISharpMeasuresUnitValidationContext context, SharpMeasuresUnitDefinition definition)
    {
        var typeAlreadyUnit = context.UnitPopulation.DuplicatelyDefinedUnits.ContainsKey(context.Type.AsNamedType());

        return ValidityWithDiagnostics.Conditional(typeAlreadyUnit is false, () => Diagnostics.TypeAlreadyUnit(context, definition));
    }

    private IOptionalWithDiagnostics<IScalarBaseType> ResolveQuantity(ISharpMeasuresUnitValidationContext context, SharpMeasuresUnitDefinition definition)
    {
        var scalarBaseCorrectlyResolved = context.ScalarPopulation.ScalarBases.TryGetValue(definition.Quantity, out var scalarBase);

        return OptionalWithDiagnostics.Conditional(scalarBaseCorrectlyResolved, scalarBase, () => Diagnostics.QuantityNotScalar(context, definition));
    }

    private IValidityWithDiagnostics ValidateQuantityNotBiased(ISharpMeasuresUnitValidationContext context, SharpMeasuresUnitDefinition definition, IScalarBaseType scalarBase)
    {
        return ValidityWithDiagnostics.Conditional(scalarBase.Definition.UseUnitBias, () => Diagnostics.QuantityBiased(context, definition));
    }
}
