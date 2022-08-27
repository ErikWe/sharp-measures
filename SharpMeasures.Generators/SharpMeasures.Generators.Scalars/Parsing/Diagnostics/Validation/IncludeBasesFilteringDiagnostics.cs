namespace SharpMeasures.Generators.Scalars.Parsing.Diagnostics.Validation;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Scalars.Parsing.IncludeBases;

internal class IncludeBasesFilteringDiagnostics : IIncludeBasesFilteringDiagnostics
{
    public static IncludeBasesFilteringDiagnostics Instance { get; } = new();

    private IncludeBasesFilteringDiagnostics() { }

    public Diagnostic UnionInclusionStackingModeRedundant(IIncludeBasesFilteringContext context, IncludeBasesDefinition definition)
    {
        return DiagnosticConstruction.UnionInclusionStackingModeRedundant(definition.Locations.StackingMode?.AsRoslynLocation(), context.Type.Name);
    }

    public Diagnostic UnrecognizedUnit(IIncludeBasesFilteringContext context, IncludeBasesDefinition definition, int index)
    {
        return DiagnosticConstruction.UnrecognizedUnitName(definition.Locations.IncludeBasesElements[definition.LocationMap[index]].AsRoslynLocation(), definition.IncludedBases[index], context.UnitType.Type.Name);
    }

    public Diagnostic BaseAlreadyIncluded(IIncludeBasesFilteringContext context, IncludeBasesDefinition definition, int index)
    {
        return DiagnosticConstruction.IncludingAlreadyIncludedUnitWithUnion<ExcludeBasesAttribute>(definition.Locations.IncludeBasesElements[definition.LocationMap[index]].AsRoslynLocation(), definition.IncludedBases[index]);
    }

    public Diagnostic BaseExcluded(IIncludeBasesFilteringContext context, IncludeBasesDefinition definition, int index)
    {
        return DiagnosticConstruction.IncludingExcludedUnit(definition.Locations.IncludeBasesElements[definition.LocationMap[index]].AsRoslynLocation(), definition.IncludedBases[index]);
    }
}
