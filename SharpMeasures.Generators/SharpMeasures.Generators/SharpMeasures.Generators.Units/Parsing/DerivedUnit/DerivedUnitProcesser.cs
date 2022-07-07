namespace SharpMeasures.Generators.Units.Parsing.DerivedUnit;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Units.Parsing.Abstractions;

using System.Collections.Generic;
using System.Linq;

internal interface IDerivedUnitProcessingDiagnostics : IUnitProcessingDiagnostics<RawDerivedUnitDefinition, DerivedUnitLocations>
{
    public abstract Diagnostic? EmptyUnitList(IUnitProcessingContext context, RawDerivedUnitDefinition definition);
    public abstract Diagnostic? NullUnitElement(IUnitProcessingContext context, RawDerivedUnitDefinition definition, int index);
    public abstract Diagnostic? EmptyUnitElement(IUnitProcessingContext context, RawDerivedUnitDefinition definition, int index);
}

internal class DerivedUnitProcesser : AUnitProcesser<IUnitProcessingContext, RawDerivedUnitDefinition, DerivedUnitLocations, UnresolvedDerivedUnitDefinition>
{
    private IDerivedUnitProcessingDiagnostics Diagnostics { get; }

    public DerivedUnitProcesser(IDerivedUnitProcessingDiagnostics diagnostics) : base(diagnostics)
    {
        Diagnostics = diagnostics;
    }

    public override IOptionalWithDiagnostics<UnresolvedDerivedUnitDefinition> Process(IUnitProcessingContext context, RawDerivedUnitDefinition definition)
    {
        if (VerifyRequiredPropertiesSet(definition) is false)
        {
            return OptionalWithDiagnostics.Empty<UnresolvedDerivedUnitDefinition>();
        }

        var validity = CheckUnitValidity(context, definition);
        IEnumerable<Diagnostic> allDiagnostics = validity.Diagnostics;

        if (validity.IsInvalid)
        {
            return OptionalWithDiagnostics.Empty<UnresolvedDerivedUnitDefinition>(allDiagnostics);
        }

        var processedPlural = ProcessPlural(context, definition);
        allDiagnostics = allDiagnostics.Concat(processedPlural.Diagnostics);

        if (processedPlural.LacksResult)
        {
            return OptionalWithDiagnostics.Empty<UnresolvedDerivedUnitDefinition>(allDiagnostics);
        }

        var processedUnits = ProcessUnits(context, definition);
        allDiagnostics = allDiagnostics.Concat(processedUnits.Diagnostics);

        if (processedUnits.LacksResult)
        {
            return OptionalWithDiagnostics.Empty<UnresolvedDerivedUnitDefinition>(allDiagnostics);
        }

        UnresolvedDerivedUnitDefinition product = new(definition.Name!, processedPlural.Result, definition.SignatureID, processedUnits.Result, definition.Locations);

        return OptionalWithDiagnostics.Result(product, allDiagnostics);
    }

    private IOptionalWithDiagnostics<IReadOnlyList<string>> ProcessUnits(IUnitProcessingContext context, RawDerivedUnitDefinition definition)
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
