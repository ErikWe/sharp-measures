namespace SharpMeasures.Generators.Vectors;

using SharpMeasures.Generators.Vectors.Documentation;

internal sealed record class VectorSourceBuildingContext
{
    public int HeaderContentLevel { get; }

    public IVectorDocumentationStrategy Documentation { get; }

    public VectorSourceBuildingContext(int headerContentLevel, IVectorDocumentationStrategy documentation)
    {
        HeaderContentLevel = headerContentLevel;

        Documentation = documentation;
    }
}
