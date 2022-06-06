namespace SharpMeasures.Generators.Quantities.Parsing.Diagnostics;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing.Quantities;
using SharpMeasures.Generators.Diagnostics;

internal abstract class ADimensionalEquivalenceDiagnostics : IDimensionalEquivalenceDiagnostics
{
    public virtual Diagnostic EmptyQuantityList(IDimensionalEquivalenceProcessingContext context, RawDimensionalEquivalence definition)
    {
        return DiagnosticConstruction.EmptyList_Quantity(definition.Locations.QuantitiesCollection?.AsRoslynLocation());
    }

    public abstract Diagnostic NullQuantity(IDimensionalEquivalenceProcessingContext context, RawDimensionalEquivalence definition, int index);

    public virtual Diagnostic DuplicateQuantity(IDimensionalEquivalenceProcessingContext context, RawDimensionalEquivalence definition, int index)
    {
        return DiagnosticConstruction.DuplicateListing_Quantity(definition.Locations.QuantitiesElements[index].AsRoslynLocation(),
            definition.Quantities[index]!.Value.Name);
    }

    public virtual Diagnostic UnrecognizedCastOperatorBehaviour(IDimensionalEquivalenceProcessingContext context, RawDimensionalEquivalence definition)
    {
        return DiagnosticConstruction.UnrecognizedConversionOperationBehaviour(definition.Locations.CastOperatorBehaviour?.AsRoslynLocation(),
            definition.CastOperatorBehaviour);
    }
}
