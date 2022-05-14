namespace SharpMeasures.Generators.Units.Extraction;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing.Extraction;
using SharpMeasures.Generators.Attributes.Parsing.Units;
using SharpMeasures.Generators.Diagnostics;

internal abstract class UnitDefinitionValidator<TDefinition> : IValidator<TDefinition> where TDefinition : IUnitDefinition
{
    public virtual ExtractionValidity Check(AttributeData attributeData, TDefinition definition)
    {
        if (string.IsNullOrEmpty(definition.Name))
        {
            return ExtractionValidity.Invalid(CreateInvalidUnitNameDiagnostics(definition));
        }

        if (string.IsNullOrEmpty(definition.Plural))
        {
            return ExtractionValidity.Invalid(CreateInvalidUnitPluralFormDiagnostics(definition));
        }

        return ExtractionValidity.Valid;
    }

    private static Diagnostic CreateInvalidUnitNameDiagnostics(TDefinition definition)
    {
        return Diagnostic.Create(DiagnosticRules.InvalidUnitName, definition.Locations.Name, definition.Name);
    }

    private static Diagnostic CreateInvalidUnitPluralFormDiagnostics(TDefinition definition)
    {
        return Diagnostic.Create(DiagnosticRules.InvalidUnitPluralForm, definition.Locations.Plural, definition.Plural);
    }
}
