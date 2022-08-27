namespace SharpMeasures.Generators.Vectors.Parsing.VectorConstant;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Quantities.Parsing.QuantityConstant;

internal interface IVectorConstantValidationDiagnostics : IQuantityConstantValidationDiagnostics<VectorConstantDefinition, VectorConstantLocations>
{
    public abstract Diagnostic? InvalidConstantDimensionality(IVectorConstantValidationContext context, VectorConstantDefinition definition);
}

internal interface IVectorConstantValidationContext : IQuantityConstantValidationContext
{
    public abstract int Dimension { get; }
}

internal class VectorConstantValidator : AQuantityConstantValidator<IVectorConstantValidationContext, VectorConstantDefinition, VectorConstantLocations>
{
    private IVectorConstantValidationDiagnostics Diagnostics { get; }

    public VectorConstantValidator(IVectorConstantValidationDiagnostics diagnostics) : base(diagnostics)
    {
        Diagnostics = diagnostics;
    }

    public override IValidityWithDiagnostics Validate(IVectorConstantValidationContext context, VectorConstantDefinition definition)
    {
        return base.Validate(context, definition)
            .Validate(() => ValidateValueDimension(context, definition));
    }

    private IValidityWithDiagnostics ValidateValueDimension(IVectorConstantValidationContext context, VectorConstantDefinition definition)
    {
        return ValidityWithDiagnostics.Conditional(definition.Value.Count == context.Dimension, () => Diagnostics.InvalidConstantDimensionality(context, definition));
    }
}
