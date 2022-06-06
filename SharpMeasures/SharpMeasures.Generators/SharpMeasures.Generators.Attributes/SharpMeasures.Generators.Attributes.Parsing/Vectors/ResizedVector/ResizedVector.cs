namespace SharpMeasures.Generators.Attributes.Parsing.Vectors;

public record class ResizedVector : AAttributeDefinition<ResizedVectorLocations>
{
    public NamedType AssociatedVector { get; }

    public int Dimension { get; }

    public bool GenerateDocumentation { get; }

    public ResizedVector(NamedType associatedVector, int dimension, bool generateDocumentation, ResizedVectorLocations locations)
        : base(locations)
    {
        AssociatedVector = associatedVector;

        Dimension = dimension;

        GenerateDocumentation = generateDocumentation;
    }
}
