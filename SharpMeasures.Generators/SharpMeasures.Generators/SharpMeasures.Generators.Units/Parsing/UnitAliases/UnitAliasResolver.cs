namespace SharpMeasures.Generators.Units.Parsing.UnitAlias;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Raw.Units.UnitInstances;
using SharpMeasures.Generators.Units.Parsing.Abstractions;

internal class UnitAliasResolver : ADependantUnitResolver<IDependantUnitResolutionContext, RawUnitAliasDefinition, UnitAliasLocations, UnitAliasDefinition>
{
    public UnitAliasResolver(IDependantUnitResolutionDiagnostics<RawUnitAliasDefinition, UnitAliasLocations> diagnostics) : base(diagnostics) { }

    public override IOptionalWithDiagnostics<UnitAliasDefinition> Process(IDependantUnitResolutionContext context, RawUnitAliasDefinition definition)
    {
        return ProcessDependantOn(context, definition)
            .Transform((dependantOn) => ProduceResult(definition, dependantOn));
    }

    private static UnitAliasDefinition ProduceResult(RawUnitAliasDefinition definition, IRawUnitInstance aliasOf)
    {
        return new(definition.Name, definition.Plural, aliasOf, definition.Locations);
    }
}
