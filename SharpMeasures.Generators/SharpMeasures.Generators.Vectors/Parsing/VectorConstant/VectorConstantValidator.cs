namespace SharpMeasures.Generators.Vectors.Parsing.VectorConstant;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Quantities.Parsing.QuantityConstant;

internal interface IVectorConstantValidationDiagnostics : IQuantityConstantValidationDiagnostics<VectorConstantDefinition, VectorConstantLocations>
{
    public abstract Diagnostic? InvalidValueDimensionality(IVectorConstantValidationContext context, VectorConstantDefinition definition);
    public abstract Diagnostic? InvalidExpressionsDimensionality(IVectorConstantValidationContext context, VectorConstantDefinition definition);
}

internal interface IVectorConstantValidationContext : IQuantityConstantValidationContext
{
    public abstract int Dimension { get; }
}

internal sealed class VectorConstantValidator : AQuantityConstantValidator<IVectorConstantValidationContext, VectorConstantDefinition, VectorConstantLocations>
{
    private IVectorConstantValidationDiagnostics Diagnostics { get; }

    public VectorConstantValidator(IVectorConstantValidationDiagnostics diagnostics) : base(diagnostics)
    {
        Diagnostics = diagnostics;
    }

    protected override VectorConstantDefinition ProduceResult(VectorConstantDefinition definition, bool generateMultiples) => new(definition.Name, definition.UnitInstanceName, definition.Value, definition.Expressions, generateMultiples, definition.Multiples, definition.Locations);

    protected override IValidityWithDiagnostics ValidateConstant(IVectorConstantValidationContext context, VectorConstantDefinition definition)
    {
        return base.ValidateConstant(context, definition)
            .Validate(() => ValidateValueDimension(context, definition))
            .Validate(() => ValidateExpressionsDimension(context, definition));
    }

    private IValidityWithDiagnostics ValidateValueDimension(IVectorConstantValidationContext context, VectorConstantDefinition definition)
    {
        var validDimension = definition.Locations.ExplicitlySetValue is false || definition.Value?.Count == context.Dimension;

        return ValidityWithDiagnostics.Conditional(validDimension, () => Diagnostics.InvalidValueDimensionality(context, definition));
    }

    private IValidityWithDiagnostics ValidateExpressionsDimension(IVectorConstantValidationContext context, VectorConstantDefinition definition)
    {
        var validDimension = definition.Locations.ExplicitlySetExpressions is false || definition.Expressions?.Count == context.Dimension;

        return ValidityWithDiagnostics.Conditional(validDimension, () => Diagnostics.InvalidExpressionsDimensionality(context, definition));
    }
}
