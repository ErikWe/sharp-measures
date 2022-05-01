namespace SharpMeasures.Generators.Units.Extraction;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing.Units;

internal class ScaledUnitValidator : DependantUnitDefinitionValidator<ScaledUnitParameters>
{
    public ScaledUnitValidator(INamedTypeSymbol unitType) : base(unitType) { }

    protected override int NameArgumentIndex(AttributeData attributeData) => ScaledUnitParser.NameIndex(attributeData);
    protected override int PluralArgumentIndex(AttributeData attributeData) => ScaledUnitParser.PluralIndex(attributeData);
    protected override int DependantOnArgumentIndex(AttributeData attributeData) => ScaledUnitParser.FromIndex(attributeData);
}
