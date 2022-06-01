namespace SharpMeasures.Generators.Attributes.Parsing.Units;

using SharpMeasures.Generators.Diagnostics;

using System;

public class ScaledUnitProcesser : ADependantUnitProcesser<IDependantUnitProcessingContext, RawScaledUnitDefinition, ScaledUnitDefinition>
{
    public ScaledUnitProcesser(IDependantUnitDiagnostics<RawScaledUnitDefinition> diagnostics) : base(diagnostics) { }

    public override IOptionalWithDiagnostics<ScaledUnitDefinition> Process(IDependantUnitProcessingContext context, RawScaledUnitDefinition definition)
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
            return OptionalWithDiagnostics.Empty<ScaledUnitDefinition>(validity.Diagnostics);
        }

        ScaledUnitDefinition product = new(definition.Name!, definition.ParsingData.InterpretedPlural!, definition.From!, definition.Scale, definition.Locations);
        return OptionalWithDiagnostics.Result(product, validity.Diagnostics);
    }
}