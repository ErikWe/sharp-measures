namespace SharpMeasures.Generators.Attributes.Parsing.Units;

public readonly record struct ScaledUnitParameters(string Name, string Plural, string From, double Scale)
    : IUnitDefinitionParameters, IDependantUnitDefinitionParameters
{
    string IDependantUnitDefinitionParameters.DependantOn => From;
}