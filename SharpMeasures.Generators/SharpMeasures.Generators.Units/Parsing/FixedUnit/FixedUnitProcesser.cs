namespace SharpMeasures.Generators.Units.Parsing.FixedUnit;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Units.Parsing.Abstractions;

internal class FixedUnitProcesser : AUnitProcesser<IUnitProcessingContext, RawFixedUnitDefinition, FixedUnitLocations, FixedUnitDefinition>
{
    public FixedUnitProcesser(IUnitProcessingDiagnostics<RawFixedUnitDefinition, FixedUnitLocations> diagnostics) : base(diagnostics) { }

    public override IOptionalWithDiagnostics<FixedUnitDefinition> Process(IUnitProcessingContext context, RawFixedUnitDefinition definition)
    {
        return VerifyRequiredPropertiesSet(definition)
            .Validate(() => ValidateUnitName(context, definition))
            .Merge(() => ProcessPlural(context, definition))
            .Transform((processedPlural) => ProduceResult(definition, processedPlural));
    }

    private static FixedUnitDefinition ProduceResult(RawFixedUnitDefinition definition, string processedPlural)
    {
        return new(definition.Name!, processedPlural, definition.Locations);
    }
}
