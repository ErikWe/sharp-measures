namespace SharpMeasures.Generators.Vectors.Refinement;

using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Units;

internal readonly record struct RefinedGeneratedVectorDefinition
{
    public UnitInterface Unit { get; }
    public ScalarInterface? Scalar { get; }

    public int Dimension { get; }

    public string? DefaultUnit { get; }
    public string? DefaultUnitSymbol { get; }

    public bool GenerateDocumentation { get; }

    public RefinedGeneratedVectorDefinition(UnitInterface unit, ScalarInterface? scalar, int dimension, string? defaultUnit, string? defaultUnitSymbol,
        bool generateDocumentation)
    {
        Unit = unit;
        Scalar = scalar;

        Dimension = dimension;

        DefaultUnit = defaultUnit;
        DefaultUnitSymbol = defaultUnitSymbol;

        GenerateDocumentation = generateDocumentation;
    }
}
