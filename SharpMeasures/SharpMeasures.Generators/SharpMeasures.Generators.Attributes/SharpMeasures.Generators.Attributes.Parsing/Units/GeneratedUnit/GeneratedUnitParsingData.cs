namespace SharpMeasures.Generators.Attributes.Parsing.Units;

public record class GeneratedUnitParsingData : AAttributeParsingData<GeneratedUnitLocations>
{
    internal static GeneratedUnitParsingData Empty { get; } = new();

    public bool ExplicitlySetGenerateDocumentation { get; init; }

    private GeneratedUnitParsingData() : base(GeneratedUnitLocations.Empty) { }
}