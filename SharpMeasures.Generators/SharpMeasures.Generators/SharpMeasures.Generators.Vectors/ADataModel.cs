namespace SharpMeasures.Generators.Vectors;

using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Units;

internal abstract record class ADataModel<TVectorType, TDocumentation>
    where TVectorType : IVectorGroupType
{
    public TVectorType Vector { get; }

    public IUnitPopulation UnitPopulation { get; }
    public IScalarPopulation ScalarPopulation { get; }
    public IVectorPopulation VectorPopulation { get; }

    public TDocumentation Documentation { get; init; }

    protected ADataModel(TVectorType vector, IUnitPopulation unitPopulation, IScalarPopulation scalarPopulation, IVectorPopulation vectorPopulation,
        TDocumentation documentation)
    {
        Vector = vector;

        UnitPopulation = unitPopulation;
        ScalarPopulation = scalarPopulation;
        VectorPopulation = vectorPopulation;

        Documentation = documentation;
    }
}
