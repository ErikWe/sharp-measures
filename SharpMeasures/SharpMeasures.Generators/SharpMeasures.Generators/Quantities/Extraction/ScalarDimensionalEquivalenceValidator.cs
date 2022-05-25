namespace SharpMeasures.Generators.Quantities.Extraction;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing.Extraction;
using SharpMeasures.Generators.Attributes.Parsing.Quantities;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Scalars;

internal class ScalarDimensionalEquivalenceValidator : ADimensionalEquivalenceValidator
{
    public static ScalarDimensionalEquivalenceValidator Validator { get; } = new();

    private ScalarDimensionalEquivalenceValidator() { }

    protected override ExtractionValidity Check(DimensionalEquivalenceDefinition definition, int index)
    {
        if (definition.Quantities[index].HasAttributeOfType<GeneratedScalarAttribute>() is false)
        {
            return ExtractionValidity.Invalid(CreateTypeNotScalarDiagnostics(definition, index));
        }

        return ExtractionValidity.Valid;
    }

    protected override Diagnostic CreateDuplicateListingDiagnostics(DimensionalEquivalenceDefinition definition, int index)
    {
        return DiagnosticConstruction.DuplicateListing_Scalar(definition.Locations.QuantitiesComponents[index], definition.Quantities[index].Name);
    }

    private static Diagnostic CreateTypeNotScalarDiagnostics(DimensionalEquivalenceDefinition definition, int index)
    {
        return DiagnosticConstruction.TypeNotScalar(definition.Locations.QuantitiesComponents[index], definition.Quantities[index].Name);
    }
}
