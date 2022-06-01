namespace SharpMeasures.Generators.Vectors;

internal record class RootVectorInterface : VectorInterface
{
    public NamedType UnitType { get; }

    public RootVectorInterface(NamedType vectorType, int dimension, NamedType unitType) : base(vectorType, dimension)
    {
        UnitType = unitType;
    }
}
