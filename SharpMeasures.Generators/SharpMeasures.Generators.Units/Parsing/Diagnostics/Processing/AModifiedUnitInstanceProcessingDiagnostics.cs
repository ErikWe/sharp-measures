namespace SharpMeasures.Generators.Units.Parsing.Diagnostics.Processing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Units.Parsing.Abstractions;

internal abstract class AModifiedUnitInstanceProcessingDiagnostics<TRawDefinition, TLocations> : AUnitInstanceProcessingDiagnostics<TRawDefinition, TLocations>,
    IModifiedUnitInstanceProcessingDiagnostics<TRawDefinition, TLocations>
    where TRawDefinition : IRawModifiedUnitInstance<TLocations>
    where TLocations : IModifiedUnitInstanceLocations
{
    public Diagnostic NullOriginalUnitInstance(IUnitInstanceProcessingContext context, TRawDefinition definition)
    {
        return DiagnosticConstruction.NullUnrecognizedUnitInstanceName(definition.Locations.OriginalUnitInstance?.AsRoslynLocation(), context.Type.Name);
    }

    public Diagnostic EmptyOriginalUnitInstance(IUnitInstanceProcessingContext context, TRawDefinition definition) => NullOriginalUnitInstance(context, definition);

    public Diagnostic OriginalUnitInstanceIsSelf(IUnitInstanceProcessingContext context, TRawDefinition definition)
    {
        return DiagnosticConstruction.CyclicallyModifiedUnitInstances(definition.Locations.OriginalUnitInstance?.AsRoslynLocation(), definition.Name!, context.Type.Name);
    }
}
