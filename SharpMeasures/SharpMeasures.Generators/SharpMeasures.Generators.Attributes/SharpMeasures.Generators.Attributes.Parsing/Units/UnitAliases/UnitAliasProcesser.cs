namespace SharpMeasures.Generators.Attributes.Parsing.Units;

using SharpMeasures.Generators.Diagnostics;

using System;

public class UnitAliasProcesser : ADependantUnitProcesser<IDependantUnitProcessingContext, RawUnitAlias, UnitAlias>
{
    public UnitAliasProcesser(IDependantUnitDiagnostics<RawUnitAlias> diagnostics) : base(diagnostics) { }

    public override IOptionalWithDiagnostics<UnitAlias> Process(IDependantUnitProcessingContext context, RawUnitAlias definition)
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
            return OptionalWithDiagnostics.Empty<UnitAlias>(validity.Diagnostics);
        }

        UnitAlias product = new(definition.Name!, definition.ParsingData.InterpretedPlural!, definition.AliasOf!, definition.Locations);
        return OptionalWithDiagnostics.Result(product, validity.Diagnostics);
    }
}