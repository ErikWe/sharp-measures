namespace SharpMeasures.Generators.Units.Parsing.Diagnostics.Processing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Units.Parsing.Abstractions;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Units.Parsing.DerivedUnitInstance;

internal sealed class DerivedUnitInstanceProcessingDiagnostics : AUnitInstanceProcessingDiagnostics<RawDerivedUnitInstanceDefinition, DerivedUnitInstanceLocations>, IDerivedUnitInstanceProcessingDiagnostics
{
    public static DerivedUnitInstanceProcessingDiagnostics Instance { get; } = new();

    private DerivedUnitInstanceProcessingDiagnostics() { }

    public Diagnostic EmptyUnitList(IUnitInstanceProcessingContext context, RawDerivedUnitInstanceDefinition definition)
    {
        return DiagnosticConstruction.EmptyUnitList(definition.Locations.UnitsCollection?.AsRoslynLocation());
    }

    public Diagnostic NullUnitsElement(IUnitInstanceProcessingContext context, RawDerivedUnitInstanceDefinition definition, int index)
    {
        return DiagnosticConstruction.NullUnrecognizedUnitInstanceNameUnknownType(definition.Locations.UnitsElements[index].AsRoslynLocation());
    }

    public Diagnostic EmptyUnitsElement(IUnitInstanceProcessingContext context, RawDerivedUnitInstanceDefinition definition, int index) => NullUnitsElement(context, definition, index);
}
