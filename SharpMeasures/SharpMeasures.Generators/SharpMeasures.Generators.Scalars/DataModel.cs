namespace SharpMeasures.Generators.Scalars;

using SharpMeasures.Generators.Documentation;
using SharpMeasures.Generators.Scalars.Parsing;
using SharpMeasures.Generators.Units;
using SharpMeasures.Generators.Vectors;

internal record class DataModel
{
    public ParsedScalar Scalar { get; }

    public UnitInterface Unit { get; }
    public VectorCollection VectorCollection { get; }

    public ScalarPopulation ScalarPopulation { get; }
    public VectorPopulation VectorPopulation { get; }

    public DocumentationFile Documentation { get; init; } = DocumentationFile.Empty;

    public DataModel(ParsedScalar scalar, UnitInterface unit, VectorCollection vectorCollection, ScalarPopulation scalarPopulation,
        VectorPopulation vectorPopulation)
    {
        Scalar = scalar;

        Unit = unit;
        VectorCollection = vectorCollection;

        ScalarPopulation = scalarPopulation;
        VectorPopulation = vectorPopulation;
    }
}
