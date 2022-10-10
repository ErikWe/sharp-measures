namespace SharpMeasures.Generators.Scalars;

using SharpMeasures.Generators.Scalars.Documentation;

internal sealed record class SourceBuildingContext
{
    public int HeaderContentLevel { get; }

    public IDocumentationStrategy Documentation { get; }

    public SourceBuildingContext(int headerContentLevel, IDocumentationStrategy documentation)
    {
        HeaderContentLevel = headerContentLevel;

        Documentation = documentation;
    }
}
