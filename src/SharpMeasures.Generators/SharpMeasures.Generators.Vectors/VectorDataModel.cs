namespace SharpMeasures.Generators.Vectors;

using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Units;
using SharpMeasures.Generators.Vectors.Abstraction;

internal sealed record class VectorDataModel : ADataModel
{
    public ResolvedVectorType Vector { get; }

    public VectorSourceBuildingContext SourceBuildingContext { get; }

    public VectorDataModel(ResolvedVectorType vector, IUnitPopulation unitPopulation, IResolvedScalarPopulation scalarPopulation, IResolvedVectorPopulation vectorPopulation, VectorSourceBuildingContext sourceBuildingContext) : base(unitPopulation, scalarPopulation, vectorPopulation)
    {
        Vector = vector;

        SourceBuildingContext = sourceBuildingContext;
    }
}
