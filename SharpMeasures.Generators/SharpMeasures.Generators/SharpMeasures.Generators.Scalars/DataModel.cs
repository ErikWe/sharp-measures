namespace SharpMeasures.Generators.Scalars;

using SharpMeasures.Generators.Scalars.Documentation;
using SharpMeasures.Generators.Scalars.Parsing;
using SharpMeasures.Generators.Scalars.Refinement.GeneratedScalar;
using SharpMeasures.Generators.Vectors;

internal record class DataModel
{
    public RefinedGeneratedScalarDefinition ScalarDefinition { get; }
    public ParsedScalar ScalarData { get; }

    public ScalarPopulation ScalarPopulation { get; }
    public VectorPopulation VectorPopulation { get; }

    public IDocumentationStrategy Documentation { get; init; } = EmptyDocumentation.Instance;

    public DataModel(RefinedGeneratedScalarDefinition scalarDefinition, ParsedScalar scalarData, ScalarPopulation scalarPopulation, VectorPopulation vectorPopulation)
    {
        ScalarDefinition = scalarDefinition;
        ScalarData = scalarData;

        ScalarPopulation = scalarPopulation;
        VectorPopulation = vectorPopulation;
    }
}
