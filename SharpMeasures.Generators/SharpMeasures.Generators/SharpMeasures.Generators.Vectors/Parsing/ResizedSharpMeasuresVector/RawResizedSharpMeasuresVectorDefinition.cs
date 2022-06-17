namespace SharpMeasures.Generators.Vectors.Parsing.ResizedSharpMeasuresVector;

using SharpMeasures.Generators.Attributes.Parsing;

internal record class RawResizedSharpMeasuresVectorDefinition : ARawAttributeDefinition<ResizedSharpMeasuresVectorLocations>
{
    public static RawResizedSharpMeasuresVectorDefinition Empty => new();

    public NamedType? AssociatedVector { get; init; }

    public int Dimension { get; init; }

    public bool GenerateDocumentation { get; init; }

    private RawResizedSharpMeasuresVectorDefinition() : base(ResizedSharpMeasuresVectorLocations.Empty) { }
}
