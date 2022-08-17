namespace SharpMeasures.Generators.Units.Parsing.Diagnostics.Processing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Units.Parsing.Abstractions;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Units.Parsing.DerivedUnit;

internal class DerivedUnitProcessingDiagnostics : AUnitProcessingDiagnostics<UnprocessedDerivedUnitDefinition, DerivedUnitLocations>, IDerivedUnitProcessingDiagnostics
{
    public static DerivedUnitProcessingDiagnostics Instance { get; } = new();

    private DerivedUnitProcessingDiagnostics() { }

    public Diagnostic EmptyUnitList(IUnitProcessingContext context, UnprocessedDerivedUnitDefinition definition)
    {
        return DiagnosticConstruction.EmptyUnitList(definition.Locations.UnitsCollection?.AsRoslynLocation());
    }

    public Diagnostic NullUnitElement(IUnitProcessingContext context, UnprocessedDerivedUnitDefinition definition, int index)
    {
        return DiagnosticConstruction.NullUnrecognizedUnitNameUnknownType(definition.Locations.UnitsElements[index].AsRoslynLocation());
    }

    public Diagnostic EmptyUnitElement(IUnitProcessingContext context, UnprocessedDerivedUnitDefinition definition, int index) => NullUnitElement(context, definition, index);
}
