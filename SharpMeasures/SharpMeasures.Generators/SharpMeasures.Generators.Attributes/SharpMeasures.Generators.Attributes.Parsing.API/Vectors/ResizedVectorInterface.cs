namespace SharpMeasures.Generators.Vectors;

public class ResizedVectorInterface
{
    public NamedType VectorType { get; }

    public NamedType AssociatedVector { get; }
    public int Dimension { get; }

    public ResizedVectorInterface(NamedType vectorType, NamedType associatedVector, int dimension)
    {
        VectorType = vectorType;

        AssociatedVector = associatedVector;
        Dimension = dimension;
    }
}
