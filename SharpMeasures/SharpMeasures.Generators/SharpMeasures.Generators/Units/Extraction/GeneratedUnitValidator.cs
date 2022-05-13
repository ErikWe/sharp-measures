namespace SharpMeasures.Generators.Units.Extraction;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing.Extraction;
using SharpMeasures.Generators.Attributes.Parsing.Scalars;
using SharpMeasures.Generators.Attributes.Parsing.Units;
using SharpMeasures.Generators.Diagnostics;

internal class GeneratedUnitValidator : IValidator<GeneratedUnitDefinition>
{
    public static GeneratedUnitValidator Validator { get; } = new();

    private GeneratedUnitValidator() { }

    public ExtractionValidity Check(AttributeData attributeData, GeneratedUnitDefinition definition)
    {
        if (CheckQuantityValidity(definition) is ExtractionValidity { IsInvalid: true } invalidQuantity)
        {
            return invalidQuantity;
        }

        return ExtractionValidity.Valid;
    }

    private static ExtractionValidity CheckQuantityValidity(GeneratedUnitDefinition definition)
    {
        if (definition.Quantity is null
            || GeneratedScalarQuantityParser.Parser.ParseFirst(definition.Quantity) is not GeneratedScalarQuantityParameters quantityParameters)
        {
            return ExtractionValidity.Invalid(CreateTypeNotScalarQuantityDiagnostics(definition));
        }

        if (quantityParameters.Biased)
        {
            return ExtractionValidity.Invalid(CreateTypeNotUnbiasedDiagnostics(definition));
        }

        return ExtractionValidity.Valid;
    }

    private static Diagnostic? CreateTypeNotScalarQuantityDiagnostics(GeneratedUnitDefinition definition)
    {
        if (definition.Quantity is null)
        {
            return TypeNotScalarQuantityDiagnostics.CreateForNull(definition.Locations.Quantity);
        }

        return TypeNotScalarQuantityDiagnostics.Create(definition.Locations.Quantity, definition.Quantity);
    }

    private static Diagnostic? CreateTypeNotUnbiasedDiagnostics(GeneratedUnitDefinition definition)
    {
        if (definition.Quantity is null)
        {
            return null;
        }

        return TypeNotUnbiasedScalarQuantityDiagnostics.Create(definition.Locations.Quantity, definition.Quantity);
    }
}
