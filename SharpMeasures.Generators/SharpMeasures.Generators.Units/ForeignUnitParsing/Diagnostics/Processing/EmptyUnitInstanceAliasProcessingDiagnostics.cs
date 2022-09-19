namespace SharpMeasures.Generators.Units.ForeignUnitParsing.Diagnostics.Processing;

using SharpMeasures.Generators.Units.Parsing.UnitInstanceAlias;

internal sealed class EmptyUnitInstanceAliasProcessingDiagnostics : AEmptyModifiedUnitInstanceProcessingDiagnostics<RawUnitInstanceAliasDefinition, UnitInstanceAliasLocations>
{
    public static EmptyUnitInstanceAliasProcessingDiagnostics Instance { get; } = new();

    private EmptyUnitInstanceAliasProcessingDiagnostics() { }
}
