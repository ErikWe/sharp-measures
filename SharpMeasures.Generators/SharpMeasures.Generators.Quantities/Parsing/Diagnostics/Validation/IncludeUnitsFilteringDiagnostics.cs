namespace SharpMeasures.Generators.Quantities.Parsing.Diagnostics.Validation;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Quantities.Parsing.IncludeUnits;

public class IncludeUnitsFilteringDiagnostics : IIncludeUnitsFilteringDiagnostics
{
    public static IncludeUnitsFilteringDiagnostics Instance { get; } = new();

    private IncludeUnitsFilteringDiagnostics() { }

    public Diagnostic UnionInclusionStackingModeRedundant(IIncludeUnitsFilteringContext context, IncludeUnitsDefinition definition)
    {
        return DiagnosticConstruction.UnionInclusionStackingModeRedundant(definition.Locations.StackingMode?.AsRoslynLocation(), context.Type.Name);
    }

    public Diagnostic UnrecognizedUnit(IIncludeUnitsFilteringContext context, IncludeUnitsDefinition definition, int index)
    {
        return DiagnosticConstruction.UnrecognizedUnitName(definition.Locations.IncludeUnitsElements[definition.LocationMap[index]].AsRoslynLocation(), definition.IncludedUnits[index], context.UnitType.Type.Name);
    }

    public Diagnostic UnitAlreadyIncluded(IIncludeUnitsFilteringContext context, IncludeUnitsDefinition definition, int index)
    {
        return DiagnosticConstruction.IncludingAlreadyIncludedUnitWithUnion<ExcludeUnitsAttribute>(definition.Locations.IncludeUnitsElements[definition.LocationMap[index]].AsRoslynLocation(), definition.IncludedUnits[index]);
    }

    public Diagnostic UnitExcluded(IIncludeUnitsFilteringContext context, IncludeUnitsDefinition definition, int index)
    {
        return DiagnosticConstruction.IncludingExcludedUnit(definition.Locations.IncludeUnitsElements[definition.LocationMap[index]].AsRoslynLocation(), definition.IncludedUnits[index]);
    }
}
