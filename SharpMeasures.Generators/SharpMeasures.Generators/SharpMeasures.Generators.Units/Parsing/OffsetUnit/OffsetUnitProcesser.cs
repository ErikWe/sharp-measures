namespace SharpMeasures.Generators.Units.Parsing.OffsetUnit;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Units.Parsing.Abstractions;

internal class OffsetUnitProcesser : ADependantUnitProcesser<IDependantUnitProcessingContext, RawOffsetUnitDefinition, OffsetUnitDefinition>
{
    public OffsetUnitProcesser(IDependantUnitProcessingDiagnostics<RawOffsetUnitDefinition> diagnostics) : base(diagnostics) { }

    public override IOptionalWithDiagnostics<OffsetUnitDefinition> Process(IDependantUnitProcessingContext context, RawOffsetUnitDefinition definition)
    {
        if (VerifyRequiredPropertiesSet(definition) is false)
        {
            return OptionalWithDiagnostics.Empty<OffsetUnitDefinition>();
        }

        var validity = CheckDependantUnitValidity(context, definition);

        if (validity.IsInvalid)
        {
            return OptionalWithDiagnostics.Empty<OffsetUnitDefinition>(validity.Diagnostics);
        }

        OffsetUnitDefinition product = new(definition.Name!, definition.ParsingData.InterpretedPlural!, definition.From!, definition.Offset, definition.Locations);
        return OptionalWithDiagnostics.Result(product, validity.Diagnostics);
    }
}
