namespace SharpMeasures.Generators.Units.Diagnostics;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Units.Parsing.FixedUnit;

internal class FixedUnitDiagnostics : AUnitDiagnostics<RawFixedUnitDefinition>, IFixedUnitProcessingDiagnostics
{
    public static FixedUnitDiagnostics Instance { get; } = new();

    private FixedUnitDiagnostics() { }

    public Diagnostic UnitNotSupportingBiasTerm(IFixedUnitProcessingContext context, RawFixedUnitDefinition definition)
    {
        return DiagnosticConstruction.FixedUnitBiasSpecifiedButUnitNotBiased(definition.Locations.Bias?.AsRoslynLocation(), context.Type.Name);
    }
}
