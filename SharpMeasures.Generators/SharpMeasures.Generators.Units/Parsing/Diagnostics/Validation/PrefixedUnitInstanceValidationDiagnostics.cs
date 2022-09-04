﻿namespace SharpMeasures.Generators.Units.Parsing.Diagnostics.Validation;

using SharpMeasures.Generators.Units.Parsing.PrefixedUnitInstance;

internal class PrefixedUnitInstanceValidationDiagnostics : AModifiedUnitInstanceValidationDiagnostics<PrefixedUnitInstanceDefinition>
{
    public static PrefixedUnitInstanceValidationDiagnostics Instance { get; } = new();

    private PrefixedUnitInstanceValidationDiagnostics() { }
}