namespace SharpMeasures.Generators.Quantities.Parsing.Diagnostics.Validation;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Quantities.Parsing.DerivedQuantity;

public sealed class DerivedQuantityValidationDiagnostics : IDerivedQuantityValidationDiagnostics
{
    public static DerivedQuantityValidationDiagnostics Instance { get; } = new();

    private DerivedQuantityValidationDiagnostics() { }

    public Diagnostic TypeNotQuantity(IDerivedQuantityValidationContext context, DerivedQuantityDefinition definition, int index)
    {
        return DiagnosticConstruction.TypeNotQuantity(definition.Locations.SignatureElements[index].AsRoslynLocation(), definition.Signature[index].Name);
    }

    public Diagnostic MalformedExpression(IDerivedQuantity definition)
    {
        return DiagnosticConstruction.MalformedDerivationExpression(definition.Locations.Expression?.AsRoslynLocation());
    }

    public Diagnostic IncompatibleQuantities(IDerivedQuantity definition)
    {
        return DiagnosticConstruction.IncompatibleQuantitiesInDerivation(definition.Locations.Expression?.AsRoslynLocation());
    }

    public Diagnostic DerivationUnexpectedlyResultsInScalar(IDerivedQuantity definition)
    {
        return DiagnosticConstruction.DerivationUnexpectedlyResultInScalar(definition.Locations.AttributeName.AsRoslynLocation());
    }

    public Diagnostic DerivationUnexpectedlyResultsInVector(IDerivedQuantity definition)
    {
        return DiagnosticConstruction.DerivationUnexpectedlyResultInVector(definition.Locations.AttributeName.AsRoslynLocation());
    }

    public Diagnostic DerivationResultsInUnexpectedDimension(IDerivedQuantity definition, NamedType vector)
    {
        return DiagnosticConstruction.DerivationResultsInNonOverlappingDimension(definition.Locations.AttributeName.AsRoslynLocation(), vector.Name);
    }
}
