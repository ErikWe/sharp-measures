namespace SharpMeasures.Generators.Quantities.Parsing.Diagnostics.Resolution;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Quantities.Parsing.DerivedQuantity;

public class DerivedQuantityResolutionDiagnostics : IDerivedQuantityResolutionDiagnostics
{
    public static DerivedQuantityResolutionDiagnostics Instance { get; } = new();

    private DerivedQuantityResolutionDiagnostics() { }

    public Diagnostic TypeNotQuantity(IDerivedQuantityResolutionContext context, UnresolvedDerivedQuantityDefinition definition, int index)
    {
        return DiagnosticConstruction.TypeNotQuantity(definition.Locations.SignatureElements[index].AsRoslynLocation(), definition.Signature[index].Name);
    }
}
