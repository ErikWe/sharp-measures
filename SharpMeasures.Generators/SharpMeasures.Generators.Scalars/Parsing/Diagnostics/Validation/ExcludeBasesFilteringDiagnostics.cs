namespace SharpMeasures.Generators.Scalars.Parsing.Diagnostics.Validation;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Scalars.Parsing.ExcludeBases;

internal class ExcludeBasesFilteringDiagnostics : IExcludeBasesFilteringDiagnostics
{
    public static ExcludeBasesFilteringDiagnostics Instance { get; } = new();

    private ExcludeBasesFilteringDiagnostics() { }

    public Diagnostic UnrecognizedUnit(IExcludeBasesFilteringContext context, ExcludeBasesDefinition definition, int index)
    {
        return DiagnosticConstruction.UnrecognizedUnitName(definition.Locations.ExcludeBasesElements[definition.LocationMap[index]].AsRoslynLocation(), definition.ExcludedBases[index], context.UnitType.Type.Name);
    }

    public Diagnostic? BaseAlreadyExcluded(IExcludeBasesFilteringContext context, ExcludeBasesDefinition definition, int index)
    {
        return DiagnosticConstruction.ExcludingAlreadyExcludedUnit(definition.Locations.ExcludeBasesElements[definition.LocationMap[index]].AsRoslynLocation(), definition.ExcludedBases[index]);
    }
}
