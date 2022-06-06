namespace SharpMeasures.Generators.Units.Processing.Diagnostics;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing.Units;
using SharpMeasures.Generators.Diagnostics;

internal static class DerivableUnitDiagnostics
{
    public static Diagnostic TypeNotUnit(DerivableUnit definition, int index, NamedType type)
    {
        return DiagnosticConstruction.TypeNotUnit(definition.Locations.SignatureElements[index].AsRoslynLocation(), type.Name);
    }
}
