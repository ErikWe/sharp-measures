namespace SharpMeasures.Generators.Units.Parsing.FixedUnit;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;

internal class FixedUnitResolver : AProcesser<IProcessingContext, RawFixedUnitDefinition, FixedUnitDefinition>
{
    public override IOptionalWithDiagnostics<FixedUnitDefinition> Process(IProcessingContext context, RawFixedUnitDefinition definition)
    {
        return OptionalWithDiagnostics.Result(ProduceResult(definition));
    }

    private static FixedUnitDefinition ProduceResult(RawFixedUnitDefinition definition)
    {
        return new(definition.Name, definition.Plural, definition.Locations);
    }
}
