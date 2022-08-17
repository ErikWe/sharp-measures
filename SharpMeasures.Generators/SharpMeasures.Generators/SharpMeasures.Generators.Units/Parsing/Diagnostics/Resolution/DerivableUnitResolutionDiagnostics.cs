﻿namespace SharpMeasures.Generators.Units.Parsing.Diagnostics.Resolution;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Units.Parsing.DerivableUnit;

internal class DerivableUnitResolutionDiagnostics : IDerivableUnitResolutionDiagnostics
{
    public static DerivableUnitResolutionDiagnostics Instance { get; } = new();

    private DerivableUnitResolutionDiagnostics() { }

    public Diagnostic SignatureElementNotUnit(IDerivableUnitResolutionContext context, RawDerivableUnitDefinition definition, int index)
    {
        return DiagnosticConstruction.TypeNotUnit(definition.Locations.SignatureElements[index].AsRoslynLocation(), definition.Signature[index].Name);
    }
}
