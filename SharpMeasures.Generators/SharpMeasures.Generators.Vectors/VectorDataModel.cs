namespace SharpMeasures.Generators.Vectors;

using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Units;
using SharpMeasures.Generators.Vectors.Abstraction;
using SharpMeasures.Generators.Vectors.Documentation;

internal sealed record class VectorDataModel : ADataModel
{
    public ResolvedVectorType Vector { get; }

    public IVectorDocumentationStrategy Documentation { get; init; } = EmptyDocumentation.Instance;

    public VectorDataModel(ResolvedVectorType vector, IUnitPopulation unitPopulation, IResolvedScalarPopulation scalarPopulation, IResolvedVectorPopulation vectorPopulation) : base(unitPopulation, scalarPopulation, vectorPopulation)
    {
        Vector = vector;
    }
}
