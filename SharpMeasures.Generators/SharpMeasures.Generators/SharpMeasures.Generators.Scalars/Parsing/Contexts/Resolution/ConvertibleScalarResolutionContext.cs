namespace SharpMeasures.Generators.Scalars.Parsing.Contexts.Resolution;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Scalars.Parsing.ConvertibleScalar;
using SharpMeasures.Generators.Unresolved.Scalars;

internal record class ConvertibleScalarResolutionContext : SimpleProcessingContext, IConvertibleScalarResolutionContext
{
    public bool UseUnitBias { get; }

    public IUnresolvedScalarPopulation ScalarPopulation { get; }

    public ConvertibleScalarResolutionContext(DefinedType type, bool useUnitBias, IUnresolvedScalarPopulation scalarPopulation) : base(type)
    {
        UseUnitBias = useUnitBias;

        ScalarPopulation = scalarPopulation;
    }
}
