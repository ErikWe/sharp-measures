namespace SharpMeasures.Generators.Units.Parsing.DerivedUnitInstance;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Units.Parsing.Abstractions;

using System.Collections.Generic;

internal interface IDerivedUnitInstanceProcessingDiagnostics : IUnitInstanceProcessingDiagnostics<RawDerivedUnitInstanceDefinition, DerivedUnitInstanceLocations>
{
    public abstract Diagnostic? EmptyUnitList(IUnitInstanceProcessingContext context, RawDerivedUnitInstanceDefinition definition);
    public abstract Diagnostic? NullUnitsElement(IUnitInstanceProcessingContext context, RawDerivedUnitInstanceDefinition definition, int index);
    public abstract Diagnostic? EmptyUnitsElement(IUnitInstanceProcessingContext context, RawDerivedUnitInstanceDefinition definition, int index);
}

internal class DerivedUnitInstanceProcesser : AUnitInstanceProcesser<IUnitInstanceProcessingContext, RawDerivedUnitInstanceDefinition, DerivedUnitInstanceLocations, DerivedUnitInstanceDefinition>
{
    private IDerivedUnitInstanceProcessingDiagnostics Diagnostics { get; }

    public DerivedUnitInstanceProcesser(IDerivedUnitInstanceProcessingDiagnostics diagnostics) : base(diagnostics)
    {
        Diagnostics = diagnostics;
    }

    public override IOptionalWithDiagnostics<DerivedUnitInstanceDefinition> Process(IUnitInstanceProcessingContext context, RawDerivedUnitInstanceDefinition definition)
    {
        var interpretedPluralForm = VerifyRequiredPropertiesSet(definition)
            .Validate(() => ValidateUnitInstanceName(context, definition))
            .Merge(() => ProcessUnitInstancePluralForm(context, definition));

        if (interpretedPluralForm.LacksResult)
        {
            return interpretedPluralForm.AsEmptyOptional<DerivedUnitInstanceDefinition>();
        }

        return ProcessUnits(context, definition)
            .Transform((units) => ProduceResult(definition, interpretedPluralForm.Result, units))
            .AddDiagnostics(interpretedPluralForm);
    }

    private static DerivedUnitInstanceDefinition ProduceResult(RawDerivedUnitInstanceDefinition definition, string interpretedPluralForm, IReadOnlyList<string> units)
    {
        return new(definition.Name!, interpretedPluralForm, definition.DerivationID, units, definition.Locations);
    }

    private IOptionalWithDiagnostics<IReadOnlyList<string>> ProcessUnits(IUnitInstanceProcessingContext context, RawDerivedUnitInstanceDefinition definition)
    {
        var units = ValidateUnitListNotEmpty(context, definition)
            .Transform(() => new string[definition.Units.Count]);

        for (int i = 0; i < definition.Units.Count; i++)
        {
            units = units.Merge((unitList) => ProcessElement(context, definition, i, unitList));
        }

        return units.Transform((unitList) => (IReadOnlyList<string>)unitList);
    }

    private IValidityWithDiagnostics ValidateUnitListNotEmpty(IUnitInstanceProcessingContext context, RawDerivedUnitInstanceDefinition definition)
    {
        return ValidityWithDiagnostics.Conditional(definition.Units.Count is not 0, () => Diagnostics.EmptyUnitList(context, definition));
    }

    private IOptionalWithDiagnostics<string[]> ProcessElement(IUnitInstanceProcessingContext context, RawDerivedUnitInstanceDefinition definition, int index, string[] units)
    {
        return ValidateElementNotNull(context, definition, index)
            .Validate(() => ValidateElementNotEmpty(context, definition, index))
            .Transform(() => AppendValidElement(definition, index, units));
    }

    private static string[] AppendValidElement(RawDerivedUnitInstanceDefinition definition, int index, string[] units)
    {
        units[index] = definition.Units[index]!;

        return units;
    }

    private IValidityWithDiagnostics ValidateElementNotNull(IUnitInstanceProcessingContext context, RawDerivedUnitInstanceDefinition definition, int index)
    {
        return ValidityWithDiagnostics.Conditional(definition.Units[index] is not null, () => Diagnostics.NullUnitsElement(context, definition, index));
    }

    private IValidityWithDiagnostics ValidateElementNotEmpty(IUnitInstanceProcessingContext context, RawDerivedUnitInstanceDefinition definition, int index)
    {
        return ValidityWithDiagnostics.Conditional(definition.Units[index]!.Length is not 0, () => Diagnostics.EmptyUnitsElement(context, definition, index));
    }
}
