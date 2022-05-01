namespace SharpMeasures.Generators.Attributes.Parsing.Units;

public readonly record struct FixedUnitParameters(string Name, string Plural, double Value, double Bias)
    : IUnitDefinitionParameters;