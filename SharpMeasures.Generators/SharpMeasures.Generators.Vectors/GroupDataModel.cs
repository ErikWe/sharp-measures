namespace SharpMeasures.Generators.Vectors;

using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Units;
using SharpMeasures.Generators.Vectors.Abstraction;

internal sealed record class GroupDataModel : ADataModel
{
    public ResolvedGroupType Group { get; }

    public GroupSourceBuildingContext SourceBuildingContext { get; }

    public GroupDataModel(ResolvedGroupType group, IUnitPopulation unitPopulation, IResolvedScalarPopulation scalarPopulation, IResolvedVectorPopulation vectorPopulation, GroupSourceBuildingContext sourceBuildingContext) : base(unitPopulation, scalarPopulation, vectorPopulation)
    {
        Group = group;

        SourceBuildingContext = sourceBuildingContext;
    }
}
