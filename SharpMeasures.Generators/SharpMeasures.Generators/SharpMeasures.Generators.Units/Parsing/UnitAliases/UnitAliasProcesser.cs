namespace SharpMeasures.Generators.Units.Parsing.UnitAlias;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Units.Parsing.Abstractions;

using System.Linq;

internal class UnitAliasProcesser : ADependantUnitProcesser<IUnitProcessingContext, RawUnitAliasDefinition, UnitAliasLocations, UnresolvedUnitAliasDefinition>
{
    public UnitAliasProcesser(IDependantUnitProcessingDiagnostics<RawUnitAliasDefinition, UnitAliasLocations> diagnostics) : base(diagnostics) { }

    public override IOptionalWithDiagnostics<UnresolvedUnitAliasDefinition> Process(IUnitProcessingContext context, RawUnitAliasDefinition definition)
    {
        if (VerifyRequiredPropertiesSet(definition) is false)
        {
            return OptionalWithDiagnostics.Empty<UnresolvedUnitAliasDefinition>();
        }

        var validity = CheckDependantUnitValidity(context, definition);
        var allDiagnostics = validity.Diagnostics;

        if (validity.IsInvalid)
        {
            return OptionalWithDiagnostics.Empty<UnresolvedUnitAliasDefinition>(allDiagnostics);
        }

        var processedPlural = ProcessPlural(context, definition);
        allDiagnostics = allDiagnostics.Concat(processedPlural.Diagnostics);

        if (processedPlural.LacksResult)
        {
            return OptionalWithDiagnostics.Empty<UnresolvedUnitAliasDefinition>(allDiagnostics);
        }

        UnresolvedUnitAliasDefinition product = new(definition.Name!, processedPlural.Result, definition.AliasOf!, definition.Locations);
        return OptionalWithDiagnostics.Result(product, allDiagnostics);
    }
}
