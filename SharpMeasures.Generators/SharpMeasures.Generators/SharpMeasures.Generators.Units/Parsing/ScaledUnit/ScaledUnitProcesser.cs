namespace SharpMeasures.Generators.Units.Parsing.ScaledUnit;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Units.Parsing.Abstractions;

internal class ScaledUnitProcesser : ADependantUnitProcesser<IDependantUnitProcessingContext, RawScaledUnitDefinition, ScaledUnitDefinition>
{
    public ScaledUnitProcesser(IDependantUnitProcessingDiagnostics<RawScaledUnitDefinition> diagnostics) : base(diagnostics) { }

    public override IOptionalWithDiagnostics<ScaledUnitDefinition> Process(IDependantUnitProcessingContext context, RawScaledUnitDefinition definition)
    {
        var validity = CheckUnitValidity(context, definition);
        
        if (validity.IsInvalid)
        {
            return OptionalWithDiagnostics.Empty<ScaledUnitDefinition>(validity.Diagnostics);
        }

        ScaledUnitDefinition product = new(definition.Name!, definition.ParsingData.InterpretedPlural!, definition.From!, definition.Scale, definition.Locations);
        return OptionalWithDiagnostics.Result(product, validity.Diagnostics);
    }
}
