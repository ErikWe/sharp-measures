namespace SharpMeasures.Generators.Quantities.Diagnostics;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Quantities.Parsing.DimensionalEquivalence;
using SharpMeasures.Generators.Diagnostics;

public abstract class ADimensionalEquivalenceProcessingDiagnostics : IDimensionalEquivalenceProcessingDiagnostics
{
    public virtual Diagnostic EmptyQuantityList(IDimensionalEquivalenceProcessingContext context, RawDimensionalEquivalenceDefinition definition)
    {
        return DiagnosticConstruction.EmptyQuantityList(definition.Locations.QuantitiesCollection?.AsRoslynLocation());
    }

    public abstract Diagnostic NullQuantity(IDimensionalEquivalenceProcessingContext context, RawDimensionalEquivalenceDefinition definition, int index);

    public virtual Diagnostic DuplicateQuantity(IDimensionalEquivalenceProcessingContext context, RawDimensionalEquivalenceDefinition definition, int index)
    {
        return DiagnosticConstruction.DuplicateQuantityListing(definition.Locations.QuantitiesElements[index].AsRoslynLocation(),
            definition.Quantities[index]!.Value.Name);
    }

    public virtual Diagnostic UnrecognizedCastOperatorBehaviour(IDimensionalEquivalenceProcessingContext context, RawDimensionalEquivalenceDefinition definition)
    {
        return DiagnosticConstruction.UnrecognizedConversionOperationBehaviour(definition.Locations.CastOperatorBehaviour?.AsRoslynLocation(),
            definition.CastOperatorBehaviour);
    }
}
