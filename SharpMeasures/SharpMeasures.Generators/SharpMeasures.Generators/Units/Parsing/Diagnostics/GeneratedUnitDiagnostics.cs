namespace SharpMeasures.Generators.Units.Parsing.Diagnostics;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Attributes.Parsing.Units;
using SharpMeasures.Generators.Diagnostics;

internal class GeneratedUnitDiagnostics : IGeneratedUnitDiagnostics
{
    public static GeneratedUnitDiagnostics Instance { get; } = new();

    private GeneratedUnitDiagnostics() { }

    public Diagnostic NullQuantity(IProcessingContext context, RawGeneratedUnitDefinition definition)
    {
        return DiagnosticConstruction.TypeNotScalar_Null(definition.Locations.Quantity?.AsRoslynLocation());
    }
}
