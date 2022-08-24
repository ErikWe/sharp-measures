namespace SharpMeasures.Generators.Quantities.Parsing.Diagnostics.Validation;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Quantities.Parsing.UnitList;

public class UnitExclusionFilteringDiagnostics : AUnitListFilteringDiagnostics
{
    public static UnitExclusionFilteringDiagnostics Instance { get; } = new();

    private UnitExclusionFilteringDiagnostics() { }

    public override Diagnostic? UnitAlreadyListedThroughInheritance(IUnitListFilteringContext context, UnitListDefinition definition, int index)
    {
        return DiagnosticConstruction.UnitAlreadyExcluded(definition.Locations.UnitsElements[index].AsRoslynLocation(), definition.Units[index]);
    }
}
