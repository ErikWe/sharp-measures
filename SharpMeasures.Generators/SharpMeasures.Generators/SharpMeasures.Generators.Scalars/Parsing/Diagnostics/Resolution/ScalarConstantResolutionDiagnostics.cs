namespace SharpMeasures.Generators.Scalars.Parsing.Diagnostics.Resolution;

using SharpMeasures.Generators.Quantities.Parsing.Diagnostics.Resolution;
using SharpMeasures.Generators.Scalars.Parsing.ScalarConstant;

internal static class ScalarConstantResolutionDiagnostics
{
    public static QuantityConstantResolutionDiagnostics<UnresolvedScalarConstantDefinition, ScalarConstantLocations> Instance => QuantityConstantResolutionDiagnostics<UnresolvedScalarConstantDefinition, ScalarConstantLocations>.Instance;
}
