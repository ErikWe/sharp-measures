namespace SharpMeasures.Generators.Vectors;

using SharpMeasures.Generators.Vectors.Documentation;

internal sealed record class GroupSourceBuildingContext
{
    public int HeaderContentLevel { get; }

    public IGroupDocumentationStrategy Documentation { get; }

    public GroupSourceBuildingContext(int headerContentLevel, IGroupDocumentationStrategy documentation)
    {
        HeaderContentLevel = headerContentLevel;

        Documentation = documentation;
    }
}
