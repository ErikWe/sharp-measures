namespace SharpMeasures.Generators.Units.ForeignUnitParsing.Diagnostics;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Units.Parsing.Abstractions;

internal class EmptyModifiedUnitInstanceProcessingDiagnostics<TDefinition, TLocations> : EmptyUnitInstanceProcessingDiagnostics<TDefinition, TLocations>, IModifiedUnitInstanceProcessingDiagnostics<TDefinition, TLocations>
    where TDefinition : IRawModifiedUnitInstance<TLocations>
    where TLocations : IModifiedUnitInstanceLocations
{
    new public static EmptyModifiedUnitInstanceProcessingDiagnostics<TDefinition, TLocations> Instance { get; } = new();

    Diagnostic? IModifiedUnitInstanceProcessingDiagnostics<TDefinition, TLocations>.EmptyOriginalUnitInstance(IUnitInstanceProcessingContext context, TDefinition definition) => null;
    Diagnostic? IModifiedUnitInstanceProcessingDiagnostics<TDefinition, TLocations>.NullOriginalUnitInstance(IUnitInstanceProcessingContext context, TDefinition definition) => null;
    Diagnostic? IModifiedUnitInstanceProcessingDiagnostics<TDefinition, TLocations>.OriginalUnitInstanceIsSelf(IUnitInstanceProcessingContext context, TDefinition definition) => null;
}
