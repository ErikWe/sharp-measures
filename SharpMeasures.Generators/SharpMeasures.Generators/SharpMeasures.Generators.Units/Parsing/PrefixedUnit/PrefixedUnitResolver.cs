namespace SharpMeasures.Generators.Units.Parsing.PrefixedUnit;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Units.Parsing.Abstractions;
using SharpMeasures.Generators.Units.UnitInstances;

using System;

internal class PrefixedUnitResolver : ADependantUnitResolver<IDependantUnitResolutionContext, UnresolvedPrefixedUnitDefinition, PrefixedUnitLocations, PrefixedUnitDefinition>
{
    public PrefixedUnitResolver(IDependantUnitResolutionDiagnostics<UnresolvedPrefixedUnitDefinition, PrefixedUnitLocations> diagnostics) : base(diagnostics) { }

    public override IOptionalWithDiagnostics<PrefixedUnitDefinition> Process(IDependantUnitResolutionContext context, UnresolvedPrefixedUnitDefinition definition)
    {
        var processedDependency = ProcessDependantOn(context, definition);
        var allDiagnostics = processedDependency.Diagnostics;

        if (processedDependency.LacksResult)
        {
            return OptionalWithDiagnostics.Empty<PrefixedUnitDefinition>(allDiagnostics);
        }

        PrefixedUnitDefinition product = definition.SpecifiedPrefixType switch
        {
            PrefixType.Metric => new(definition.Name, definition.Plural, processedDependency.Result, definition.MetricPrefix!.Value, definition.Locations),
            PrefixType.Binary => new(definition.Name, definition.Plural, processedDependency.Result, definition.BinaryPrefix!.Value, definition.Locations),
            _ => throw new NotSupportedException()
        };

        return OptionalWithDiagnostics.Result(product, allDiagnostics);
    }
}
