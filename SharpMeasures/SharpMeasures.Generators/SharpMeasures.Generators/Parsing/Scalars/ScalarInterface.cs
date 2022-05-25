namespace SharpMeasures.Generators.Parsing.Scalars;

internal record class ScalarInterface
{
    public NamedType ScalarType { get; }
    public NamedType UnitType { get; }

    public bool Biased { get; }

    public ScalarInterface(NamedType scalarType, NamedType unittype, bool biased)
    {
        ScalarType = scalarType;
        UnitType = unittype;

        Biased = biased;
    }
}
