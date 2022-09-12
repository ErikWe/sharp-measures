namespace SharpMeasures.Generators.Units.Parsing.Diagnostics.Processing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Units.Parsing.FixedUnitInstance;

internal class FixedUnitInstanceProcessingDiagnostics : AUnitInstanceProcessingDiagnostics<RawFixedUnitInstanceDefinition, FixedUnitInstanceLocations>, IFixedUnitInstanceProcessingDiagnostics
{
    public static FixedUnitInstanceProcessingDiagnostics Instance { get; } = new();

    private FixedUnitInstanceProcessingDiagnostics() { }

    public Diagnostic UnitIsDerivable(IFixedUnitInstanceProcessingContext context, RawFixedUnitInstanceDefinition definition)
    {
        return DiagnosticConstruction.DerivableUnitShouldNotUseFixed(definition.Locations.AttributeName.AsRoslynLocation(), context.Type.Name);
    }
}
