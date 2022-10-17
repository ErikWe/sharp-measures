namespace SharpMeasures.Generators.Vectors.Parsing.VectorConstant;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Quantities.Parsing.QuantityConstant;

using System.Linq;

internal interface IVectorConstantProcessingDiagnostics : IQuantityConstantProcessingDiagnostics<RawVectorConstantDefinition, VectorConstantLocations>
{
    public abstract Diagnostic? NullExpressionElement(IQuantityConstantProcessingContext context, RawVectorConstantDefinition definition, int index);
    public abstract Diagnostic? EmptyExpressionElement(IQuantityConstantProcessingContext context, RawVectorConstantDefinition definition, int index);
}

internal sealed class VectorConstantProcesser : AQuantityConstantProcesser<IQuantityConstantProcessingContext, RawVectorConstantDefinition, VectorConstantLocations, VectorConstantDefinition>
{
    private IVectorConstantProcessingDiagnostics Diagnostics { get; }

    public VectorConstantProcesser(IVectorConstantProcessingDiagnostics diagnostics) : base(diagnostics)
    {
        Diagnostics = diagnostics;
    }

    public override IOptionalWithDiagnostics<VectorConstantDefinition> Process(IQuantityConstantProcessingContext context, RawVectorConstantDefinition definition)
    {
        var validity = VerifyRequiredPropertiesSet(definition)
            .Validate(() => Validate(context, definition));

        if (validity.IsInvalid)
        {
            return validity.AsEmptyOptional<VectorConstantDefinition>();
        }

        var processedMultiplesPropertyData = ProcessMultiplesPropertyData(context, definition);

        var product = ProduceResult(definition, processedMultiplesPropertyData.Result.Generate, processedMultiplesPropertyData.Result.Name);

        var allDiagnostics = validity.Concat(processedMultiplesPropertyData);

        return OptionalWithDiagnostics.Result(product, allDiagnostics);
    }

    private static VectorConstantDefinition ProduceResult(RawVectorConstantDefinition definition, bool generateMultiples, string? multiplesName) => new(definition.Name!, definition.UnitInstanceName!, definition.Value, definition.Expressions, generateMultiples, multiplesName, definition.Locations);

    protected override IValidityWithDiagnostics Validate(IQuantityConstantProcessingContext context, RawVectorConstantDefinition definition)
    {
        return base.Validate(context, definition)
            .Validate(() => ValidateExpressions(context, definition));
    }

    private IValidityWithDiagnostics ValidateExpressions(IQuantityConstantProcessingContext context, RawVectorConstantDefinition definition)
    {
        if (definition.Expressions is null)
        {
            return ValidityWithDiagnostics.Valid;
        }

        var validity = ValidityWithDiagnostics.Valid;

        for (int i = 0; i < definition.Expressions.Count; i++)
        {
            validity = validity.Validate(ValidateExpressionsElement(context, definition, i));

            if (validity.IsInvalid)
            {
                break;
            }
        }

        return validity;
    }

    private IValidityWithDiagnostics ValidateExpressionsElement(IQuantityConstantProcessingContext context, RawVectorConstantDefinition definition, int index)
    {
        return ValidateExpressionNotNull(context, definition, index)
            .Validate(() => ValidateExpressionNotEmpty(context, definition, index));
    }

    private IValidityWithDiagnostics ValidateExpressionNotNull(IQuantityConstantProcessingContext context, RawVectorConstantDefinition definition, int index)
    {
        return ValidityWithDiagnostics.Conditional(definition.Expressions![index] is not null, () => Diagnostics.NullExpressionElement(context, definition, index));
    }

    private IValidityWithDiagnostics ValidateExpressionNotEmpty(IQuantityConstantProcessingContext context, RawVectorConstantDefinition definition, int index)
    {
        return ValidityWithDiagnostics.Conditional(definition.Expressions![index].Length > 0, () => Diagnostics.EmptyExpressionElement(context, definition, index));
    }
}
