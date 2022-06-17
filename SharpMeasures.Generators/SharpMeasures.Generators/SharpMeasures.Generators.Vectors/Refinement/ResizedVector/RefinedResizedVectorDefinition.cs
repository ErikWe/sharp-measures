namespace SharpMeasures.Generators.Vectors.Refinement.ResizedVector;

using SharpMeasures.Generators.Units;
using SharpMeasures.Generators.Scalars;

internal readonly record struct RefinedResizedVectorDefinition
{
    public IVectorInterface AssociatedVector { get; }
    public ResizedVectorGroup VectorGroup { get; }

    public UnitInterface Unit { get; }
    public ScalarInterface? Scalar { get; }

    public int Dimension { get; }

    public string? DefaultUnitName { get; }
    public string? DefaultUnitSymbol { get; }

    public bool GenerateDocumentation { get; }

    public RefinedResizedVectorDefinition(IVectorInterface associatedVector, ResizedVectorGroup vectorGroup, UnitInterface unit, ScalarInterface? scalar,
        int dimension, string? defaultUnitName, string? defaultUnitSymbol, bool generateDocumentation)
    {
        AssociatedVector = associatedVector;
        VectorGroup = vectorGroup;

        Unit = unit;
        Scalar = scalar;

        Dimension = dimension;

        DefaultUnitName = defaultUnitName;
        DefaultUnitSymbol = defaultUnitSymbol;

        GenerateDocumentation = generateDocumentation;
    }
}
