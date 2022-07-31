﻿namespace SharpMeasures.Generators.Units.Parsing.Diagnostics.Processing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Units.Parsing.Abstractions;
using SharpMeasures.Generators.Units.Parsing.BiasedUnit;

internal class BiasedUnitProcessingDiagnostics : ADependantUnitProcessingDiagnostics<RawBiasedUnitDefinition, BiasedUnitLocations>, IBiasedUnitProcessingDiagnostics
{
    public static BiasedUnitProcessingDiagnostics Instance { get; } = new();

    private BiasedUnitProcessingDiagnostics() { }

    public Diagnostic NullExpression(IUnitProcessingContext context, RawBiasedUnitDefinition definition)
    {
        return DiagnosticConstruction.NullBiasedUnitExpression(definition.Locations.Expression?.AsRoslynLocation());
    }

    public Diagnostic EmptyExpression(IUnitProcessingContext context, RawBiasedUnitDefinition definition)
    {
        return DiagnosticConstruction.EmptyBiasedUnitExpression(definition.Locations.Expression?.AsRoslynLocation());
    }
}
