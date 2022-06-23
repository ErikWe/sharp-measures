namespace SharpMeasures.Generators.Units.Diagnostics;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Units.Parsing.Abstractions;

internal abstract class ADependantUnitDiagnostics<TRawDefinition> : AUnitDiagnostics<TRawDefinition>, IDependantUnitProcessingDiagnostics<TRawDefinition>
    where TRawDefinition : IRawDependantUnitDefinition
{
    public Diagnostic NullDependency(IDependantUnitProcessingContext context, TRawDefinition definition)
    {
        return DiagnosticConstruction.NullUnrecognizedUnitName(definition.Locations.DependantOn?.AsRoslynLocation(), context.Type.Name);
    }

    public Diagnostic EmptyDependency(IDependantUnitProcessingContext context, TRawDefinition definition) => NullDependency(context, definition);

    public Diagnostic UnrecognizedDependency(IDependantUnitProcessingContext context, TRawDefinition definition)
    {
        return DiagnosticConstruction.UnrecognizedUnitName(definition.Locations.DependantOn?.AsRoslynLocation(), definition.DependantOn!, context.Type.Name);
    }

    public Diagnostic DependantOnSelf(IDependantUnitProcessingContext context, TRawDefinition definition)
    {
        return DiagnosticConstruction.CyclicUnitDependency(definition.Locations.DependantOn?.AsRoslynLocation(), definition.Name!, context.Type.Name);
    }
}
