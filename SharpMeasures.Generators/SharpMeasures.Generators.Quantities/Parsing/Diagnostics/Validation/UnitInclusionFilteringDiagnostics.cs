namespace SharpMeasures.Generators.Quantities.Parsing.Diagnostics.Validation;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Quantities.Parsing.UnitList;

public class UnitInclusionFilteringDiagnostics : AUnitListFilteringDiagnostics
{
    public static UnitInclusionFilteringDiagnostics Instance { get; } = new();

    private UnitInclusionFilteringDiagnostics() { }

    public override Diagnostic? UnitAlreadyListedThroughInheritance(IUnitListFilteringContext context, UnitListDefinition definition, int index)
    {
        return DiagnosticConstruction.UnitAlreadyIncluded(definition.Locations.UnitsElements[index].AsRoslynLocation(), definition.Units[index]);
    }
}
