namespace SharpMeasures.Generators.Units.Parsing.SharpMeasuresUnit;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;

internal interface ISharpMeasuresUnitProcessingDiagnostics
{
    public abstract Diagnostic? NullQuantity(IProcessingContext context, RawSharpMeasuresUnitDefinition definition);
}

internal sealed class SharpMeasuresUnitProcesser : AProcesser<IProcessingContext, RawSharpMeasuresUnitDefinition, SharpMeasuresUnitDefinition>
{
    private ISharpMeasuresUnitProcessingDiagnostics Diagnostics { get; }

    public SharpMeasuresUnitProcesser(ISharpMeasuresUnitProcessingDiagnostics diagnostics)
    {
        Diagnostics = diagnostics;
    }

    public override IOptionalWithDiagnostics<SharpMeasuresUnitDefinition> Process(IProcessingContext context, RawSharpMeasuresUnitDefinition definition)
    {
        return VerifyRequiredPropertiesSet(definition)
            .Validate(() => ValidateQuantityNotNull(context, definition))
            .Validate(() => ValidateQuantityNotUndefined(definition))
            .Transform(() => ProduceResult(definition));
    }

    private static SharpMeasuresUnitDefinition ProduceResult(RawSharpMeasuresUnitDefinition definition) => new(definition.Quantity!.Value, definition.BiasTerm, definition.Locations);

    private static IValidityWithDiagnostics VerifyRequiredPropertiesSet(RawSharpMeasuresUnitDefinition definition)
    {
        return ValidityWithDiagnostics.ConditionalWithoutDiagnostics(definition.Locations.ExplicitlySetQuantity);
    }

    private IValidityWithDiagnostics ValidateQuantityNotNull(IProcessingContext context, RawSharpMeasuresUnitDefinition definition)
    {
        return ValidityWithDiagnostics.Conditional(definition.Quantity is not null, () => Diagnostics.NullQuantity(context, definition));
    }

    private static IValidityWithDiagnostics ValidateQuantityNotUndefined(RawSharpMeasuresUnitDefinition definition)
    {
        return ValidityWithDiagnostics.ConditionalWithoutDiagnostics(definition.Quantity!.Value != NamedType.Empty);
    }
}
