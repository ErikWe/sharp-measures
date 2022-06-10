namespace SharpMeasures.Generators.Vectors;

public record class VectorInterface
{
    public NamedType VectorType { get; }

    public NamedType UnitType { get; }
    public NamedType? ScalarType { get; }

    public int Dimension { get; }

    public VectorInterface(NamedType vectorType, NamedType unitType, NamedType? scalarType, int dimension)
    {
        VectorType = vectorType;

        UnitType = unitType;
        ScalarType = scalarType;

        Dimension = dimension;
    }
}
