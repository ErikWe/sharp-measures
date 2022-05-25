namespace SharpMeasures.Generators.Attributes.Parsing.Vectors;

public record class GeneratedVectorParsingData : AAttributeParsingData<GeneratedVectorLocations>
{
    internal static GeneratedVectorParsingData Empty { get; } = new();

    public bool SpecifiedScalar { get; init; }
    public bool ExplicitlyDisabledDocumentation { get; init; }

    private GeneratedVectorParsingData() : base(GeneratedVectorLocations.Empty) { }
}