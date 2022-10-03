namespace SharpMeasures.Generators.Scalars.Parsing.Diagnostics.Processing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Scalars.Parsing.Abstraction;

internal sealed class ScalarTypeProcessingDiagnostics : IScalarTypeProcessingDiagnostics
{
    public static ScalarTypeProcessingDiagnostics Instance { get; } = new();

    private ScalarTypeProcessingDiagnostics() { }

    public Diagnostic ContradictoryUnitInstanceInclusionAndExclusion(IScalar scalar)
    {
        return DiagnosticConstruction.ContradictoryAttributes<IncludeUnitsAttribute, ExcludeUnitsAttribute>(scalar.Locations.AttributeName.AsRoslynLocation());
    }

    public Diagnostic ContradictoryUnitBaseInstanceInclusionAndExclusion(IScalar scalar)
    {
        return DiagnosticConstruction.ContradictoryAttributes<IncludeUnitBasesAttribute, ExcludeUnitBasesAttribute>(scalar.Locations.AttributeName.AsRoslynLocation());
    }
}
