namespace SharpMeasures.Generators.Units;

using SharpMeasures.Generators.Units.Documentation;

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
