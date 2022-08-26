namespace SharpMeasures.Generators.Units.Parsing.Diagnostics.Validation;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Units.Parsing.Abstractions;

internal abstract class ADependantUnitValidationDiagnostics<TDefinition, TLocations> : IDependantUnitValidationDiagnostics<TDefinition, TLocations>
    where TDefinition : IDependantUnitDefinition<TLocations>
    where TLocations : IDependantUnitLocations
{
    public Diagnostic UnrecognizedDependency(IDependantUnitValidationContext context, TDefinition definition)
    {
        return DiagnosticConstruction.UnrecognizedUnitName(definition.Locations.DependantOn?.AsRoslynLocation(), definition.DependantOn!, context.Type.Name);
    }

    public Diagnostic CyclicDependency(IDependantUnitValidationContext context, TDefinition definition)
    {
        return DiagnosticConstruction.CyclicUnitDependency(definition.Locations.DependantOn?.AsRoslynLocation(), definition.Name, context.Type.Name);
    }
}
