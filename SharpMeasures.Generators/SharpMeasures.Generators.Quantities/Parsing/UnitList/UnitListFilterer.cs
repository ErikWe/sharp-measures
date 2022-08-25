namespace SharpMeasures.Generators.Quantities.Parsing.UnitList;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Units;

using System.Collections.Generic;

public interface IUnitListFilteringDiagnostics
{
    public abstract Diagnostic? UnrecognizedUnit(IUnitListFilteringContext context, UnitListDefinition definition, int index);
    public abstract Diagnostic? UnitAlreadyListedThroughInheritance(IUnitListFilteringContext context, UnitListDefinition definition, int index);
}

public interface IUnitListFilteringContext : IProcessingContext
{
    public IUnitType UnitType { get; }

    public HashSet<string> InheritedUnits { get; }
}

public class UnitListFilterer : AProcesser<IUnitListFilteringContext, UnitListDefinition, UnitListDefinition>
{
    private IUnitListFilteringDiagnostics Diagnostics { get; }

    public UnitListFilterer(IUnitListFilteringDiagnostics diagnostics)
    {
        Diagnostics = diagnostics;
    }

    public override IOptionalWithDiagnostics<UnitListDefinition> Process(IUnitListFilteringContext context, UnitListDefinition definition)
    {
        List<string> validUnits = new();
        List<int> locationMap = new();

        List<Diagnostic> allDiagnostics = new();

        for (var i = 0; i < definition.Units.Count; i++)
        {
            var validity = ValidateUnitRecognized(context, definition, i)
                .Validate(() => ValidateUnitNotAlreadyListedThroughInheritance(context, definition, i));

            allDiagnostics.AddRange(validity);

            if (validity.IsValid)
            {
                validUnits.Add(definition.Units[i]);
                locationMap.Add(i);
            }
        }

        return OptionalWithDiagnostics.ConditionalWithDefiniteDiagnostics(validUnits.Count is not 0, () => new UnitListDefinition(validUnits, definition.Locations, locationMap), allDiagnostics);
    }

    private IValidityWithDiagnostics ValidateUnitRecognized(IUnitListFilteringContext context, UnitListDefinition definition, int index)
    {
        var recognizedUnit = context.UnitType.UnitsByName.ContainsKey(definition.Units[index]);

        return ValidityWithDiagnostics.Conditional(recognizedUnit, () => Diagnostics.UnrecognizedUnit(context, definition, index));
    }

    private IValidityWithDiagnostics ValidateUnitNotAlreadyListedThroughInheritance(IUnitListFilteringContext context, UnitListDefinition definition, int index)
    {
        var unitNotInherited = context.InheritedUnits.Contains(definition.Units[index]) is false;

        return ValidityWithDiagnostics.Conditional(unitNotInherited, () => Diagnostics.UnitAlreadyListedThroughInheritance(context, definition, index));
    }
}
