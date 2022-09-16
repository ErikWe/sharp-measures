namespace SharpMeasures.Generators.Units.Parsing.SharpMeasuresUnit;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Scalars;

internal interface ISharpMeasuresUnitValidationDiagnostics
{
    public abstract Diagnostic? QuantityNotScalar(ISharpMeasuresUnitValidationContext context, SharpMeasuresUnitDefinition definition);
    public abstract Diagnostic? QuantityBiased(ISharpMeasuresUnitValidationContext context, SharpMeasuresUnitDefinition definition);
}

internal interface ISharpMeasuresUnitValidationContext : IValidationContext
{
    public abstract UnitProcessingData ProcessingData { get; }
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
        return ValidateTypeNotDuplicatelyDefined(context)
            .Merge(() => ResolveQuantity(context, definition))
            .Validate((scalarBase) => ValidateQuantityNotBiased(context, definition, scalarBase))
            .Reduce();
    }

    private static IValidityWithDiagnostics ValidateTypeNotDuplicatelyDefined(ISharpMeasuresUnitValidationContext context)
    {
        var typeDuplicatelyDefined = context.ProcessingData.DuplicatelyDefinedUnits.ContainsKey(context.Type.AsNamedType());

        return ValidityWithDiagnostics.ConditionalWithoutDiagnostics(typeDuplicatelyDefined is false);
    }

    private IOptionalWithDiagnostics<IScalarBaseType> ResolveQuantity(ISharpMeasuresUnitValidationContext context, SharpMeasuresUnitDefinition definition)
    {
        var scalarBaseCorrectlyResolved = context.ScalarPopulation.ScalarBases.TryGetValue(definition.Quantity, out var scalarBase);

        return OptionalWithDiagnostics.Conditional(scalarBaseCorrectlyResolved, scalarBase, () => Diagnostics.QuantityNotScalar(context, definition));
    }

    private IValidityWithDiagnostics ValidateQuantityNotBiased(ISharpMeasuresUnitValidationContext context, SharpMeasuresUnitDefinition definition, IScalarBaseType scalarBase)
    {
        return ValidityWithDiagnostics.Conditional(scalarBase.Definition.UseUnitBias is false, () => Diagnostics.QuantityBiased(context, definition));
    }
}
