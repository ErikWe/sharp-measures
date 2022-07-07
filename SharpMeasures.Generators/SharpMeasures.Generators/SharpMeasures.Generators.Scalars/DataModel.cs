namespace SharpMeasures.Generators.Scalars;

using SharpMeasures.Generators.Scalars.Documentation;
using SharpMeasures.Generators.Scalars.Refinement.SharpMeasuresScalar;
using SharpMeasures.Generators.Vectors.Populations;

internal record class DataModel
{
    public RefinedSharpMeasuresScalarDefinition ScalarDefinition { get; }
    public BaseScalarType ScalarData { get; }

    public IScalarPopulation ScalarPopulation { get; }
    public VectorPopulation VectorPopulation { get; }

    public IDocumentationStrategy Documentation { get; init; } = EmptyDocumentation.Instance;

    public DataModel(RefinedSharpMeasuresScalarDefinition scalarDefinition, BaseScalarType scalarData, IScalarPopulation scalarPopulation, VectorPopulation vectorPopulation)
    {
        ScalarDefinition = scalarDefinition;
        ScalarData = scalarData;

        ScalarPopulation = scalarPopulation;
        VectorPopulation = vectorPopulation;
    }
}
