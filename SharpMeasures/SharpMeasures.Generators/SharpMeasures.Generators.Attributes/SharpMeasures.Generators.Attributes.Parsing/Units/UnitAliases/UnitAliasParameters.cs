namespace SharpMeasures.Generators.Attributes.Parsing.Units;

public readonly record struct UnitAliasParameters(string Name, string Plural, string AliasOf)
    : IUnitDefinitionParameters, IDependantUnitDefinitionParameters
{
    string IDependantUnitDefinitionParameters.DependantOn => AliasOf;
}