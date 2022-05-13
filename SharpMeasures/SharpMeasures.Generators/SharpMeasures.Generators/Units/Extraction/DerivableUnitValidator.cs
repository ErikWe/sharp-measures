namespace SharpMeasures.Generators.Units.Extraction;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing.Extraction;
using SharpMeasures.Generators.Attributes.Parsing.Units;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Diagnostics.DerivableUnits;

internal class DerivableUnitValidator : IValidator<DerivableUnitDefinition>
{
    public static DerivableUnitValidator Validator { get; } = new();

    private DerivableUnitValidator() { }

    public ExtractionValidity Check(AttributeData attributeData, DerivableUnitDefinition definition)
    {
        if (CheckExpressionValidity(definition) is ExtractionValidity { IsInvalid: true } invalidExpression)
        {
            return invalidExpression;
        }

        if (CheckSignatureValidity(definition) is ExtractionValidity { IsInvalid: true } invalidSignature)
        {
            return invalidSignature;
        }

        return ExtractionValidity.Valid;
    }

    private static ExtractionValidity CheckExpressionValidity(DerivableUnitDefinition definition)
    {
        if (string.IsNullOrEmpty(definition.Expression))
        {
            return ExtractionValidity.Invalid(CreateInvalidExpressionDiagnostics(definition));
        }

        return ExtractionValidity.Valid;
    }

    private static ExtractionValidity CheckSignatureValidity(DerivableUnitDefinition definition)
    {
        if (definition.ParsingData.SignatureValid)
        {
            return ExtractionValidity.Invalid(CreateEmptySignatureDiagnostics(definition));
        }

        if (definition.ParsingData.SignatureComponentNotUnitIndex != -1)
        {
            return ExtractionValidity.Invalid(CreateTypeNotUnitDiagnostics(definition));
        }

        return ExtractionValidity.Valid;
    }

    private static Diagnostic CreateInvalidExpressionDiagnostics(DerivableUnitDefinition definition)
    {
        return InvalidUnitDerivationExpressionDiagnostics.Create(definition.Locations.Expression, definition.Expression);
    }

    private static Diagnostic CreateEmptySignatureDiagnostics(DerivableUnitDefinition definition)
    {
        return EmptyUnitDerivationSignatureDiagnostics.Create(definition.Locations.Signature);
    }

    private static Diagnostic? CreateTypeNotUnitDiagnostics(DerivableUnitDefinition parameters)
    {
        if (parameters.ParsingData.SignatureComponentNotUnitIndex < 0 || parameters.ParsingData.SignatureComponentNotUnitIndex >= parameters.Signature.Count)
        {
            return null;
        }

        INamedTypeSymbol invalidSymbol = parameters.Signature[parameters.ParsingData.SignatureComponentNotUnitIndex];
        Location invalidLocation = parameters.Locations.SignatureComponents[parameters.ParsingData.SignatureComponentNotUnitIndex];

        return TypeNotUnitDiagnostics.Create(invalidLocation, invalidSymbol);
    }
}
