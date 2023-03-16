namespace SharpMeasures.Generators.Vectors.Abstraction;

using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Units;

internal abstract record class ADataModel
{
    public IUnitPopulation UnitPopulation { get; }
    public IResolvedScalarPopulation ScalarPopulation { get; }
    public IResolvedVectorPopulation VectorPopulation { get; }

    protected ADataModel(IUnitPopulation unitPopulation, IResolvedScalarPopulation scalarPopulation, IResolvedVectorPopulation vectorPopulation)
    {
        UnitPopulation = unitPopulation;
        ScalarPopulation = scalarPopulation;
        VectorPopulation = vectorPopulation;
    }
}
