namespace SharpMeasures.Generators.Attributes.Parsing.Units;

public readonly record struct OffsetUnitParameters(string Name, string Plural, string From, double Offset)
    : IUnitDefinitionParameters, IDependantUnitDefinitionParameters
{
    string IDependantUnitDefinitionParameters.DependantOn => From;
}