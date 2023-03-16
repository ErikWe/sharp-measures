namespace SharpMeasures.Generators.Units.Parsing.Diagnostics.Processing;

using SharpMeasures.Generators.Units.Parsing.UnitInstanceAlias;

internal sealed class UnitInstanceAliasProcessingDiagnostics : AModifiedUnitInstanceProcessingDiagnostics<RawUnitInstanceAliasDefinition, UnitInstanceAliasLocations>, IUnitInstanceAliasProcessingDiagnostics
{
    public static UnitInstanceAliasProcessingDiagnostics Instance { get; } = new();

    private UnitInstanceAliasProcessingDiagnostics() { }
}
