namespace SharpMeasures.Generators.Units.Parsing.Diagnostics.Empty.Processing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Units.Parsing.Abstractions;

internal abstract class AEmptyModifiedUnitInstanceProcessingDiagnostics<TDefinition, TLocations> : AEmptyUnitInstanceProcessingDiagnostics<TDefinition, TLocations>, IModifiedUnitInstanceProcessingDiagnostics<TDefinition, TLocations>
    where TDefinition : IRawModifiedUnitInstance<TLocations>
    where TLocations : IModifiedUnitInstanceLocations
{
    public Diagnostic? EmptyOriginalUnitInstance(IUnitInstanceProcessingContext context, TDefinition definition) => null;
    public Diagnostic? NullOriginalUnitInstance(IUnitInstanceProcessingContext context, TDefinition definition) => null;
    public Diagnostic? OriginalUnitInstanceIsSelf(IUnitInstanceProcessingContext context, TDefinition definition) => null;
}
