namespace SharpMeasures.Generators.Scalars.Parsing.ScalarConstant;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Quantities.Parsing.QuantityConstant;

using System.Linq;

internal interface IScalarConstantProcessingDiagnostics : IQuantityConstantProcessingDiagnostics<RawScalarConstantDefinition, ScalarConstantLocations>
{
    public abstract Diagnostic? NullExpression(IQuantityConstantProcessingContext context, RawScalarConstantDefinition definition);
    public abstract Diagnostic? EmptyExpression(IQuantityConstantProcessingContext context, RawScalarConstantDefinition definition);
}

internal sealed class ScalarConstantProcesser : AQuantityConstantProcesser<IQuantityConstantProcessingContext, RawScalarConstantDefinition, ScalarConstantLocations, ScalarConstantDefinition>
{
    private IScalarConstantProcessingDiagnostics Diagnostics { get; }

    public ScalarConstantProcesser(IScalarConstantProcessingDiagnostics diagnostics) : base(diagnostics)
    {
        Diagnostics = diagnostics;
    }

    public override IOptionalWithDiagnostics<ScalarConstantDefinition> Process(IQuantityConstantProcessingContext context, RawScalarConstantDefinition definition)
    {
        var validity = VerifyRequiredPropertiesSet(definition)
            .Validate(() => Validate(context, definition));

        if (validity.IsInvalid)
        {
            return validity.AsEmptyOptional<ScalarConstantDefinition>();
        }

        var processedMultiplesPropertyData = ProcessMultiplesPropertyData(context, definition);

        var product = ProduceResult(definition, processedMultiplesPropertyData.Result.Generate, processedMultiplesPropertyData.Result.Name);

        var allDiagnostics = validity.Concat(processedMultiplesPropertyData);

        return OptionalWithDiagnostics.Result(product, allDiagnostics);
    }

    private static ScalarConstantDefinition ProduceResult(RawScalarConstantDefinition definition, bool generateMultiples, string? multiplesName) => new(definition.Name!, definition.UnitInstanceName!, definition.Value, definition.Expression, generateMultiples, multiplesName, definition.Locations);
    
    protected override IValidityWithDiagnostics Validate(IQuantityConstantProcessingContext context, RawScalarConstantDefinition definition)
    {
        return base.Validate(context, definition)
            .Validate(() => ValidateExpressionNotNull(context, definition))
            .Validate(() => ValidateExpressionNotEmpty(context, definition));
    }

    private IValidityWithDiagnostics ValidateExpressionNotNull(IQuantityConstantProcessingContext context, RawScalarConstantDefinition definition)
    {
        var validExpression = definition.Locations.ExplicitlySetExpression is false || definition.Expression is not null;

        return ValidityWithDiagnostics.Conditional(validExpression, () => Diagnostics.NullExpression(context, definition));
    }

    private IValidityWithDiagnostics ValidateExpressionNotEmpty(IQuantityConstantProcessingContext context, RawScalarConstantDefinition definition)
    {
        var validExpression = definition.Locations.ExplicitlySetExpression is false || definition.Expression!.Length > 0;

        return ValidityWithDiagnostics.Conditional(validExpression, () => Diagnostics.EmptyExpression(context, definition));
    }
}
