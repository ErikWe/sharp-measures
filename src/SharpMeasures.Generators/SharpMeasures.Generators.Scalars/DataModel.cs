namespace SharpMeasures.Generators.Scalars;

using SharpMeasures.Generators.Units;
using SharpMeasures.Generators.Vectors;

internal sealed record class DataModel
{
    public ResolvedScalarType Scalar { get; }

    public IUnitPopulation UnitPopulation { get; }
    public IResolvedScalarPopulation ScalarPopulation { get; }
    public IResolvedVectorPopulation VectorPopulation { get; }

    public SourceBuildingContext SourceBuildingContext { get; }

    public DataModel(ResolvedScalarType scalar, IUnitPopulation unitPopulation, IResolvedScalarPopulation scalarPopulation, IResolvedVectorPopulation vectorPopulation, SourceBuildingContext sourceBuildingContext)
    {
        Scalar = scalar;

        UnitPopulation = unitPopulation;
        ScalarPopulation = scalarPopulation;
        VectorPopulation = vectorPopulation;

        SourceBuildingContext = sourceBuildingContext;
    }
}
