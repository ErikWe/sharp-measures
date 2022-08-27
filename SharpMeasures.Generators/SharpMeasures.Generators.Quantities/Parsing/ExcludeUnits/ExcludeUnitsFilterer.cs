namespace SharpMeasures.Generators.Quantities.Parsing.ExcludeUnits;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Units;

using System.Collections.Generic;

public interface IExcludeUnitsFilteringDiagnostics
{
    public abstract Diagnostic? UnrecognizedUnit(IExcludeUnitsFilteringContext context, ExcludeUnitsDefinition definition, int index);
    public abstract Diagnostic? UnitAlreadyExcluded(IExcludeUnitsFilteringContext context, ExcludeUnitsDefinition definition, int index);
}

public interface IExcludeUnitsFilteringContext : IProcessingContext
{
    public IUnitType UnitType { get; }

    public HashSet<string> IncludedUnits { get; }
}

public class ExcludeUnitsFilterer : AProcesser<IExcludeUnitsFilteringContext, ExcludeUnitsDefinition, ExcludeUnitsDefinition>
{
    private IExcludeUnitsFilteringDiagnostics Diagnostics { get; }

    public ExcludeUnitsFilterer(IExcludeUnitsFilteringDiagnostics diagnostics)
    {
        Diagnostics = diagnostics;
    }

    public override IOptionalWithDiagnostics<ExcludeUnitsDefinition> Process(IExcludeUnitsFilteringContext context, ExcludeUnitsDefinition definition)
    {
        List<string> validUnits = new();
        List<int> locationMap = new();

        List<Diagnostic> allDiagnostics = new();

        for (var i = 0; i < definition.ExcludedUnits.Count; i++)
        {
            var validity = ValidateUnitRecognized(context, definition, i)
                .Validate(() => ValidateUnitNotAlreadyExcluded(context, definition, i));

            allDiagnostics.AddRange(validity);

            if (validity.IsValid)
            {
                validUnits.Add(definition.ExcludedUnits[i]);
                locationMap.Add(i);
            }
        }

        return OptionalWithDiagnostics.ConditionalWithDefiniteDiagnostics(validUnits.Count is not 0, () => new ExcludeUnitsDefinition(validUnits, definition.Locations, locationMap), allDiagnostics);
    }

    private IValidityWithDiagnostics ValidateUnitRecognized(IExcludeUnitsFilteringContext context, ExcludeUnitsDefinition definition, int index)
    {
        var recognizedUnit = context.UnitType.UnitsByName.ContainsKey(definition.ExcludedUnits[index]);

        return ValidityWithDiagnostics.Conditional(recognizedUnit, () => Diagnostics.UnrecognizedUnit(context, definition, index));
    }

    private IValidityWithDiagnostics ValidateUnitNotAlreadyExcluded(IExcludeUnitsFilteringContext context, ExcludeUnitsDefinition definition, int index)
    {
        var unitNotAlreadyExcluded = context.IncludedUnits.Contains(definition.ExcludedUnits[index]);

        return ValidityWithDiagnostics.Conditional(unitNotAlreadyExcluded, () => Diagnostics.UnitAlreadyExcluded(context, definition, index));
    }
}
