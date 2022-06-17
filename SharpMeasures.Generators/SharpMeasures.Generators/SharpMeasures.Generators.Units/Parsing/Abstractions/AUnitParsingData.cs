namespace SharpMeasures.Generators.Units.Parsing.Abstractions;
internal abstract record class AUnitParsingData : IUnitParsingData
{
    public string? InterpretedPlural { get; init; }
}
