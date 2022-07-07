namespace SharpMeasures.Generators.Units.Parsing.BiasedUnit;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Units.Parsing.Abstractions;

internal interface IBiasedUnitResolutionDiagnostics : IDependantUnitResolutionDiagnostics<UnresolvedBiasedUnitDefinition, BiasedUnitLocations>
{
    public abstract Diagnostic? UnitNotIncludingBiasTerm(IBiasedUnitResolutionContext context, UnresolvedBiasedUnitDefinition definition);
}

internal interface IBiasedUnitResolutionContext : IDependantUnitResolutionContext
{
    public abstract bool UnitIncludesBiasTerm { get; }
}

internal class BiasedUnitResolver : ADependantUnitResolver<IBiasedUnitResolutionContext, UnresolvedBiasedUnitDefinition, BiasedUnitLocations, BiasedUnitDefinition>
{
    private IBiasedUnitResolutionDiagnostics Diagnostics { get; }

    public BiasedUnitResolver(IBiasedUnitResolutionDiagnostics diagnostics) : base(diagnostics)
    {
        Diagnostics = diagnostics;
    }

    public override IOptionalWithDiagnostics<BiasedUnitDefinition> Process(IBiasedUnitResolutionContext context, UnresolvedBiasedUnitDefinition definition)
    {
        if (context.UnitIncludesBiasTerm is false)
        {
            return OptionalWithDiagnostics.Empty<BiasedUnitDefinition>(Diagnostics.UnitNotIncludingBiasTerm(context, definition));
        }

        var processedDependency = ProcessDependantOn(context, definition);
        var allDiagnostics = processedDependency.Diagnostics;

        if (processedDependency.LacksResult)
        {
            return OptionalWithDiagnostics.Empty<BiasedUnitDefinition>(allDiagnostics);
        }

        var product = new BiasedUnitDefinition(definition.Name, definition.Plural, processedDependency.Result, definition.Expression, definition.Locations);
        return OptionalWithDiagnostics.Result(product, allDiagnostics);
    }
}
