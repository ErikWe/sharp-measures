namespace SharpMeasures.Generators.Units.Parsing.ScaledUnit;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Units.Parsing.Abstractions;

internal class ScaledUnitResolver : ADependantUnitResolver<IDependantUnitResolutionContext, RawScaledUnitDefinition, ScaledUnitLocations, ScaledUnitDefinition>
{
    public ScaledUnitResolver(IDependantUnitResolutionDiagnostics<RawScaledUnitDefinition, ScaledUnitLocations> diagnostics) : base(diagnostics) { }

    public override IOptionalWithDiagnostics<ScaledUnitDefinition> Process(IDependantUnitResolutionContext context, RawScaledUnitDefinition definition)
    {
        var processedDependency = ProcessDependantOn(context, definition);
        var allDiagnostics = processedDependency.Diagnostics;

        if (processedDependency.LacksResult)
        {
            return OptionalWithDiagnostics.Empty<ScaledUnitDefinition>(allDiagnostics);
        }

        var product = new ScaledUnitDefinition(definition.Name, definition.Plural, processedDependency.Result, definition.Expression, definition.Locations);
        return OptionalWithDiagnostics.Result(product, allDiagnostics);
    }
}
