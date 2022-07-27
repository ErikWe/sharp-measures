namespace SharpMeasures.Generators.Scalars;

using SharpMeasures.Generators.Scalars.Documentation;
using SharpMeasures.Generators.Units;
using SharpMeasures.Generators.Vectors;

internal record class DataModel
{
    public ScalarType Scalar { get; }

    public IUnitPopulation UnitPopulation { get; }
    public IScalarPopulation ScalarPopulation { get; }
    public IVectorPopulation VectorPopulation { get; }

    public IDocumentationStrategy Documentation { get; init; } = EmptyDocumentation.Instance;

    public DataModel(ScalarType scalar, IUnitPopulation unitPopulation, IScalarPopulation scalarPopulation, IVectorPopulation vectorPopulation)
    {
        Scalar = scalar;

        UnitPopulation = unitPopulation;
        ScalarPopulation = scalarPopulation;
        VectorPopulation = vectorPopulation;
    }
}
