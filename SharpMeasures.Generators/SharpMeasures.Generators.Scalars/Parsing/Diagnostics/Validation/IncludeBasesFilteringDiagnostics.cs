namespace SharpMeasures.Generators.Scalars.Parsing.Diagnostics.Validation;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Scalars.Parsing.IncludeUnitBases;

internal class IncludeBasesFilteringDiagnostics : IIncludeUnitBasesFilteringDiagnostics
{
    public static IncludeBasesFilteringDiagnostics Instance { get; } = new();

    private IncludeBasesFilteringDiagnostics() { }

    public Diagnostic UnionInclusionStackingModeRedundant(IIncludeUnitBasesFilteringContext context, IncludeUnitBasesDefinition definition)
    {
        return DiagnosticConstruction.UnionInclusionStackingModeRedundant(definition.Locations.StackingMode?.AsRoslynLocation(), context.Type.Name);
    }

    public Diagnostic UnrecognizedUnitInstance(IIncludeUnitBasesFilteringContext context, IncludeUnitBasesDefinition definition, int index)
    {
        return DiagnosticConstruction.UnrecognizedUnitInstanceName(definition.Locations.UnitInstancesElements[definition.LocationMap[index]].AsRoslynLocation(), definition.UnitInstances[index], context.UnitType.Type.Name);
    }

    public Diagnostic UnitInstanceAlreadyIncluded(IIncludeUnitBasesFilteringContext context, IncludeUnitBasesDefinition definition, int index)
    {
        return DiagnosticConstruction.IncludingAlreadyIncludedUnitInstanceWithUnion<ExcludeUnitBasesAttribute>(definition.Locations.UnitInstancesElements[definition.LocationMap[index]].AsRoslynLocation(), definition.UnitInstances[index]);
    }

    public Diagnostic UnitInstanceExcluded(IIncludeUnitBasesFilteringContext context, IncludeUnitBasesDefinition definition, int index)
    {
        return DiagnosticConstruction.IncludingExcludedUnitInstance(definition.Locations.UnitInstancesElements[definition.LocationMap[index]].AsRoslynLocation(), definition.UnitInstances[index]);
    }
}
