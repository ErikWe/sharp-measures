namespace SharpMeasures.Generators.Units.Parsing.DerivedUnit;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Units.Parsing.Abstractions;

using System.Collections.Generic;

internal interface IDerivedUnitProcessingDiagnostics : IUnitProcessingDiagnostics<UnprocessedDerivedUnitDefinition, DerivedUnitLocations>
{
    public abstract Diagnostic? EmptyUnitList(IUnitProcessingContext context, UnprocessedDerivedUnitDefinition definition);
    public abstract Diagnostic? NullUnitElement(IUnitProcessingContext context, UnprocessedDerivedUnitDefinition definition, int index);
    public abstract Diagnostic? EmptyUnitElement(IUnitProcessingContext context, UnprocessedDerivedUnitDefinition definition, int index);
}

internal class DerivedUnitProcesser : AUnitProcesser<IUnitProcessingContext, UnprocessedDerivedUnitDefinition, DerivedUnitLocations, RawDerivedUnitDefinition>
{
    private IDerivedUnitProcessingDiagnostics Diagnostics { get; }

    public DerivedUnitProcesser(IDerivedUnitProcessingDiagnostics diagnostics) : base(diagnostics)
    {
        Diagnostics = diagnostics;
    }

    public override IOptionalWithDiagnostics<RawDerivedUnitDefinition> Process(IUnitProcessingContext context, UnprocessedDerivedUnitDefinition definition)
    {
        var interpretedPlural = VerifyRequiredPropertiesSet(definition)
            .Validate(() => ValidateUnitName(context, definition))
            .Merge(() => ProcessPlural(context, definition));

        return interpretedPlural.Reduce().Merge(() => ProcessUnits(context, definition))
            .Transform((units) => ProduceResult(definition, interpretedPlural.Result, units));
    }

    private static RawDerivedUnitDefinition ProduceResult(UnprocessedDerivedUnitDefinition definition, string interpretedPlural, IReadOnlyList<string> units)
    {
        return new(definition.Name!, interpretedPlural, definition.DerivationID, units, definition.Locations);
    }

    private IOptionalWithDiagnostics<IReadOnlyList<string>> ProcessUnits(IUnitProcessingContext context, UnprocessedDerivedUnitDefinition definition)
    {
        if (definition.Units.Count is 0)
        {
            return OptionalWithDiagnostics.Empty<IReadOnlyList<string>>(Diagnostics.EmptyUnitList(context, definition));
        }

        string[] units = new string[definition.Units.Count];

        for (int i = 0; i < definition.Units.Count; i++)
        {
            if (definition.Units[i] is not string unit)
            {
                return OptionalWithDiagnostics.Empty<IReadOnlyList<string>>(Diagnostics.NullUnitElement(context, definition, i));
            }

            if (unit.Length is 0)
            {
                return OptionalWithDiagnostics.Empty<IReadOnlyList<string>>(Diagnostics.EmptyUnitElement(context, definition, i));
            }

            units[i] = unit;
        }

        return OptionalWithDiagnostics.Result(units as IReadOnlyList<string>);
    }
}
