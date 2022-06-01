namespace SharpMeasures.Generators.Attributes.Parsing.Vectors;

public record class RawResizedVectorDefinition : ARawAttributeDefinition<ResizedVectorLocations>
{
    internal static RawResizedVectorDefinition Empty => new();

    public NamedType? AssociatedVector { get; init; }

    public int Dimension { get; init; }

    public bool GenerateDocumentation { get; init; }

    private RawResizedVectorDefinition() : base(ResizedVectorLocations.Empty) { }
}