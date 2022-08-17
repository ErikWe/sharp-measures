﻿namespace SharpMeasures.Generators.Quantities.Parsing.Diagnostics.Resolution;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Quantities.Parsing.UnitList;

public class UnitListResolutionDiagnostics : IUnitListResolutionDiagnostics
{
    public static UnitListResolutionDiagnostics Instance { get; } = new();

    private UnitListResolutionDiagnostics() { }

    public Diagnostic UnrecognizedUnit(IUnitListResolutionContext context, RawUnitListDefinition definition, int index)
    {
        return DiagnosticConstruction.UnrecognizedUnitName(definition.Locations.UnitsElements[definition.LocationMap[index]].AsRoslynLocation(), definition.Units[index], context.Unit.Type.Name);
    }
}
