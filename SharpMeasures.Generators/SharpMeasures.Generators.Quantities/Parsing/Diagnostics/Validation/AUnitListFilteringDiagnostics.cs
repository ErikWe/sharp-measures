namespace SharpMeasures.Generators.Quantities.Parsing.Diagnostics.Validation;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Quantities.Parsing.UnitList;

public abstract class AUnitListFilteringDiagnostics : IUnitListFilteringDiagnostics
{
    public Diagnostic UnrecognizedUnit(IUnitListFilteringContext context, UnitListDefinition definition, int index)
    {
        return DiagnosticConstruction.UnrecognizedUnitName(definition.Locations.UnitsElements[definition.LocationMap[index]].AsRoslynLocation(), definition.Units[index], context.UnitType.Type.Name);
    }

    public abstract Diagnostic? UnitAlreadyListedThroughInheritance(IUnitListFilteringContext context, UnitListDefinition definition, int index);
}
