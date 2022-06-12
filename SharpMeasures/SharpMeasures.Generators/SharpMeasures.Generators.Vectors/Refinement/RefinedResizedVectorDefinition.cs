namespace SharpMeasures.Generators.Vectors.Refinement;

using SharpMeasures.Generators.Units;
using SharpMeasures.Generators.Scalars;

internal readonly record struct RefinedResizedVectorDefinition
{
    public IVectorInterface AssociatedVector { get; }
    public ResizedVectorGroup VectorGroup { get; }

    public UnitInterface Unit { get; }
    public ScalarInterface? Scalar { get; }

    public int Dimension { get; }

    public bool GenerateDocumentation { get; }

    public RefinedResizedVectorDefinition(IVectorInterface associatedVector, ResizedVectorGroup vectorGroup, UnitInterface unit, ScalarInterface? scalar,
        int dimension, bool generateDocumentation)
    {
        AssociatedVector = associatedVector;
        VectorGroup = vectorGroup;
        
        Unit = unit;
        Scalar = scalar;

        Dimension = dimension;

        GenerateDocumentation = generateDocumentation;
    }
}
