namespace SharpMeasures.Generators.Quantities.Extraction;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing.Extraction;
using SharpMeasures.Generators.Attributes.Parsing.Quantities;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Vectors;

internal class VectorDimensionalEquivalenceValidator : ADimensionalEquivalenceValidator
{
    public static VectorDimensionalEquivalenceValidator Validator { get; } = new();

    private VectorDimensionalEquivalenceValidator() { }

    protected override ExtractionValidity Check(DimensionalEquivalenceDefinition definition, int index)
    {
        if (definition.Quantities[index].HasAttributeOfType<GeneratedVectorAttribute>() is false)
        {
            return ExtractionValidity.Invalid(CreateTypeNotVectorDiagnostics(definition, index));
        }

        return ExtractionValidity.Valid;
    }

    protected override Diagnostic CreateDuplicateListingDiagnostics(DimensionalEquivalenceDefinition definition, int index)
    {
        return DiagnosticConstruction.DuplicateListing_Vector(definition.Locations.QuantitiesComponents[index], definition.Quantities[index].Name);
    }

    private static Diagnostic CreateTypeNotVectorDiagnostics(DimensionalEquivalenceDefinition definition, int index)
    {
        return DiagnosticConstruction.TypeNotVector(definition.Locations.QuantitiesComponents[index], definition.Quantities[index].Name);
    }
}
