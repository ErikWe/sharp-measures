namespace SharpMeasures.Generators.Vectors.Refinement.GeneratedVector;

using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Units;

internal readonly record struct RefinedGeneratedVectorDefinition
{
    public UnitInterface Unit { get; }
    public ScalarInterface? Scalar { get; }
    public ResizedVectorGroup VectorGroup { get; }

    public int Dimension { get; }

    public string? DefaultUnitName { get; }
    public string? DefaultUnitSymbol { get; }

    public bool GenerateDocumentation { get; }

    public RefinedGeneratedVectorDefinition(UnitInterface unit, ScalarInterface? scalar, ResizedVectorGroup vectorGroup, int dimension, string? defaultUnitName,
        string? defaultUnitSymbol, bool generateDocumentation)
    {
        Unit = unit;
        Scalar = scalar;
        VectorGroup = vectorGroup;

        Dimension = dimension;

        DefaultUnitName = defaultUnitName;
        DefaultUnitSymbol = defaultUnitSymbol;

        GenerateDocumentation = generateDocumentation;
    }
}
