namespace SharpMeasures.Generators.Units.Parsing.Diagnostics;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing.Units;
using SharpMeasures.Generators.Diagnostics;

internal abstract class ADependantUnitDiagnostics<TDefinition> : AUnitDiagnostics<TDefinition>, IDependantUnitDiagnostics<TDefinition>
    where TDefinition : IRawDependantUnitDefinition
{
    public Diagnostic NullDependency(IDependantUnitProcessingContext context, TDefinition definition)
    {
        return DiagnosticConstruction.InvalidUnitName_Null(definition.Locations.DependantOn?.AsRoslynLocation());
    }

    public Diagnostic EmptyDependency(IDependantUnitProcessingContext context, TDefinition definition) => NullDependency(context, definition);

    public Diagnostic UnrecognizedDependency(IDependantUnitProcessingContext context, TDefinition definition)
    {
        return DiagnosticConstruction.UnrecognizedUnitName(definition.Locations.DependantOn?.AsRoslynLocation(), definition.DependantOn!, context.Type.Name);
    }

    public Diagnostic DependantOnSelf(IDependantUnitProcessingContext context, TDefinition definition)
    {
        return DiagnosticConstruction.CyclicDependency(definition.Locations.Attribute.AsRoslynLocation(), definition.Name!, context.Type.Name);
    }
}
