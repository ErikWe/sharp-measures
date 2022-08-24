namespace SharpMeasures.Generators.Units.Parsing.UnitAlias;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Units.Parsing.Abstractions;

internal class UnitAliasProcesser : ADependantUnitProcesser<IUnitProcessingContext, RawUnitAliasDefinition, UnitAliasLocations, UnitAliasDefinition>
{
    public UnitAliasProcesser(IDependantUnitProcessingDiagnostics<RawUnitAliasDefinition, UnitAliasLocations> diagnostics) : base(diagnostics) { }

    public override IOptionalWithDiagnostics<UnitAliasDefinition> Process(IUnitProcessingContext context, RawUnitAliasDefinition definition)
    {
        return VerifyRequiredPropertiesSet(definition)
            .Validate(() => ValidateUnitName(context, definition))
            .Validate(() => ValidateDependantOn(context, definition))
            .Merge(() => ProcessPlural(context, definition))
            .Transform((interpretedPlural) => ProduceResult(definition, interpretedPlural));
    }

    private static UnitAliasDefinition ProduceResult(RawUnitAliasDefinition definition, string interpretedPlural)
    {
        return new(definition.Name!, interpretedPlural, definition.AliasOf!, definition.Locations);
    }
}
