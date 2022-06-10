namespace SharpMeasures.Generators.Vectors.Parsing.ResizedVector;

using SharpMeasures.Generators.Attributes.Parsing;

internal record class RawResizedVectorDefinition : ARawAttributeDefinition<ResizedVectorLocations>
{
    public static RawResizedVectorDefinition Empty => new();

    public NamedType? AssociatedVector { get; init; }

    public int Dimension { get; init; }

    public bool GenerateDocumentation { get; init; }

    private RawResizedVectorDefinition() : base(ResizedVectorLocations.Empty) { }
}
