namespace SharpMeasures.Generators.Units.Parsing.Diagnostics.Processing;

using SharpMeasures.Generators.Units.Parsing.UnitInstanceAlias;

internal class UnitInstanceAliasProcessingDiagnostics : AModifiedUnitInstanceProcessingDiagnostics<RawUnitInstanceAliasDefinition, UnitInstanceAliasLocations>
{
    public static UnitInstanceAliasProcessingDiagnostics Instance { get; } = new();

    private UnitInstanceAliasProcessingDiagnostics() { }
}
