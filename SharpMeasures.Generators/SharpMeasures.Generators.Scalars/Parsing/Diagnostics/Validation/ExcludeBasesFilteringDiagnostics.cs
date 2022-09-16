namespace SharpMeasures.Generators.Scalars.Parsing.Diagnostics.Validation;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Scalars.Parsing.ExcludeUnitBases;

internal class ExcludeBasesFilteringDiagnostics : IExcludeUnitBasesFilteringDiagnostics
{
    public static ExcludeBasesFilteringDiagnostics Instance { get; } = new();

    private ExcludeBasesFilteringDiagnostics() { }

    public Diagnostic UnrecognizedUnitInstance(IExcludeUnitBasesFilteringContext context, ExcludeUnitBasesDefinition definition, int index)
    {
        return DiagnosticConstruction.UnrecognizedUnitInstanceName(definition.Locations.UnitInstancesElements[definition.LocationMap[index]].AsRoslynLocation(), definition.UnitInstances[index], context.UnitType.Type.Name);
    }

    public Diagnostic? UnitInstanceAlreadyExcluded(IExcludeUnitBasesFilteringContext context, ExcludeUnitBasesDefinition definition, int index)
    {
        return DiagnosticConstruction.ExcludingAlreadyExcludedUnitInstance(definition.Locations.UnitInstancesElements[definition.LocationMap[index]].AsRoslynLocation(), definition.UnitInstances[index]);
    }
}
