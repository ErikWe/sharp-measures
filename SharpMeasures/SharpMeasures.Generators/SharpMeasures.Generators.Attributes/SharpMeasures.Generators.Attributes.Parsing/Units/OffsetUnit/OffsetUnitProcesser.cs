namespace SharpMeasures.Generators.Attributes.Parsing.Units;

using SharpMeasures.Generators.Diagnostics;

using System;

public class OffsetUnitProcesser : ADependantUnitProcesser<IDependantUnitProcessingContext, RawOffsetUnit, OffsetUnit>
{
    public OffsetUnitProcesser(IDependantUnitDiagnostics<RawOffsetUnit> diagnostics) : base(diagnostics) { }

    public override IOptionalWithDiagnostics<OffsetUnit> Process(IDependantUnitProcessingContext context, RawOffsetUnit definition)
    {
        if (context is null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        if (definition is null)
        {
            throw new ArgumentNullException(nameof(definition));
        }

        var validity = CheckDependantUnitValidity(context, definition);

        if (validity.IsInvalid)
        {
            return OptionalWithDiagnostics.Empty<OffsetUnit>(validity.Diagnostics);
        }

        OffsetUnit product = new(definition.Name!, definition.ParsingData.InterpretedPlural!, definition.From!, definition.Offset, definition.Locations);
        return OptionalWithDiagnostics.Result(product, validity.Diagnostics);
    }
}