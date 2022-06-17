namespace SharpMeasures.Generators.Vectors.Parsing.ResizedVector;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Vectors.Parsing.Abstractions;

internal record class ResizedVectorDefinition : AAttributeDefinition<ResizedVectorLocations>, IVectorDefinition
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
