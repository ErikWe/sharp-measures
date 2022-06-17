namespace SharpMeasures.Generators.Units.Parsing.FixedUnit;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Units.Parsing.Abstractions;

internal class FixedUnitProcesser : AUnitProcesser<IUnitProcessingContext, RawFixedUnitDefinition, FixedUnitDefinition>
{
    public FixedUnitProcesser(IUnitProcessingDiagnostics<RawFixedUnitDefinition> diagnostics) : base(diagnostics) { }

    public override IOptionalWithDiagnostics<FixedUnitDefinition> Process(IUnitProcessingContext context, RawFixedUnitDefinition definition)
    {
        if (VerifyRequiredPropertiesSet(definition) is false)
        {
            return OptionalWithDiagnostics.Empty<FixedUnitDefinition>();
        }

        var validity = CheckUnitValidity(context, definition);

        if (validity.IsInvalid)
        {
            return OptionalWithDiagnostics.Empty<FixedUnitDefinition>(validity.Diagnostics);
        }

        FixedUnitDefinition product = new(definition.Name!, definition.ParsingData.InterpretedPlural!, definition.Value, definition.Bias, definition.Locations);
        return OptionalWithDiagnostics.Result(product, validity.Diagnostics);
    }
}
