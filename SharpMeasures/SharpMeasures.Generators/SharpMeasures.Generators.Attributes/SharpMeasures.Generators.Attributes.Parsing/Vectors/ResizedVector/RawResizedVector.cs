namespace SharpMeasures.Generators.Attributes.Parsing.Vectors;

public record class RawResizedVector : ARawAttributeDefinition<ResizedVectorLocations>
{
    internal static RawResizedVector Empty => new();

    public NamedType? AssociatedVector { get; init; }

    public int Dimension { get; init; }

    public bool GenerateDocumentation { get; init; }

    private RawResizedVector() : base(ResizedVectorLocations.Empty) { }
}
