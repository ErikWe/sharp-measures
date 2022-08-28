namespace SharpMeasures.Generators.Units.Parsing.Diagnostics.Processing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Units.Parsing.FixedUnit;

internal class FixedUnitProcessingDiagnostics : AUnitProcessingDiagnostics<RawFixedUnitDefinition, FixedUnitLocations>, IFixedUnitProcessingDiagnostics
{
    public static FixedUnitProcessingDiagnostics Instance { get; } = new();

    private FixedUnitProcessingDiagnostics() { }

    public Diagnostic UnitIsDerivable(IFixedUnitProcessingContext context, RawFixedUnitDefinition definition)
    {
        return DiagnosticConstruction.DerivableUnitShouldNotUseFixed(definition.Locations.AttributeName.AsRoslynLocation(), context.Type.Name);
    }
}
