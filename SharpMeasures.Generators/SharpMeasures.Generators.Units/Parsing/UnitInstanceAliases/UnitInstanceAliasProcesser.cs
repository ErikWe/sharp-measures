namespace SharpMeasures.Generators.Units.Parsing.UnitInstanceAlias;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Units.Parsing.Abstractions;

internal class UnitInstanceAliasProcesser : AModifiedUnitInstanceProcesser<IUnitInstanceProcessingContext, RawUnitInstanceAliasDefinition, UnitInstanceAliasLocations, UnitInstanceAliasDefinition>
{
    public UnitInstanceAliasProcesser(IModifiedUnitInstanceProcessingDiagnostics<RawUnitInstanceAliasDefinition, UnitInstanceAliasLocations> diagnostics) : base(diagnostics) { }

    public override IOptionalWithDiagnostics<UnitInstanceAliasDefinition> Process(IUnitInstanceProcessingContext context, RawUnitInstanceAliasDefinition definition)
    {
        return VerifyRequiredPropertiesSet(definition)
            .Validate(() => ValidateUnitInstanceName(context, definition))
            .Validate(() => ValidateOriginalUnitInstance(context, definition))
            .Merge(() => ProcessUnitInstancePluralForm(context, definition))
            .Transform((interpretedPluralForm) => ProduceResult(definition, interpretedPluralForm));
    }

    private static UnitInstanceAliasDefinition ProduceResult(RawUnitInstanceAliasDefinition definition, string interpretedPluralForm)
    {
        return new(definition.Name!, interpretedPluralForm, definition.OriginalUnitInstance!, definition.Locations);
    }
}
