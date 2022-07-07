namespace SharpMeasures.Generators.Units.Parsing.UnitAlias;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Units.Parsing.Abstractions;

internal class UnitAliasResolver : ADependantUnitResolver<IDependantUnitResolutionContext, UnresolvedUnitAliasDefinition, UnitAliasLocations, UnitAliasDefinition>
{
    public UnitAliasResolver(IDependantUnitResolutionDiagnostics<UnresolvedUnitAliasDefinition, UnitAliasLocations> diagnostics) : base(diagnostics) { }

    public override IOptionalWithDiagnostics<UnitAliasDefinition> Process(IDependantUnitResolutionContext context, UnresolvedUnitAliasDefinition definition)
    {
        var processedDependency = ProcessDependantOn(context, definition);
        var allDiagnostics = processedDependency.Diagnostics;

        if (processedDependency.LacksResult)
        {
            return OptionalWithDiagnostics.Empty<UnitAliasDefinition>(allDiagnostics);
        }

        var product = new UnitAliasDefinition(definition.Name, definition.Plural, processedDependency.Result, definition.Locations);
        return OptionalWithDiagnostics.Result(product, allDiagnostics);
    }
}
