namespace SharpMeasures.Generators.Scalars.Pipelines.Vectors;

using SharpMeasures.Generators.Scalars.Documentation;
using SharpMeasures.Generators.Unresolved.Vectors;

internal readonly record struct DataModel
{
    public DefinedType Scalar { get; }

    public IUnresolvedVectorGroupType VectorGroup { get; }

    public IDocumentationStrategy Documentation { get; }

    public DataModel(DefinedType scalar, IUnresolvedVectorGroupType vectorGroup, IDocumentationStrategy documentation)
    {
        Scalar = scalar;

        VectorGroup = vectorGroup;

        Documentation = documentation;
    }
}
