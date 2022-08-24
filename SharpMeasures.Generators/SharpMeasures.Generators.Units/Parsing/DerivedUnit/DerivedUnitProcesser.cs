namespace SharpMeasures.Generators.Units.Parsing.DerivedUnit;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Units.Parsing.Abstractions;

using System.Collections.Generic;

internal interface IDerivedUnitProcessingDiagnostics : IUnitProcessingDiagnostics<RawDerivedUnitDefinition, DerivedUnitLocations>
{
    public abstract Diagnostic? EmptyUnitList(IUnitProcessingContext context, RawDerivedUnitDefinition definition);
    public abstract Diagnostic? NullUnitElement(IUnitProcessingContext context, RawDerivedUnitDefinition definition, int index);
    public abstract Diagnostic? EmptyUnitElement(IUnitProcessingContext context, RawDerivedUnitDefinition definition, int index);
}

internal class DerivedUnitProcesser : AUnitProcesser<IUnitProcessingContext, RawDerivedUnitDefinition, DerivedUnitLocations, DerivedUnitDefinition>
{
    private IDerivedUnitProcessingDiagnostics Diagnostics { get; }

    public DerivedUnitProcesser(IDerivedUnitProcessingDiagnostics diagnostics) : base(diagnostics)
    {
        Diagnostics = diagnostics;
    }

    public override IOptionalWithDiagnostics<DerivedUnitDefinition> Process(IUnitProcessingContext context, RawDerivedUnitDefinition definition)
    {
        var interpretedPlural = VerifyRequiredPropertiesSet(definition)
            .Validate(() => ValidateUnitName(context, definition))
            .Merge(() => ProcessPlural(context, definition));

        if (interpretedPlural.LacksResult)
        {
            return interpretedPlural.AsEmptyOptional<DerivedUnitDefinition>();
        }

        return ProcessUnits(context, definition)
            .Transform((units) => ProduceResult(definition, interpretedPlural.Result, units))
            .ConcatDiagnostics(interpretedPlural);
    }

    private static DerivedUnitDefinition ProduceResult(RawDerivedUnitDefinition definition, string interpretedPlural, IReadOnlyList<string> units)
    {
        return new(definition.Name!, interpretedPlural, definition.DerivationID, units, definition.Locations);
    }

    private IOptionalWithDiagnostics<IReadOnlyList<string>> ProcessUnits(IUnitProcessingContext context, RawDerivedUnitDefinition definition)
    {
        var units = ValidateUnitListNotEmpty(context, definition)
            .Transform(() => new string[definition.Units.Count] as IList<string>);

        for (int i = 0; i < definition.Units.Count; i++)
        {
            units = units.Merge((unitList) => ProcessElement(context, definition, i, unitList));
        }

        return units.Transform((unitList) => (IReadOnlyList<string>)unitList);
    }

    private IValidityWithDiagnostics ValidateUnitListNotEmpty(IUnitProcessingContext context, RawDerivedUnitDefinition definition)
    {
        return ValidityWithDiagnostics.Conditional(definition.Units.Count is not 0, () => Diagnostics.EmptyUnitList(context, definition));
    }

    private IOptionalWithDiagnostics<IList<string>> ProcessElement(IUnitProcessingContext context, RawDerivedUnitDefinition definition, int index, IList<string> units)
    {
        return ValidateElementNotNull(context, definition, index)
            .Validate(() => ValidateElementNotEmpty(context, definition, index))
            .Transform(() => AppendValidElement(definition, index, units));
    }

    private static IList<string> AppendValidElement(RawDerivedUnitDefinition definition, int index, IList<string> units)
    {
        units.Add(definition.Units[index]!);

        return units;
    }

    private IValidityWithDiagnostics ValidateElementNotNull(IUnitProcessingContext context, RawDerivedUnitDefinition definition, int index)
    {
        return ValidityWithDiagnostics.Conditional(definition.Units[index] is not null, () => Diagnostics.NullUnitElement(context, definition, index));
    }

    private IValidityWithDiagnostics ValidateElementNotEmpty(IUnitProcessingContext context, RawDerivedUnitDefinition definition, int index)
    {
        return ValidityWithDiagnostics.Conditional(definition.Units[index]!.Length is not 0, () => Diagnostics.EmptyUnitElement(context, definition, index));
    }
}
