﻿namespace SharpMeasures.Generators.Units.Parsing.Diagnostics.Processing;

using SharpMeasures.Generators.Units.Parsing.UnitAlias;

internal class UnitAliasProcessingDiagnostics : ADependantUnitProcessingDiagnostics<UnprocessedUnitAliasDefinition, UnitAliasLocations>
{
    public static UnitAliasProcessingDiagnostics Instance { get; } = new();

    private UnitAliasProcessingDiagnostics() { }
}
