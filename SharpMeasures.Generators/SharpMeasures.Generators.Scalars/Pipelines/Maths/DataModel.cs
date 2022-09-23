namespace SharpMeasures.Generators.Scalars.Pipelines.Maths;

using SharpMeasures.Generators.Scalars.Documentation;

internal readonly record struct DataModel
{
    public DefinedType Scalar { get; }

    public NamedType Unit { get; }

    public bool ImplementSum { get; }
    public bool ImplementDifference { get; }

    public NamedType? Difference { get; }

    public IDocumentationStrategy Documentation { get; }

    public DataModel(DefinedType scalar, NamedType unit, bool implementSum, bool implementDifference, NamedType? difference, IDocumentationStrategy documentation)
    {
        Scalar = scalar;

        Unit = unit;

        ImplementSum = implementSum;
        ImplementDifference = implementDifference;

        Difference = difference;

        Documentation = documentation;
    }
}
