namespace SharpMeasures.Generators.Attributes.Parsing.Units;

public abstract record class AUnitParsingData : IUnitParsingData
{
    public string? InterpretedPlural { get; init; }
}
