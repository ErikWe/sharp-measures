namespace SharpMeasures.Generators.Quantities.Parsing.Diagnostics.Empty.Validation;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators;
using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Quantities.Parsing.DerivedQuantity;

public sealed class EmptyDerivedQuantityValidationDiagnostics : IDerivedQuantityValidationDiagnostics
{
    public static EmptyDerivedQuantityValidationDiagnostics Instance { get; } = new();

    private EmptyDerivedQuantityValidationDiagnostics() { }

    public Diagnostic? TypeNotQuantity(IDerivedQuantityValidationContext context, DerivedQuantityDefinition definition, int index) => null;
    public Diagnostic? MalformedExpression(IDerivedQuantity definition) => null;
    public Diagnostic? IncompatibleQuantities(IDerivedQuantity definition) => null;
    public Diagnostic? DerivationUnexpectedlyResultsInScalar(IDerivedQuantity definition) => null;
    public Diagnostic? DerivationUnexpectedlyResultsInVector(IDerivedQuantity definition) => null;
    public Diagnostic? DerivationResultsInUnexpectedDimension(IDerivedQuantity definition, NamedType vector) => null;
}
