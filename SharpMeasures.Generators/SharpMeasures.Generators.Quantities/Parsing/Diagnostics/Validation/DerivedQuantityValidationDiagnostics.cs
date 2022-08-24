namespace SharpMeasures.Generators.Quantities.Parsing.Diagnostics.Validation;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Quantities.Parsing.DerivedQuantity;

public class DerivedQuantityValidationDiagnostics : IDerivedQuantityValidationDiagnostics
{
    public static DerivedQuantityValidationDiagnostics Instance { get; } = new();

    private DerivedQuantityValidationDiagnostics() { }

    public Diagnostic TypeNotQuantity(IDerivedQuantityValidationContext context, DerivedQuantityDefinition definition, int index)
    {
        return DiagnosticConstruction.TypeNotQuantity(definition.Locations.SignatureElements[index].AsRoslynLocation(), definition.Signature[index].Name);
    }
}
