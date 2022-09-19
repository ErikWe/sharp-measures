namespace SharpMeasures.Generators.Quantities.Parsing.Diagnostics.Validation;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Quantities.Parsing.ExcludeUnits;

public sealed class ExcludeUnitsFilteringDiagnostics : IExcludeUnitsFilteringDiagnostics
{
    public static ExcludeUnitsFilteringDiagnostics Instance { get; } = new();

    private ExcludeUnitsFilteringDiagnostics() { }

    public Diagnostic UnrecognizedUnitInstance(IExcludeUnitsFilteringContext context, ExcludeUnitsDefinition definition, int index)
    {
        return DiagnosticConstruction.UnrecognizedUnitInstanceName(definition.Locations.UnitInstancesElements[definition.LocationMap[index]].AsRoslynLocation(), definition.UnitInstances[index], context.UnitType.Type.Name);
    }

    public Diagnostic? UnitInstanceAlreadyExcluded(IExcludeUnitsFilteringContext context, ExcludeUnitsDefinition definition, int index)
    {
        return DiagnosticConstruction.ExcludingAlreadyExcludedUnitInstance(definition.Locations.UnitInstancesElements[definition.LocationMap[index]].AsRoslynLocation(), definition.UnitInstances[index]);
    }
}
