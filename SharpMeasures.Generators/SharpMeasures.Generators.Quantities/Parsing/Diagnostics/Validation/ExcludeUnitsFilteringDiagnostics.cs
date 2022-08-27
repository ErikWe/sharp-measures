namespace SharpMeasures.Generators.Quantities.Parsing.Diagnostics.Validation;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Quantities.Parsing.ExcludeUnits;

public class ExcludeUnitsFilteringDiagnostics : IExcludeUnitsFilteringDiagnostics
{
    public static ExcludeUnitsFilteringDiagnostics Instance { get; } = new();

    private ExcludeUnitsFilteringDiagnostics() { }

    public Diagnostic UnrecognizedUnit(IExcludeUnitsFilteringContext context, ExcludeUnitsDefinition definition, int index)
    {
        return DiagnosticConstruction.UnrecognizedUnitName(definition.Locations.ExcludeUnitsElements[definition.LocationMap[index]].AsRoslynLocation(), definition.ExcludedUnits[index], context.UnitType.Type.Name);
    }

    public Diagnostic? UnitAlreadyExcluded(IExcludeUnitsFilteringContext context, ExcludeUnitsDefinition definition, int index)
    {
        return DiagnosticConstruction.ExcludingAlreadyExcludedUnit(definition.Locations.ExcludeUnitsElements[definition.LocationMap[index]].AsRoslynLocation(), definition.ExcludedUnits[index]);
    }
}
