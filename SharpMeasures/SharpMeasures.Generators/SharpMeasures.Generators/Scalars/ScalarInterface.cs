namespace SharpMeasures.Generators.Scalars;

internal record class ScalarInterface
{
    public DefinedType ScalarType { get; }
    public NamedType UnitType { get; }

    public bool Biased { get; }

    public ScalarInterface(DefinedType scalarType, NamedType unittype, bool biased)
    {
        ScalarType = scalarType;
        UnitType = unittype;

        Biased = biased;
    }
}
