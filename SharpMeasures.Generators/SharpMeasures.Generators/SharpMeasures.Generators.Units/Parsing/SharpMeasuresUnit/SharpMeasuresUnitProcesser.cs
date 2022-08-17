namespace SharpMeasures.Generators.Units.Parsing.SharpMeasuresUnit;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;

internal interface ISharpMeasuresUnitProcessingDiagnostics
{
    public abstract Diagnostic? NullQuantity(IProcessingContext context, UnprocessedSharpMeasuresUnitDefinition definition);
}

internal class SharpMeasuresUnitProcesser : AProcesser<IProcessingContext, UnprocessedSharpMeasuresUnitDefinition, RawSharpMeasuresUnitDefinition>
{
    private ISharpMeasuresUnitProcessingDiagnostics Diagnostics { get; }

    public SharpMeasuresUnitProcesser(ISharpMeasuresUnitProcessingDiagnostics diagnostics)
    {
        Diagnostics = diagnostics;
    }

    public override IOptionalWithDiagnostics<RawSharpMeasuresUnitDefinition> Process(IProcessingContext context, UnprocessedSharpMeasuresUnitDefinition definition)
    {
        return VerifyRequiredPropertiesSet(definition)
            .Validate(() => ValidateQuantityNotNull(context, definition))
            .Transform(() => ProduceResult(definition));
    }

    private static RawSharpMeasuresUnitDefinition ProduceResult(UnprocessedSharpMeasuresUnitDefinition definition)
    {
        return new(definition.Quantity!.Value, definition.BiasTerm, definition.GenerateDocumentation, definition.Locations);
    }

    private static IValidityWithDiagnostics VerifyRequiredPropertiesSet(UnprocessedSharpMeasuresUnitDefinition definition)
    {
        return ValidityWithDiagnostics.ConditionalWithoutDiagnostics(definition.Locations.ExplicitlySetQuantity);
    }

    private IValidityWithDiagnostics ValidateQuantityNotNull(IProcessingContext context, UnprocessedSharpMeasuresUnitDefinition definition)
    {
        return ValidityWithDiagnostics.Conditional(definition.Quantity is not null, () => Diagnostics.NullQuantity(context, definition));
    }
}
