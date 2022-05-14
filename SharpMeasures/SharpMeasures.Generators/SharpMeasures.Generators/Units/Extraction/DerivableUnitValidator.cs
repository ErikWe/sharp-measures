namespace SharpMeasures.Generators.Units.Extraction;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing.Extraction;
using SharpMeasures.Generators.Attributes.Parsing.Units;
using SharpMeasures.Generators.Diagnostics;

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
        if (definition.ParsingData.SignatureCouldBeParsed is false)
        {
            return ExtractionValidity.InvalidWithoutDiagnostics;
        }

        if (definition.Signature.Count is 0)
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
        return Diagnostic.Create(DiagnosticRules.InvalidUnitDerivationExpression, definition.Locations.Expression, definition.Expression);
    }

    private static Diagnostic CreateEmptySignatureDiagnostics(DerivableUnitDefinition definition)
    {
        return Diagnostic.Create(DiagnosticRules.InvalidUnitDerivationExpression, definition.Locations.Signature);
    }

    private static Diagnostic? CreateTypeNotUnitDiagnostics(DerivableUnitDefinition parameters)
    {
        if (parameters.ParsingData.SignatureComponentNotUnitIndex < 0 || parameters.ParsingData.SignatureComponentNotUnitIndex >= parameters.Signature.Count)
        {
            return null;
        }

        string invalidName = parameters.Signature[parameters.ParsingData.SignatureComponentNotUnitIndex].Name;
        Location invalidLocation = parameters.Locations.SignatureComponents[parameters.ParsingData.SignatureComponentNotUnitIndex];

        return Diagnostic.Create(DiagnosticRules.TypeNotUnit, invalidLocation, invalidName);
    }
}
