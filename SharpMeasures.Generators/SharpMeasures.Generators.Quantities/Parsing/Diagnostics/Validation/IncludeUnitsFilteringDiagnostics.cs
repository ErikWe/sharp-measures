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

    public Diagnostic UnrecognizedUnitInstance(IIncludeUnitsFilteringContext context, IncludeUnitsDefinition definition, int index)
    {
        return DiagnosticConstruction.UnrecognizedUnitInstanceName(definition.Locations.UnitInstancesElements[definition.LocationMap[index]].AsRoslynLocation(), definition.UnitInstances[index], context.UnitType.Type.Name);
    }

    public Diagnostic UnitInstanceAlreadyIncluded(IIncludeUnitsFilteringContext context, IncludeUnitsDefinition definition, int index)
    {
        return DiagnosticConstruction.IncludingAlreadyIncludedUnitInstanceWithUnion<ExcludeUnitsAttribute>(definition.Locations.UnitInstancesElements[definition.LocationMap[index]].AsRoslynLocation(), definition.UnitInstances[index]);
    }

    public Diagnostic UnitInstanceExcluded(IIncludeUnitsFilteringContext context, IncludeUnitsDefinition definition, int index)
    {
        return DiagnosticConstruction.IncludingExcludedUnitInstance(definition.Locations.UnitInstancesElements[definition.LocationMap[index]].AsRoslynLocation(), definition.UnitInstances[index]);
    }
}
