namespace SharpMeasures.Generators.Attributes.Parsing.Units;

using SharpMeasures.Generators.Diagnostics;

using System;

public class UnitAliasProcesser : ADependantUnitProcesser<IDependantUnitProcessingContext, RawUnitAliasDefinition, UnitAliasDefinition>
{
    public UnitAliasProcesser(IDependantUnitDiagnostics<RawUnitAliasDefinition> diagnostics) : base(diagnostics) { }

    public override IOptionalWithDiagnostics<UnitAliasDefinition> Process(IDependantUnitProcessingContext context, RawUnitAliasDefinition definition)
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
            return OptionalWithDiagnostics.Empty<UnitAliasDefinition>(validity.Diagnostics);
        }

        UnitAliasDefinition product = new(definition.Name!, definition.ParsingData.InterpretedPlural!, definition.AliasOf!, definition.Locations);
        return OptionalWithDiagnostics.Result(product, validity.Diagnostics);
    }
}