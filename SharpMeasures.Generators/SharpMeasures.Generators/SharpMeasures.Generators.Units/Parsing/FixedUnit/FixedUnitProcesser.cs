namespace SharpMeasures.Generators.Units.Parsing.FixedUnit;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Units.Parsing.Abstractions;

internal class FixedUnitProcesser : AUnitProcesser<IUnitProcessingContext, UnprocessedFixedUnitDefinition, FixedUnitLocations, RawFixedUnitDefinition>
{
    public FixedUnitProcesser(IUnitProcessingDiagnostics<UnprocessedFixedUnitDefinition, FixedUnitLocations> diagnostics) : base(diagnostics) { }

    public override IOptionalWithDiagnostics<RawFixedUnitDefinition> Process(IUnitProcessingContext context, UnprocessedFixedUnitDefinition definition)
    {
        var requiredPropertiesSet = VerifyRequiredPropertiesSet(definition);
        var unitValidity = requiredPropertiesSet.Validate(context, definition, ValidateUnitName);
        var processedPlural = unitValidity.Merge(context, definition, ProcessPlural);
        return processedPlural.Transform(definition, ProduceResult);
    }

    private RawFixedUnitDefinition ProduceResult(UnprocessedFixedUnitDefinition definition, string processedPlural)
    {
        return new(definition.Name!, processedPlural, definition.Locations);
    }
}
