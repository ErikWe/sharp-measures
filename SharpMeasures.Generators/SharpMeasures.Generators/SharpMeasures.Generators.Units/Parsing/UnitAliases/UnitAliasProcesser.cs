namespace SharpMeasures.Generators.Units.Parsing.UnitAlias;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Units.Parsing.Abstractions;

internal class UnitAliasProcesser : ADependantUnitProcesser<IDependantUnitProcessingContext, RawUnitAliasDefinition, UnitAliasDefinition>
{
    public UnitAliasProcesser(IDependantUnitProcessingDiagnostics<RawUnitAliasDefinition> diagnostics) : base(diagnostics) { }

    public override IOptionalWithDiagnostics<UnitAliasDefinition> Process(IDependantUnitProcessingContext context, RawUnitAliasDefinition definition)
    {
        if (VerifyRequiredPropertiesSet(definition) is false)
        {
            return OptionalWithDiagnostics.Empty<UnitAliasDefinition>();
        }

        var validity = CheckDependantUnitValidity(context, definition);

        if (validity.IsInvalid)
        {
            return OptionalWithDiagnostics.Empty<UnitAliasDefinition>(validity.Diagnostics);
        }

        UnitAliasDefinition product = new(definition.Name!, definition.ParsingData.InterpretedPlural!, definition.AliasOf!, definition.Locations);
        return OptionalWithDiagnostics.Result(product, validity.Diagnostics);
    }
}
