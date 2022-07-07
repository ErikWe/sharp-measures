namespace SharpMeasures.Generators.Vectors.Refinement.ResizedSharpMeasuresVector;

using SharpMeasures.Generators.Units;
using SharpMeasures.Generators.Scalars;

internal readonly record struct RefinedResizedSharpMeasuresVectorDefinition
{
    public IVector AssociatedVector { get; }
    public ResizedGroup VectorGroup { get; }

    public IUnitType Unit { get; }
    public IScalarType? Scalar { get; }

    public int Dimension { get; }

    public bool ImplementSum { get; }
    public bool ImplementDifference { get; }

    public NamedType Difference { get; }

    public string? DefaultUnitName { get; }
    public string? DefaultUnitSymbol { get; }

    public bool GenerateDocumentation { get; }

    public RefinedResizedSharpMeasuresVectorDefinition(IVector associatedVector, ResizedGroup vectorGroup, IUnitType unit, IScalarType? scalar,
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
