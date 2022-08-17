namespace SharpMeasures.Generators.Units.Parsing.FixedUnit;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Units.Parsing.Abstractions;

internal class FixedUnitProcesser : AUnitProcesser<IUnitProcessingContext, UnprocessedFixedUnitDefinition, FixedUnitLocations, RawFixedUnitDefinition>
{
    public FixedUnitProcesser(IUnitProcessingDiagnostics<UnprocessedFixedUnitDefinition, FixedUnitLocations> diagnostics) : base(diagnostics) { }

    public override IOptionalWithDiagnostics<RawFixedUnitDefinition> Process(IUnitProcessingContext context, UnprocessedFixedUnitDefinition definition)
    {
        return VerifyRequiredPropertiesSet(definition)
            .Validate(() => ValidateUnitName(context, definition))
            .Merge(() => ProcessPlural(context, definition))
            .Transform((processedPlural) => ProduceResult(definition, processedPlural));
    }

    private static RawFixedUnitDefinition ProduceResult(UnprocessedFixedUnitDefinition definition, string processedPlural)
    {
        return new(definition.Name!, processedPlural, definition.Locations);
    }
}
