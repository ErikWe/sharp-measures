namespace SharpMeasures.Generators.Units.Diagnostics.Resolution;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Units.Parsing.Abstractions;

internal abstract class ADependantUnitResolutionDiagnostics<TUnresolvedDefinition, TLocations> : IDependantUnitResolutionDiagnostics<TUnresolvedDefinition, TLocations>
    where TUnresolvedDefinition : IUnresolvedDependantUnitDefinition<TLocations>
    where TLocations : IDependantUnitLocations
{
    public Diagnostic UnrecognizedDependency(IDependantUnitResolutionContext context, TUnresolvedDefinition definition)
    {
        return DiagnosticConstruction.UnrecognizedUnitName(definition.Locations.DependantOn?.AsRoslynLocation(), definition.DependantOn!, context.Type.Name);
    }
}
