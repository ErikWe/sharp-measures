namespace SharpMeasures.Generators.Units.Parsing.Diagnostics.Empty.Processing;

using SharpMeasures.Generators.Units.Parsing.UnitInstanceAlias;

internal sealed class EmptyUnitInstanceAliasProcessingDiagnostics : AEmptyModifiedUnitInstanceProcessingDiagnostics<RawUnitInstanceAliasDefinition, UnitInstanceAliasLocations>, IUnitInstanceAliasProcessingDiagnostics
{
    public static EmptyUnitInstanceAliasProcessingDiagnostics Instance { get; } = new();

    private EmptyUnitInstanceAliasProcessingDiagnostics() { }
}
