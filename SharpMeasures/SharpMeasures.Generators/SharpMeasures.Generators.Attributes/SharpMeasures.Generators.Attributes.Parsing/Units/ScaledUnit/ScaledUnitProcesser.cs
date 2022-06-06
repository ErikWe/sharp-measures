namespace SharpMeasures.Generators.Attributes.Parsing.Units;

using SharpMeasures.Generators.Diagnostics;

using System;

public class ScaledUnitProcesser : ADependantUnitProcesser<IDependantUnitProcessingContext, RawScaledUnit, ScaledUnit>
{
    public ScaledUnitProcesser(IDependantUnitDiagnostics<RawScaledUnit> diagnostics) : base(diagnostics) { }

    public override IOptionalWithDiagnostics<ScaledUnit> Process(IDependantUnitProcessingContext context, RawScaledUnit definition)
    {
        if (context is null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        if (definition is null)
        {
            throw new ArgumentNullException(nameof(definition));
        }

        var validity = CheckUnitValidity(context, definition);
        
        if (validity.IsInvalid)
        {
            return OptionalWithDiagnostics.Empty<ScaledUnit>(validity.Diagnostics);
        }

        ScaledUnit product = new(definition.Name!, definition.ParsingData.InterpretedPlural!, definition.From!, definition.Scale, definition.Locations);
        return OptionalWithDiagnostics.Result(product, validity.Diagnostics);
    }
}