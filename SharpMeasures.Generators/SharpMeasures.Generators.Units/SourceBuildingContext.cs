namespace SharpMeasures.Generators.Units;

using SharpMeasures.Generators.Configuration;
using SharpMeasures.Generators.Units.Documentation;

internal sealed record class SourceBuildingContext
{
    public GeneratedFileHeaderContent HeaderContent { get; }

    public IDocumentationStrategy Documentation { get; init; }

    public SourceBuildingContext(GeneratedFileHeaderContent headerContent, IDocumentationStrategy documentation)
    {
        HeaderContent = headerContent;

        Documentation = documentation;
    }
}
