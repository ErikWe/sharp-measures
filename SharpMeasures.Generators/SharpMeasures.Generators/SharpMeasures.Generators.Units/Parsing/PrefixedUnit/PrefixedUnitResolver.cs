namespace SharpMeasures.Generators.Units.Parsing.PrefixedUnit;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Units.Parsing.Abstractions;
using SharpMeasures.Generators.Units.UnitInstances;
using SharpMeasures.Generators.Raw.Units.UnitInstances;

using System;

internal class PrefixedUnitResolver : ADependantUnitResolver<IDependantUnitResolutionContext, RawPrefixedUnitDefinition, PrefixedUnitLocations, PrefixedUnitDefinition>
{
    public PrefixedUnitResolver(IDependantUnitResolutionDiagnostics<RawPrefixedUnitDefinition, PrefixedUnitLocations> diagnostics) : base(diagnostics) { }

    public override IOptionalWithDiagnostics<PrefixedUnitDefinition> Process(IDependantUnitResolutionContext context, RawPrefixedUnitDefinition definition)
    {
        return ProcessDependantOn(context, definition)
            .Transform((dependantOn) => ProduceResult(definition, dependantOn));
    }

    private static PrefixedUnitDefinition ProduceResult(RawPrefixedUnitDefinition definition, IRawUnitInstance from)
    {
        return definition.SpecifiedPrefixType switch
        {
            PrefixType.Metric => new(definition.Name, definition.Plural, from, definition.MetricPrefix!.Value, definition.Locations),
            PrefixType.Binary => new(definition.Name, definition.Plural, from, definition.BinaryPrefix!.Value, definition.Locations),
            _ => throw new NotSupportedException()
        };
    }
}
