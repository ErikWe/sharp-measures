namespace SharpMeasures.Generators.Vectors.Refinement;

using SharpMeasures.Generators.Units;
using SharpMeasures.Generators.Scalars;

internal readonly record struct RefinedResizedVectorDefinition
{
    public VectorInterface AssociatedVector { get; }
    public UnitInterface Unit { get; }
    public ScalarInterface? Scalar { get; }

    public int Dimension { get; }

    public bool GenerateDocumentation { get; }

    public RefinedResizedVectorDefinition(VectorInterface associatedVector, UnitInterface unit, ScalarInterface? scalar, int dimension, bool generateDocumentation)
    {
        AssociatedVector = associatedVector;
        Unit = unit;
        Scalar = scalar;

        Dimension = dimension;

        GenerateDocumentation = generateDocumentation;
    }
}
