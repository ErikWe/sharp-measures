namespace SharpMeasures.Generators.Vectors;

using SharpMeasures.Generators.Configuration;
using SharpMeasures.Generators.Vectors.Documentation;

internal sealed record class GroupSourceBuildingContext
{
    public GeneratedFileHeaderContent HeaderContent { get; }

    public IGroupDocumentationStrategy Documentation { get; init; }

    public GroupSourceBuildingContext(GeneratedFileHeaderContent headerContent, IGroupDocumentationStrategy documentation)
    {
        HeaderContent = headerContent;

        Documentation = documentation;
    }
}
