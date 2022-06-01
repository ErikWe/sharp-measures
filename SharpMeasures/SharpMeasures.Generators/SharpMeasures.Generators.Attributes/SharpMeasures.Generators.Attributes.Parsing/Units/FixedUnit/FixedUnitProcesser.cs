namespace SharpMeasures.Generators.Attributes.Parsing.Units;

using SharpMeasures.Generators.Diagnostics;

using System;

public class FixedUnitProcesser : AUnitProcesser<IUnitProcessingContext, RawFixedUnitDefinition, FixedUnitDefinition>
{
    public FixedUnitProcesser(IUnitDiagnostics<RawFixedUnitDefinition> diagnostics) : base(diagnostics) { }

    public override IOptionalWithDiagnostics<FixedUnitDefinition> Process(IUnitProcessingContext context, RawFixedUnitDefinition definition)
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
            return OptionalWithDiagnostics.Empty<FixedUnitDefinition>(validity.Diagnostics);
        }

        FixedUnitDefinition product = new(definition.Name!, definition.ParsingData.InterpretedPlural!, definition.Value, definition.Bias, definition.Locations);
        return OptionalWithDiagnostics.Result(product, validity.Diagnostics);
    }
}