namespace SharpMeasures.Generators.Vectors.Parsing.ResizedVector;

using SharpMeasures.Generators.Attributes.Parsing;

internal record class ResizedVectorDefinition : AAttributeDefinition<ResizedVectorLocations>
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
