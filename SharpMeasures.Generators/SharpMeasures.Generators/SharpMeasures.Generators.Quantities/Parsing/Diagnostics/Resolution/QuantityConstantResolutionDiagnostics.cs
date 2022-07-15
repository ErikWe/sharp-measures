namespace SharpMeasures.Generators.Quantities.Parsing.Diagnostics.Resolution;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Quantities.Parsing.QuantityConstant;

using System.Diagnostics.CodeAnalysis;

public class QuantityConstantResolutionDiagnostics<TDefinition, TLocations> : IQuantityConstantResolutionDiagnostics<TDefinition, TLocations>
    where TDefinition : AUnresolvedQuantityConstantDefinition<TLocations>
    where TLocations : AQuantityConstantLocations<TLocations>
{
    [SuppressMessage("Design", "CA1000", Justification = "Property")]
    public static QuantityConstantResolutionDiagnostics<TDefinition, TLocations> Instance { get; } = new();

    protected QuantityConstantResolutionDiagnostics() { }

    public Diagnostic UnrecognizedUnit(IQuantityConstantResolutionContext context, TDefinition definition)
    {
        return DiagnosticConstruction.UnrecognizedUnitName(definition.Locations.Unit?.AsRoslynLocation(), definition.Unit, context.Unit.Type.Name);
    }
}
