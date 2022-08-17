namespace SharpMeasures.Generators.Units.Parsing.ScaledUnit;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Raw.Units.UnitInstances;
using SharpMeasures.Generators.Units.Parsing.Abstractions;

internal class ScaledUnitResolver : ADependantUnitResolver<IDependantUnitResolutionContext, RawScaledUnitDefinition, ScaledUnitLocations, ScaledUnitDefinition>
{
    public ScaledUnitResolver(IDependantUnitResolutionDiagnostics<RawScaledUnitDefinition, ScaledUnitLocations> diagnostics) : base(diagnostics) { }

    public override IOptionalWithDiagnostics<ScaledUnitDefinition> Process(IDependantUnitResolutionContext context, RawScaledUnitDefinition definition)
    {
        return ProcessDependantOn(context, definition)
            .Transform((dependantOn) => ProduceResult(definition, dependantOn));
    }

    private static ScaledUnitDefinition ProduceResult(RawScaledUnitDefinition definition, IRawUnitInstance from)
    {
        return new(definition.Name, definition.Plural, from, definition.Expression, definition.Locations);
    }
}
