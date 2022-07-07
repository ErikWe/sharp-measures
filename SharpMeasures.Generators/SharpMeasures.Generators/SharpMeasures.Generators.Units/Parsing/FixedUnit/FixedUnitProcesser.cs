namespace SharpMeasures.Generators.Units.Parsing.FixedUnit;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Units.Parsing.Abstractions;

using System.Linq;

internal class FixedUnitProcesser : AUnitProcesser<IUnitProcessingContext, RawFixedUnitDefinition, FixedUnitLocations, UnresolvedFixedUnitDefinition>
{
    public FixedUnitProcesser(IUnitProcessingDiagnostics<RawFixedUnitDefinition, FixedUnitLocations> diagnostics) : base(diagnostics) { }

    public override IOptionalWithDiagnostics<UnresolvedFixedUnitDefinition> Process(IUnitProcessingContext context, RawFixedUnitDefinition definition)
    {
        if (VerifyRequiredPropertiesSet(definition) is false)
        {
            return OptionalWithDiagnostics.Empty<UnresolvedFixedUnitDefinition>();
        }

        var validity = CheckUnitValidity(context, definition);
        var allDiagnostics = validity.Diagnostics;

        if (validity.IsInvalid)
        {
            return OptionalWithDiagnostics.Empty<UnresolvedFixedUnitDefinition>(allDiagnostics);
        }

        var processedPlural = ProcessPlural(context, definition);
        allDiagnostics = allDiagnostics.Concat(processedPlural.Diagnostics);

        if (processedPlural.LacksResult)
        {
            return OptionalWithDiagnostics.Empty<UnresolvedFixedUnitDefinition>(allDiagnostics);
        }

        UnresolvedFixedUnitDefinition product = new(definition.Name!, processedPlural.Result, definition.Locations);
        return OptionalWithDiagnostics.Result(product, allDiagnostics);
    }
}
