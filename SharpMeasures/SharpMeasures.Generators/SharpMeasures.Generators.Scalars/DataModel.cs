namespace SharpMeasures.Generators.Scalars;

using SharpMeasures.Generators.Documentation;
using SharpMeasures.Generators.Scalars.Parsing;
using SharpMeasures.Generators.Scalars.Refinement;
using SharpMeasures.Generators.Vectors;

internal record class DataModel
{
    public RefinedGeneratedScalarDefinition ScalarDefinition { get; }
    public ParsedScalar ScalarData { get; }

    public ScalarPopulation ScalarPopulation { get; }
    public VectorPopulation VectorPopulation { get; }

    public DocumentationFile Documentation { get; init; } = DocumentationFile.Empty;

    public DataModel(RefinedGeneratedScalarDefinition scalarDefinition, ParsedScalar scalarData, ScalarPopulation scalarPopulation, VectorPopulation vectorPopulation)
    {
        ScalarDefinition = scalarDefinition;
        ScalarData = scalarData;

        ScalarPopulation = scalarPopulation;
        VectorPopulation = vectorPopulation;
    }
}
