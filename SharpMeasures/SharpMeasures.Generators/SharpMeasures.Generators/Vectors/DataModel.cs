namespace SharpMeasures.Generators.Vectors;

using SharpMeasures.Generators.Documentation;
using SharpMeasures.Generators.Vectors.Parsing;
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Units;

internal record class DataModel
{
    public ParsedVector Vector { get; }
    public UnitInterface Unit { get; }
    public ScalarInterface? Scalar { get; }

    public DocumentationFile Documentation { get; init; }

    public DataModel(ParsedVector vector, UnitInterface unit, ScalarInterface? scalar)
    {
        Vector = vector;
        Unit = unit;
        Scalar = scalar;
    }
}
