﻿namespace SharpMeasures.Generators.Units.Processing.Diagnostics;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing.Units;
using SharpMeasures.Generators.Diagnostics;

internal static class GeneratedScalarDiagnostics
{
    public static Diagnostic QuantityNotScalar(GeneratedUnit definition)
    {
        return DiagnosticConstruction.TypeNotScalar(definition.Locations.Quantity?.AsRoslynLocation(), definition.Quantity.Name);
    }

    public static Diagnostic QuantityNotUnbiased(GeneratedUnit definition)
    {
        return DiagnosticConstruction.ScalarNotUnbiased_UnitDefinition(definition.Locations.Quantity?.AsRoslynLocation(), definition.Quantity.Name);
    }
}
