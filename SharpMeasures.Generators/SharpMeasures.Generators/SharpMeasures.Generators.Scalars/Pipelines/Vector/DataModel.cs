namespace SharpMeasures.Generators.Scalars.Pipelines.Vectors;

using SharpMeasures.Generators.Scalars.Documentation;
using SharpMeasures.Generators.Vectors.Groups;

internal readonly record struct DataModel
{
    public DefinedType Scalar { get; }

    public IVectorGroupType VectorGroup { get; }

    public IDocumentationStrategy Documentation { get; }

    public DataModel(DefinedType scalar, IVectorGroupType vectorGroup, IDocumentationStrategy documentation)
    {
        Scalar = scalar;

        VectorGroup = vectorGroup;

        Documentation = documentation;
    }
}
