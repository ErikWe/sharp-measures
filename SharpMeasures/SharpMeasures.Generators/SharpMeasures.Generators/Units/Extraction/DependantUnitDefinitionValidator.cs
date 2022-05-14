namespace SharpMeasures.Generators.Units.Extraction;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing.Extraction;
using SharpMeasures.Generators.Attributes.Parsing.Units;
using SharpMeasures.Generators.Diagnostics;

internal abstract class DependantUnitDefinitionValidator<TDefinition> : UnitDefinitionValidator<TDefinition>
    where TDefinition : IDependantUnitDefinition
{
    protected INamedTypeSymbol UnitType { get; }

    public DependantUnitDefinitionValidator(INamedTypeSymbol unitType)
    {
        UnitType = unitType;
    }

    public override ExtractionValidity Check(AttributeData attributeData, TDefinition definition)
    {
        if (base.Check(attributeData, definition) is ExtractionValidity { IsInvalid: true } invalidUnit)
        {
            return invalidUnit;
        }

        if (string.IsNullOrEmpty(definition.DependantOn))
        {
            return ExtractionValidity.Invalid(CreateUnitNameNotRecognizedDiagnostics(definition));
        }

        return ExtractionValidity.Valid;
    }

    private Diagnostic? CreateUnitNameNotRecognizedDiagnostics(TDefinition definition)
    {
        return Diagnostic.Create(DiagnosticRules.InvalidUnitName, definition.Locations.DependantOn, definition.DependantOn, UnitType.Name);
    }
}
