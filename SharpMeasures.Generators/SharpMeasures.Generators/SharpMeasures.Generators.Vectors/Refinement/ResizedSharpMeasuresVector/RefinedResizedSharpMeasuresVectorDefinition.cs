namespace SharpMeasures.Generators.Vectors.Refinement.ResizedSharpMeasuresVector;

using SharpMeasures.Generators.Units;
using SharpMeasures.Generators.Scalars;

internal readonly record struct RefinedResizedSharpMeasuresVectorDefinition
{
    public IVectorInterface AssociatedVector { get; }
    public ResizedVectorGroup VectorGroup { get; }

    public UnitInterface Unit { get; }
    public ScalarInterface? Scalar { get; }

    public int Dimension { get; }

    public bool ImplementSum { get; }
    public bool ImplementDifference { get; }

    public NamedType Difference { get; }

    public string? DefaultUnitName { get; }
    public string? DefaultUnitSymbol { get; }

    public bool GenerateDocumentation { get; }

    public RefinedResizedSharpMeasuresVectorDefinition(IVectorInterface associatedVector, ResizedVectorGroup vectorGroup, UnitInterface unit, ScalarInterface? scalar,
        int dimension, bool implementSum, bool implementDifference, NamedType difference, string? defaultUnitName, string? defaultUnitSymbol,
        bool generateDocumentation)
    {
        AssociatedVector = associatedVector;
        VectorGroup = vectorGroup;

        Unit = unit;
        Scalar = scalar;

        Dimension = dimension;

        ImplementSum = implementSum;
        ImplementDifference = implementDifference;

        Difference = difference;

        DefaultUnitName = defaultUnitName;
        DefaultUnitSymbol = defaultUnitSymbol;

        GenerateDocumentation = generateDocumentation;
    }
}
