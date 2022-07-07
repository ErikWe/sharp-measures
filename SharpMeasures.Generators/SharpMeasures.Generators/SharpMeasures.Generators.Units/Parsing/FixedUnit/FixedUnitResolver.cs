namespace SharpMeasures.Generators.Units.Parsing.FixedUnit;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;

internal class FixedUnitResolver : AProcesser<IProcessingContext, UnresolvedFixedUnitDefinition, FixedUnitDefinition>
{
    public override IOptionalWithDiagnostics<FixedUnitDefinition> Process(IProcessingContext context, UnresolvedFixedUnitDefinition definition)
    {
        FixedUnitDefinition product = new(definition.Name, definition.Plural, definition.Locations);
        return OptionalWithDiagnostics.Result(product);
    }
}
