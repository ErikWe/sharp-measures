namespace SharpMeasures.Generators.Units.ForeignUnitParsing.Diagnostics.Processing;

using Microsoft.CodeAnalysis;
using SharpMeasures.Generators.Units;

using SharpMeasures.Generators.Units.Parsing.Abstractions;

internal abstract class AEmptyModifiedUnitInstanceProcessingDiagnostics<TDefinition, TLocations> : AEmptyUnitInstanceProcessingDiagnostics<TDefinition, TLocations>, IModifiedUnitInstanceProcessingDiagnostics<TDefinition, TLocations>
    where TDefinition : IRawModifiedUnitInstance<TLocations>
    where TLocations : IModifiedUnitInstanceLocations
{
    Diagnostic? IModifiedUnitInstanceProcessingDiagnostics<TDefinition, TLocations>.EmptyOriginalUnitInstance(IUnitInstanceProcessingContext context, TDefinition definition) => null;
    Diagnostic? IModifiedUnitInstanceProcessingDiagnostics<TDefinition, TLocations>.NullOriginalUnitInstance(IUnitInstanceProcessingContext context, TDefinition definition) => null;
    Diagnostic? IModifiedUnitInstanceProcessingDiagnostics<TDefinition, TLocations>.OriginalUnitInstanceIsSelf(IUnitInstanceProcessingContext context, TDefinition definition) => null;
}
