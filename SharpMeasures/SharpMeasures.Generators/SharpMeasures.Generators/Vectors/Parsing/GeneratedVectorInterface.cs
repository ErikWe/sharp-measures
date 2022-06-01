namespace SharpMeasures.Generators.Vectors.Parsing;

internal class GeneratedVectorInterface
{
    public NamedType VectorType { get; }
    public int Dimension { get; }

    public NamedType UnitType { get; }

    public GeneratedVectorInterface(NamedType vectorType, int dimension, NamedType unitType)
    {
        VectorType = vectorType;
        Dimension = dimension;

        UnitType = unitType;
    }
}
