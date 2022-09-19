﻿namespace SharpMeasures.Generators.Units.Parsing.Diagnostics.Validation;

using SharpMeasures.Generators.Units.Parsing.ScaledUnitInstance;

internal sealed class ScaledUnitInstanceValidationDiagnostics : AModifiedUnitInstanceValidationDiagnostics<ScaledUnitInstanceDefinition>
{
    public static ScaledUnitInstanceValidationDiagnostics Instance { get; } = new();

    private ScaledUnitInstanceValidationDiagnostics() { }
}
