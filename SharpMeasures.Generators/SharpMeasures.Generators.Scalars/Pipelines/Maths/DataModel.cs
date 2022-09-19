namespace SharpMeasures.Generators.Scalars.Pipelines.Maths;

using SharpMeasures.Generators.Scalars.Documentation;

internal readonly record struct DataModel
{
    public DefinedType Scalar { get; }

    public NamedType Unit { get; }

    public bool ImplementSum { get; }
    public bool ImplementDifference { get; }

    public NamedType? Difference { get; }

    public NamedType? Reciprocal { get; }
    public NamedType? Square { get; }
    public NamedType? Cube { get; }
    public NamedType? SquareRoot { get; }
    public NamedType? CubeRoot { get; }

    public IDocumentationStrategy Documentation { get; }

    public DataModel(DefinedType scalar, NamedType unit, bool implementSum, bool implementDifference, NamedType? difference, NamedType? reciprocal, NamedType? square, NamedType? cube, NamedType? squareRoot, NamedType? cubeRoot, IDocumentationStrategy documentation)
    {
        Scalar = scalar;

        Unit = unit;

        ImplementSum = implementSum;
        ImplementDifference = implementDifference;

        Difference = difference;

        Reciprocal = reciprocal;
        Square = square;
        Cube = cube;
        SquareRoot = squareRoot;
        CubeRoot = cubeRoot;

        Documentation = documentation;
    }
}
