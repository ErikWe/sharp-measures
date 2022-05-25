namespace SharpMeasures.Generators.Parsing.Vectors;

internal record class VectorInterface
{
    public NamedType VectorType { get; }
    public NamedType UnitType { get; }

    public VectorInterface(NamedType vectorType, NamedType unittype)
    {
        VectorType = vectorType;
        UnitType = unittype;
    }
}
