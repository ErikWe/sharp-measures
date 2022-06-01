namespace SharpMeasures.Generators.Attributes.Parsing.Units;

using SharpMeasures.Generators.Diagnostics;

using System;

public class OffsetUnitProcesser : ADependantUnitProcesser<IDependantUnitProcessingContext, RawOffsetUnitDefinition, OffsetUnitDefinition>
{
    public OffsetUnitProcesser(IDependantUnitDiagnostics<RawOffsetUnitDefinition> diagnostics) : base(diagnostics) { }

    public override IOptionalWithDiagnostics<OffsetUnitDefinition> Process(IDependantUnitProcessingContext context, RawOffsetUnitDefinition definition)
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
            return OptionalWithDiagnostics.Empty<OffsetUnitDefinition>(validity.Diagnostics);
        }

        OffsetUnitDefinition product = new(definition.Name!, definition.ParsingData.InterpretedPlural!, definition.From!, definition.Offset, definition.Locations);
        return OptionalWithDiagnostics.Result(product, validity.Diagnostics);
    }
}