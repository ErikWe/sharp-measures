namespace SharpMeasures.Generators.Scalars.Parsing.Contexts.Validation;

using SharpMeasures.Generators.Scalars.Parsing.ConvertibleScalar;

internal sealed record class ConvertibleScalarFilteringContext : IConvertibleScalarFilteringContext
{
    public DefinedType Type { get; }

    public bool UseUnitBias { get; }
    public IScalarPopulation ScalarPopulation { get; }

    public ConvertibleScalarFilteringContext(DefinedType type, bool useUnitBias, IScalarPopulation scalarPopulation)
    {
        Type = type;

        UseUnitBias = useUnitBias;
        ScalarPopulation = scalarPopulation;
    }
}
