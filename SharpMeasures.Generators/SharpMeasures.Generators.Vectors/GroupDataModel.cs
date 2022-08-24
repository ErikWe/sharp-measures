namespace SharpMeasures.Generators.Vectors;

using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Units;
using SharpMeasures.Generators.Vectors.Abstraction;
using SharpMeasures.Generators.Vectors.Documentation;

internal record class GroupDataModel : ADataModel
{
    public ResolvedGroupType VectorGroup { get; }

    public IGroupDocumentationStrategy Documentation { get; init; } = EmptyDocumentation.Instance;

    public GroupDataModel(ResolvedGroupType vectorGroup, IUnitPopulation unitPopulation, IResolvedScalarPopulation scalarPopulation, IResolvedVectorPopulation vectorPopulation)
        : base(unitPopulation, scalarPopulation, vectorPopulation)
    {
        VectorGroup = vectorGroup;
    }
}
