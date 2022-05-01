namespace SharpMeasures.Generators.Units.Extraction;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing.Units;

internal class UnitAliasValidator : DependantUnitDefinitionValidator<UnitAliasParameters>
{
    public UnitAliasValidator(INamedTypeSymbol unitType) : base(unitType) { }

    protected override int NameArgumentIndex(AttributeData attributeData) => UnitAliasParser.NameIndex(attributeData);
    protected override int PluralArgumentIndex(AttributeData attributeData) => UnitAliasParser.PluralIndex(attributeData);
    protected override int DependantOnArgumentIndex(AttributeData attributeData) => UnitAliasParser.AliasOfIndex(attributeData);
}
