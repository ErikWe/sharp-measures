namespace SharpMeasures.Generators.Vectors.ForeignVectorParsing.Diagnostics.Validation;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators;
using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Quantities.Parsing.DerivedQuantity;

internal sealed class EmptyDerivedQuantityValidationDiagnostics : IDerivedQuantityValidationDiagnostics
{
    public static EmptyDerivedQuantityValidationDiagnostics Instance { get; } = new();

    private EmptyDerivedQuantityValidationDiagnostics() { }

    Diagnostic? IDerivedQuantityValidationDiagnostics.TypeNotQuantity(IDerivedQuantityValidationContext context, DerivedQuantityDefinition definition, int index) => null;
    Diagnostic? IDerivedQuantityExpandingDiagnostics.MalformedExpression(IDerivedQuantity definition) => null;
    Diagnostic? IDerivedQuantityExpandingDiagnostics.IncompatibleQuantities(IDerivedQuantity definition) => null;
    Diagnostic? IDerivedQuantityExpandingDiagnostics.DerivationUnexpectedlyResultsInScalar(IDerivedQuantity definition) => null;
    Diagnostic? IDerivedQuantityExpandingDiagnostics.DerivationUnexpectedlyResultsInVector(IDerivedQuantity definition) => null;
    Diagnostic? IDerivedQuantityExpandingDiagnostics.DerivationResultsInUnexpectedDimension(IDerivedQuantity definition, NamedType vector) => null;
}
