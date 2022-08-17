namespace SharpMeasures.Generators.Units.Parsing.UnitAlias;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Units.Parsing.Abstractions;

internal class UnitAliasProcesser : ADependantUnitProcesser<IUnitProcessingContext, UnprocessedUnitAliasDefinition, UnitAliasLocations, RawUnitAliasDefinition>
{
    public UnitAliasProcesser(IDependantUnitProcessingDiagnostics<UnprocessedUnitAliasDefinition, UnitAliasLocations> diagnostics) : base(diagnostics) { }

    public override IOptionalWithDiagnostics<RawUnitAliasDefinition> Process(IUnitProcessingContext context, UnprocessedUnitAliasDefinition definition)
    {
        return VerifyRequiredPropertiesSet(definition)
            .Validate(() => ValidateUnitName(context, definition))
            .Validate(() => ValidateDependantOn(context, definition))
            .Merge(() => ProcessPlural(context, definition))
            .Transform((interpretedPlural) => ProduceResult(definition, interpretedPlural));
    }

    private static RawUnitAliasDefinition ProduceResult(UnprocessedUnitAliasDefinition definition, string interpretedPlural)
    {
        return new(definition.Name!, interpretedPlural, definition.AliasOf!, definition.Locations);
    }
}
