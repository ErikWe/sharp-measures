namespace SharpMeasures.Generators.Units.Processing.Diagnostics;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing.Units;
using SharpMeasures.Generators.Diagnostics;

internal static class DerivedUnitDiagnostics
{
    public static Diagnostic UnrecognizedUnit(DerivedUnitDefinition definition, int index, NamedType unit)
    {
        return DiagnosticConstruction.UnrecognizedUnitName(definition.Locations.UnitsElements[index].AsRoslynLocation(), definition.Units[index],
            unit.Name);
    }
}
