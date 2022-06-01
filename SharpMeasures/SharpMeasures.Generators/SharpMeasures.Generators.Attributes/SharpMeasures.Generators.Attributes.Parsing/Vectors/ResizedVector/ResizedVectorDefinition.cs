namespace SharpMeasures.Generators.Attributes.Parsing.Vectors;

public record class ResizedVectorDefinition : AAttributeDefinition<ResizedVectorLocations>
{
    public NamedType AssociatedVector { get; }

    public int Dimension { get; }

    public bool GenerateDocumentation { get; }

    public ResizedVectorDefinition(NamedType associatedVector, int dimension, bool generateDocumentation, ResizedVectorLocations locations)
        : base(locations)
    {
        AssociatedVector = associatedVector;

        Dimension = dimension;

        GenerateDocumentation = generateDocumentation;
    }
}