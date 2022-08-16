namespace SharpMeasures.Generators.Scalars.Parsing.Contexts.Resolution;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Scalars.Parsing.ConvertibleScalar;
using SharpMeasures.Generators.Raw.Scalars;

internal record class ConvertibleScalarResolutionContext : SimpleProcessingContext, IConvertibleScalarResolutionContext
{
    public bool UseUnitBias { get; }

    public IRawScalarPopulation ScalarPopulation { get; }

    public ConvertibleScalarResolutionContext(DefinedType type, bool useUnitBias, IRawScalarPopulation scalarPopulation) : base(type)
    {
        UseUnitBias = useUnitBias;

        ScalarPopulation = scalarPopulation;
    }
}
