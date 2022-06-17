namespace SharpMeasures.Generators.Units.Diagnostics;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Units.Parsing.GeneratedUnit;
using SharpMeasures.Generators.Units.Refinement.GeneratedUnit;

internal class GeneratedUnitDiagnostics : IGeneratedUnitProcessingDiagnostics, IGeneratedUnitRefinementDiagnostics
{
    public static GeneratedUnitDiagnostics Instance { get; } = new();

    private GeneratedUnitDiagnostics() { }

    public Diagnostic NullQuantity(IProcessingContext context, RawGeneratedUnitDefinition definition)
    {
        return DiagnosticConstruction.NullTypeNotScalar(definition.Locations.Quantity?.AsRoslynLocation());
    }

    public Diagnostic QuantityNotScalar(IGeneratedUnitRefinementContext context, GeneratedUnitDefinition definition)
    {
        return DiagnosticConstruction.TypeNotScalar(definition.Locations.Quantity?.AsRoslynLocation(), definition.Quantity.Name);
    }

    public Diagnostic QuantityBiased(IGeneratedUnitRefinementContext context, GeneratedUnitDefinition definition)
    {
        return DiagnosticConstruction.ScalarBiased(definition.Locations.Quantity?.AsRoslynLocation(), definition.Quantity.Name);
    }
}
