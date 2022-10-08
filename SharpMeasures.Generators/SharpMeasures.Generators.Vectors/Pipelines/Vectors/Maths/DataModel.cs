namespace SharpMeasures.Generators.Vectors.Pipelines.Vectors.Maths;

using SharpMeasures.Generators.Vectors.Documentation;

internal readonly record struct DataModel
{
    public DefinedType Vector { get; }
    public int Dimension { get; }

    public bool ImplementSum { get; }
    public bool ImplementDifference { get; }

    public NamedType? Difference { get; }
    public NamedType? DifferenceScalar { get; }

    public NamedType? Scalar { get; }

    public NamedType Unit { get; }
    public NamedType UnitQuantity { get; }

    public IVectorDocumentationStrategy Documentation { get; }

    public DataModel(DefinedType vector, int dimension, bool implementSum, bool implementDifference, NamedType? difference, NamedType? differenceScalar, NamedType? scalar, NamedType unit, NamedType unitQuantity, IVectorDocumentationStrategy documentation)
    {
        Vector = vector;
        Dimension = dimension;

        ImplementSum = implementSum;
        ImplementDifference = implementDifference;

        Difference = difference;
        DifferenceScalar = differenceScalar;

        Scalar = scalar;

        Unit = unit;
        UnitQuantity = unitQuantity;

        Documentation = documentation;
    }
}
