﻿namespace SharpMeasures.Generators.Units.Diagnostics.Processing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Units.Parsing.SharpMeasuresUnit;

internal class SharpMeasuresUnitProcessingDiagnostics : ISharpMeasuresUnitProcessingDiagnostics
{
    public static SharpMeasuresUnitProcessingDiagnostics Instance { get; } = new();

    private SharpMeasuresUnitProcessingDiagnostics() { }

    public Diagnostic NullQuantity(IProcessingContext context, RawSharpMeasuresUnitDefinition definition)
    {
        return DiagnosticConstruction.NullTypeNotScalar(definition.Locations.Quantity?.AsRoslynLocation());
    }
}
