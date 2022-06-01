namespace SharpMeasures.Generators.Vectors;

using SharpMeasures.Generators.Vectors.Parsing;

internal record class VectorInterface
{
    public static VectorInterface From(ResizedVectorInterface resizedVector)
    {
        return new(resizedVector.VectorType, resizedVector.Dimension);
    }

    public NamedType VectorType { get; }
    public int Dimension { get; }

    public VectorInterface(NamedType vectorType, int dimension)
    {
        VectorType = vectorType;
        Dimension = dimension;
    }
}
