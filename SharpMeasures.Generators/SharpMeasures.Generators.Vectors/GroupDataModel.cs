namespace SharpMeasures.Generators.Vectors;

using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Units;
using SharpMeasures.Generators.Vectors.Abstraction;
using SharpMeasures.Generators.Vectors.Documentation;

internal sealed record class GroupDataModel : ADataModel
{
    public ResolvedGroupType Group { get; }

    public IGroupDocumentationStrategy Documentation { get; init; } = EmptyDocumentation.Instance;

    public GroupDataModel(ResolvedGroupType group, IUnitPopulation unitPopulation, IResolvedScalarPopulation scalarPopulation, IResolvedVectorPopulation vectorPopulation) : base(unitPopulation, scalarPopulation, vectorPopulation)
    {
        Group = group;
    }
}
