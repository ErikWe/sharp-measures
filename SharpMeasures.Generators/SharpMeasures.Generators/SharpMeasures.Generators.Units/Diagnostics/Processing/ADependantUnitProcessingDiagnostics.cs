namespace SharpMeasures.Generators.Units.Diagnostics.Processing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Units.Parsing.Abstractions;

internal abstract class ADependantUnitProcessingDiagnostics<TRawDefinition, TLocations> : AUnitProcessingDiagnostics<TRawDefinition, TLocations>,
    IDependantUnitProcessingDiagnostics<TRawDefinition, TLocations>
    where TRawDefinition : IRawDependantUnitDefinition<TLocations>
    where TLocations : IDependantUnitLocations
{
    public Diagnostic NullDependency(IUnitProcessingContext context, TRawDefinition definition)
    {
        return DiagnosticConstruction.NullUnrecognizedUnitName(definition.Locations.DependantOn?.AsRoslynLocation(), context.Type.Name);
    }

    public Diagnostic EmptyDependency(IUnitProcessingContext context, TRawDefinition definition) => NullDependency(context, definition);

    public Diagnostic DependantOnSelf(IUnitProcessingContext context, TRawDefinition definition)
    {
        return DiagnosticConstruction.CyclicUnitDependency(definition.Locations.DependantOn?.AsRoslynLocation(), definition.Name!, context.Type.Name);
    }
}
