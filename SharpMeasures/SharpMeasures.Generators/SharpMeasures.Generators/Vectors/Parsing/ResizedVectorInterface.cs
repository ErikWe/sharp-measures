namespace SharpMeasures.Generators.Vectors.Parsing;

internal class ResizedVectorInterface
{
    public NamedType VectorType { get; }
    public int Dimension { get; }

    public NamedType AssociatedTo { get; }

    public ResizedVectorInterface(NamedType vectorType, int dimension, NamedType associatedTo)
    {
        VectorType = vectorType;
        Dimension = dimension;

        AssociatedTo = associatedTo;
    }
}
