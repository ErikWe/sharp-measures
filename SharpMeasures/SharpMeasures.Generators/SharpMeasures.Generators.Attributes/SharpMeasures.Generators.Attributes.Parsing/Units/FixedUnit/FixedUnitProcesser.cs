namespace SharpMeasures.Generators.Attributes.Parsing.Units;

using SharpMeasures.Generators.Diagnostics;

using System;

public class FixedUnitProcesser : AUnitProcesser<IUnitProcessingContext, RawFixedUnit, FixedUnit>
{
    public FixedUnitProcesser(IUnitDiagnostics<RawFixedUnit> diagnostics) : base(diagnostics) { }

    public override IOptionalWithDiagnostics<FixedUnit> Process(IUnitProcessingContext context, RawFixedUnit definition)
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
            return OptionalWithDiagnostics.Empty<FixedUnit>(validity.Diagnostics);
        }

        FixedUnit product = new(definition.Name!, definition.ParsingData.InterpretedPlural!, definition.Value, definition.Bias, definition.Locations);
        return OptionalWithDiagnostics.Result(product, validity.Diagnostics);
    }
}